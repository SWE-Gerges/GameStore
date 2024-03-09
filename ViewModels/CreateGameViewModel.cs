using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using GameStore.Attributes;
using GameStore.Models;
using GameStore.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameStore.ViewModels;

public class CreateGameViewModel{

    [MaxLength(length: 250)]
    public string Name { get; set; } = string.Empty;

    //[Extension]
    [AllowedExtentions(FileSettings.allowedExtentions), MaxFileSize(FileSettings.maxFileSizeByte)]
    public IFormFile Cover { get; set; } = default!;

    [Display(Name ="Category")]
    public int CategoryId { get; set; }

    public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
    [Display(Name = "Supported Devices")]
    public List<int> SelectedDevices { get; set; } = default!;

    public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();


    [MaxLength(length:2500)]
    public string Description { get; set; } = string.Empty;

    
}