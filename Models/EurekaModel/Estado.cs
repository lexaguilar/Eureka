using System;
using System.Collections.Generic;

namespace Eureka.Models.EurekaModel
{
    public partial class Estado
    {
        public Estado()
        {
            Area = new HashSet<Area>();
            Compra = new HashSet<Compra>();
            Ente = new HashSet<Ente>();
            Entrada = new HashSet<Entrada>();
            Familia = new HashSet<Familia>();
            FormaPago = new HashSet<FormaPago>();
            Inventario = new HashSet<Inventario>();
            Perfil = new HashSet<Perfil>();
            Presentacion = new HashSet<Presentacion>();
            Recurso = new HashSet<Recurso>();
            Rol = new HashSet<Rol>();
            Salida = new HashSet<Salida>();
            Servicio = new HashSet<Servicio>();
            ServicioEstandar = new HashSet<ServicioEstandar>();
            TipoEntrada = new HashSet<TipoEntrada>();
            TipoSalida = new HashSet<TipoSalida>();
            Um = new HashSet<Um>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Area> Area { get; set; }
        public ICollection<Compra> Compra { get; set; }
        public ICollection<Ente> Ente { get; set; }
        public ICollection<Entrada> Entrada { get; set; }
        public ICollection<Familia> Familia { get; set; }
        public ICollection<FormaPago> FormaPago { get; set; }
        public ICollection<Inventario> Inventario { get; set; }
        public ICollection<Perfil> Perfil { get; set; }
        public ICollection<Presentacion> Presentacion { get; set; }
        public ICollection<Recurso> Recurso { get; set; }
        public ICollection<Rol> Rol { get; set; }
        public ICollection<Salida> Salida { get; set; }
        public ICollection<Servicio> Servicio { get; set; }
        public ICollection<ServicioEstandar> ServicioEstandar { get; set; }
        public ICollection<TipoEntrada> TipoEntrada { get; set; }
        public ICollection<TipoSalida> TipoSalida { get; set; }
        public ICollection<Um> Um { get; set; }
    }
}
