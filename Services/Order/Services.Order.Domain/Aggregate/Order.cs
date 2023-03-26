using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Order.Domain.Core;

namespace Services.Order.Domain.Aggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedAt { get; set; }
        public Address Address { get; set; }
        public string UserId { get; set; } // satın alan kişi
        public List<OrderItem> OrderItems { get; set; }

        public Order()
        {
        }

        public Order(string userId, Address address)
        {
            OrderItems = new List<OrderItem>();
            CreatedAt = DateTime.Now;
            UserId = userId;
            Address = address;
        }

        public decimal GetTotalPrice() => OrderItems.Sum(x => x.Price);
    }
}