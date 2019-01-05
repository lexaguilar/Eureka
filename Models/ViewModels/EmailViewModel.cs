using System;
using System.ComponentModel.DataAnnotations;

namespace Eureka.Models.ViewModels
{
    public class EmailViewModel
    {
        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress]
        public string Email { get; set; }        
        
    }
}