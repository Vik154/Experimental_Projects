using Microsoft.AspNetCore.Mvc;

namespace RunGroopWebApp.Interfaces;

public interface IPhotoService {
    Task<string> AddPhotoAsync(IFormFile file);
    Task<string> DeletePhotoAsync(string publicUrl);
}
