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
    public class PresentacionesController : Controller
    {
        private readonly EurekaContext _context;
        JsonSerializerSettings config = new JsonSerializerSettings { MaxDepth = 1, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        public PresentacionesController(EurekaContext context)
        {
            _context = context;
        }

        // GET: Presentaciones
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Listar()
        {
            var eurekaContext = _context.Presentaciones.Include(p => p.Estado);
            return Json(await eurekaContext.ToListAsync(), config);
        }

        // GET: Presentaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentacion = await _context.Presentaciones
                    .Include(p => p.Estado)
            .SingleOrDefaultAsync(m => m.Id == id);
            if (presentacion == null)
            {
                return NotFound();
            }

            return View(presentacion);
        }

        // GET: Presentaciones/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Descripcion");
            return View();
        }

        // POST: Presentaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,EstadoId")] Presentacion presentacion)
        {
            var user = this.GetServiceUser();
            if (ModelState.IsValid)
            {
                _context.Add(presentacion);
                await _context.SaveChangesAsync();
                return Json(presentacion);
            }

            return View(presentacion);
        }

        // GET: Presentaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentacion = await _context.Presentaciones.SingleOrDefaultAsync(m => m.Id == id);
            if (presentacion == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Descripcion", presentacion.EstadoId);
            return View(presentacion);
        }

        // POST: Presentaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Descripcion,EstadoId")] Presentacion presentacion)
        {
            var user = this.GetServiceUser();
            if (ModelState.IsValid)
            {
                try
                {
                    var presentacionOld = await _context.Presentaciones.SingleOrDefaultAsync(m => m.Id == presentacion.Id);

                    presentacionOld.Descripcion = presentacion.Descripcion;
                    presentacionOld.EstadoId = presentacion.EstadoId;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresentacionExists(presentacion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(presentacion);
            }

            return BadRequest(presentacion);
        }

        // GET: Presentaciones/delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentacion = await _context.Presentaciones
    .Include(p => p.Estado)
            .SingleOrDefaultAsync(m => m.Id == id);
            if (presentacion == null)
            {
                return NotFound();
            }

            return View(presentacion);
        }

        // POST: Presentaciones/delete/5

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)

        {
            var presentacion = await _context.Presentaciones
.Include(p => p.Estado)
            .SingleOrDefaultAsync(m => m.Id == id);

            _context.Remove(presentacion);
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
                        ModelState.AddModelError(string.Empty, $"No se puede eliminiar el Presentaciones por que está en uso");
                        return View(presentacion);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction(nameof(Index));

        }

        private bool PresentacionExists(int id)
        {
            return _context.Presentaciones.Any(e => e.Id == id);
        }



    }
}
