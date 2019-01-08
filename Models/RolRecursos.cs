using System;
using System.Collections.Generic;

namespace Eureka.Models
{
    public partial class RolRecursos
    {
        public int RolId { get; set; }
        public int RecursoId { get; set; }

        public Recurso Recurso { get; set; }
        public Rol Rol { get; set; }
    }
}
