using System;
using System.Collections.Generic;

namespace Eureka.Models.EurekaModel
{
    public partial class Sexo
    {
        public Sexo()
        {
            Ente = new HashSet<Ente>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Ente> Ente { get; set; }
    }
}
