using System;
using System.Collections.Generic;

namespace UserMenu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            
            List<Order> orders = new List<Order>()
            {
                new Order()
                {
                    OrderID = "ajgfj37", DeliveryDate = "25.09.2024", DeliveryStatus = DeliveryStatus.OnItsWay
                },
                new Order()
                {
                    OrderID = "hjsfg21", DeliveryDate = "26.09.2024", DeliveryStatus = DeliveryStatus.Delivered
                },
                new Order()
                {
                    OrderID = "mklsd89", DeliveryDate = "27.09.2024", DeliveryStatus = DeliveryStatus.Canceled
                },
                new Order()
                {
                    OrderID = "pqowe45", DeliveryDate = "28.09.2024", DeliveryStatus = DeliveryStatus.OnItsWay
                },
                new Order()
                {
                    OrderID = "tyuiw12", DeliveryDate = "29.09.2024", DeliveryStatus = DeliveryStatus.Delivered
                },
                new Order()
                {
                    OrderID = "zxvbn03", DeliveryDate = "30.09.2024", DeliveryStatus = DeliveryStatus.Canceled
                },
                new Order()
                {
                    OrderID = "aswer56", DeliveryDate = "01.10.2024", DeliveryStatus = DeliveryStatus.OnItsWay
                },
                new Order()
                {
                    OrderID = "qweasd78", DeliveryDate = "02.10.2024", DeliveryStatus = DeliveryStatus.Delivered
                },
                new Order()
                {
                    OrderID = "lkjhgf43", DeliveryDate = "03.10.2024", DeliveryStatus = DeliveryStatus.OnItsWay
                },
                new Order()
                {
                    OrderID = "mnbvcd11", DeliveryDate = "04.10.2024", DeliveryStatus = DeliveryStatus.Canceled
                },
                new Order()
                {
                    OrderID = "poiuyn65", DeliveryDate = "05.10.2024", DeliveryStatus = DeliveryStatus.Delivered
                }
            };
            while (true)
            {
                Console.Write("Type ID of an order that you want to search for: ");
                string orderID = Console.ReadLine();
            
                Order userOrder = orders.Where(p => p.OrderID == orderID).First();

                switch (userOrder.DeliveryStatus)
                {
                    case DeliveryStatus.OnItsWay:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"Order \"{userOrder.OrderID}\" was successfully delivered!");
                        break;
                    case DeliveryStatus.Canceled:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"Order \"{userOrder.OrderID}\" was canceled delivered!");
                        break;
                    case DeliveryStatus.Delivered:
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine($"Order \"{userOrder.OrderID}\" will be delivered on {userOrder.DeliveryDate}!");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"Error! Order \"{userOrder.DeliveryDate}\" doesn't have a delivery date!");
                        break;
                }
            }
            
            

        }
    }
   
    
    class Order
    {
        public string OrderID { get; set; }
        public string DeliveryDate { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
    }

    enum DeliveryStatus
    {
        Canceled,
        Delivered,
        OnItsWay
    }
}
