using Eureka.Extensions;
using Eureka.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Eureka.Models
{
    [Table("FormaPago")]
    public partial class FormaPago : BusinessLogicInitializer<FormaPago>
    {
        internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel { successed = true };

            if (_context.FormaPagos.Any(x => x.Descripcion == Descripcion))
                badRequestViewModel = badRequestViewModel.AsError("Descripcion", $"Ya existe una forma de pago con la descripción {Descripcion}");
                        
            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, FormaPago formaPago)
        {
            var badRequestViewModel = formaPago.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
