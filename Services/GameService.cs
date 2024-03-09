using GameStore.Data;
using GameStore.Models;
using GameStore.Settings;
using GameStore.ViewModels;

namespace GameStore.Services;

public class GameService : IGameService
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    private readonly string _imagesPath;
    public GameService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.imagesPath}";
    }

    public async Task Create(CreateGameViewModel model)
    {
        var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";

        var path = Path.Combine(_imagesPath, coverName);

        using var stream = File.Create(path);
        await model.Cover.CopyToAsync(stream);
        stream.Dispose();

        Game game = new()
        {
            Name = model.Name,
            Description = model.Description,
            CategoryId = model.CategoryId,
            Devices = model.SelectedDevices.Select(d => new GameDevice{DeviceId = d}).ToList()
        };

        _context.Add(game);
        _context.SaveChanges();
    }
}