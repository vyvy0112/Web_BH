﻿using System;
using System.Collections.Generic;

namespace WEB.Data;

public partial class Contact
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public bool? Status { get; set; }
}
