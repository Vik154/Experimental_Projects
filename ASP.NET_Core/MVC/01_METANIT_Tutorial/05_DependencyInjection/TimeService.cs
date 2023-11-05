namespace _05_DependencyInjection;


public interface ITimeService {
    string GetTime();
}

public class ShortTimeService : ITimeService {
    public string GetTime() => DateTime.Now.ToShortTimeString();        
}
