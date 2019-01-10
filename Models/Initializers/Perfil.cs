using Eureka.Extensions;
using Eureka.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Eureka.Models
{
    [Table("Perfil")]
    public partial class Perfil : BusinessLogicInitializer<Perfil>
    {
        internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel { successed = true };

            if (_context.Perfiles.Any(x => x.Username == Username))
                badRequestViewModel = badRequestViewModel.AsError("Descripcion", $"Ya existe un perfil con el username {Username}");

            if (_context.Perfiles.Any(x => x.Nombre == Nombre))
                badRequestViewModel = badRequestViewModel.AsError("Descripcion", $"Ya existe un perfil con el nombre {Nombre}");

            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Perfil perfil)
        {
            var badRequestViewModel = perfil.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
