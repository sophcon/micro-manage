using MicroManage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MicroM.Services
{
    public class OrderService
    {
        public MicroManageContext Context { get; private set; }

        public OrderService (MicroManageContext context) {
            this.Context = context;
        }

        public async void CreateOrder(int CustomerId,List<OrderProduct> products)
        {
            //create order
            Order order = new Order();
            order.CustomerId = CustomerId;
            order.AddedOn = DateTime.Now;

            //send to db
            Context.Orders.Add(order);
            await Context.SaveChangesAsync();

            //create orderproduct records and link to new order
            foreach(OrderProduct i in products)
            {
                i.OrderId = order.Id;
                Context.OrderProducts.Add(i);
            }

            //send order products to db
            await Context.SaveChangesAsync();
        }
        
    }
}