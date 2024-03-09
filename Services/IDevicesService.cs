using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameStore.Services;

public interface IDevicesService{
    IEnumerable<SelectListItem> GetSelectList();
}