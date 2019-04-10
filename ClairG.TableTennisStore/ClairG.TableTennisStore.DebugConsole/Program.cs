using ClairG.TableTennisStore.Domain.Concrete;
using ClairG.TableTennisStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClairG.TableTennisStore.DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new EFDbContext())
            {
                var product = new Product() { };
                ctx.Products.Add(product);
                ctx.SaveChanges();
            }
        }
    }
}
