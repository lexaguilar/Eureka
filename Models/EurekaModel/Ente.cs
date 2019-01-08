using System;
using System.Collections.Generic;

namespace Eureka.Models.EurekaModel
{
    public partial class Ente
    {
        public Ente()
        {
            Compra = new HashSet<Compra>();
            Entrada = new HashSet<Entrada>();
            Salida = new HashSet<Salida>();
        }

        public int Id { get; set; }
        public int TipoEnteId { get; set; }
        public int TipoIdentificacionId { get; set; }
        public string Identificacion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string NombreCompleto { get; set; }
        public int? SexoId { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public bool TieneCredito { get; set; }
        public decimal CreditoMaximo { get; set; }
        public bool EsPredeterminado { get; set; }
        public string Observacion { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEl { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? ModificadoEl { get; set; }
        public int EstadoId { get; set; }
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }
        public Perfil CreadoPorNavigation { get; set; }
        public Estado Estado { get; set; }
        public Perfil ModificadoPorNavigation { get; set; }
        public Sexo Sexo { get; set; }
        public TipoEnte TipoEnte { get; set; }
        public TipoIdentificacion TipoIdentificacion { get; set; }
        public ICollection<Compra> Compra { get; set; }
        public ICollection<Entrada> Entrada { get; set; }
        public ICollection<Salida> Salida { get; set; }
    }
}
