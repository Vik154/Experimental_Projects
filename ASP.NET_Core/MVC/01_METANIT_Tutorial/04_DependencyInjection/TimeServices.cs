namespace _04_DependencyInjection;

/// <summary> Предназначен для получения времени </summary>
interface ITimeService {
    string GetTime();
}

/// <summary> Возвращает текущее время в формате hh:mm(то есть часы и минуты) </summary>
public class ShortTimeServices : ITimeService {
    public string GetTime() => DateTime.Now.ToShortTimeString();
}

/// <summary> время в формате hh:mm:ss </summary>
class LongTimeService : ITimeService {
    public string GetTime() => DateTime.Now.ToLongTimeString();
}

/// <summary> Сервис как конкретный класс - Возвращает текущее время в формате hh:mm </summary>
public class TimeService {
    public string GetTime() => DateTime.Now.ToShortTimeString();
}

/// <summary> Расширения для добавления сервисов </summary>
public static class ServiceProviderExtensions {
    public static void AddTimeService(this IServiceCollection services) {
        services.AddTransient<TimeService>();
    }
}
