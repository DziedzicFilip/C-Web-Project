using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Firma.Data.Data.CMS
{
    public class Onas
    {
        [Key]
        public int IdOnas { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; } 
    }
}
