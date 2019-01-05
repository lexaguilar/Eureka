using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("SalidaEstado")]
    public partial class SalidaEstado
    {
        public SalidaEstado()
        {
            Salidas = new HashSet<Salida>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Salida> Salidas { get; set; }
    }
}
