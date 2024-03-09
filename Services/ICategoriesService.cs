using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameStore.Services;

public interface ICategoriesService{
     IEnumerable<SelectListItem> GetSelectList(); 
}