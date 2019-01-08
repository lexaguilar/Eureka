using System;
using System.Collections.Generic;

namespace Eureka.Models.EurekaModel
{
    public partial class TipoSalida
    {
        public TipoSalida()
        {
            Salida = new HashSet<Salida>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int EstadoId { get; set; }

        public Estado Estado { get; set; }
        public ICollection<Salida> Salida { get; set; }
    }
}
