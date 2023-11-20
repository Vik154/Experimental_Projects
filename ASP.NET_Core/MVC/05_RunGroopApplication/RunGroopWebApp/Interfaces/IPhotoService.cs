using Microsoft.AspNetCore.Mvc;

namespace RunGroopWebApp.Interfaces;

public interface IPhotoService {
    Task<FileResult> AddPhotoAsync(IFormFile file);
    Task<FileResult> DeletePhotoAsync(string publicUrl);
}
