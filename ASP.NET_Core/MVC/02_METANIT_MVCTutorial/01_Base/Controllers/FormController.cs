using Microsoft.AspNetCore.Mvc;

namespace _01_Base.Controllers; 

/// <summary> Передача данных в контроллер через формы </ summary>
public class FormController : Controller {
    
    [HttpGet]
    public async Task Index() {
        string content = @"<form method='post'>
                <label>Name:</label><br />
                <input name='name' /><br />
                <label>Age:</label><br />
                <input type='number' name='age' /><br />
                <input type='submit' value='Send' />
            </form>";
        Response.ContentType = "text/html;charset=utf-8";
        await Response.WriteAsync(content);
    }
    
    // [HttpPost]
    // public string Index(string name, int age) => $"{name}: {age}";

    /// <summary> Получение объектов </summary>
    [HttpPost]
    public string Index(Person person) => $"Person: {person.Name}: {person.Age}";

    /// <summary> Получение массивов </summary>
    public async Task IndexArray() {
        string form = @"<form method='post'>
                <p><input name='names' /></p>
                <p><input name='names' /></p>
                <p><input name='names' /></p>
                <input type='submit' value='Send' />
            </form>";
        Response.ContentType = "text/html;charset=utf-8";
        await Response.WriteAsync(form);
    }

    /// <summary> Получение массивов </summary>
    [HttpPost]
    public string IndexArray(string[] names) {
        string result = "";
        foreach (string name in names) {
            result = $"{result} {name}";
        }
        return result;
    }


    /// <summary> Передача словарей Dictionary </summary>
    [HttpGet]
    public async Task IndexDict() {
        string content = @"<form method='post'>
        <p>
            Германия:
            <input type='text' name='items[germany]' />
        </p>
        <p>
            Франция:
            <input type='text' name='items[france]' />
        </p>
        <p>
            Испания:
            <input type='text' name='items[spain]' />
        </p>
        <p>
            <input type='submit' value='Отправить' />
        </p>
    </form>";
        Response.ContentType = "text/html;charset=utf-8";
        await Response.WriteAsync(content);
    }
   
    [HttpPost]
    public string IndexDict(Dictionary<string, string> items) {
        string result = "";
        foreach (var item in items) {
            result = $"{result} {item.Key} - {item.Value}; ";
        }
        return result;
    }

    /// <summary> Получение данных из контекста запроса </summary>
    [HttpGet]
    public async Task IndexRequest() {
        string content = @"<form method='post' action='/Form/PersonData'>
                <label>Name:</label><br />
                <input name='name' /><br />
                <label>Age:</label><br />
                <input type='number' name='age' /><br />
                <input type='submit' value='Send' />
            </form>";
        Response.ContentType = "text/html;charset=utf-8";
        await Response.WriteAsync(content);
    }

    /// <summary> Получение данных из контекста запроса </summary>
    [HttpPost]
    public string PersonData() {
        string? name = Request.Form["name"];
        string? age = Request.Form["age"];
        return $"{name}: {age}";
    }
}
