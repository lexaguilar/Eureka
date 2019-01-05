using System;
using System.ComponentModel.DataAnnotations;

namespace Eureka.Models.ViewModels
{
    public class LoginViewModel 
    {        
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Username { get; set; }      

        [Display( Description="Contraseña")]
        [Required(ErrorMessage = "La contraseña es requerida")]        
        [DataType(DataType.Password)]
        public string Password  { get; set; }      
    }
}