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


        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order()
        {
        }

        public Order(string userId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedAt = DateTime.Now;
            UserId = userId;
            Address = address;
        }

        public void AddOrderItem(string CourseId, string CourseName, string PictureUrl, decimal Price)
        {
            var exists = _orderItems.Any(x => x.CourseId == CourseId);

            if (!exists)
            {
                _orderItems.Add(new OrderItem(courseId: CourseId, courseName: CourseName, pictureUrl: PictureUrl,
                    price: Price));
            }
        }

        public decimal GetTotalPrice() => _orderItems.Sum(x => x.Price);
    }
}