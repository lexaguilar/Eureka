using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("TipoEnte")]
    public partial class TipoEnte
    {
        public TipoEnte()
        {
            Entes = new HashSet<Ente>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Ente> Entes { get; set; }
    }
}
