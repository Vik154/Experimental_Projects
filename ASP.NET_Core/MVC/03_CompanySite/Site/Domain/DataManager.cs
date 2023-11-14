using Site.Domain.Repositories.Abstract;

namespace Site.Domain;

/// <summary> Класс-помощник, точка входа для DataBase контекста </summary>
public class DataManager {

    public ITextFieldsRepository TextFields { get; set; }
    public IServiceItemsRepository ServiceItems { get; set; }

    public DataManager(ITextFieldsRepository textFields, IServiceItemsRepository serviceItems) {
        TextFields = textFields;
        ServiceItems = serviceItems;
    }
}