using Eureka.Extensions;
using Eureka.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Eureka.Models
{
    [Table("TipoEnte")]
    public partial class TipoEnte : BusinessLogicInitializer<TipoEnte>
    {
        internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel { successed = true };

            if (_context.TipoEntes.Any(x => x.Descripcion == Descripcion))
                badRequestViewModel = badRequestViewModel.AsError("Descripcion", $"Ya existe un tipo de ente con la descripción {Descripcion}");

            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, TipoEnte tipoEnte)
        {
            var badRequestViewModel = tipoEnte.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
