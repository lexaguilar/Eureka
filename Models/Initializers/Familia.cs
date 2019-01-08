using Eureka.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eureka.Models
{
    public partial class Familia : BusinessLogicInitializer<Familia>
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
                badRequestViewModel.mensaje = $"Ya existe una familia con la descripcion {Descripcion}";
            }

            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Familia familia)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel
            {
                successed = true
            };

            badRequestViewModel = familia.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
