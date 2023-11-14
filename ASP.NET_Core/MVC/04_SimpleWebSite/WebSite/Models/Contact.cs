using System.ComponentModel.DataAnnotations;

namespace WebSite.Models;

public class Contact {

    [Display(Name = "Введите имя:")]
    [Required(ErrorMessage = "Обязательное поле")]
    public string Name { get; set; }

    [Display(Name = "Введите фамилию:")]
    [Required(ErrorMessage = "Обязательное поле")]
    public string Surname { get; set; }

    [Display(Name = "Введите возраст:")]
    [Required(ErrorMessage = "Обязательное поле")]
    public int Age { get; set; }

    [Display(Name = "Введите Email:")]
    [Required(ErrorMessage = "Обязательное поле")]
    public string Email { get; set; }

    [Display(Name = "Введите сообщение:")]
    [Required(ErrorMessage = "Обязательное поле")]
    public string Message { get; set; }
}
