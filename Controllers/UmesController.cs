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
    public class UmesController : Controller
    {
        private readonly EurekaContext _context;
        JsonSerializerSettings config = new JsonSerializerSettings { MaxDepth = 1, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        public UmesController(EurekaContext context)
        {
            _context = context;
        }

        // GET: Ums
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Listar()
        {
            var eurekaContext = _context.Ums.Include(u => u.Estado);
            return Json(await eurekaContext.ToListAsync(), config);
        }

        // GET: Ums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var um = await _context.Ums
                    .Include(u => u.Estado)
            .SingleOrDefaultAsync(m => m.Id == id);
            if (um == null)
            {
                return NotFound();
            }

            return View(um);
        }

        // GET: Ums/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Descripcion");
            return View();
        }

        // POST: Ums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,EstadoId")] Um um)
        {
            var user = this.GetServiceUser();
            if (ModelState.IsValid)
            {
                _context.Add(um);
                await _context.SaveChangesAsync();
                return Json(um);
            }

            return View(um);
        }

        // GET: Ums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var um = await _context.Ums.SingleOrDefaultAsync(m => m.Id == id);
            if (um == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Descripcion", um.EstadoId);
            return View(um);
        }

        // POST: Ums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Descripcion,EstadoId")] Um um)
        {
            var user = this.GetServiceUser();
            if (ModelState.IsValid)
            {
                try
                {
                    var umOld = await _context.Ums.SingleOrDefaultAsync(m => m.Id == um.Id);

                    umOld.Descripcion = um.Descripcion;
                    umOld.EstadoId = um.EstadoId;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UmExists(um.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(um);
            }

            return BadRequest(um);
        }

        // GET: Ums/delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var um = await _context.Ums
    .Include(u => u.Estado)
            .SingleOrDefaultAsync(m => m.Id == id);
            if (um == null)
            {
                return NotFound();
            }

            return View(um);
        }

        // POST: Ums/delete/5

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)

        {
            var um = await _context.Ums
.Include(u => u.Estado)
            .SingleOrDefaultAsync(m => m.Id == id);

            _context.Remove(um);
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
                        ModelState.AddModelError(string.Empty, $"No se puede eliminiar el Ums por que está en uso");
                        return View(um);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction(nameof(Index));

        }

        private bool UmExists(int id)
        {
            return _context.Ums.Any(e => e.Id == id);
        }



    }
}
