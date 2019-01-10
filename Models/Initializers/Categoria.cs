using Eureka.Extensions;
using Eureka.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Eureka.Models
{
    [Table("Categoria")]
    public partial class Categoria : BusinessLogicInitializer<Categoria>
    {
       internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
       {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel { successed = true };

            if (_context.Categorias.Any(x => x.Descripcion == Descripcion))
                badRequestViewModel = badRequestViewModel.AsError("Descripcion", $"Ya existe una categoria con la descripción {Descripcion}");

            return badRequestViewModel;
        }
       internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Categoria categoria)
       {
            var badRequestViewModel = categoria.ValidateForCreate(_context);

            return badRequestViewModel;
       }
    }
}
