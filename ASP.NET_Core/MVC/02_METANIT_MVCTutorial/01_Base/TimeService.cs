namespace _01_Base;

/// <summary> Передача зависимостей в контроллер </summary>
public interface ITimeService {
    string Time { get; }
}

/// <summary> Передача зависимостей в контроллер </summary>
public class SimpleTimeService : ITimeService {
    public string Time => DateTime.Now.ToString("hh:mm:ss");
}
