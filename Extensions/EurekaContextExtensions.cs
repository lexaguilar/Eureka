using Eureka.Models;
using Eureka.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eureka.Extensions
{
    static class EurekaContextExtensions
    {       

        internal static void SetCodeFromFamily(this Inventario inventario, EurekaContext _context)
        {
            var maxCode = 0;
            if (_context.Inventarios.Any(x => x.Id == inventario.FamiliaId)) 
                maxCode = _context.Inventarios.Where(x => x.Id == inventario.FamiliaId).Max(x => x.CodigoControl);
            
            maxCode++;
            var familia = _context.Familias.FirstOrDefault(x => x.Id == inventario.FamiliaId);
            inventario.CodigoControl = maxCode;
            inventario.Codigo = familia.Prefijo + maxCode.ToString().PadLeft(6, '0');
        }

        internal static BadRequestViewModel AsError(this BadRequestViewModel badRequestViewModel, string item, string mensaje)
        {
            badRequestViewModel.successed = false;
            badRequestViewModel.item = item;
            badRequestViewModel.mensaje = mensaje;
            return badRequestViewModel;
        }

    }
}