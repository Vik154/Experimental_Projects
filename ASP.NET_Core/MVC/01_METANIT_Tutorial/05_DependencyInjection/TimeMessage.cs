namespace _05_DependencyInjection;

/// <summary> Конструкторы:
/// Встроенная в ASP.NET Core система внедрения зависимостей использует 
/// конструкторы классов для передачи всех зависимостей.
/// Передача сервисов через конструкторы является предпочтительным 
/// способом внедрения зависимостей.
/// </summary>
public class TimeMessage {
    ITimeService _timeService;

    /// <summary> через конструктор класса передается зависимость от ITimeService </summary>
    public TimeMessage(ITimeService timeService) => _timeService = timeService;

    /// <summary>  В методе GetTime() формируем сообщение, в котором из сервиса получаем текущее время. </summary>
    public string GetTime() => $"Time: {_timeService.GetTime()}";
}
