using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("Existencia")]
    public partial class Existencia
    {
        [Display(Name = "Area")]
        public int AreaId { get; set; }
        [Display(Name = "Producto")]
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
