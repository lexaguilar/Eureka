using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("Um")]
    public partial class Um
    {
        public Um()
        {
            Inventarios = new HashSet<Inventario>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int EstadoId { get; set; }

        public Estado Estado { get; set; }
        public ICollection<Inventario> Inventarios { get; set; }
    }
}
