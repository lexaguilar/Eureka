
using Eureka.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eureka.Models
{
    public partial class Perfil : BusinessLogicInitializer<Perfil>

    {
        internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel
            {
                successed = true
            };

            if (_context.Perfiles.Any(x => x.Username == Username))
            {
                badRequestViewModel.successed = false;
                badRequestViewModel.item = "Descripcion";
                badRequestViewModel.mensaje = $"Ya existe un perfil con el alias {Username}";
            }

            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Perfil perfil)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel
            {
                successed = true
            };

            badRequestViewModel = perfil.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
