using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controllers;
using Domain;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Init()
        {
            Controllers.Orders.ResetDatabase();
        }

        [TestMethod]
        public void AddOrder_StateIsNew()
        {
            OrderHeader header = Controllers.Orders.CreateNewOrderHeader();
            Assert.AreEqual(1, header.Id);
            Assert.AreEqual(OrderStates.New, header.State.State);

            var stock = Controllers.StockItems.GetStockItems()[0];
            Assert.AreEqual(1, stock.Id);
            header = Controllers.Orders.UpsertOrderItem(header.Id, stock.Id, 2);

            Assert.AreEqual(1, header.OrderItems.Count);
            Assert.AreEqual(OrderStates.New, header.State.State);
        }

        [TestMethod]
        public void SubmitOrder_StateIsPending()
        {
            OrderHeader header = Controllers.Orders.CreateNewOrderHeader();
            Assert.AreEqual(OrderStates.New, header.State.State);

            var stock = Controllers.StockItems.GetStockItems()[1];

            Controllers.Orders.UpsertOrderItem(header.Id, stock.Id, 5);
            header = Controllers.Orders.SubmitOrder(header.Id);
            Assert.AreEqual(OrderStates.Pending, header.State.State);
        }

        [TestMethod]
        public void ProcessOrder_StateIsComplete()
        {
            OrderHeader header = Controllers.Orders.CreateNewOrderHeader();
            Assert.AreEqual(OrderStates.New, header.State.State);

            var stock = Controllers.StockItems.GetStockItems()[2];

            Controllers.Orders.UpsertOrderItem(header.Id, stock.Id, 1);
            header = Controllers.Orders.SubmitOrder(header.Id);
            Assert.AreEqual(OrderStates.Pending, header.State.State);

            header = Controllers.Orders.ProcessOrder(header.Id);
            Assert.AreEqual(OrderStates.Complete, header.State.State);

            stock = Controllers.StockItems.GetStockItems()[2];
            Assert.AreEqual(stock.InStock, 9);
        }

        [TestMethod]
        public void ProcessOrder_StateIsReject()
        {
            OrderHeader header = Controllers.Orders.CreateNewOrderHeader();
            Assert.AreEqual(OrderStates.New, header.State.State);

            var stock = Controllers.StockItems.GetStockItems()[3];
            Assert.AreEqual(4, stock.Id);
            Assert.AreEqual(10, stock.InStock);

            Controllers.Orders.UpsertOrderItem(header.Id, stock.Id, 15);
            header = Controllers.Orders.SubmitOrder(header.Id);
            Assert.AreEqual(OrderStates.Pending, header.State.State);

            header = Controllers.Orders.ProcessOrder(header.Id);
            Assert.AreEqual(OrderStates.Rejected, header.State.State);

            stock = Controllers.StockItems.GetStockItems()[3];
            Assert.AreEqual(4, stock.Id);
            Assert.AreEqual(10, stock.InStock);
        }

        [TestMethod]
        public void OrderItem_EqualToOne_Duplicate_Item()
        {
            OrderHeader header = Controllers.Orders.CreateNewOrderHeader();
            Assert.AreEqual(OrderStates.New, header.State.State);

            var stock = Controllers.StockItems.GetStockItems()[4];
            Controllers.Orders.UpsertOrderItem(header.Id, stock.Id, 1);
            Controllers.Orders.UpsertOrderItem(header.Id, stock.Id, 3);

            header = Controllers.Orders.SubmitOrder(header.Id);
            Assert.AreEqual(OrderStates.Pending, header.State.State);
            Assert.AreEqual(1, header.OrderItems.Count);        
        }

        [TestMethod]
        public void OrderItems_EqualToThree()
        {
            OrderHeader header = Controllers.Orders.CreateNewOrderHeader();
            Assert.AreEqual(OrderStates.New, header.State.State);

            var stock = Controllers.StockItems.GetStockItems()[5];
            Controllers.Orders.UpsertOrderItem(header.Id, stock.Id, 1);
            stock = Controllers.StockItems.GetStockItems()[6];
            Controllers.Orders.UpsertOrderItem(header.Id, stock.Id, 1);
            stock = Controllers.StockItems.GetStockItems()[7];
            Controllers.Orders.UpsertOrderItem(header.Id, stock.Id, 1);

            header = Controllers.Orders.SubmitOrder(header.Id);
            Assert.AreEqual(OrderStates.Pending, header.State.State);
            Assert.AreEqual(3, header.OrderItems.Count);
        }

        [TestMethod]
        public void DeleteOrder()
        {
            OrderHeader header = Controllers.Orders.CreateNewOrderHeader();
            Assert.AreEqual(OrderStates.New, header.State.State);

            var stock = Controllers.StockItems.GetStockItems()[8];
            Controllers.Orders.UpsertOrderItem(header.Id, stock.Id, 1);
            header = Controllers.Orders.SubmitOrder(header.Id);
            Assert.AreEqual(OrderStates.Pending, header.State.State);
            int headerId = header.Id;
            Controllers.Orders.DeleteOrderHeaderAndOrderItems(header.Id);
            header = Controllers.Orders.GetOrderHeaderById(headerId);
            Assert.IsNull(header);
        }

    }
}
