using OnlineShopping.Models;
using System.Collections.Generic;

namespace OnlineShopping.Data.ViewModels
{
    public class CategoryVM
    {
        public CategoryVM()
        {
            Categories = new List<Category>();
        }

        public List<Category> Categories { get; set; }
    }
}
