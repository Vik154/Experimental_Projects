namespace _02_Views;

public interface ITimeService {
    string Time { get; }
}

public class SimpleTimeService : ITimeService {
    public string Time => DateTime.Now.ToShortTimeString();
}
