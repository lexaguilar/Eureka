
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
    public class AreasController : Controller
    {
        private readonly EurekaContext _context;
        readonly JsonSerializerSettings config = new JsonSerializerSettings { MaxDepth = 1, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        public AreasController(EurekaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var eurekaContext = _context.Areas
                .Include(a => a.Estado).ToArray();
            return View();
        }

        [Route("Areas/obtener-por/estado")]
        public async Task<IActionResult> obtenerListaEstado(int? id, int page)
        {
            var query = _context.Areas.Where(a => a.EstadoId == id);
            var total = query.Count();
            query = query.Skip((page - 1) * 10).Take(10);
            return Json(new
            {
                total,

                data = await query.Select(a => new
                {
                    a.Id,
                    a.Descripcion,
                    Estado = a.Estado.Descripcion,
                }).ToListAsync()
            }, config);
        }



        public async Task<IActionResult> Listar()
        {
            var eurekaContext = _context.Areas
                .Include(a => a.Estado);

            var result = await eurekaContext.Select(a => new
            {
                a.Id,
                a.Descripcion,
                a.EstadoId,
                Estado = a.Estado.Descripcion
            }).ToListAsync();

            return Json(result, config);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,EstadoId")] Area area)
        {
            var user = this.GetServiceUser();

            if (ModelState.IsValid)
            {
                var result = area.ValidateForCreate(_context);

                if (!result.successed)
                    return BadRequest(result);

                _context.Add(area);
                await _context.SaveChangesAsync();
                return Json(area, config);
            }

            return BadRequest(area);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas.SingleOrDefaultAsync(a => a.Id == id);
            if (area == null)
            {
                return NotFound();
            }
            return View(area);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Descripcion,EstadoId")] Area area)
        {
            var user = this.GetServiceUser();

            if (ModelState.IsValid)
            {
                try
                {

                    var areaOld = await _context.Areas.SingleOrDefaultAsync(a => a.Id == area.Id);

                    var result = area.ValidateForEdit(_context, areaOld);

                    if (!result.successed)
                        return BadRequest(result);

                    areaOld.CopyFrom(area, a => new
                    {
                        a.Descripcion,
                        a.EstadoId
                    });

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
                return Json(area, config);
            }

            return BadRequest(area);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas
                .Include(a => a.Estado)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (area == null)
            {
                return NotFound();
            }
            return View(area);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var area = await _context.Areas.SingleOrDefaultAsync(A => A.Id == id);

            _context.Remove(area);
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
                        ModelState.AddModelError(string.Empty, $"No se puede eliminiar la area con id {id} por que está en uso");
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas
                .Include(A => A.Estado)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (area == null)
            {
                return NotFound();
            }
            return View(area);
        }

        private bool AreaExists(int id)
        {
            return _context.Areas.Any(a => a.Id == id);
        }

    }
}
