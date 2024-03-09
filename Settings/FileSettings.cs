namespace GameStore.Settings;

public static class FileSettings{

    public const string imagesPath = "/assets/images/games";
    public const string allowedExtentions = ".jpg,.jpeg,.png";

    public const int maxFileSizeMB = 1;

    public const int maxFileSizeByte = maxFileSizeMB * 1024 * 1024;
}