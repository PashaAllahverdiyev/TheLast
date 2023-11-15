using Hotel.Business.ViewModels.BathroomVM;
using Hotel.Core.Entities;
using Hotel.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;

namespace Hotel.UI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
public class BathroomController : Controller
{
private readonly AppDbContext _context;
public BathroomController(AppDbContext context)
{
    _context = context;
}
public IActionResult Index()
{
    List<Bathroom> bathrooms = _context.Bathrooms.ToList();
    return View(bathrooms);
}
public IActionResult Create()
{
    return View();
}
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(BathroomListViewModel bathroom)
{
    if (!ModelState.IsValid) return View(bathroom);
    Bathroom bathroom1 = new()
    {
        Shower=bathroom.Shower,
        Toileet=bathroom.Toileet,
        BathroomSupplies= bathroom.BathroomSupplies,
        Dressing = bathroom.Dressing
    };
    await _context.Bathrooms.AddAsync(bathroom1);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}
    [HttpPost]
    [ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Bathroom bathroom = await _context.Bathrooms.FindAsync(id);
        if (bathroom == null) return NotFound();
        _context.Bathrooms.Remove(bathroom);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
