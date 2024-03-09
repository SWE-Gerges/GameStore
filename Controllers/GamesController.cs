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
        return View();
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
}
