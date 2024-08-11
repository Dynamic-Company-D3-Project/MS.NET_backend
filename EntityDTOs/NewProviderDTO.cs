namespace WEBAPI.EntityDTOs
{
    public class NewProviderDTO
    {
        
            public string Email { get; set; } = null!;
            public string FirstName { get; set; } = null!;
            public string? Gender { get; set; }
            public string LastName { get; set; } = null!;
            public string Password { get; set; } = null!;
            public string? PhoneNumber { get; set; }
            public string? City { get; set; }
            public string? Country { get; set; }
            public ulong? IsDeleted { get; set; }
            public string? ProviderImagePath { get; set; }
            public long? ZipCode { get; set; }
        

    }
}
