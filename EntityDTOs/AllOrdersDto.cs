namespace WEBAPI.EntityDTOs
{
 
        public class AllOrdersDto
        {
            public long BookingId { get; set; }
            public DateTime BookingDate { get; set; }
            public string Status { get; set; }
            public TimeSpan BookingTime { get; set; }
            public long ProviderId { get; set; }
            public long SubcategoryId { get; set; }
            public long UserId { get; set; }

            public UserDTO User { get; set; }
            public ProviderDTO Provider { get; set; }
        }

        public class UserDTO
        {
            public long UserId { get; set; }
            public string Name { get; set; }
        public string Gender { get; set; }

    }

        public class ProviderDTO
        {
            public long ProviderId { get; set; }
            public string Name { get; set; }

        }
    }


