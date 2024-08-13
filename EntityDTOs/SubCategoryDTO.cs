namespace WEBAPI.EntityDTOs
{
    public class SubCategoryDTO
    {
            public int Id { get; set; }

            public string? CategoryName { get; set; }

            public string? Description { get; set; }

            public string? Image { get; set; }

            public int IsVisible { get; set; }

            public DateTime? LastUpdated { get; set; }

            public double Price { get; set; }

            public int Rating { get; set; }

            public long CategoryId { get; set; }
    }

    
}
