namespace RunGroopWebApp.Extensions;

public static class ImageConverter {

    public static byte[] ImageToByteArray(string path) {
        return File.ReadAllBytes(path);
    }
}
