using System;
using System.Collections.Generic;

namespace Eureka.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Perfil = new HashSet<Perfil>();
            RolRecursos = new HashSet<RolRecursos>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int EstadoId { get; set; }

        public Estado Estado { get; set; }
        public ICollection<Perfil> Perfil { get; set; }
        public ICollection<RolRecursos> RolRecursos { get; set; }
    }
}
