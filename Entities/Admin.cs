using System;
using System.Collections.Generic;

namespace WEBAPI.Entities;

public partial class Admin
{
    public long Id { get; set; }

    public DateTime? CreationTime { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Gender { get; set; }

    public DateTime? LastLoginTime { get; set; }

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? PhoneNumber { get; set; }
}
