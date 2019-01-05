using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("Inventario")]
    public partial class Inventario
    {
        public Inventario()
        {
            CompraDetalles = new HashSet<CompraDetalle>();
            EntradaDetalles = new HashSet<EntradaDetalle>();
            Existencias = new HashSet<Existencia>();
            SalidaDetalles = new HashSet<SalidaDetalle>();
            ServiciosEstandares = new HashSet<ServicioEstandar>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Um { get; set; }
        public int FamiliaId { get; set; }
        public int PresentacionId { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEl { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? ModificadoEl { get; set; }
        public int EstadoId { get; set; }

        public Perfil CreadoPorNavigation { get; set; }
        public Estado Estado { get; set; }
        public Familia Familia { get; set; }
        public Perfil ModificadoPorNavigation { get; set; }
        public Presentacion Presentacion { get; set; }
        public Um UmNavigation { get; set; }
        public ICollection<CompraDetalle> CompraDetalles { get; set; }
        public ICollection<EntradaDetalle> EntradaDetalles { get; set; }
        public ICollection<Existencia> Existencias { get; set; }
        public ICollection<SalidaDetalle> SalidaDetalles { get; set; }
        public ICollection<ServicioEstandar> ServiciosEstandares { get; set; }
    }
}
