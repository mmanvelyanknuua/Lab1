using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IStaTP_Lab1.Models;

public partial class Reader
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Читач")]
    public string Name { get; set; } = null!;

    [Display(Name = "Адреса")]
    public string? Address { get; set; }

    [Display(Name = "Інформація про читача")]
    public string? Info { get; set; }

    public virtual ICollection<ReadersBook> ReadersBooks { get; set; } = new List<ReadersBook>();
}
