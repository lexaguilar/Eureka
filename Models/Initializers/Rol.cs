using Eureka.Extensions;
using Eureka.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Eureka.Models
{
    [Table("Rol")]
    public partial class Rol : BusinessLogicInitializer<Rol>
    {
        internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel { successed = true };

            if (_context.Roles.Any(x => x.Descripcion == Descripcion))
                badRequestViewModel = badRequestViewModel.AsError("Descripcion", $"Ya existe un rol con la descripción {Descripcion}");

            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Rol rol)
        {
            var badRequestViewModel = rol.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
