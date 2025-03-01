using System;
using System.Collections.Generic;

namespace WEB.Data;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public string? Password { get; set; }

    public string? CustomerName { get; set; }

    public DateTime? DateofBirth { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public int? Status { get; set; }
}
