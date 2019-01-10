using Eureka.Extensions;
using Eureka.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Eureka.Models
{
    [Table("Presentacion")]
    public partial class Presentacion : BusinessLogicInitializer<Presentacion>
    {
        internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel { successed = true };

            if (_context.Presentaciones.Any(x => x.Descripcion == Descripcion))
                badRequestViewModel = badRequestViewModel.AsError("Descripcion", $"Ya existe una presentacion con la descripción {Descripcion}");
                        
            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Presentacion presentacion)
        {      
            var badRequestViewModel = presentacion.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
