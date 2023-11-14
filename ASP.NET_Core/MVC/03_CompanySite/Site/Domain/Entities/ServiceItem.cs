using System.ComponentModel.DataAnnotations;

namespace Site.Domain.Entities;

/// <summary> Класс представляющий услугу на сайте </summary>
public class ServiceItem : EntityBase {

    [Required(ErrorMessage = "Заполните название услуги")]
    [Display(Name = "Название услуги")]
    public override string? Title { get; set; }

    [Display(Name = "Краткое описание услуги")]
    public override string? Subtitle { get; set; }

    [Display(Name = "Полное описание услуги")]
    public override string? Text { get; set; }
}
