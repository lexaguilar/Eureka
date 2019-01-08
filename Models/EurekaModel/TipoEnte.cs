using System;
using System.Collections.Generic;

namespace Eureka.Models.EurekaModel
{
    public partial class TipoEnte
    {
        public TipoEnte()
        {
            Ente = new HashSet<Ente>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Ente> Ente { get; set; }
    }
}
