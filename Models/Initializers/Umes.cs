using Eureka.Extensions;
using Eureka.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Eureka.Models
{
    [Table("Um")]
    public partial class Um : BusinessLogicInitializer<Um>
    {
        internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel
            {
                successed = true
            };

            if (_context.Ums.Any(x => x.Descripcion == Descripcion))
                badRequestViewModel = badRequestViewModel.AsError("Descripcion", $"Ya existe una unidad de medida  con la descripción {Descripcion}");
            
            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Um um)
        {
            var badRequestViewModel = um.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
