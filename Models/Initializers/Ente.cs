using Eureka.Extensions;
using Eureka.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Eureka.Models
{
    [Table("Ente")]
    public partial class Ente : BusinessLogicInitializer<Ente>
    {
        internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel { successed = true };

            if (_context.Entes.Any(x => x.NombreCompleto == NombreCompleto))
                badRequestViewModel = badRequestViewModel.AsError("Descripcion", $"Ya existe un ente con el nombre {NombreCompleto}");

            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Ente ente)
        {
            var badRequestViewModel = ente.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
