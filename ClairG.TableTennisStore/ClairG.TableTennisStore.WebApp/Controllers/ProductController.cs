using ClairG.TableTennisStore.Domain.Abstract;
using ClairG.TableTennisStore.Domain.Entities;
using ClairG.TableTennisStore.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClairG.TableTennisStore.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository repository;
        public int PageSize = 4;

        public ProductController(IProductsRepository productsRepository)
        {
            this.repository = productsRepository;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List(string category, int page = 1)
        {
            var categoryProducts = repository
                .Products
                .Where(p => category == null || p.Category == category);
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = categoryProducts                
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = categoryProducts.Count()
                },

                CurrentCategory = category
            };

            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            Product prod = repository
            .Products
            .FirstOrDefault(p => p.ProductId == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}