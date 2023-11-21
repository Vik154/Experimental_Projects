using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Extensions;
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
    public async Task<byte[]> AddPhotoAsync(IFormFile file) {
        if (file != null && file.Length > 0) { 
            // считываем переданный файл в массив байтов
            using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                return binaryReader.ReadBytes((int)file.Length);
            }
        }
        return await ImageConverter.ImageToByteArrayAsync(
            $"{Directory.GetCurrentDirectory()}/wwwroot/img/running.webp");
    }

    public Task<byte[]> DeletePhotoAsync(string publicUrl) {
        throw new NotImplementedException();
    }
}
