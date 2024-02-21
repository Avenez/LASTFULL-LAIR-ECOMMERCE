using System;
using System.Collections.Generic;

namespace U4_BW1_LL
{
    public class Order
    {
        private int id;
        public int Id { get => id; }
        private double totalPrice;
        public double TotalPrice { get => totalPrice; }
        private DateTime orderDate;
        public DateTime OrderDate { get => orderDate; }
        private List<OrderDetails> orderDetails;

        public Order(int id, double totalPrice, DateTime orderDate)
        {
            this.id = id;
            this.totalPrice = totalPrice;
            this.orderDate = orderDate;
        }

        public void SetOrderDetails(List<OrderDetails> orderDetails)
        {
            this.orderDetails = orderDetails;
        }

        public List<OrderDetails> GetOrderDetails()
        {
            return orderDetails;
        }
    }
}