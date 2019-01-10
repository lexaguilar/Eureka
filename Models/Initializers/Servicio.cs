using Eureka.Extensions;
using Eureka.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Eureka.Models
{
    [Table("Servicio")]
    public partial class Servicio : BusinessLogicInitializer<Servicio>
    {
        internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel { successed = true };

            if (_context.Servicios.Any(x => x.Descripcion == Descripcion))
                badRequestViewModel = badRequestViewModel.AsError("Descripcion", $"Ya existe un servicio con la descripción {Descripcion}");
          
            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Servicio servicio)
        {
            var badRequestViewModel = servicio.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
