namespace _13_WebAPI;


public class Program {

    // начальные данные, с которыми будет работать пользователь:
    static List<Person> users = new List<Person> {
        new() { Id = Guid.NewGuid().ToString(), Name = "Tom", Age = 37 },
        new() { Id = Guid.NewGuid().ToString(), Name = "Bob", Age = 41 },
        new() { Id = Guid.NewGuid().ToString(), Name = "Sam", Age = 24 }
    };  

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        // Подключение функциональности статических файлов:
        app.UseDefaultFiles();
        app.UseStaticFiles();

        // Конечная точка, которая обрабатывает запрос типа GET по маршруту "api/users":
        app.MapGet("/api/users", () => users);

        // Когда клиент обращается к приложению для получения одного объекта по id
        // в запрос типа GET по адресу "api/users/{id}", то срабатывает другая конечная точка:
        app.MapGet("/api/users/{id}", (string id) =>
        {
            // получаем пользователя по id
            Person? user = users.FirstOrDefault(u => u.Id == id);
            // если не найден, отправляем статусный код и сообщение об ошибке
            if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

            // если пользователь найден, отправляем его
            return Results.Json(user);
        });

        // При получении запроса типа DELETE по маршруту "/api/users/{id}" срабатывает другая конечная точка:
        app.MapDelete("/api/users/{id}", (string id) =>
        {
            // получаем пользователя по id
            Person? user = users.FirstOrDefault(u => u.Id == id);

            // если не найден, отправляем статусный код и сообщение об ошибке
            if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

            // если пользователь найден, удаляем его
            users.Remove(user);
            return Results.Json(user);
        });

        // При получении запроса с методом POST по адресу "/api/users" срабатывает следующая конечная точка:
        app.MapPost("/api/users", (Person user) => {

            // устанавливаем id для нового пользователя
            user.Id = Guid.NewGuid().ToString();
            // добавляем пользователя в список
            users.Add(user);
            return user;
        });

        // Если приложению приходит PUT-запрос по адресу "/api/users", то аналогичным образом получаем
        // отправленные клиентом данные в виде объекта Person и пытаемся найти подобный объект в списке users.
        // Если объект не найден, отправляем статусный код 404.
        // Если объект найден, то изменяем его данные и отправляем обратно клиенту:
        app.MapPut("/api/users", (Person userData) => {

            // получаем пользователя по id
            var user = users.FirstOrDefault(u => u.Id == userData.Id);
            // если не найден, отправляем статусный код и сообщение об ошибке
            if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
            // если пользователь найден, изменяем его данные и отправляем обратно клиенту

            user.Age = userData.Age;
            user.Name = userData.Name;
            return Results.Json(user);
        });

        app.Run();
    }
}

public class Person {
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}