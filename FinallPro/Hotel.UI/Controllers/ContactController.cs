using Hotel.Business.Services.Interfaces;
using Hotel.Core.Entities;
using Hotel.DataAccess;
using Hotel.UI.ViewModels.ContactVm;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.UI.Controllers;

public class ContactController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webEnv;
    private readonly IFileService _fileservice;
    public ContactController(AppDbContext context,
                            IWebHostEnvironment webEnv,
                            IFileService fileservice)
    {
        _context = context;
        _webEnv = webEnv;
        _fileservice = fileservice;
    }
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContactCreateViewModel contact)
    {
        if (!ModelState.IsValid) return View(contact);
        try
        {
            Contact contacts = new()
            {
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone,
                Message = contact.Message,

            };
            await _context.Contacts.AddAsync(contacts);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(contact);
        }
        return RedirectToAction(nameof(Index));
    }
}
