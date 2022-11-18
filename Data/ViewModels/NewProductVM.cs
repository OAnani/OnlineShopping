using OnlineShopping.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class NewProductVM
    {
        public int Id { get; set; }

        [Display(Name = "Product name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Product description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Product image poster URL")]
        [Required(ErrorMessage = "Product image URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Select a Category")]
        [Required(ErrorMessage = "Category is required")]
        public Category Category{ get; set; }
    }
}