using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IStaTP_Lab1.Models;

public partial class Author
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Поле не повинно бути порожнім")]
	[Display(Name = "Автор")]
	public string Name { get; set; } = null!;

	[Display(Name = "Інформація про автора")]
	public string? Info { get; set; }

    public virtual ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
}
