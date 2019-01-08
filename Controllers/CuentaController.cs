using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eureka.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Eureka.Extensions;
using Eureka.Models.ViewModels;
using Microsoft.Extensions.Options;
using Eureka.Services;
using Microsoft.EntityFrameworkCore;

namespace Eureka.Controllers
{
   
     [AllowAnonymous]
    public class CuentaController : Controller
    {
        private readonly EurekaContext db;
        public SmtpConfig SmtpConfig { get; }

        public CuentaController (EurekaContext _db, IOptions<SmtpConfig> smtpConfig) {
            SmtpConfig = smtpConfig.Value;
            db = _db;
        } 
       

        public IActionResult Login(string returnUrl = null)
        {
           //await HttpContext.SignOutAsync();
           
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
             ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                AppUser svcUser = null;
                var unEncrytp = Extensions.HelperExtensions.ObtenerContrasenaHashed(model.Password);
                var result = db.Perfiles.Include(p => p.Area)
                    .Where(p => p.Username == model.Username && p.Contrasena == unEncrytp)
                    .FirstOrDefault();

                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, "El usuario y/o la contraseña es incorrecto.");
                    return View("Login",model);
                }

                if (result.EstadoId == (int)Estados.Desactivado)
                {
                    ModelState.AddModelError(string.Empty, "Usuario bloqueado.");
                    return View("Login", model);
                }

                svcUser = new AppUser()
                {
                    Username = result.Username,
                    Area = result.Area.Descripcion,
                    AreaId = result.AreaId,
                    Correo = result.Correo
                };

                await HttpContext.SignInAsync(svcUser.CreatePrincipal());

                return RedirectToLocal(returnUrl);               
            }            
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult OlvidarCredenciales()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult OlvidarCredenciales(EmailViewModel emailViewModel)
        {
            if(ModelState.IsValid){
                var emailSender = new EmailSender(SmtpConfig);
                string[] emails = { emailViewModel.Email };            

                var urlTemporal = CrearHashUrlTemporal(emailViewModel.Email);
                if(!urlTemporal.Ok)
                {
                    ModelState.AddModelError(string.Empty, urlTemporal.Mensaje);
                    return View(emailViewModel);
                }

                var mensaje = $@"<p>Estimado usuario, para restablecer su contraseña, por favor ingrese al siguiente link
                <br />
                <a target='_blank' href='{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/cuenta/recuperar/{urlTemporal.Mensaje}'>{HttpContext.Request.Host}/cuenta/recuperar/{urlTemporal.Mensaje}<a></p>
                
                www.eurekani.com";
                
                emailSender.SendEmailAsync(emails,"Recuperar credenciales", mensaje);
                return View("ConfirmarEnvio", emailViewModel.Email);
            }
            return View(emailViewModel);
        }
        
        public IActionResult Recuperar(string id)
        {
            var perfil = db.Perfiles.FirstOrDefault(p => p.UrlTemporal == id);
            if(perfil == null)
                return Error404("No se encontró la url");

            var TotalMinutes = (DateTime.Now - (perfil.UtcreadoEl??DateTime.Now)).TotalMinutes;

            if(TotalMinutes > 30)            
                return Error404("El link ya expiró, por favor ingrese a la opción de recuperar contraseña");

            return View("RestablecerCredencial",new ResetPasswordViewModel{ Username = perfil.Username} );
            
        }

        public IActionResult Error404(string resultado="Página no encontrada")
        {
            return View("Error404", resultado);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult restablecerCredenciales(ResetPasswordViewModel model)
        {
            if(ModelState.IsValid){
                var perfil = db.Perfiles.FirstOrDefault(p => p.Username == model.Username);
                if(perfil == null)
                    return NotFound();

                perfil.Contrasena = Extensions.HelperExtensions.ObtenerContrasenaHashed(model.Password);
                db.SaveChanges();

                return View("login");
            }
            return View("RestablecerCredencial", model);
            
        }

        private ValidacionViewModel CrearHashUrlTemporal(string email)
        {
            var perfil = db.Perfiles.FirstOrDefault(p => p.Correo == email);
            if(perfil == null)            
                return new ValidacionViewModel{Mensaje = "El correo electrónico no esta registrado"};
            
            var UrlTemporal = Extensions.HelperExtensions.ObtenerContrasenaHashed(DateTime.Now.Ticks.ToString());
            perfil.UrlTemporal = UrlTemporal;
            perfil.UtcreadoEl = DateTime.Now;
            db.SaveChanges();
            return new ValidacionViewModel{Mensaje = UrlTemporal, Ok = true};
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Lockout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(CuentaController.Login));
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

    }
}
