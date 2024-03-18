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
        var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";

        var path = Path.Combine(_imagesPath, coverName);

        using var stream = File.Create(path);
        await model.Cover.CopyToAsync(stream);
        

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
    public void Edit(int id, Game updatedGame)
    {
       _context.Update(updatedGame);

    
    }



}