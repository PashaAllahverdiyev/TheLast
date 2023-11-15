using Hotel.Business.Services.Interfaces;
using Hotel.Business.Utilities;
using Hotel.Business.ViewModels.RoomVM;
using Hotel.Core.Entities;
using Hotel.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Image = Hotel.Core.Entities.Image;

namespace Hotel.UI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
public class RoomController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webEnv;
    private readonly IFileService _fileservice;
    public RoomController(AppDbContext context,
                            IWebHostEnvironment webEnv,
                            IFileService fileservice)
    {
        _context = context;
        _webEnv = webEnv;
        _fileservice = fileservice;
    }
   
    public IActionResult Index()
    {
       
        List<RoomListViewModel> product = _context.Rooms.Select(p => new RoomListViewModel()
        {
            Id = p.Id,
            Name = p.Name,
            Images = p.Images.FirstOrDefault(i => i.IsMain.Equals(true)).Url,
            
        }).ToList();


        return View(product);
    }
    public IActionResult Create()
    {
        ViewBag.Sizes = _context.Sizes.ToList();
        return View();
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoomCreateViewModel model)
    {
        ViewBag.Sizes = _context.Sizes.ToList();

        string filename = string.Empty;
        string filenames = string.Empty;
        Room newRoom = new()
        {
            Name = model.Name,
            Price = model.Price,
            Status = model.Status,
            RoomLandScapeId = model.RoomLandScapeId,
            BathRoomId = model.BathRoomId,
            RoomDetailId = model.RoomDetailId,
            HotellId = model.HotelId,
        };
        filename = await _fileservice.UploadFile(model.MainImage, _webEnv.WebRootPath, 1000, "assets", "images");
        Image MainImage = new()
        {
            IsMain = true,
            Url = filename
        };
        newRoom.Images.Add(MainImage);
        foreach (IFormFile image in model.Images)
        {
            if (!image.CheckFileSize(1000))
            {
                return View(nameof(Create));
            };
            if (!image.CheckFileType("image/"))
            {
                return View(nameof(Create));
            }
            filenames = await _fileservice.UploadFile(image, _webEnv.WebRootPath, 1000, "assets", "images");

            Image NotMainImage = new()
            {
                IsMain = false,
                Url = filenames
            };
            newRoom.Images.Add(NotMainImage);
        }
     
        foreach (int id in model.SizeIds)
        {
            RoomSize roomSize = new()
            {
                RoomId = id,    
                SizeId = id,
            };
            newRoom.Sizes.Add(roomSize);
        }
        _context.Rooms.Add(newRoom);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));

    }
    [HttpPost]
    [ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Room room = await _context.Rooms
            .Include(r=>r.Images)
            .FirstOrDefaultAsync(r=>r.Id == id);
        if (room == null) return NotFound();
        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }


}
