using Eureka.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Eureka.Models
{
    [Table("Compra")]
    public partial class Compra : BusinessLogicInitializer<Compra>
    {
        internal override BadRequestViewModel ValidateForCreate(EurekaContext _context)
        {
            BadRequestViewModel badRequestViewModel = new BadRequestViewModel { successed = true };

            return badRequestViewModel;
        }
        internal override BadRequestViewModel ValidateForEdit(EurekaContext _context, Compra compra)
        {
            var badRequestViewModel = compra.ValidateForCreate(_context);

            return badRequestViewModel;
        }
    }
}
