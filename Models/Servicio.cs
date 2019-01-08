using System;
using System.Collections.Generic;

namespace Eureka.Models
{
    public partial class Servicio
    {
        public Servicio()
        {
            ServicioEstandar = new HashSet<ServicioEstandar>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string ReglaPrecio { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEl { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? ModificadoEl { get; set; }
        public int EstadoId { get; set; }

        public Perfil CreadoPorNavigation { get; set; }
        public Estado Estado { get; set; }
        public Perfil ModificadoPorNavigation { get; set; }
        public ICollection<ServicioEstandar> ServicioEstandar { get; set; }
    }
}
