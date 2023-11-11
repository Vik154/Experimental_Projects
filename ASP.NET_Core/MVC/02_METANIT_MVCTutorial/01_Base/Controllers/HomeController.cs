using Microsoft.AspNetCore.Mvc;

namespace _01_Base.Controllers;

public record class Person(string Name, int Age);

public class HomeController : Controller {

    public string Index() => "Index View";

    [HttpGet]
    public string Get() => "Get";

    public async Task Info() {
        Response.ContentType = "text/html;charset=utf-8";
        await Response.WriteAsync("<h2>Hello World</h2>");
    }

    public async Task HeaderInfo() {
        Response.ContentType = "text/html;charset=utf-8";
        System.Text.StringBuilder tableBuilder = new("<h2>Request headers</h2><table>");
        foreach (var header in Request.Headers) {
            tableBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
        }
        tableBuilder.Append("</table>");
        await Response.WriteAsync(tableBuilder.ToString());
    }

    /// <summary> Передача сложных объектов </summary>
    public string PersonInfo(Person person) => $"Person Name: {person.Name}  Person Age: {person.Age}";

    /// <summary> Передача массивов </summary>
    /// https://localhost:7288/Home/PersonsArray?people[0].name=Tom&people[0].age=37&people[1].name=Bob&people[1].age=41
    /// https://localhost:7288/Home/PersonsArray?[0].name=Tom&[0].age=37&[1].name=Bob&[1].age=41
    public string PersonsArray(Person[] people) {
        string result = "";
        foreach (Person person in people) {
            result = $"{result} {person.Name}; ";
        }
        return result;
    }

    /// <summary> Передача словарей Dictionary </summary>
    /// https://localhost:7288/Home/PersonsDict?items[germany]=berlin&items[france]=paris&items[spain]=madrid
    public string PersonsDict(Dictionary<string, string> items) {
        string result = "";
        foreach (var item in items) {
            result = $"{result} {item.Key} - {item.Value}; ";
        }
        return result;
    }

    /// <summary> Объект Request.Query </summary>
    /// https://localhost:7288/Home/RequestQuery?name=Tom&age=37.
    public string RequestQuery() {
        string? name = Request.Query["name"];
        string? age = Request.Query["age"];
        return $"Name: {name}  Age: {age}";
    }
}
