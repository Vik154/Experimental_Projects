using Microsoft.AspNetCore.Mvc;

namespace RunGroopWebApp.Interfaces;

public interface IPhotoService {
    Task<byte[]> AddPhotoAsync(IFormFile file);
    Task<byte[]> DeletePhotoAsync(string publicUrl);
}
