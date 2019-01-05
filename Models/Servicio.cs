using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("Servicio")]
    public partial class Servicio
    {
        public Servicio()
        {
            ServiciosEstandares = new HashSet<ServicioEstandar>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string ReglaPrecio { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEl { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? ModificadoEl { get; set; }
        [Display(Name = "Estado")]
        public int EstadoId { get; set; }

        public Perfil CreadoPorNavigation { get; set; }
        public Estado Estado { get; set; }
        public Perfil ModificadoPorNavigation { get; set; }
        public ICollection<ServicioEstandar> ServiciosEstandares { get; set; }
    }
}
