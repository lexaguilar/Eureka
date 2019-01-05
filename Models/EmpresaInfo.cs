using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Eureka.Models
{
    [Table("EmpresaInfo")]
    public partial class EmpresaInfo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Logotipo { get; set; }
        public string FormatoFecha { get; set; }
        public int Decimales { get; set; }
    }
}
