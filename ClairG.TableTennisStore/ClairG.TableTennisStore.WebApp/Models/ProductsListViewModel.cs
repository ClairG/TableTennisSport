using ClairG.TableTennisStore.Domain.Entities;
using System.Collections.Generic;

namespace ClairG.TableTennisStore.WebApp.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}