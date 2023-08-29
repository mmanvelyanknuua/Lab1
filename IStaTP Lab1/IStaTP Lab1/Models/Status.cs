using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IStaTP_Lab1.Models;

public partial class Status
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Статус")]
    public string Name { get; set; } = null!;

    public virtual ICollection<ReadersBook> ReadersBooks { get; set; } = new List<ReadersBook>();
}
