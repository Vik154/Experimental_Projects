namespace _06_DependencyInjection;


public interface ICounter {
    int Value { get; }
}

/// <summary> Генерация случайного числа в диапазоне от 0 до 1000000 </summary>
public class RandomCounter : ICounter {

    static Random rnd = new Random();
    private int _value;

    public RandomCounter() => _value = rnd.Next(0, 1000000);
    public int Value => _value;
}