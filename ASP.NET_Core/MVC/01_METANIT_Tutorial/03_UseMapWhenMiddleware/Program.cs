namespace _03_UseMapWhenMiddleware;


public class Program {

    static string date = "";

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        /// <summary> �������� �������� �� ��������� ������ 1 </summary>
        // app.Use(UseTest1);
        // app.Run(RunTest1);

        /// <summary> ��������� ������ � ��������� � ������� UseWhen </summary>
        // app.UseWhen(UseWhenTestPredicate, UseWhenTestConfiguration);
        // app.Run(UseWhenRun);

        /// <summary> ��������� ������ � ��������� � ������� MapWhen </summary>
        // app.MapWhen(UseMapWhenTestPredicate, UseMapWhenTestConfiguration);
        // app.Run(UseMapWhenRun);

        /// <summary> ��������� ������ � ������� Map; ��������� �������� �� ���� "/time" </summary>
        // app.Map("/time", MapTest);
        // app.Run(RunHelloWorld);

        /// <summary> ��������� �������� </summary>
        //app.Map("/home", appBuilder => {
        //    appBuilder.Map("/index", MapIndex);             // middleware ��� "/home/index"
        //    appBuilder.Map("/about", MapAbout);             // middleware ��� "/home/about"
        //    appBuilder.Run(async (context) => await         // middleware ��� "/home"
        //        context.Response.WriteAsync("Home Page"));
        //});
        //app.Run(async (context) => await context.Response.WriteAsync($"Hello MAP"));

        // ��� ���������� ���������� middleware, ������� ������������ �����,
        // � �������� ��������� ������� ����������� ����� UseMiddleware():
        /// <summary> ��������� middleware �������������� ������� MyMiddleware </summary>
        // app.UseMiddleware<MyMiddleware>();
        // app.Run(async c => await c.Response.WriteAsync("Hello middleware"));

        /// <summary> ����� ���������� ��� ����������� middleware </summary>
        app.UseMyMiddleware();
        app.Run(async c => await c.Response.WriteAsync("Hello extension middleware"));


        app.Run();
    }


    #region ����� IApplicationBuilder.Use()
    // ����� ���������� Use() ��������� ��������� middleware, ������� ��������� �������� ���������
    // ������� ����� ��������� � ��������� �����������. �� ����� ��������� ������
    // public static IApplicationBuilder Use(this IApplicationBuilder app, Func<HttpContext, Func<Task>, Task> middleware);
    // public static IApplicationBuilder Use(this IApplicationBuilder app, Func<HttpContext, RequestDelegate, Task> middleware)

    /*
     � ����� ������ ���������� ������ Use() �������� ��������� �������:

        app.Use(async (context, next) => {
            // �������� ����� ��������� ������� � ��������� middleware
            await next.Invoke();
            // �������� ����� ��������� ������� ��������� middleware
        });
     */

    /// <summary> �������� �������� �� ��������� ������ 1 </summary>
    static async Task UseTest1(HttpContext context, Func<Task> next) {
        date = DateTime.Now.ToShortDateString();    // �������� ����� ��������� ������� � ��������� middleware
        await next.Invoke();                        // �������� ��������� ������� ���������� ���������� � ���������
        Console.WriteLine($"Current date: {date}"); // �������� ����� ��������� ������� ��������� middleware
    }

    /// <summary> �������� �������� �� ��������� ������ 1 </summary>
    static async Task RunTest1(HttpContext context) {
        await context.Response.WriteAsync($"Date: {date}");
    }

    #endregion

    #region �������� ����� ��������� UseWhen
    // ����� UseWhen() �� ��������� ���������� ������� ���������
    // ������� ����������� ��������� ��� ��������� �������:
    // public static IApplicationBuilder UseWhen (this IApplicationBuilder app,
    //               Func<HttpContext,bool> predicate, Action<IApplicationBuilder> configuration);

    // app.Use(bool:context => a == b, void:config)
    static bool UseWhenTestPredicate(HttpContext context) => context.Request.Path == "/time";

    static void UseWhenTestConfiguration(IApplicationBuilder builder) {
        // �������� ������ - ������� �� ������� ����������
        builder.Use(async (context, next) => {
            var time = DateTime.Now.ToShortTimeString();
            Console.WriteLine($"Time: {time}");
            await next();   // �������� ��������� middleware
        });

        // ���������� �����
        builder.Run(async context => {
            var time = DateTime.Now.ToShortTimeString();
            await context.Response.WriteAsync($"Time: {time}");
        });
    }

    static async Task UseWhenRun(HttpContext context) {
        await context.Response.WriteAsync($"Hello World");
    }

    #endregion

    #region �������� ����� ��������� MapWhen
    // ����� MapWhen(), ��� � ����� UseWhen(), �� ��������� ���������� �������
    // ��������� ������� ����������� ���������:
    // public static IApplicationBuilder MapWhen (this IApplicationBuilder app,
    //               Func<HttpContext,bool> predicate, Action<IApplicationBuilder> configuration);


    static bool UseMapWhenTestPredicate(HttpContext context) => context.Request.Path == "/time";

    static void UseMapWhenTestConfiguration(IApplicationBuilder builder) {

        builder.Run(async context => {
            var time = DateTime.Now.ToShortTimeString();
            await context.Response.WriteAsync($"Time: {time}");
        });
    }

    static async Task UseMapWhenRun(HttpContext context) {
        await context.Response.WriteAsync($"Hello World");
    }

    #endregion

    #region ����� Map
    // ����� Map() ����������� ��� �������� ����� ���������, ������� �����
    // ������������ ������ �� ������������� ����.
    // public static IApplicationBuilder Map (this IApplicationBuilder app,
    //               string pathMatch, Action<IApplicationBuilder> configuration);

    // � �������� ��������� pathMatch ����� ��������� ���� �������, � ������� ����� �������������� �����.
    // � �������� configuration ������������ �������, � ������� ���������� ������ IApplicationBuilder
    // � � ������� ����� ����������� ����� ���������.

    /// <summary> ������� ����������� ���������, ������� ����� ������������ ������� �� ���� "/time" </summary>
    static void MapTest(IApplicationBuilder builder) {
        var time = DateTime.Now.ToShortTimeString();

        // ��������� ������ - ������� �� ������� ����������
        builder.Use(async (context, next) => {
            Console.WriteLine($"Time: {time}");
            await next();   // �������� ��������� middleware
        });

        builder.Run(async context => await context.Response.WriteAsync($"Time: {time}"));
    }

    static async Task RunHelloWorld(HttpContext context) => await context.Response.WriteAsync("Hello world");

    /// <summary> ��������� �������� </summary>
    static void MapIndex(IApplicationBuilder appBuilder) {
        appBuilder.Run(async context => await context.Response.WriteAsync("Index"));
    }

    /// <summary> ��������� �������� </summary>
    static void MapAbout(IApplicationBuilder appBuilder) {
        appBuilder.Run(async context => await context.Response.WriteAsync("About"));
    }

    #endregion
}
