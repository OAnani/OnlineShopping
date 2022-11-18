using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopping.Data.Services;
using OnlineShopping.Data.Static;
using OnlineShopping.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var Products = await _service.GetAllAsync(n => n);
            return View(Products);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var Products = await _service.GetAllAsync(n => n);

            if (!string.IsNullOrEmpty(searchString))
            {

                var filteredResultNew = Products.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", Products);
        }

        //GET: Products/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var productDetail = await _service.GetProductByIdAsync(id);
            return View(productDetail);
        }

        //GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var productDropdownsData = await _service.GetNewProductDropdownsValues();

            ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductVM product)
        {
            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownsValues();

                ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Name");
                return View(product);
            }

            await _service.AddNewProductAsync(product);
            return RedirectToAction(nameof(Index));
        }


        //GET: Products/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _service.GetProductByIdAsync(id);
            if (productDetails == null) return View("NotFound");

            var response = new NewProductVM()
            {
                Id = productDetails.ID,
                Name = productDetails.Name,
                Price = productDetails.Price,
                ImageURL = productDetails.ImageURL,
                Category = productDetails.Category,
            };

            var productDropdownsData = await _service.GetNewProductDropdownsValues();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductVM product)
        {
            if (id != product.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownsValues();

                ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Name");
            }

            await _service.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}