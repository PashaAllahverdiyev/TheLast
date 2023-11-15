using Hotel.Business.Contracts.Email;
using Hotel.Business.Services.Implementations;
using Hotel.Business.Services.Interfaces;
using Hotel.Core.Entities;
using Hotel.DataAccess;
using Hotel.UI.ViewModels.HomeVM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using Org.BouncyCastle.Asn1.X509;

namespace Hotel.UI.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    private readonly IWebHostEnvironment _webHostEnvironment;
            private readonly UserManager<AppUser> _userManager;
    public HomeController(AppDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<AppUser> userManager)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index()
    {
        HomeViewModel vm = new()
        {
            Rooms = await _context.Rooms
            .Include(r=>r.Images)
            .Include(r=>r.RoomLandScape)
            .Include(r=>r.Sizes)
            .Include (r=>r.Bathroom)
            .Include(r => r.RoomDetail)
            .Include(r => r.Hotells)
            .ToListAsync(),
        };
  
        return View(vm);


    }
}

