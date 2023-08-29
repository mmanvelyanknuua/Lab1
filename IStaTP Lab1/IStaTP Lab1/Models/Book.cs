using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IStaTP_Lab1.Models;

public partial class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Книга")]
    public string Name { get; set; } = null!;

    [Display(Name = "Інформація про книгу")]
    public string? Info { get; set; }

    public int CategoryId { get; set; }

    public virtual ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();

    [Display(Name = "Категорія")]
    public virtual Category? Category { get; set; } = null!;

    public virtual ICollection<ReadersBook> ReadersBooks { get; set; } = new List<ReadersBook>();
}
