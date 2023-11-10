using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _01_Base.Pages;

public class ParametrsModel : PageModel {
    public string Message { get; private set; } = "";
    //public void OnGet(string name, int age) {
    //    Message = $"Name: {name}  Age: {age}";
    //}

    /// <summary> Передача массивов сложных объектов </summary>
    public void OnGet(Person[] people) {
        string result = "";
        foreach (Person person in people) {
            result = $"{result} {person.Name}; ";
        }
        Message = result;
    }

    /// <summary> Передача словарей Dictionary </summary>
    //public void OnGet(Dictionary<string, string> items) {
    //    string result = "";
    //    foreach (var item in items) {
    //        result = $"{result} {item.Key} - {item.Value}; ";
    //    }
    //    Message = result;
    //}
}

public record class Person(string Name, int Age);