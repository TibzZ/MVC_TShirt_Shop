using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TShirt.Models
{
    public class DesignType
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Design Type")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
