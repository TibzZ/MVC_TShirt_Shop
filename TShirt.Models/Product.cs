using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TShirt.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        public string TshirtTitle { get; set; }
        public string Description { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string Designer { get; set; }
        [Required]
        [Range(1, 10000)]
        public double ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        public double MainPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price50Items { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price100Items { get; set; }
        public string ImageUrl { get; set; }
        //The following will create a foreign Key relation for EF core:
        [Required]
        public int CategoryId { get; set; } // Same name as category with Id, it will automatically make CategoryId a foreign key
        [ForeignKey("CategoryId")] //Not required unless name is not straightforward
        public Category Category { get; set; }      //EF knows this is a navigation property to the category class

        [Required]
        public int DesignTypeId { get; set; }
        public DesignType DesignType { get; set; } 

    }
}
