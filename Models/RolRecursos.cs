using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("RolRecursos")]
    public partial class RolRecursos
    {
        public int RolId { get; set; }
        public int RecursoId { get; set; }

        public Recurso Recurso { get; set; }
        public Rol Rol { get; set; }
    }
}
