using Eureka.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eureka.Models
{
    public partial class Inventario : BusinessLogicInitializer<Inventario>
    {
        internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel
            {
                successed = true
            };

            if (_context.Familias.Any(x => x.Descripcion == Descripcion))
            {
                badRequestViewModel.successed = false;
                badRequestViewModel.item = "Descripcion";
                badRequestViewModel.mensaje = $"Ya existe un producto con la descripcion {Descripcion}";
            }

            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Inventario inventario)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel
            {
                successed = true
            };

            badRequestViewModel = inventario.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
