using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Tests
{
    public class InventoryOrderServiceTests
    {
        [Fact]
        public void ProcessOrder_ValidOrder_ReturnsSuccess()
        {
            var service = new InventoryOrderService();

            service.AddProduct(new Product
            {
                Id = "P100",
                Name = "Keyboard",
                UnitPrice = 100m,
                StockQuantity = 20
            });

            var result = service.ProcessOrder("P100", 5, 0.05m);

            Assert.True(result.IsSuccess);


        }


        [Fact]
        public void ProcessOrder_ReducesStockQuantity()
        {
            var service = new InventoryOrderService();

            service.AddProduct(new Product
            {
                Id = "P100",
                Name = "Keyboard",
                UnitPrice = 100m,
                StockQuantity = 20
            });

            service.ProcessOrder("P100", 5, 0m);

            var product = service.GetProduct("P100");

            Assert.Equal(15, product!.StockQuantity);
        }

        [Fact]
        public void ProcessOrder_Quantity50_AppliesTwentyPercentDiscount()
        {
            var service = new InventoryOrderService();

            service.AddProduct(new Product
            {
                Id = "P100",
                Name = "Keyboard",
                UnitPrice = 100m,
                StockQuantity = 100
            });

            var result = service.ProcessOrder("P100", 50, 0m);

            Assert.Equal(4000m, result.TotalCost);
        }
    
    [Fact]
        public void ProcessOrder_QuantityEqualsStock_Succeeds()
        {
            var service = new InventoryOrderService();

            service.AddProduct(new Product
            {
                Id = "P100",
                Name = "Keyboard",
                UnitPrice = 100m,
                StockQuantity = 5
            });

            var result = service.ProcessOrder("P100", 5, 0m);

            Assert.True(result.IsSuccess);
        }
        [Fact]
        public void ProcessOrder_Quantity10_ShouldReceiveTenPercentDiscount()
        {
            var service = new InventoryOrderService();

            service.AddProduct(new Product
            {
                Id = "P100",
                Name = "Keyboard",
                UnitPrice = 100m,
                StockQuantity = 20
            });

            var result = service.ProcessOrder("P100", 10, 0m);

            Assert.Equal(900m, result.TotalCost);
        }

        [Fact]
        public void ProcessOrder_ZeroQuantity_ShouldFail()
        {
            var service = new InventoryOrderService();

            service.AddProduct(new Product
            {
                Id = "P100",
                Name = "Keyboard",
                UnitPrice = 100m,
                StockQuantity = 20
            });

            var result = service.ProcessOrder("P100", 0, 0m);

            Assert.False(result.IsSuccess);
        }
        [Fact]
        public void AddProduct_NullProduct_ThrowsArgumentException()
        {
            var service = new InventoryOrderService();

            Assert.Throws<ArgumentException>(() => service.AddProduct(null!));
        }
        }
    }