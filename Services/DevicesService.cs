using GameStore.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Services;

public class DevicesServices : IDevicesService
{
    private readonly ApplicationDbContext _context;

    public DevicesServices(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<SelectListItem> GetSelectList()
    {
       return _context.Devices
            .Select(d => new SelectListItem{Value = d.Id.ToString(), Text = d.Name})
            .OrderBy(d=> d.Text)
            .AsNoTracking()
            .ToList();
    }
}