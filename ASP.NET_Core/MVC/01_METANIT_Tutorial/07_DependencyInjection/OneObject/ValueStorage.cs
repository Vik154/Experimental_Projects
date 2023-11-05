namespace _07_DependencyInjection.OneObject;

class ValueStorage : IGenerator, IReader {

    int value;
    public int GenerateValue() {
        value = new Random().Next();
        return value;
    }

    public int ReadValue() => value;
}