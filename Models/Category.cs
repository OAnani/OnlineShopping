using OnlineShopping.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopping.Models
{
    public class Category: IEntityBase
    {
        
        public int ID { get; set; }
        [Required(ErrorMessage = "Category Name is Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charaters are allwed", MinimumLength = 3)]
        public string Name { get; set; }

        [InverseProperty(nameof(Product.Category))]
        public ICollection<Product>? Products { get; set; }
    }
}
