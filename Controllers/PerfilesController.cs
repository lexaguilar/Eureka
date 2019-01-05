using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eureka.Extensions;
using System.Data.SqlClient;
using Eureka.Models;
using Newtonsoft.Json;
namespace Eureka.Controllers
{
    public class PerfilesController : Controller
    {
        private readonly EurekaContext _context;
        JsonSerializerSettings config = new JsonSerializerSettings { MaxDepth = 1, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        public PerfilesController(EurekaContext context)
        {
            _context = context;
        }

        // GET: Perfiles
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Listar()
        {
            var eurekaContext = _context.Perfiles.Include(p => p.Area).Include(p => p.Estado).Include(p => p.Rol);
            return Json(await eurekaContext.ToListAsync(), config);
        }

        public async Task<IActionResult> ObtenerListarPorArea(int? id, int page)
        {
            var query = _context.Perfiles.Include(p=>p.Estado).Include(p => p.Rol).Where(p => p.AreaId > 0);
            if(id.HasValue)
                query = query.Where(p => p.AreaId == id.Value);

            var total = query.Count();
            query = query.Skip((page - 1) * 10).Take(10);

            return Json(new
            {
                total,
                data = await query.Select(x =>
                new
                {
                    x.Username,
                    x.Nombre,
                    x.Correo,
                    x.Telefono,
                    x.Rol.Descripcion,
                    Estado = x.Estado.Descripcion
                }).ToListAsync()
            }, config);
        }

        // GET: Perfiles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfil = await _context.Perfiles
                    .Include(p => p.Area)
                    .Include(p => p.Estado)
                    .Include(p => p.Rol)
            .SingleOrDefaultAsync(m => m.Username == id);
            if (perfil == null)
            {
                return NotFound();
            }

            return View(perfil);
        }

        // GET: Perfiles/Create
        public IActionResult Create()
        {
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Descripcion");
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Descripcion");
            ViewData["RolId"] = new SelectList(_context.Roles, "Id", "Descripcion");
            return View();
        }

        // POST: Perfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Username,Nombre,FechaNacimiento,RolId,Correo,Telefono,Contrasena,AreaId,UrlTemporal,UtcreadoEl,EstadoId")] Perfil perfil)
        {
            var user = this.GetServiceUser();
            if (ModelState.IsValid)
            {
                _context.Add(perfil);
                await _context.SaveChangesAsync();
                return Json(perfil);
            }
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Descripcion", perfil.AreaId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Descripcion", perfil.EstadoId);
            ViewData["RolId"] = new SelectList(_context.Roles, "Id", "Descripcion", perfil.RolId);
            return View(perfil);
        }

        // GET: Perfiles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfil = await _context.Perfiles.SingleOrDefaultAsync(m => m.Username == id);
            if (perfil == null)
            {
                return NotFound();
            }
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Descripcion", perfil.AreaId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Descripcion", perfil.EstadoId);
            ViewData["RolId"] = new SelectList(_context.Roles, "Id", "Descripcion", perfil.RolId);
            return View(perfil);
        }

        // POST: Perfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Username,Nombre,FechaNacimiento,RolId,Correo,Telefono,Contrasena,AreaId,UrlTemporal,UtcreadoEl,EstadoId")] Perfil perfil)
        {
            var user = this.GetServiceUser();
            if (ModelState.IsValid)
            {
                try
                {
                    var perfilOld = await _context.Perfiles.SingleOrDefaultAsync(m => m.Username == perfil.Username);

                    perfilOld.Nombre = perfil.Nombre;
                    perfilOld.FechaNacimiento = perfil.FechaNacimiento;
                    perfilOld.RolId = perfil.RolId;
                    perfilOld.Correo = perfil.Correo;
                    perfilOld.Telefono = perfil.Telefono;
                    perfilOld.Contrasena = perfil.Contrasena;
                    perfilOld.AreaId = perfil.AreaId;
                    perfilOld.UrlTemporal = perfil.UrlTemporal;
                    perfilOld.UtcreadoEl = perfil.UtcreadoEl;
                    perfilOld.EstadoId = perfil.EstadoId;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfilExists(perfil.Username))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(perfil);
            }
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Descripcion", perfil.AreaId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Descripcion", perfil.EstadoId);
            ViewData["RolId"] = new SelectList(_context.Roles, "Id", "Descripcion", perfil.RolId);
            return BadRequest(perfil);
        }

        // GET: Perfiles/delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfil = await _context.Perfiles
    .Include(p => p.Area)
    .Include(p => p.Estado)
    .Include(p => p.Rol)
            .SingleOrDefaultAsync(m => m.Username == id);
            if (perfil == null)
            {
                return NotFound();
            }

            return View(perfil);
        }

        // POST: Perfiles/delete/5

        [HttpPost, ActionName("DeleteConfirm")]
        public async Task<IActionResult> DeleteConfirm(string id)

        {
            var perfil = await _context.Perfiles
.Include(p => p.Area)
.Include(p => p.Estado)
.Include(p => p.Rol)
            .SingleOrDefaultAsync(m => m.Username == id);

            _context.Remove(perfil);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbex)
            {
                var sqlException = dbex.GetBaseException() as SqlException;

                if (sqlException != null)
                {
                    var number = sqlException.Number;

                    if (number == 547)
                    {
                        ModelState.AddModelError(string.Empty, $"No se puede eliminiar el Perfiles por que está en uso");
                        return View(perfil);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction(nameof(Index));

        }

        private bool PerfilExists(string id)
        {
            return _context.Perfiles.Any(e => e.Username == id);
        }



    }
}
