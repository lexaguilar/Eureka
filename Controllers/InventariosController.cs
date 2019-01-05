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
    public class InventariosController : Controller
    {
        private readonly EurekaContext _context;
        JsonSerializerSettings config = new JsonSerializerSettings { MaxDepth = 1, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        public InventariosController(EurekaContext context)
        {
            _context = context;
        }

        // GET: Inventarios
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Listar()
        {
            var eurekaContext = _context.Inventarios.Include(i => i.CreadoPorNavigation).Include(i => i.Estado).Include(i => i.Familia).Include(i => i.ModificadoPorNavigation).Include(i => i.Presentacion).Include(i => i.UmNavigation);
            return Json(await eurekaContext.ToListAsync(), config);
        }

        // GET: Inventarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventarios
                    .Include(i => i.CreadoPorNavigation)
                    .Include(i => i.Estado)
                    .Include(i => i.Familia)
                    .Include(i => i.ModificadoPorNavigation)
                    .Include(i => i.Presentacion)
                    .Include(i => i.UmNavigation)
            .SingleOrDefaultAsync(m => m.Id == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // GET: Inventarios/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Descripcion");
            ViewData["FamiliaId"] = new SelectList(_context.Familias, "Id", "Descripcion");
            ViewData["PresentacionId"] = new SelectList(_context.Presentaciones, "Id", "Descripcion");
            ViewData["Um"] = new SelectList(_context.Ums, "Id", "Descripcion");
            return View();
        }

        // POST: Inventarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Um,FamiliaId,PresentacionId,EstadoId")] Inventario inventario)
        {
            var user = this.GetServiceUser();
            inventario.CreadoPor = user.Username;
            ModelState.Remove("CreadoPor");
            inventario.CreadoEl = DateTime.Now;
            ModelState.Remove("CreadoEl");
            if (ModelState.IsValid)
            {
                _context.Add(inventario);
                await _context.SaveChangesAsync();
                return Json(inventario);
            }

            return View(inventario);
        }

        // GET: Inventarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventarios.SingleOrDefaultAsync(m => m.Id == id);
            if (inventario == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Descripcion", inventario.EstadoId);
            ViewData["FamiliaId"] = new SelectList(_context.Familias, "Id", "Descripcion", inventario.FamiliaId);
            ViewData["PresentacionId"] = new SelectList(_context.Presentaciones, "Id", "Descripcion", inventario.PresentacionId);
            ViewData["Um"] = new SelectList(_context.Ums, "Id", "Descripcion", inventario.Um);
            return View(inventario);
        }

        // POST: Inventarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Nombre,Descripcion,Um,FamiliaId,PresentacionId,EstadoId")] Inventario inventario)
        {
            var user = this.GetServiceUser();
            inventario.ModificadoPor = user.Username;
            inventario.ModificadoEl = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    var inventarioOld = await _context.Inventarios.SingleOrDefaultAsync(m => m.Id == inventario.Id);

                    inventarioOld.Nombre = inventario.Nombre;
                    inventarioOld.Descripcion = inventario.Descripcion;
                    inventarioOld.Um = inventario.Um;
                    inventarioOld.FamiliaId = inventario.FamiliaId;
                    inventarioOld.PresentacionId = inventario.PresentacionId;
                    inventarioOld.EstadoId = inventario.EstadoId;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarioExists(inventario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(inventario);
            }

            return BadRequest(inventario);
        }

        // GET: Inventarios/delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventarios
    .Include(i => i.CreadoPorNavigation)
    .Include(i => i.Estado)
    .Include(i => i.Familia)
    .Include(i => i.ModificadoPorNavigation)
    .Include(i => i.Presentacion)
    .Include(i => i.UmNavigation)
            .SingleOrDefaultAsync(m => m.Id == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // POST: Inventarios/delete/5

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)

        {
            var inventario = await _context.Inventarios
.Include(i => i.CreadoPorNavigation)
.Include(i => i.Estado)
.Include(i => i.Familia)
.Include(i => i.ModificadoPorNavigation)
.Include(i => i.Presentacion)
.Include(i => i.UmNavigation)
            .SingleOrDefaultAsync(m => m.Id == id);

            _context.Remove(inventario);
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
                        ModelState.AddModelError(string.Empty, $"No se puede eliminiar el Inventarios por que está en uso");
                        return View(inventario);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction(nameof(Index));

        }

        private bool InventarioExists(int id)
        {
            return _context.Inventarios.Any(e => e.Id == id);
        }



    }
}
