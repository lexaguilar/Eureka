using Eureka.Extensions;
using Eureka.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Eureka.Models
{
    [Table("Familia")]
    public partial class Familia : BusinessLogicInitializer<Familia>
    {
        internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel { successed = true };

            if (_context.Familias.Any(x => x.Descripcion == Descripcion))            
                badRequestViewModel = badRequestViewModel
                    .AsError("Descripcion", $"Ya existe una familia con la descripción {Descripcion}");

            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Familia familia)
        {
            var badRequestViewModel = familia.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
