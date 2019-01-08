using System;
using System.Collections.Generic;

namespace Eureka.Models.EurekaModel
{
    public partial class EmpresaInfo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Logotipo { get; set; }
        public string FormatoFecha { get; set; }
        public int Decimales { get; set; }
    }
}
