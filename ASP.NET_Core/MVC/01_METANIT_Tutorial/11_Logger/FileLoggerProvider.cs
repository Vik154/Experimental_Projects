namespace _11_Logger;

/// <summary> 
/// Этот класс представляет провайдер логгирования. 
/// Он должен реализовать интерфейс ILoggerProvider.
/// </summary>
public class FileLoggerProvider : ILoggerProvider {
    string path;

    public FileLoggerProvider(string path) => this.path = path;

    public ILogger CreateLogger(string categoryName) => new FileLogger(path);

    public void Dispose() { }
}
