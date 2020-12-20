using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using Controllers;

namespace HookIn
{
    class Program
    {
        static void Main(string[] args)
        {
            //var order = new Orders();

            //Console.WriteLine("GetOrderHeaders() test:");
            //Console.WriteLine("=========================");
            //List<OrderHeader> orderHeaders = (List<OrderHeader>)order.GetOrderHeaders();
            //foreach (OrderHeader oh in orderHeaders)
            //{
            //    Console.WriteLine(oh.Id);
            //    Console.WriteLine("Count:" + oh.OrderItems.Count);
            //    foreach (OrderItem oi in oh.OrderItems)
            //        Console.WriteLine("OrderItem Price: " + oi.Price);
            //    Console.WriteLine("Total: " + oh.Total);
            //}

            //Console.WriteLine("GetOrderHeader(2) test:");
            //Console.WriteLine("=========================");
            //OrderHeader orderHeader = order.GetOrderHeader(2);
            //Console.WriteLine(orderHeader.State);
            //foreach (OrderItem oi in orderHeader.OrderItems)
            //    Console.WriteLine("OrderItem Price: " + oi.Price);
            //Console.WriteLine("Total: " + orderHeader.Total);

            ///*OrderHeader oh1 = new OrderHeader();
            //Console.WriteLine(order.InsertOrderHeader().Id);*/


            //Console.WriteLine("GetStockItems() test:");
            //Console.WriteLine("=========================");
            //var stockItems = new StockItems();
            //List<StockItem> lSI1 = (List<StockItem>)stockItems.GetStockItems();
            //foreach (StockItem si in lSI1)
            //{
            //    Console.WriteLine(si.Name);
            //}

            //Console.WriteLine("GetStockItems(5) test:");
            //Console.WriteLine("=========================");
            //StockItem si1 = stockItems.GetStockItem(5);
            //Console.WriteLine("StockItem 5: " + si1.Name);



            //Console.WriteLine("UpdateStockItemAmount() test:");
            //Console.WriteLine("=========================");
            //foreach (StockItem si in lSI1)
            //{
            //    Console.WriteLine(si.Name + " " + si.InStock);
            //}
            //OrderHeader oh1 = order.GetOrderHeader(2);
            //Console.WriteLine("--------------------------");
            ////stockItems.UpdateStockItemAmount(oh1);
            //foreach (StockItem si in lSI1)
            //{
            //    Console.WriteLine(si.Name + " " + si.InStock);
            //}




            ////try
            //{
            //    Console.WriteLine("UpdateOrderState(orderHeader2) test:");
            //    Console.WriteLine("=========================");
            //    OrderHeader orderHeader2 = order.GetOrderHeader(2);
            //    Console.WriteLine("Before: " + orderHeader.State);
            //    orderHeader2.Submit();
            //    order.UpdateOrderState(orderHeader2);
            //}
            ///*catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}*/

            //Console.ReadLine();


            //Console.ReadLine();
        }
    }
}
