using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("Recurso")]
    public partial class Recurso
    {
        public Recurso()
        {
            RolRecursos = new HashSet<RolRecursos>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int EstadoId { get; set; }

        public Estado Estado { get; set; }
        public ICollection<RolRecursos> RolRecursos { get; set; }
    }
}
