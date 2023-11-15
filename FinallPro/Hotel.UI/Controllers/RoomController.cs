using Hotel.Core.Entities;
using Hotel.DataAccess;
using Hotel.UI.ViewModels.HomeVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.UI.Controllers;

public class RoomController : Controller
{
    private readonly AppDbContext _context;
    public RoomController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index(string name=null,decimal? price=null,string size=null)
    {
          
        HomeViewModel vm = new()
        {
            Rooms = await _context.Rooms
                     .Include(r => r.Images)
                     .Include(r => r.Sizes)
                     .Include(r => r.RoomLandScape)
                     .Include(r => r.Bathroom)
                     .Include(r => r.RoomDetail)
                     .Include(r => r.Hotells).ToListAsync()
        };
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
        var room = await _context.Rooms
             .Include(r => r.Images)
                     .Include(r => r.Sizes)
                     .Include(r => r.RoomLandScape)
                     .Include(r => r.Bathroom)
                     .Include(r => r.RoomDetail)
                     .Include(r=>r.Reviews).ThenInclude(r=>r.Comment)
                     .FirstOrDefaultAsync(r => r.Id == id);
        if (room == null)
        {
            return NotFound();
        }


     //   var reservationsForRoom = _context.Reservations
     //.Where(r => r.RoomId == id)
     //.ToList();

        //   var viewModel = new RoomDetailCreateViewModel
        //   {
        //       Room = room,
        //       Reservation = reservationsForRoom
        //   };

        
        

        return View(room);
    }
    [HttpPost]
    public async Task<IActionResult> Detail(int id, int rate, string text)
    {
        var room = await _context.Rooms
             .Include(r => r.Images)
                     .Include(r => r.Sizes)
                     .Include(r => r.RoomLandScape)
                     .Include(r => r.Bathroom)
                     .Include(r => r.RoomDetail)
                     .Include (r => r.Reviews).ThenInclude(r=>r.Comment)
                     .FirstOrDefaultAsync(r => r.Id == id);
        if (room == null)
        {
            return NotFound();
        }
        Comment comment = new()
        {
            Text = text,
        };
        Review review = new()
        {
            Room = room,
            Comment = comment
        };
        review.Rating += rate;
        _context.Reviews.Add(review);
        _context.SaveChanges();





        //   var reservationsForRoom = _context.Reservations
        //.Where(r => r.RoomId == id)
        //.ToList();

        //   var viewModel = new RoomDetailCreateViewModel
        //   {
        //       Room = room,
        //       Reservation = reservationsForRoom
        //   };




        return View(room);
    }
}
