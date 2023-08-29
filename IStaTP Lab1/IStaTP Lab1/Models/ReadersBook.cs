using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IStaTP_Lab1.Models;

public partial class ReadersBook
{
    public int Id { get; set; }


    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Читач")]
    public int ReaderId { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Книга")]
    public int BookId { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Дата отримання")]
    public DateTime Issue { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Планова дата повернення")]
    public DateTime PlanReturn { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Статус")]
    public int StatusId { get; set; }

    [Display(Name = "Фактична дата повернення")]
    public DateTime? FactReturn { get; set; }

    [Display(Name = "Книга")]
    public virtual Book? Book { get; set; } = null!;

    [Display(Name = "Читач")]
    public virtual Reader? Reader { get; set; } = null!;

    [Display(Name = "Статус")]
    public virtual Status? Status { get; set; } = null!;
}
