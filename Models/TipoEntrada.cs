using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("TipoEntrada")]
    public partial class TipoEntrada
    {
        public TipoEntrada()
        {
            Entradas = new HashSet<Entrada>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        [Display(Name = "Estado")]
        public int EstadoId { get; set; }

        public Estado Estado { get; set; }
        public ICollection<Entrada> Entradas { get; set; }
    }
}
