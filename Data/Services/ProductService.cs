using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data.Base;
using OnlineShopping.Data.ViewModels;
using OnlineShopping.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Data.Services
{
    public class ProductService : EntityBaseRepository<Product>, IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewProductAsync(NewProductVM data)
        {
            var newProduct = new Product()
            {
                Name = data.Name,
                Price = data.Price,
                ImageURL = data.ImageURL,
                Category = data.Category
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productDetails = await _context.Products
                .Include(c => c.Category)
                .FirstOrDefaultAsync(n => n.ID == id);

            return productDetails;
        }

        public async Task<CategoryVM> GetNewProductDropdownsValues()
        {
            var response = new CategoryVM()
            {
                Categories = await _context.Categories.OrderBy(n => n.ID).ToListAsync()
            };

            return response;
        }

        public async Task UpdateProductAsync(NewProductVM data)
        {
            var product = await _context.Products.FirstOrDefaultAsync(n => n.ID == data.Id);

            if(product != null)
            {
                product.Name = data.Name;
                product.Price = data.Price;
                product.ImageURL = data.ImageURL;
            }
            await _context.SaveChangesAsync();
        }
    }
}
