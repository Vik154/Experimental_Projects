namespace _10_Configuration;

/// <summary> TextConfigurationProvider будет представлять провайдер конфигурации и 
/// поэтому должен быть унаследован от класса ConfigurationProvider. </summary>
public class TextConfigurationProvider : ConfigurationProvider {
    public string? FilePath { get; set; }

    public TextConfigurationProvider(string? path) {
        FilePath = path;
    }

    /// <summary> 
    /// с помощью StreamReader происходит считывание текстового файла и добавление данных в словарь data.
    /// Для загрузки данных переопределяется метод Load(), определенный в базовом классе.
    /// </summary>
    public override void Load() {
        var data = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        
        if (FilePath == null) { return; }

        using (StreamReader textReader = new StreamReader(FilePath)) {
            string? line;
            while ((line = textReader.ReadLine()) != null) {
                string key = line.Trim();
                string? value = textReader.ReadLine() ?? "";
                data.Add(key, value);
            }
        }
        // Data унаследовано от ConfigurationProvider.
        // Это свойство хранит все те конфигурационные настройки, которые потом используются в программе.
        Data = data;
    }
}
