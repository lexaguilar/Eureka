using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("FormaPago")]
    public partial class FormaPago
    {
        public FormaPago()
        {
            Compras = new HashSet<Compra>();
            Entradas = new HashSet<Entrada>();
            Salidas = new HashSet<Salida>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int EstadoId { get; set; }

        public Estado Estado { get; set; }
        public ICollection<Compra> Compras { get; set; }
        public ICollection<Entrada> Entradas { get; set; }
        public ICollection<Salida> Salidas { get; set; }
    }
}
