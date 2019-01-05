﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("Salida")]
    public partial class Salida
    {
        public Salida()
        {
            SalidaDetalles= new HashSet<SalidaDetalle>();
        }

        public int Id { get; set; }
        [Display(Name = "Area")]
        public int AreaId { get; set; }
        [Display(Name = "Tipo Salida")]
        public int TipoSalidaId { get; set; }
        [Display(Name = "Forma Pago")]
        public int FormaPagoId { get; set; }
        public DateTime Fecha { get; set; }
        [Display(Name = "Cliente")]
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
        [Display(Name = "Estado")]
        public int EstadoId { get; set; }
        [Display(Name = "Salida estado")]
        public int SalidaEstadoId { get; set; }

        public Area Area { get; set; }
        public Perfil CreadoPorNavigation { get; set; }
        public Ente Ente { get; set; }
        public Estado Estado { get; set; }
        public FormaPago FormaPago { get; set; }
        public Perfil ModificadoPorNavigation { get; set; }
        public SalidaEstado SalidaEstado { get; set; }
        public TipoSalida TipoSalida { get; set; }
        public ICollection<SalidaDetalle> SalidaDetalles { get; set; }
    }
}