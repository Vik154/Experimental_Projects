namespace _06_DependencyInjection;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        /// <summary> Метод AddTransient() создает transient-объекты.
        /// Такие объекты создаются при каждом обращении к ним.
        /// Т.е. при втором и последующих запросах к контроллеру будут создаваться новые объекты RandomCounter
        /// </summary>
        //builder.Services.AddTransient<ICounter, RandomCounter>();
        //builder.Services.AddTransient<CounterService>();
        // Вывод:
        // Запрос 1; Counter: 233213; Service: 720197
        // Запрос 5; Counter: 971003; Service: 309547
        // Запрос 8; Counter: 474480; Service: 621652

        /*------------------------------------------------------------------------------------------*/

        /// <summary> Метод AddScoped создает один экземпляр объекта для всего запроса.
        /// Теперь в рамках одного и того же запроса и CounterMiddleware и сервис CounterService 
        /// будут использовать один и тот же объект RandomCounter. 
        /// При следующем запросе к приложению будет генерироваться новый объект RandomCounter.</summary>
        // builder.Services.AddScoped<ICounter, RandomCounter>();
        // builder.Services.AddScoped<CounterService>();
        // Вывод:
        // Запрос 1; Counter: 706925; Service: 706925
        // Запрос 2; Counter: 632447; Service: 632447

        /*------------------------------------------------------------------------------------------*/

        /// <summary> AddSingleton создает один объект для всех последующих запросов, 
        /// при этом объект создается только тогда, когда он непосредственно необходим.</summary>
        builder.Services.AddSingleton<ICounter, RandomCounter>();
        builder.Services.AddSingleton<CounterService>();
        // Вывод:
        // Запрос 1; Counter: 298641; Service: 298641
        // Запрос 2; Counter: 298641; Service: 298641

        var app = builder.Build();

        app.UseMiddleware<CounterMiddleware>();

        app.Run();
    }
}
