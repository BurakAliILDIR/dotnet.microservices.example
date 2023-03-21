namespace Services.Basket.Dto
{
    public class BasketItemDto
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public uint Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
