using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Order.Domain.Core;

namespace Services.Order.Domain.Aggregate
{
    public class OrderItem : Entity
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }


        public OrderItem(string courseId, string courseName, string pictureUrl, decimal price)
        {
            CourseId = courseId;
            CourseName = courseName;
            PictureUrl = pictureUrl;
            Price = price;
        }

        public void UpdateOrderItem(string courseName, string pictureUrl, decimal price)
        {
            CourseName = courseName;
            PictureUrl = pictureUrl;
            Price = price;
        }
    }
}