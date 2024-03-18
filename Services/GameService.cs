using GameStore.Data;
using GameStore.Models;
using GameStore.Settings;
using GameStore.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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
        var coverName = await saveCover(model.Cover);
        Game game = new()
        {
            Name = model.Name,
            Description = model.Description,
            CategoryId = model.CategoryId,
            Cover = coverName,
            Devices = model.SelectedDevices.Select(d => new GameDevice{DeviceId = d}).ToList()
        };

        _context.Add(game);
        _context.SaveChanges();

        
    }

    public IEnumerable<Game> GetAll()
    {
        var games = _context.Games
        .Include(g => g.Category)
        .Include(g => g.Devices)
        .ThenInclude(d => d.Device)
        .AsNoTracking()
        .ToList();

        return games;
    }

    public Game? GetById(int id)
        {
      var game = _context.Games
      .Include(g => g.Category)
      .Include(g => g.Devices)
      .ThenInclude(d => d.Device)
      .AsNoTracking()
      .SingleOrDefault(g => g.Id == id);


      return game;
    }
    public async Task<Game?> Edit(UpdateGameViewModel model)
    {
       var game = _context.Games
       .Include(g => g.Devices)
       .SingleOrDefault(g => g.Id == model.Id);

       if(game is null) return null;

        var hasNewCover = model.Cover is not null;
        var oldCover = game.Cover;
       game.Name = model.Name;
       game.Description = model.Description;
       game.CategoryId = model.CategoryId;
       game.Devices = model.SelectedDevices.Select(d => new GameDevice{DeviceId = d}).ToList();

        if(hasNewCover)
        {
            game.Cover = await saveCover(model.Cover!);
        }

        var effectedRows = _context.SaveChanges();

        if(effectedRows > 0 )
        {
            if(hasNewCover)
            {
                var cover = Path.Combine(_imagesPath, oldCover);
                File.Delete(cover);
            }

            return game;
        }

        else{
            var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
            return null;
        }
    
    }

   

    private async Task<string> saveCover(IFormFile cover)
{
     var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

        var path = Path.Combine(_imagesPath, coverName);

        using var stream = File.Create(path);
        await cover.CopyToAsync(stream);

        return coverName;
}
}