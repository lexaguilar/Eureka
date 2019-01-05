using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("Categoria")]
    public partial class Categoria
    {
        public Categoria()
        {
            Entes = new HashSet<Ente>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Nota { get; set; }

        public ICollection<Ente> Entes { get; set; }
    }
}
