using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Eureka.Models
{
    [Table("CompraEstado")]
    public partial class CompraEstado
    {
        public CompraEstado()
        {
            Compras = new HashSet<Compra>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Compra> Compras { get; set; }
    }
}
