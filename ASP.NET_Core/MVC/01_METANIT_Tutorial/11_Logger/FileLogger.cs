namespace _11_Logger;

/// <summary> провайдер логгирования </summary>
public class FileLogger : ILogger, IDisposable {

    string filePath;
    static object _lock = new object();

    public FileLogger(string path) => filePath = path;

    /// <summary> 
    /// BeginScope: этот метод возвращает объект IDisposable, который представляет 
    /// некоторую область видимости для логгера. В данном случае нам этот метод не важен,
    /// поэтому возвращаем значение this - ссылку на текущий объект класса, который реализует интерфейс IDisposable.
    /// </summary>
    public IDisposable BeginScope<TState>(TState state) where TState : notnull {
        return this;
    }

    public void Dispose() { }

    /// <summary> 
    /// IsEnabled: возвращает значения true или false, которые указывает, доступен
    /// ли логгер для использования. Здесь можно задать различную логику. В частности, 
    /// в этот метод передается объект LogLevel, и мы можем, к примеру, задействовать логгер 
    /// в зависимости от значения этого объекта. Но в данном случае просто возвращаем true, 
    /// то есть логгер доступен всегда.
    /// </summary>
    public bool IsEnabled(LogLevel logLevel) {
        //return logLevel == LogLevel.Trace;
        return true;
    }

    /// <summary> Log: этот метод предназначен для выполнения логгирования </summary>
    public void Log<TState>(LogLevel logLevel,      // уровень детализации текущего сообщения
                            EventId eventId,        // идентификатор события
                            TState state,           // некоторый объект состояния, который хранит сообщение
                            Exception? exception,   //информация об исключении
                            // функция форматирования, которая с помощью двух предыдущих
                            // параметров позволяет получить собственно сообщение для логгирования
                            Func<TState, Exception?, string> formatter) 
    {
        lock (_lock) {
            File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
        }
    }
}
