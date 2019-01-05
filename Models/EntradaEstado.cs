using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("EntradaEstado")]
    public partial class EntradaEstado
    {
        public EntradaEstado()
        {
            Entradas = new HashSet<Entrada>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Entrada> Entradas { get; set; }
    }
}
