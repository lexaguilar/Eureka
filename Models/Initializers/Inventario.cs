using Eureka.Extensions;
using Eureka.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Eureka.Models
{
    [Table("Inventario")]
    public partial class Inventario : BusinessLogicInitializer<Inventario>
    {
        internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel { successed = true };

            if (_context.Inventarios.Any(x => x.Descripcion == Descripcion))
                badRequestViewModel = badRequestViewModel.AsError("Descripcion", $"Ya existe un producto con la descripción {Descripcion}");
                       
            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Inventario inventario)
        {
            var badRequestViewModel = inventario.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
