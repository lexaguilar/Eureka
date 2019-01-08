using System;
using System.Collections.Generic;

namespace Eureka.Models
{
    public partial class TipoIdentificacion
    {
        public TipoIdentificacion()
        {
            Ente = new HashSet<Ente>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Ente> Ente { get; set; }
    }
}
