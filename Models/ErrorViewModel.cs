using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eureka.Models
{
    [Table("CompraDetalle")]
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}