using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WEB.Data;

public partial class Review
{
    public int Id { get; set; }

    public int ProductId { get; set; }
	[Required(ErrorMessage = "Name không được để trống!")]
	[EmailAddress(ErrorMessage = "Name không hợp lệ!")]
	public string Name { get; set; } = string.Empty;
	[Required(ErrorMessage = "Email không được để trống!")]
	[EmailAddress(ErrorMessage = "Email không hợp lệ!")]
	public string Email { get; set; }

    public string Comment { get; set; }

    public int? Star { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
