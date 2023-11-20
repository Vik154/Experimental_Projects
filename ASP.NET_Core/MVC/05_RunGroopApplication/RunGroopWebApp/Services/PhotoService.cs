using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Interfaces;

namespace RunGroopWebApp.Services;

public class PhotoService : IPhotoService {

    /* Тут можно логику запилить из серии загрузки файлов из облака,
     * но надо API смотреть какие лучше подходят и ключи с токенами 
     * хранить где-то отдельно, ну точно в репозиториях на гите */

    public Task<FileResult> AddPhotoAsync(IFormFile file) {
        //if (file.Length > 0) {

        //}
        throw new NotImplementedException();

    }

    public Task<FileResult> DeletePhotoAsync(string publicUrl) {
        throw new NotImplementedException();
    }
}
