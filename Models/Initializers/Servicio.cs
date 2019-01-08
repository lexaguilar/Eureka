using Eureka.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eureka.Models
{
    public partial class Servicio : BusinessLogicInitializer<Servicio>
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
                badRequestViewModel.mensaje = $"Ya existe un servicio con la descripcion {Descripcion}";
            }

            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Servicio servicio)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel
            {
                successed = true
            };

            badRequestViewModel = servicio.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
