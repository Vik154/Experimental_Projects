using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Interfaces;

namespace RunGroopWebApp.Services;

public class PhotoService : IPhotoService {

    /* Тут можно логику запилить из серии загрузки файлов из облака,
     * но надо API смотреть какие лучше подходят и ключи с токенами 
     * хранить где-то отдельно, ну точно не в репозиториях на гите */

    /* Умные мысли его посещали, но он был быстрее
     * В общем, клиент загружает с локальной машины картинку
     * вместо cloud сервиса - она на сервер падает сразу в БД
     */
    public async Task<string> AddPhotoAsync(IFormFile file) {
        if (file.Length > 0) {
            var path = $"{Directory.GetCurrentDirectory()}/wwwroot/img/{file.FileName}";
            using (var fileStream = new FileStream(path, FileMode.Create)) {
                await file.CopyToAsync(fileStream);
            }
            return $"wwwroot/img/{file.FileName}";
        }
        return "wwwroot/img/rikmorty.jpg";
    }

    public Task<string> DeletePhotoAsync(string publicUrl) {
        throw new NotImplementedException();
    }
}
