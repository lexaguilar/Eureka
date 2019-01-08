﻿using System;
using System.Collections.Generic;

namespace Eureka.Models.EurekaModel
{
    public partial class Presentacion
    {
        public Presentacion()
        {
            Inventario = new HashSet<Inventario>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int EstadoId { get; set; }

        public Estado Estado { get; set; }
        public ICollection<Inventario> Inventario { get; set; }
    }
}
