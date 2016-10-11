using Xunit;
using Bangazon.Orders;
using System;
using System.Collections.Generic;

namespace Bangazon.Tests
{
    public class OrderTests
    {
        [Fact]
        public void TestTheTestingFramework()
        {
            Assert.True(true);
        }

        [Fact]
        public void OrdersCanExist()
        {
            Order ord = new Order();
            Assert.NotNull(ord);
        }

        [Fact]
        public void NewOrdersHaveGuidOfTypeGuid()
        {
            Order ord = new Order();
            Assert.NotNull(ord.orderNumber);
            Assert.IsType<Guid>(ord.orderNumber);
        }

        [Fact]
        public void NewOrdersShouldHaveAnEmptyProductListOfStrings()
        {
            Order ord = new Order();
            Assert.NotNull(ord.products);
            Assert.IsType<List<string>>(ord.products);
            Assert.Empty(ord.products);
        }

        [Theory]
        [InlineData("Banana")]
        [InlineDataAttribute("7463722")]
        [InlineDataAttribute("A product with spaces")]
        [InlineDataAttribute("Product, that has a, comma?")]
        public void OrdersCanHaveAProductAddedToThem(string product)
        {
            Order ord = new Order();
            ord.addProduct(product);
            Assert.Equal(1, ord.products.Count);
            Assert.Contains<string>(product, ord.products);
        }

        [Theory]
        [InlineDataAttribute("Product")]
        [InlineDataAttribute("product,another product")]
        [InlineDataAttribute("a first product,someother,yet another")]
        [InlineDataAttribute("prod 1,prod 2,prod 3,prod 4")]
        public void OrdersCanHaveMultipleProductsAddedToThem(string productsStr)
        {
            string[] products = productsStr.Split(new char[] { ',' });
            Order ord = new Order();
            foreach (string product in products)
            {
                ord.addProduct(product);
            }
            Assert.Equal(products.Length, ord.products.Count);
            foreach (string product in products)
            {
                Assert.Contains<string>(product, ord.products);
            }
        }

        [Theory]
        [InlineDataAttribute("Product")]
        [InlineDataAttribute("product,another product")]
        [InlineDataAttribute("a first product,someother,yet another")]
        [InlineDataAttribute("prod 1,prod 2,prod 3,prod 4")]
        public void OrdersCanListProductsForTerminalDisplay(string productsStr)
        {
            string[] products = productsStr.Split(new char[] { ',' });
            Order ord = new Order();
            foreach (string product in products)
            {
                ord.addProduct(product);
            }
            
            foreach (string product in products)
            {
                Assert.Contains($"\nYou ordered {product}", ord.listProducts());
            }
        }

        [Fact]
        public void OrdersCanHaveAProductRemovedFromThem()
        {
            Order ord = new Order();
            ord.addProduct("Banana Bread");
            ord.addProduct("Product");
            ord.addProduct("Banana");
            ord.addProduct("Honeydew Melon");

            ord.removeProduct("Banana");

            Assert.Equal(3, ord.products.Count);
            Assert.DoesNotContain<string>("Banana", ord.products);
        }

        [Fact]
        public void OrdersCanNotRemoveAProductThatDoesNotExistFromThem()
        {
            Order ord = new Order();
            ord.addProduct("Banana Bread");
            ord.addProduct("Product");
            ord.addProduct("Banana");
            ord.addProduct("Honeydew Melon");

            ord.removeProduct("Pineapple");

            Assert.Equal(4, ord.products.Count);
        }

        [Theory]
        [InlineDataAttribute("Banana")]
        [InlineDataAttribute("Pineapple")]
        public void RemoveMethodReturnsBooleanIndicatingIfProductWasRemoved(string product)
        {
            Order ord = new Order();
            ord.addProduct("Banana Bread");
            ord.addProduct("Product");
            ord.addProduct("Banana");
            ord.addProduct("Honeydew Melon");

            bool removed = ord.removeProduct(product);

            if (product == "Banana")
            {
                Assert.True(removed);
            }
            if (product == "Pineapple")
            {
                Assert.False(removed);
            }
        }

        [Fact]
        public void AllProductsFromAnOrderCanBeDeleted()
        {
            Order ord = new Order();
            ord.addProduct("Banana Bread");
            ord.addProduct("Product");
            ord.addProduct("Banana");
            ord.addProduct("Honeydew Melon");

            ord.removeProduct();

            Assert.Empty(ord.products);
        }
    }
}