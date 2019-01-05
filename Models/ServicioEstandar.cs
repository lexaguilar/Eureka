using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("ServicioEstandar")]
    public partial class ServicioEstandar
    {
        
        public ServicioEstandar()
        {
            SalidaDetalles = new HashSet<SalidaDetalle>();
        }

        public int Id { get; set; }
        [Display(Name = "Servicio")]
        public int ServicioId { get; set; }
        [Display(Name = "Producto")]
        public int InventarioId { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEl { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? ModificadoEl { get; set; }
        [Display(Name = "Estado")]
        public int EstadoId { get; set; }

        public Perfil CreadoPorNavigation { get; set; }
        public Estado Estado { get; set; }
        public Inventario Inventario { get; set; }
        public Perfil ModificadoPorNavigation { get; set; }
        public Servicio Servicio { get; set; }
        public ICollection<SalidaDetalle> SalidaDetalles { get; set; }
    }
}
