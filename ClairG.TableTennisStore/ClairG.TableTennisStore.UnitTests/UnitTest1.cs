using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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

        [TestClass]
        public class CartTests
        {
            [TestMethod]
            public void Can_Add_New_Lines()
            {
                // Arrange - create some test products
                Product p1 = new Product { ProductID = 1, Name = "P1" };
                Product p2 = new Product { ProductID = 2, Name = "P2" };
                // Arrange - create a new cart
                Cart target = new Cart();
                // Act
                target.AddItem(p1, 1);
                target.AddItem(p2, 1);
                CartLine[] results = target.Lines.ToArray();
                // Assert
                Assert.AreEqual(results.Length, 2);
                Assert.AreEqual(results[0].Product, p1);
                Assert.AreEqual(results[1].Product, p2);
            }
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();
            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Arrange - add some products to the cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);
            // Act
            target.RemoveLine(p2);
            // Assert
            Assert.AreEqual(target.Lines.Where(c => c.Product == p2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();
            // Assert
            Assert.AreEqual(result, 450M);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Arrange - add some items
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            // Act - reset the cart
            target.Clear();
            // Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // Arrange - create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
            }.AsQueryable());
            // Arrange - create a Cart
            Cart cart = new Cart();
            // Arrange - create the controller
            CartController target = new CartController(mock.Object);
            // Act - add a product to the cart
            target.AddToCart(cart, 1, null);
            // Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }

        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            // Arrange - create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
            }.AsQueryable());
            // Arrange - create a Cart
            Cart cart = new Cart();
            // Arrange - create the controller
            CartController target = new CartController(mock.Object);
            // Act - add a product to the cart
            RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");
            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            // Arrange - create a Cart
            Cart cart = new Cart();
            // Arrange - create the controller
            CartController target = new CartController(null);
            // Act - call the Index action method
            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;
            // Assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }
    }
}
