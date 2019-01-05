using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("EntradaDetalle")]
    public partial class EntradaDetalle
    {
        public int Id { get; set; }
        public int EntradaId { get; set; }
        public int InventarioId { get; set; }
        public double Cantidad { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public decimal PrecioAnterior { get; set; }
        public decimal PrecioNuevo { get; set; }
        public decimal CostoPromedio { get; set; }
        public double Existencias { get; set; }

        public Entrada Entrada { get; set; }
        public Inventario Inventarios { get; set; }
    }
}
