using Services.Order.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order.Application.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public AddressDto AddressDto { get; set; }
        public string UserId { get; set; } // satın alan kişi
        public List<OrderItemDto> OrderItemDtos { get; set; }
    }
}