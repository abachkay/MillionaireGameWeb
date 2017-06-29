using System;
using System.ComponentModel.DataAnnotations;

namespace MillionaireGameWeb.Entities
{
    public class ExceptionLog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(255)]
        public string Thread { get; set; }        
        [Required]
        [MaxLength(4000)]
        public string Message { get; set; }       
    }
}
