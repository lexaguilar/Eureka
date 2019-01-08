using System;
using System.Collections.Generic;

namespace Eureka.Models.EurekaModel
{
    public partial class Entrada
    {
        public Entrada()
        {
            EntradaDetalle = new HashSet<EntradaDetalle>();
        }

        public int Id { get; set; }
        public int AreaId { get; set; }
        public int TipoEntradaId { get; set; }
        public int FormaPagoId { get; set; }
        public DateTime Fecha { get; set; }
        public int EnteId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public int DiasPlazo { get; set; }
        public string Observacion { get; set; }
        public decimal Abonado { get; set; }
        public int? SalidaId { get; set; }
        public int? CompraId { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEl { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? ModificadoEl { get; set; }
        public int EstadoId { get; set; }
        public int EntradaEstadoId { get; set; }

        public Area Area { get; set; }
        public Perfil CreadoPorNavigation { get; set; }
        public Ente Ente { get; set; }
        public EntradaEstado EntradaEstado { get; set; }
        public Estado Estado { get; set; }
        public FormaPago FormaPago { get; set; }
        public Perfil ModificadoPorNavigation { get; set; }
        public TipoEntrada TipoEntrada { get; set; }
        public ICollection<EntradaDetalle> EntradaDetalle { get; set; }
    }
}
