using System;
using System.Collections.Generic;

namespace Eureka.Models.EurekaModel
{
    public partial class SalidaDetalle
    {
        public int Id { get; set; }
        public int SalidaId { get; set; }
        public int InventarioId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public decimal CostoPromedio { get; set; }
        public double Existencias { get; set; }
        public int? ServicioEstandarId { get; set; }

        public Inventario Inventario { get; set; }
        public Salida Salida { get; set; }
        public ServicioEstandar ServicioEstandar { get; set; }
    }
}
