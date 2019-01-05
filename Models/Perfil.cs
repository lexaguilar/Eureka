using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("Perfil")]
    public partial class Perfil
    {
        public Perfil()
        {
            ComprasCreadas = new HashSet<Compra>();
            ComprasModificadas = new HashSet<Compra>();
            EntesCreados = new HashSet<Ente>();
            EntesModificados = new HashSet<Ente>();
            EntradasCreadas = new HashSet<Entrada>();
            EntradasModificadas = new HashSet<Entrada>();
            InventariosCreados = new HashSet<Inventario>();
            InventariosModificados = new HashSet<Inventario>();
            SalidasCreadas= new HashSet<Salida>();
            SalidasModificadas = new HashSet<Salida>();
            ServiciosCreados = new HashSet<Servicio>();
            ServiciosEstandaresCreados = new HashSet<ServicioEstandar>();
            ServiciosEstandaresModificados = new HashSet<ServicioEstandar>();
            ServiciosModificados = new HashSet<Servicio>();
        }
        [Display(Name = "Usuario")]
        public string Username { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [Display(Name = "Rol")]
        public int RolId { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        [Display(Name = "Area")]
        public int AreaId { get; set; }
        public string UrlTemporal { get; set; }
        public DateTime? UtcreadoEl { get; set; }
        [Display(Name = "Estado")]
        public int EstadoId { get; set; }

        public Area Area { get; set; }
        public Estado Estado { get; set; }
        public Rol Rol { get; set; }
        public ICollection<Compra> ComprasCreadas { get; set; }
        public ICollection<Compra> ComprasModificadas{ get; set; }
        public ICollection<Ente> EntesCreados { get; set; }
        public ICollection<Ente> EntesModificados { get; set; }
        public ICollection<Entrada> EntradasCreadas { get; set; }
        public ICollection<Entrada> EntradasModificadas { get; set; }
        public ICollection<Inventario> InventariosCreados{ get; set; }
        public ICollection<Inventario> InventariosModificados{ get; set; }
        public ICollection<Salida> SalidasCreadas{ get; set; }
        public ICollection<Salida> SalidasModificadas { get; set; }
        public ICollection<Servicio> ServiciosCreados{ get; set; }
        public ICollection<ServicioEstandar> ServiciosEstandaresCreados{ get; set; }
        public ICollection<ServicioEstandar> ServiciosEstandaresModificados{ get; set; }
        public ICollection<Servicio> ServiciosModificados{ get; set; }
    }
}
