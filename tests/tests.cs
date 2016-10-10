using System;
using Xunit;
using Bangazon.Orders;
using System.Collections.Generic;

namespace Bangazon.Tests
{
    public class TestClass
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, 4);
        }

        [Fact]
        public void OrdersExist()
        {
            Order ord = new Order();
            Assert.NotNull(ord);
        }

        [Fact]
        public void OrdersHaveAGUID()
        {
            Order ord = new Order();
            Assert.IsType<Guid>(ord.orderNumber);
        }

        [Fact]
        public void NewOrdersHaveAnEmptyProductList()
        {
            Order ord = new Order();
            Assert.IsType<List<string>>(ord.products);
            Assert.Empty(ord.products);
        }

        [Theory]
        [InlineDataAttribute("Banana")]
        [InlineDataAttribute("Squid")]
        [InlineDataAttribute("Some kind of name")]
        [InlineDataAttribute("~~~magic~~~")]
        public void AddProductMethodAddsProductToProductList(string product)
        {
            Order ord = new Order();
            ord.addProduct(product);
            Assert.Contains<string>(product, ord.products);
        }

        [Fact]
        public void MultipleProductsCanBeAddedToAnOrder()
        {
            Order ord = new Order();
            ord.addProduct("Banana");
            Assert.Equal(1, ord.products.Count);
            Assert.Contains<string>("Banana", ord.products);

            ord.addProduct("Squid");
            Assert.Equal(2, ord.products.Count);
            Assert.Contains<string>("Banana", ord.products);
            Assert.Contains<string>("Squid", ord.products);
        }
    }
}