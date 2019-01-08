using System;
using System.Collections.Generic;

namespace Eureka.Models.EurekaModel
{
    public partial class Inventario
    {
        public Inventario()
        {
            CompraDetalle = new HashSet<CompraDetalle>();
            EntradaDetalle = new HashSet<EntradaDetalle>();
            Existencia = new HashSet<Existencia>();
            SalidaDetalle = new HashSet<SalidaDetalle>();
            ServicioEstandar = new HashSet<ServicioEstandar>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
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
        public int CodigoControl { get; set; }

        public Perfil CreadoPorNavigation { get; set; }
        public Estado Estado { get; set; }
        public Familia Familia { get; set; }
        public Perfil ModificadoPorNavigation { get; set; }
        public Presentacion Presentacion { get; set; }
        public Um UmNavigation { get; set; }
        public ICollection<CompraDetalle> CompraDetalle { get; set; }
        public ICollection<EntradaDetalle> EntradaDetalle { get; set; }
        public ICollection<Existencia> Existencia { get; set; }
        public ICollection<SalidaDetalle> SalidaDetalle { get; set; }
        public ICollection<ServicioEstandar> ServicioEstandar { get; set; }
    }
}
