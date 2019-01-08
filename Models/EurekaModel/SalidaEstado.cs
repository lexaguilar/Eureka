using System;
using System.Collections.Generic;

namespace Eureka.Models.EurekaModel
{
    public partial class SalidaEstado
    {
        public SalidaEstado()
        {
            Salida = new HashSet<Salida>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Salida> Salida { get; set; }
    }
}
