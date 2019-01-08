using System;
using System.Collections.Generic;

namespace Eureka.Models
{
    public partial class ServicioEstandar
    {
        public ServicioEstandar()
        {
            SalidaDetalle = new HashSet<SalidaDetalle>();
        }

        public int Id { get; set; }
        public int ServicioId { get; set; }
        public int InventarioId { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEl { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? ModificadoEl { get; set; }
        public int EstadoId { get; set; }

        public Perfil CreadoPorNavigation { get; set; }
        public Estado Estado { get; set; }
        public Inventario Inventario { get; set; }
        public Perfil ModificadoPorNavigation { get; set; }
        public Servicio Servicio { get; set; }
        public ICollection<SalidaDetalle> SalidaDetalle { get; set; }
    }
}
