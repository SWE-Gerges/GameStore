namespace GameStore.ViewModels;

public class UpdateGameViewModel : GameFormViewModel
{
    public int Id { get; set; }

    public string? CurrentCover { get; set; }

       [AllowedExtentions(FileSettings.allowedExtentions),
     MaxFileSize(FileSettings.maxFileSizeByte)]
    public IFormFile? Cover { get; set; } = default!;
}