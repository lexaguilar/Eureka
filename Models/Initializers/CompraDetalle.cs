using Eureka.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("CompraDetalle")]
    public partial class CompraDetalle : BusinessLogicInitializer<CompraDetalle>
    {
        internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel { successed = true };

            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, CompraDetalle compraDetalle)
        {
            var badRequestViewModel = compraDetalle.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
