using System;
using System.Collections.Generic;

namespace Eureka.Models
{
    public partial class EntradaEstado
    {
        public EntradaEstado()
        {
            Entrada = new HashSet<Entrada>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Entrada> Entrada { get; set; }
    }
}
