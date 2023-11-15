using Hotel.Business.Contracts.Email;
using Hotel.Business.Services.Implementations;
using Hotel.Business.Services.Interfaces;
using Hotel.Core.Entities;
using Hotel.DataAccess;
using Hotel.UI.ViewModels.ReservationVM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.UI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IEmailService _emailService { get; set; }

        public ReservationController(AppDbContext context, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _emailService = emailService;
        }

        // Rezervasyonları listeleme
        public IActionResult Index()
        {
            var reservations = _context.Reservations.ToList();
            return View(reservations);
        }

        // Rezervasyon oluşturma formu
        public IActionResult Create(int? id)
        {
            var room = _context.Rooms.FirstOrDefault(r=> r.Id == id);
            if (room is null)
            {
                return NotFound();
            }
            var viewModel = new ReservationViewModel
            {
                RoomId = room.Id,
                Room = room,
                
                
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(ReservationViewModel reservationViewModel)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login");
            }

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser is null)
            {
                return NotFound();
            }

            var room = _context.Rooms.FirstOrDefault(r => r.Id == reservationViewModel.RoomId);

            if (room != null)
            {
                // Aynı tarih aralığında başka rezervasyon var mı kontrol et
                var existingReservation = _context.Reservations
                    .FirstOrDefault(r =>
                        r.RoomId == room.Id &&
                        ((r.CheckInTime <= reservationViewModel.CheckInTime && r.CheckOutTime >= reservationViewModel.CheckInTime) ||
                        (r.CheckInTime <= reservationViewModel.CheckOutTime && r.CheckOutTime >= reservationViewModel.CheckOutTime)));

                if (existingReservation != null)
                {
                    ModelState.AddModelError(string.Empty, "Seçtiğiniz tarih aralığında başka bir rezervasyon bulunmaktadır. Lütfen farklı bir tarih aralığı seçiniz.");
                    return View(reservationViewModel);
                }

                var reservationModel = new Reservation
                {
                    UserId = currentUser.Id,
                    RoomId = room.Id,
                    CheckInTime = reservationViewModel.CheckInTime,
                    CheckOutTime = reservationViewModel.CheckOutTime
                };
                var stausMessageDto = PrepareStausMessage(currentUser.Email);
                _emailService.Send(stausMessageDto);

                _context.Reservations.Add(reservationModel);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            //var currentUser = await _userManager.GetUserAsync(HttpContext.User);
       

            return View(reservationViewModel);


            MessageDto PrepareStausMessage(string email)
            {
                string body = "ReserVation Has Been Sucesiffuly Finished";

                string subject = EmailMessages.Subject.NOTIFICATION_MESSAGE;

                return new MessageDto(email, subject, body);
            }
        }

    }

}
