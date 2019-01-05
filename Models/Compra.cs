﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("Compra")]
    public partial class Compra
    {
        public Compra()
        {
            CompraDetalles = new HashSet<CompraDetalle>();
        }

        public int Id { get; set; }
        [Display(Name = "Area")]
        public int AreaId { get; set; }
        [Display(Name = "Forma de pago")]
        public int FormaPagoId { get; set; }
        public DateTime Fecha { get; set; }
        [Display(Name = "Proveedor")]
        public int EnteId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public string Observacion { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEl { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? ModificadoEl { get; set; }
        [Display(Name = "Estado")]
        public int EstadoId { get; set; }
        [Display(Name = "Compra Estado")]
        public int CompraEstadoId { get; set; }

        public Area Area { get; set; }
        public CompraEstado CompraEstado { get; set; }
        public Perfil CreadoPorNavigation { get; set; }
        public Ente Ente { get; set; }
        public Estado Estado { get; set; }
        public FormaPago FormaPago { get; set; }
        public Perfil ModificadoPorNavigation { get; set; }
        public ICollection<CompraDetalle> CompraDetalles { get; set; }
    }
}