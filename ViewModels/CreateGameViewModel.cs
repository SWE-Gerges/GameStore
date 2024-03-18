using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using GameStore.Attributes;
using GameStore.Models;
using GameStore.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameStore.ViewModels;

public class CreateGameViewModel : GameFormViewModel{



    //[Extension]
    [AllowedExtentions(FileSettings.allowedExtentions),
     MaxFileSize(FileSettings.maxFileSizeByte)]
    public IFormFile Cover { get; set; } = default!;

   
    
}