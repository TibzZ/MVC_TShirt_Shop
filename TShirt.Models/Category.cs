using System.ComponentModel.DataAnnotations;

namespace TShirt.Models
{
    public class Category
    {
        //Entity Framework attributes: ensure id is primary key and should also be an identity column (auto increment)
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Display(Name ="Display Order")]
        [Range(1, 100, ErrorMessage ="Please enter a number between 1 and 100")]
        public int DisplayOrder { get; set; }
        
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}
