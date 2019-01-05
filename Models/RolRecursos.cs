using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("RolRecursos")]
    public partial class RolRecursos
    {
        [Display(Name = "Rol")]
        public int RolId { get; set; }
        [Display(Name = "Recurso")]
        public int RecursoId { get; set; }

        public Recurso Recurso { get; set; }
        public Rol Rol { get; set; }
    }
}
