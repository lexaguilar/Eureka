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
    public class ServiciosController : Controller
    {
        private readonly EurekaContext _context;
        JsonSerializerSettings config = new JsonSerializerSettings { MaxDepth = 1, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        public ServiciosController(EurekaContext context)
        {
            _context = context;
        }

        // GET: Servicios
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Listar()
        {
            var eurekaContext = _context.Servicios.Include(s => s.CreadoPorNavigation).Include(s => s.Estado).Include(s => s.ModificadoPorNavigation);
            return Json(await eurekaContext.ToListAsync(), config);
        }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                    .Include(s => s.CreadoPorNavigation)
                    .Include(s => s.Estado)
                    .Include(s => s.ModificadoPorNavigation)
            .SingleOrDefaultAsync(m => m.Id == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // GET: Servicios/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Descripcion");
            return View();
        }

        // POST: Servicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,ReglaPrecio,EstadoId")] Servicio servicio)
        {
            var user = this.GetServiceUser();
            servicio.CreadoPor = user.Username;
            ModelState.Remove("CreadoPor");
            servicio.CreadoEl = DateTime.Now;
            ModelState.Remove("CreadoEl");
            if (ModelState.IsValid)
            {
                _context.Add(servicio);
                await _context.SaveChangesAsync();
                return Json(servicio);
            }

            return View(servicio);
        }

        // GET: Servicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios.SingleOrDefaultAsync(m => m.Id == id);
            if (servicio == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Descripcion", servicio.EstadoId);
            return View(servicio);
        }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Descripcion,ReglaPrecio,EstadoId")] Servicio servicio)
        {
            var user = this.GetServiceUser();
            servicio.ModificadoPor = user.Username;
            servicio.ModificadoEl = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    var servicioOld = await _context.Servicios.SingleOrDefaultAsync(m => m.Id == servicio.Id);

                    servicioOld.Descripcion = servicio.Descripcion;
                    servicioOld.ReglaPrecio = servicio.ReglaPrecio;
                    servicioOld.EstadoId = servicio.EstadoId;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(servicio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(servicio);
            }

            return BadRequest(servicio);
        }

        // GET: Servicios/delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
    .Include(s => s.CreadoPorNavigation)
    .Include(s => s.Estado)
    .Include(s => s.ModificadoPorNavigation)
            .SingleOrDefaultAsync(m => m.Id == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // POST: Servicios/delete/5

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)

        {
            var servicio = await _context.Servicios
.Include(s => s.CreadoPorNavigation)
.Include(s => s.Estado)
.Include(s => s.ModificadoPorNavigation)
            .SingleOrDefaultAsync(m => m.Id == id);

            _context.Remove(servicio);
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
                        ModelState.AddModelError(string.Empty, $"No se puede eliminiar el Servicios por que está en uso");
                        return View(servicio);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction(nameof(Index));

        }

        private bool ServicioExists(int id)
        {
            return _context.Servicios.Any(e => e.Id == id);
        }



    }
}
