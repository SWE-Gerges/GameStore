using System.ComponentModel.DataAnnotations;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameStore.ViewModels;

public class CreateGameViewModel{

    [MaxLength(length: 250)]
    public string Name { get; set; } = string.Empty;

    public IFormFile Cover { get; set; } = default!;

    public int CategoryId { get; set; }

    public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
    public List<int> SelectedDevices { get; set; } = new List<int>();

    public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();


    [MaxLength(length:2500)]
    public string Description { get; set; } = string.Empty;

    
}