using System.Linq;
using GameStore.Data;
using GameStore.Services;
using GameStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace GameStore.Controllers;

public class GamesController : Controller {

    private readonly ApplicationDbContext _context;
    private readonly ICategoriesService _categoriesService;

    private readonly IDevicesService _devicesService;

    private readonly IGameService _gameService;
    public GamesController(ApplicationDbContext context, ICategoriesService categoriesService, IDevicesService devicesService, IGameService gameService)
    {
        _context = context;
        _categoriesService = categoriesService;
        _devicesService = devicesService;
        _gameService = gameService;
    }

    public IActionResult Index()
    {
       var games = _gameService.GetAll();
       return View(games);
    }

    public IActionResult Details(int id)
    {
        var game = _gameService.GetById(id);
        if(game is null)
            return NotFound();

        return View(game);
    }

    [HttpGet]
    public IActionResult Create()
    {
        
        CreateGameViewModel viewModel = new()
        {
            Categories = _categoriesService.GetSelectList(),


            Devices = _devicesService.GetSelectList()
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateGameViewModel model)
    {
        if(!ModelState.IsValid)
        {
            model.Categories = _categoriesService.GetSelectList();

            model.Devices = _devicesService.GetSelectList();

            return View(model);
        }

        await _gameService.Create(model);
        // Save Game to DataBase
        // Save Cover to Sereve

        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public IActionResult Update(int id)
    {
        var game = _gameService.GetById(id);
        if(game is null) 
            return NotFound();
        UpdateGameViewModel viewModel = new ()
        {
            Id = id,
            Name = game.Name,
            Description = game.Description,
            CategoryId = game.CategoryId,
            SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
            Categories = _categoriesService.GetSelectList(),
            Devices = _devicesService.GetSelectList(),
            CurrentCover = game.Cover
        };




        return View(viewModel);
    }
}
