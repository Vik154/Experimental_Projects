namespace RunGroopWebApp.Extensions;

public static class ImageConverter {

    public static byte[] ImageToByteArray(string path) {
        return File.ReadAllBytes(path);
    }

    public static async Task<byte[]> ImageToByteArrayAsync(string path) {
        return await File.ReadAllBytesAsync(path);
    }
}
