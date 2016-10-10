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

        [Theory]
        [InlineDataAttribute("Banana")]
        [InlineDataAttribute("Banana,Squid")]
        [InlineDataAttribute("Banana,Squid,Some other junk")]
        public void MultipleProductsCanBeAddedToAnOrder(string products)
        {
            string[] productsArr = products.Split(new char[] {','});

            Order ord = new Order();

            foreach (string product in productsArr)
            {
                ord.addProduct(product);
            }
            // The local array of products and the order's products list
            // should have the same number of products
            Assert.Equal(productsArr.Length, ord.products.Count);

            foreach (string product in productsArr)
            {
                // Every item in the local products array should be in the order's products list
                Assert.Contains<string>(product, ord.products);
            }
        }

        [Theory]
        [InlineDataAttribute("Banana")]
        [InlineDataAttribute("Banana,Squid")]
        [InlineDataAttribute("Banana,Squid,Some other junk")]
        public void OrdersShouldHaveMethodToListProductsAttractively(string products)
        {
            string[] productsArr = products.Split(new char[] {','});

            Order ord = new Order();

            foreach (string product in productsArr)
            {
                ord.addProduct(product);
            }

            foreach (string product in productsArr)
            {
                // Every item in the local products array should be displayed by listProducts()
                Assert.Contains($"\nYou ordered {product}", ord.listProducts());
            }
        }
    }
}