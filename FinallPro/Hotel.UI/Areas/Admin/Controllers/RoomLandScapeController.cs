using Hotel.Business.ViewModels.RoomLandScapeVM;
using Hotel.Core.Entities;
using Hotel.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.UI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
public class RoomLandScapeController : Controller
{

    private readonly AppDbContext _context;

    public RoomLandScapeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int id)
    {
        RoomLandScape roomDetail = _context.RoomLandScapes.FirstOrDefault(r => r.Id == id);
        List<RoomLandScape> roomDetails = _context.RoomLandScapes.ToList();
        return View(roomDetails);
    }
    public async Task<IActionResult> Details(int id)
    {
        RoomLandScape? roomLandScape = await _context.RoomLandScapes.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        if (roomLandScape == null) return NotFound();
        return View(roomLandScape);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoomLandScapeCreateViewModel room)
    {
        if (!ModelState.IsValid) return View(room);
        RoomLandScape roomLandScape = new()
        {
            Garden = room.Garden,
            City = room.City,
            Sea = room.Sea,
            Mountain = room.Mountain,
        };
        await _context.RoomLandScapes.AddAsync(roomLandScape);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
  
    [HttpPost]
    [ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        RoomLandScape roomLandScape = await _context.RoomLandScapes.FindAsync(id);
        if (roomLandScape == null) return NotFound();
        _context.RoomLandScapes.Remove(roomLandScape);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

}
