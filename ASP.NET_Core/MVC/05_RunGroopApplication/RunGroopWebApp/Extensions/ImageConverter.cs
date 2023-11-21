using System.Collections;

namespace RunGroopWebApp.Extensions;

public static class ImageConverter {

    public static byte[] ImageToByteArray(string path) {
        return File.ReadAllBytes(path);
    }

    public static async Task<byte[]> ImageToByteArrayAsync(string path) {
        return await File.ReadAllBytesAsync(path);
    }

    public static IFormFile ByteArrayToImage(byte[] file) {
        using (var stream = new MemoryStream(file)) {
            return new FormFile(stream, 0, file.Length, "Header", Path.GetRandomFileName());
        }
    }

}
