using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("TipoSalida")]
    public partial class TipoSalida
    {
        public TipoSalida()
        {
            Salidas = new HashSet<Salida>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int EstadoId { get; set; }

        public Estado Estado { get; set; }
        public ICollection<Salida> Salidas { get; set; }
    }
}
