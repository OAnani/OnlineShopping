using OnlineShopping.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class Product:IEntityBase
    {
       
        public int ID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Product name Length is too long")]
        public string Name { get; set; }

        [Required]
        [Range(0.0, 9999, ErrorMessage ="Price is")]
        public double Price { get; set; }

        public string ImageURL { get; set; }
     
        public int CategoryId { set; get; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Products")]
        public virtual Category? Category { get; set; }

    }
}
