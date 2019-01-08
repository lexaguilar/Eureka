using System;
using System.Collections.Generic;

namespace Eureka.Models.EurekaModel
{
    public partial class Existencia
    {
        public int AreaId { get; set; }
        public int InventarioId { get; set; }
        public double Existencias { get; set; }
        public decimal CostoPromedio { get; set; }
        public double ExistenciaMinima { get; set; }
        public double ExistenciaMaxima { get; set; }
        public string ReglaDescuento { get; set; }
        public string ReglaPrecio { get; set; }

        public Area Area { get; set; }
        public Inventario Inventario { get; set; }
    }
}
