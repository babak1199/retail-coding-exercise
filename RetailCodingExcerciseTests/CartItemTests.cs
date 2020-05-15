using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RetailCodingExercise.Controllers;
using RetailCodingExercise.Models;
using System;
using System.Threading.Tasks;

namespace RetailCodingExcercise.Tests
{
    [TestClass]
    public class CartItemTests
    {
        protected DbContextOptions<ProductContext> ContextOptions;

        public CartItemTests()
        {
            ContextOptions = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase("ProductListDatabase")
                .Options;
        }

        [TestInitialize]
        public async Task Seed()
        {
            using (var context = new ProductContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var one = new CartItem()
                {
                    CartId = "cart",
                    DateCreated = new DateTime(2000, 1, 1),
                    ItemId = "cartItem1",
                    ProductId = 1,
                    Quantity = 2
                };

                var two = new CartItem()
                {
                    CartId = "cart",
                    DateCreated = new DateTime(2000, 1, 2),
                    ItemId = "cartItem2",
                    ProductId = 2,
                    Quantity = 1
                };

                await context.CartItems.AddRangeAsync(one, two);

                context.SaveChanges();
            }
        }

        [TestMethod]
        public async Task TestAddingExistingProductToCart()
        {
            using (var context = new ProductContext(ContextOptions))
            {
                var controller = new CartController(context);

                var item = new CartItem()
                {
                    CartId = "cart",
                    ProductId = 1,
                };

                var result = await controller.PostItem(item);

                Assert.AreEqual(2, await context.CartItems.CountAsync());
                Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));

                var changedItem = await context.CartItems.FindAsync("cartItem1");
                Assert.AreEqual(3, changedItem.Quantity);
            }
        }

        [TestMethod]
        public async Task TestAddingNewProductToCart()
        {
            using (var context = new ProductContext(ContextOptions))
            {
                var controller = new CartController(context);

                var item = new CartItem()
                {
                    CartId = "cart",
                    ProductId = 3,
                };

                var result = await controller.PostItem(item);

                Assert.AreEqual(3, await context.CartItems.CountAsync());
                Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));

                var resultValue = (result.Result as CreatedAtActionResult).Value;
                Assert.IsNotNull(resultValue);
                Assert.IsInstanceOfType(resultValue, typeof(CartItem));
                Assert.AreEqual(1, (resultValue as CartItem).Quantity);
            }
        }
    }
}