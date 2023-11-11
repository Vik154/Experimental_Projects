using _08_MetaData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _08_MetaData.Controllers;

public class HomeController : Controller {

    public IActionResult Create() => View();

    [HttpPost]
    public string Create(Person person) {

        if (person.Age > 110 || person.Age < 0) {
            ModelState.AddModelError("Age", "Возраст должен находиться в диапазоне от 0 до 110");
        }

        if (person.Name?.Length < 3) {
            ModelState.AddModelError("Name", "Недопустимая длина строки. Имя должно иметь больше 2 символов");
        }

        if (ModelState.IsValid)
            return $"{person.Name} - {person.Age}";

        string errorMessages = "";

        // проходим по всем элементам в ModelState
        foreach (var item in ModelState) {

            // если для определенного элемента имеются ошибки
            if (item.Value.ValidationState == ModelValidationState.Invalid) {
                errorMessages = $"{errorMessages}\nОшибки для свойства {item.Key}:\n";
            
                // пробегаемся по всем ошибкам
                foreach (var error in item.Value.Errors) {
                    errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                }
            }
        }
        return errorMessages;
    }
}
