namespace _06_DependencyInjection;

/// <summary> -- </summary>
public class TimeService {
    public TimeService() => Time = DateTime.Now.ToLongTimeString();
    public string Time { get; }
}
