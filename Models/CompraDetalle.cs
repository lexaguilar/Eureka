using System;
using System.Collections.Generic;

namespace Eureka.Models
{
    public partial class CompraDetalle
    {
        public int Id { get; set; }
        public int CompraId { get; set; }
        public int InventarioId { get; set; }
        public double CantSolicitada { get; set; }
        public double CantRecivida { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public decimal NuevoPrecio { get; set; }
        public decimal CostoPromedio { get; set; }
        public string NoFactura { get; set; }

        public Compra Compra { get; set; }
        public Inventario Inventario { get; set; }
    }
}
