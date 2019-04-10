using System;
using System.Collections.Generic;
using System.Linq;
using ClairG.TableTennisStore.Domain.Abstract;
using ClairG.TableTennisStore.Domain.Entities;
using ClairG.TableTennisStore.WebApp.Controllers;
using ClairG.TableTennisStore.WebApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ClairG.TableTennisStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void Can_Paginate()
        //{
        //    //arrange
        //    Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
        //    mock.Setup(m => m.Products)
        //        .Returns(new Product[] {
        //            new Product {ProductID = 1, Name = "p1"},
        //            new Product {ProductID = 2, Name = "p2"},
        //            new Product {ProductID = 3, Name = "p3"},
        //            new Product {ProductID = 4, Name = "p4"},
        //            new Product {ProductID = 5, Name = "p5"},
        //        });
        //    ProductController controller = new ProductController(mock.Object);
        //    controller.PageSize = 3;
        //    //act
        //    var result = (IEnumerable<Product>)controller.List(2).Model;
        //    //assert
        //    Product[] prodArray = result.ToArray();
        //    Assert.IsTrue(prodArray.Length == 2);
        //    Assert.AreEqual(prodArray[0].Name, "p4");
        //    Assert.AreEqual(prodArray[1].Name, "p5");
        //}

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            });
            // Arrange
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            // Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;
            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            // Arrange
            // - create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
            new Product {ProductID = 1, Name = "P1", Category = "Apples"},
            new Product {ProductID = 2, Name = "P2", Category = "Apples"},
            new Product {ProductID = 3, Name = "P3", Category = "Plums"},
            new Product {ProductID = 4, Name = "P4", Category = "Oranges"},
            });
            // Arrange - create the controller
            NavController target = new NavController(mock.Object);
            // Act = get the set of categories
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();
            // Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Apples");
            Assert.AreEqual(results[1], "Oranges");
            Assert.AreEqual(results[2], "Plums");
        }

        //[TestMethod]
        //public void Indicates_Selected_Category()
        //{
        //    // Arrange
        //    // - create the mock repository
        //    Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
        //    mock.Setup(m => m.Products).Returns(new Product[]
        //    {
        //        new Product {ProductID = 1, Name = "P1", Category = "Apples"},
        //        new Product {ProductID = 4, Name = "P2", Category = "Oranges"},
        //    });
        //    // Arrange - create the controller
        //    NavController target = new NavController(mock.Object);
        //    // Arrange - define the category to selected
        //    string categoryToSelect = "Apples";
        //    // Action
        //    string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;
        //    // Assert
        //    Assert.AreEqual(categoryToSelect, result);
        //}
    }
}
