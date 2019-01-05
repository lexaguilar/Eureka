using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("Estado")]

    public partial class Estado
    {
        public Estado()
        {
            Areas = new HashSet<Area>();
            Compras = new HashSet<Compra>();
            Entes = new HashSet<Ente>();
            Entradas = new HashSet<Entrada>();
            Familias = new HashSet<Familia>();
            FormaPagos = new HashSet<FormaPago>();
            Inventarios = new HashSet<Inventario>();
            Perfiles = new HashSet<Perfil>();
            Presentaciones = new HashSet<Presentacion>();
            Recursos = new HashSet<Recurso>();
            Roles = new HashSet<Rol>();
            Salidas = new HashSet<Salida>();
            Servicios = new HashSet<Servicio>();
            ServiciosEstandares = new HashSet<ServicioEstandar>();
            TipoEntradas = new HashSet<TipoEntrada>();
            TipoSalidas = new HashSet<TipoSalida>();
            Ums = new HashSet<Um>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Area> Areas { get; set; }
        public ICollection<Compra> Compras { get; set; }
        public ICollection<Ente> Entes { get; set; }
        public ICollection<Entrada> Entradas { get; set; }
        public ICollection<Familia> Familias { get; set; }
        public ICollection<FormaPago> FormaPagos { get; set; }
        public ICollection<Inventario> Inventarios { get; set; }
        public ICollection<Perfil> Perfiles { get; set; }
        public ICollection<Presentacion> Presentaciones { get; set; }
        public ICollection<Recurso> Recursos { get; set; }
        public ICollection<Rol> Roles { get; set; }
        public ICollection<Salida> Salidas { get; set; }
        public ICollection<Servicio> Servicios { get; set; }
        public ICollection<ServicioEstandar> ServiciosEstandares { get; set; }
        public ICollection<TipoEntrada> TipoEntradas { get; set; }
        public ICollection<TipoSalida> TipoSalidas { get; set; }
        public ICollection<Um> Ums { get; set; }
    }
}
