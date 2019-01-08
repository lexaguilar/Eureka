using System;
using System.Collections.Generic;

namespace Eureka.Models
{
    public partial class TipoEntrada
    {
        public TipoEntrada()
        {
            Entrada = new HashSet<Entrada>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int EstadoId { get; set; }

        public Estado Estado { get; set; }
        public ICollection<Entrada> Entrada { get; set; }
    }
}
