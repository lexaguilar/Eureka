using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eureka.Extensions;
using Eureka.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Eureka.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/catalogs")]
    public class CatalogsController : Controller
    {
        private readonly EurekaContext _context;
        readonly JsonSerializerSettings config = new JsonSerializerSettings { MaxDepth = 1, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        public CatalogsController(EurekaContext context)
        {
            _context = context;
        }

        #region Estados
        [Route("obtener-estados-for-create")]
        public async Task<IActionResult> ListarEstadosForCreate()
        {
            var estados = _context.Estados
            .Select(x => new {
                id = x.Id.ToString(),
                text = x.Descripcion
            });
            return Json(await estados.ToListAsync(), config);
        }

        [Route("obtener-estados-for-edit")]
        public async Task<IActionResult> ListarEstadosForEdit()
        {
            var estados = _context.Estados
            .Select(x => new {
                id = x.Id.ToString(),
                text = x.Descripcion
            });
            return Json(await estados.ToListAsync(), config);
        }
        #endregion

        #region Areas
        [Route("obtener-areas-for-create")]
        public async Task<IActionResult> ListarAreasForCreate()
        {
            var areas = _context.Areas.Where(a => a.EstadoId == (int)Estados.Activado)
                .Select(x => new {
                    id= x.Id, text=  x.Descripcion
                });
            return Json(await areas.ToListAsync());
        }

        [Route("obtener-areas-for-edit")]
        public async Task<IActionResult> ListarAreasForEdit()
        {
            var areas = _context.Areas.Select(x => new {
                id = x.Id, text = x.Descripcion
            }); 
            return Json(await areas.ToListAsync());
        }
        #endregion

        #region Familias
        [Route("obtener-familias-for-create")]
        public async Task<IActionResult> ListarFamiliasForCreate()
        {
            var familias = _context.Familias.Where(f => f.EstadoId == (int)Estados.Activado)
            .Select(x => new {
                id = x.Id.ToString(),
                text = x.Descripcion
            });
            return Json(await familias.ToListAsync());
        }

        [Route("obtener-familias-for-edit")]
        public async Task<IActionResult> ListarFamiliasForEdit()
        {
            var familias = _context.Familias
            .Select(x => new {
                id = x.Id.ToString(),
                text = x.Descripcion
            });
            return Json(await familias.ToListAsync());
        }
        #endregion
        #region Umes
        [Route("obtener-umes-for-create")]
        public async Task<IActionResult> ListarUmesForCreate()
        {
            var umes = _context.Ums.Where(u => u.EstadoId == (int)Estados.Activado)
            .Select(x => new {
                id = x.Id.ToString(),
                text = x.Descripcion
            });
            return Json(await umes.ToListAsync());
        }

        [Route("obtener-umes-for-edit")]
        public async Task<IActionResult> ListarUmesForEdit()
        {
            var umes = _context.Ums
            .Select(x => new {
                id = x.Id.ToString(),
                text = x.Descripcion
            });
            return Json(await umes.ToListAsync());
        }
        #endregion
        #region Presentaciones
        [Route("obtener-presentaciones-for-create")]
        public async Task<IActionResult> ListarPresentacionesForCreate()
        {
            var presentaciones = _context.Presentaciones.Where(p => p.EstadoId == (int)Estados.Activado)
            .Select(x => new {
                id = x.Id.ToString(),
                text = x.Descripcion
            });
            return Json(await presentaciones.ToListAsync());
        }

        [Route("obtener-presentaciones-for-edit")]
        public async Task<IActionResult> ListarPresentacionesForEdit()
        {
            var presentaciones = _context.Presentaciones
            .Select(x => new {
                id = x.Id.ToString(),
                text = x.Descripcion
            });
            return Json(await presentaciones.ToListAsync());
        }
        #endregion


    }
}