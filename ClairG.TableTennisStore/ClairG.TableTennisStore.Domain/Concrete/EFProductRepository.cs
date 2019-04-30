using ClairG.TableTennisStore.Domain.Abstract;
using ClairG.TableTennisStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClairG.TableTennisStore.Domain.Concrete
{
    public class EFProductRepository : IProductsRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }


        public Product DeleteProduct(int productId) //delete
        {
            Product dbEntry = context.Products.Find(productId);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Product product) 
        {
            if (product.ProductId == 0) //add new
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductId); //edit
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name; 
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }

            context.SaveChanges();
        }
    }
}
