
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eureka.Extensions;
using System.Data.SqlClient;
using Eureka.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Eureka.Controllers
{
    [Authorize]
    public class FamiliasController : Controller
    {
        private readonly EurekaContext _context;
        readonly JsonSerializerSettings config = new JsonSerializerSettings { MaxDepth = 1, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        public FamiliasController(EurekaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Familias/obtener-por/estado")]
        public async Task<IActionResult> obtenerListaEstado(int? id, int page)
        {
            var query = _context.Familias.Where(f => f.EstadoId == id);
            var total = query.Count();
            query = query.Skip((page - 1) * 10).Take(10);
            return Json(new
            {
                total,

                data = await query.Select(f => new
                {
                    f.Id,
                    f.Descripcion,
                    f.Prefijo,
                    Estado = f.Estado.Descripcion,
                }).ToListAsync()
            }, config);
        }



        public async Task<IActionResult> Listar()
        {
            var eurekaContext = _context.Familias
                .Include(f => f.Estado);

            var result = await eurekaContext.Select(f => new
            {
                f.Id,
                f.Descripcion,
                f.EstadoId,
                f.Prefijo,
                Estado = f.Estado.Descripcion
            }).ToListAsync();

            return Json(result, config);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,EstadoId,Prefijo")] Familia familia)
        {
            var user = this.GetServiceUser();

            if (ModelState.IsValid)
            {
                var result = familia.ValidateForCreate(_context);

                if (!result.successed)
                    return BadRequest(result);

                _context.Add(familia);
                await _context.SaveChangesAsync();
                return Json(familia, config);
            }

            return BadRequest(familia);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familia = await _context.Familias.SingleOrDefaultAsync(f => f.Id == id);
            if (familia == null)
            {
                return NotFound();
            }
            return View(familia);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Descripcion,EstadoId,Prefijo")] Familia familia)
        {
            var user = this.GetServiceUser();

            if (ModelState.IsValid)
            {
                try
                {

                    var familiaOld = await _context.Familias.SingleOrDefaultAsync(f => f.Id == familia.Id);

                    var result = familia.ValidateForEdit(_context, familiaOld);

                    if (!result.successed)
                        return BadRequest(result);

                    familiaOld.CopyFrom(familia, f => new {
                        f.Descripcion,
                        f.EstadoId,
                        f.Prefijo
                    });

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamiliaExists(familia.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(familia, config);
            }

            return BadRequest(familia);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familia = await _context.Familias
                .Include(f => f.Estado)
                .SingleOrDefaultAsync(f => f.Id == id);

            if (familia == null)
            {
                return NotFound();
            }
            return View(familia);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var familia = await _context.Familias.SingleOrDefaultAsync(F => F.Id == id);

            _context.Remove(familia);
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
                        ModelState.AddModelError(string.Empty, $"No se puede eliminiar la familia con id {id} por que está en uso");
                        return View(familia);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familia = await _context.Familias
                .Include(F => F.Estado)
                .SingleOrDefaultAsync(f => f.Id == id);

            if (familia == null)
            {
                return NotFound();
            }
            return View(familia);
        }

        private bool FamiliaExists(int id)
        {
            return _context.Familias.Any(f => f.Id == id);
        }

    }
}
