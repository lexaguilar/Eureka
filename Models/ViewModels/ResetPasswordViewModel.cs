using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace  Eureka.Models.ViewModels
{
    public class ResetPasswordViewModel : LoginViewModel
    {
        [Required(ErrorMessage = "La confirmacion es requerida")]    
        [Display( Description="Confirmar contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmacion no son iguales.")]
        public string ConfirmPassword { get; set; }
    }
}
