using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IStaTP_Lab1.Models;

public partial class AuthorBook
{

	[Required(ErrorMessage = "Поле не повинно бути порожнім")]
	[Display(Name = "Книга")]
	public int BookId { get; set; }

	[Required(ErrorMessage = "Поле не повинно бути порожнім")]
	[Display(Name = "Автор")]
	public int AuthorId { get; set; }

    public int Id { get; set; }

	[Display(Name = "Автор")]
	public virtual Author? Author { get; set; } = null!;

	[Display(Name = "Книга")]
	public virtual Book? Book { get; set; } = null!;
}
