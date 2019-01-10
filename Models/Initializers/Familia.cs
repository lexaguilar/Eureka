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

            if (_context.Familias.Any(x => x.Descripcion == Descripcion && x.Id != Id ))            
                badRequestViewModel = badRequestViewModel
                    .AsError("Descripcion", $"Ya existe una familia con la descripción {Descripcion}");

            if (_context.Familias.Any(x => x.Prefijo == Prefijo && x.Id != Id))
                badRequestViewModel = badRequestViewModel
                    .AsError("Prefijo", $"Ya existe el prefijo {Prefijo} en otra familia");

            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Familia familia)
        {
            var badRequestViewModel = familia.ValidateForCreate(_context);

            if (Prefijo != familia.Prefijo && _context.Inventarios.Any(i => i.FamiliaId == familia.Id))
                badRequestViewModel = badRequestViewModel
                    .AsError("Prefijo", $"El prefijo {Prefijo} ya se está usando en el inventario");

            return badRequestViewModel;
        }
    }
}
