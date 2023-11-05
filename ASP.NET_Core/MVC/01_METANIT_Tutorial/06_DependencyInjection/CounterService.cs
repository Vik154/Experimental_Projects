namespace _06_DependencyInjection;

/// <summary>Данный класс просто устанавливает объект ICounter, передаваемый через конструктор.</summary>
public class CounterService {
    public ICounter Counter { get; }
    public CounterService(ICounter counter) => Counter = counter;
}
