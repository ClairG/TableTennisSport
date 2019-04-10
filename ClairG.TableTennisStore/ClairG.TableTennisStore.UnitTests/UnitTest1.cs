using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClairG.TableTennisStore.Domain.Abstract;
using ClairG.TableTennisStore.Domain.Entities;
using ClairG.TableTennisStore.WebApp.Controllers;
using ClairG.TableTennisStore.WebApp.HtmlHelpers;
using ClairG.TableTennisStore.WebApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ClairG.TableTennisStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products)
                .Returns(new Product[] {
                    new Product {ProductID = 1, Name = "p1"},
                    new Product {ProductID = 2, Name = "p2"},
                    new Product {ProductID = 3, Name = "p3"},
                    new Product {ProductID = 4, Name = "p4"},
                    new Product {ProductID = 5, Name = "p5"},
                });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            //act
            var result = (IEnumerable<Product>)controller.List(2).Model;
            //assert
            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "p4");
            Assert.AreEqual(prodArray[1].Name, "p5");
        }

        //[TestMethod]
        //public void Can_Generate_Page_Links()
        //{
        //    // Arrange - define an HTML helper - we need to do this
        //    // in order to apply the extension method
        //    HtmlHelper myHelper = null;
        //    // Arrange - create PagingInfo data
        //    PagingInfo pagingInfo = new PagingInfo
        //    {
        //        CurrentPage = 2,
        //        TotalItems = 28,
        //        ItemsPerPage = 10
        //    };
        //    // Arrange - set up the delegate using a lambda expression
        //    Func<int, string> pageUrlDelegate = i => "Page" + i;
        //    // Act
        //    MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);
        //    // Assert
        //    Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
        //    + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
        //    + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
        //    result.ToString());
        //}
    }
}
