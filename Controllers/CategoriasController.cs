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
using Microsoft.AspNetCore.Authorization;

namespace Eureka.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly EurekaContext _context;
        readonly JsonSerializerSettings config = new JsonSerializerSettings { MaxDepth = 1, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        public CategoriasController(EurekaContext context)
        {
            _context = context;
        }

        // GET: Categorias
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Listar()
        {
            var eurekaContext = _context.Categorias;
            return Json(await eurekaContext.ToListAsync(), config);
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
            .SingleOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Nota")] Categoria categoria)
        {
            var user = this.GetServiceUser();
            if (ModelState.IsValid)
            {
                var result = categoria.ValidateForCreate(_context);

                if (!result.successed)
                    return BadRequest(result);

                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return Json(categoria, config);
            }

            return BadRequest(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.SingleOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Descripcion,Nota")] Categoria categoria)
        {
            var user = this.GetServiceUser();
            if (ModelState.IsValid)
            {
                try
                {
                    var categoriaOld = await _context.Categorias.SingleOrDefaultAsync(m => m.Id == categoria.Id);

                    var result = categoria.ValidateForEdit(_context, categoriaOld);

                    if (!result.successed)
                        return BadRequest(result);

                    categoriaOld.CopyFrom(categoria, x => new { x.Descripcion, x.Nota });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(categoria, config);
            }

            return BadRequest(categoria);
        }

        // GET: Categorias/delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
            .SingleOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/delete/5

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)

        {
            var categoria = await _context.Categorias.SingleOrDefaultAsync(m => m.Id == id);

            _context.Remove(categoria);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbex)
            {
                if (dbex.GetBaseException() is SqlException sqlException)
                {
                    var number = sqlException.Number;

                    if (number == 547)
                    {
                        ModelState.AddModelError(string.Empty, $"No se puede eliminiar el {id} por que está en uso");
                        return View(categoria);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction(nameof(Index));

        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }



    }
}
