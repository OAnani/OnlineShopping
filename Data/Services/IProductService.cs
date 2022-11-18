using OnlineShopping.Data.Base;
using OnlineShopping.Data.ViewModels;
using OnlineShopping.Models;
using System.Threading.Tasks;

namespace OnlineShopping.Data.Services
{
    public interface IProductService : IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<CategoryVM> GetNewProductDropdownsValues();
        Task AddNewProductAsync(NewProductVM data);
        Task UpdateProductAsync(NewProductVM data);
    }
}
