using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("Rol")]
    public partial class Rol
    {
        public Rol()
        {
            Perfiles = new HashSet<Perfil>();
            RolRecursos = new HashSet<RolRecursos>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int EstadoId { get; set; }

        public Estado Estado { get; set; }
        public ICollection<Perfil> Perfiles { get; set; }
        public ICollection<RolRecursos> RolRecursos { get; set; }
    }
}
