using System;
using System.Collections.Generic;

namespace Eureka.Models
{
    public partial class Perfil
    {
        public Perfil()
        {
            CompraCreadoPorNavigation = new HashSet<Compra>();
            CompraModificadoPorNavigation = new HashSet<Compra>();
            EnteCreadoPorNavigation = new HashSet<Ente>();
            EnteModificadoPorNavigation = new HashSet<Ente>();
            EntradaCreadoPorNavigation = new HashSet<Entrada>();
            EntradaModificadoPorNavigation = new HashSet<Entrada>();
            InventarioCreadoPorNavigation = new HashSet<Inventario>();
            InventarioModificadoPorNavigation = new HashSet<Inventario>();
            SalidaCreadoPorNavigation = new HashSet<Salida>();
            SalidaModificadoPorNavigation = new HashSet<Salida>();
            ServicioCreadoPorNavigation = new HashSet<Servicio>();
            ServicioEstandarCreadoPorNavigation = new HashSet<ServicioEstandar>();
            ServicioEstandarModificadoPorNavigation = new HashSet<ServicioEstandar>();
            ServicioModificadoPorNavigation = new HashSet<Servicio>();
        }

        public string Username { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int RolId { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        public int AreaId { get; set; }
        public string UrlTemporal { get; set; }
        public DateTime? UtcreadoEl { get; set; }
        public int EstadoId { get; set; }

        public Area Area { get; set; }
        public Estado Estado { get; set; }
        public Rol Rol { get; set; }
        public ICollection<Compra> CompraCreadoPorNavigation { get; set; }
        public ICollection<Compra> CompraModificadoPorNavigation { get; set; }
        public ICollection<Ente> EnteCreadoPorNavigation { get; set; }
        public ICollection<Ente> EnteModificadoPorNavigation { get; set; }
        public ICollection<Entrada> EntradaCreadoPorNavigation { get; set; }
        public ICollection<Entrada> EntradaModificadoPorNavigation { get; set; }
        public ICollection<Inventario> InventarioCreadoPorNavigation { get; set; }
        public ICollection<Inventario> InventarioModificadoPorNavigation { get; set; }
        public ICollection<Salida> SalidaCreadoPorNavigation { get; set; }
        public ICollection<Salida> SalidaModificadoPorNavigation { get; set; }
        public ICollection<Servicio> ServicioCreadoPorNavigation { get; set; }
        public ICollection<ServicioEstandar> ServicioEstandarCreadoPorNavigation { get; set; }
        public ICollection<ServicioEstandar> ServicioEstandarModificadoPorNavigation { get; set; }
        public ICollection<Servicio> ServicioModificadoPorNavigation { get; set; }
    }
}
