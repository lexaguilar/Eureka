using Eureka.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eureka.Models
{
    public partial class Um : BusinessLogicInitializer<Um>
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
                badRequestViewModel.mensaje = $"Ya existe una unidad de medida con la descripcion {Descripcion}";
            }

            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Um um)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel
            {
                successed = true
            };

            badRequestViewModel = um.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
