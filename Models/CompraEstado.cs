using System;
using System.Collections.Generic;

namespace Eureka.Models
{
    public partial class CompraEstado
    {
        public CompraEstado()
        {
            Compra = new HashSet<Compra>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Compra> Compra { get; set; }
    }
}
