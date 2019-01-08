using System;
using System.Collections.Generic;

namespace Eureka.Models
{
    public partial class Salida
    {
        public Salida()
        {
            SalidaDetalle = new HashSet<SalidaDetalle>();
        }

        public int Id { get; set; }
        public int AreaId { get; set; }
        public int TipoSalidaId { get; set; }
        public int FormaPagoId { get; set; }
        public DateTime Fecha { get; set; }
        public int EnteId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public string Observacion { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEl { get; set; }
        public string ModificadoPor { get; set; }
        public string ModificadoEl { get; set; }
        public int EstadoId { get; set; }
        public int SalidaEstadoId { get; set; }

        public Area Area { get; set; }
        public Perfil CreadoPorNavigation { get; set; }
        public Ente Ente { get; set; }
        public Estado Estado { get; set; }
        public FormaPago FormaPago { get; set; }
        public Perfil ModificadoPorNavigation { get; set; }
        public SalidaEstado SalidaEstado { get; set; }
        public TipoSalida TipoSalida { get; set; }
        public ICollection<SalidaDetalle> SalidaDetalle { get; set; }
    }
}
