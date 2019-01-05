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
    public class AreasController : Controller
    {
        private readonly EurekaContext _context;
        JsonSerializerSettings config = new JsonSerializerSettings { MaxDepth = 1, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        public AreasController(EurekaContext context)
        {
            _context = context;
        }

        // GET: Areas
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Listar()
        {
            var eurekaContext = _context.Areas.Include(a => a.Estado);
            return Json(await eurekaContext.ToListAsync(), config);
        }

        public async Task<IActionResult> ListarPerfiles()
        {
            var result = _context.Areas.Include(a => a.Perfiles).Select(p=>p.Perfiles.Take(100));
            return Json(await result.ToListAsync(), config);
        }

        // GET: Areas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas
                    .Include(a => a.Estado)
            .SingleOrDefaultAsync(m => m.Id == id);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // GET: Areas/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Descripcion");
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,EstadoId")] Area area)
        {
            var user = this.GetServiceUser();
            if (ModelState.IsValid)
            {
                _context.Add(area);
                await _context.SaveChangesAsync();
                return Json(area);
            }
            return View(area);
        }

        // GET: Areas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas.SingleOrDefaultAsync(m => m.Id == id);
            if (area == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Descripcion", area.EstadoId);
            return View(area);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Descripcion,EstadoId")] Area area)
        {
            var user = this.GetServiceUser();
            if (ModelState.IsValid)
            {
                try
                {
                    var areaOld = await _context.Areas.SingleOrDefaultAsync(m => m.Id == area.Id);

                    areaOld.Descripcion = area.Descripcion;
                    areaOld.EstadoId = area.EstadoId;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaExists(area.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(area);
            }
            return BadRequest(area);
        }

        // GET: Areas/delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas
            .Include(a => a.Estado)
            .SingleOrDefaultAsync(m => m.Id == id);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // POST: Areas/delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var area = await _context.Areas
            .Include(a => a.Estado)
            .SingleOrDefaultAsync(m => m.Id == id);

            _context.Remove(area);
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
                        ModelState.AddModelError(string.Empty, $"No se puede eliminiar el Areas por que está en uso");
                        return View(area);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction(nameof(Index));

        }

        private bool AreaExists(int id)
        {
            return _context.Areas.Any(e => e.Id == id);
        }



    }
}
