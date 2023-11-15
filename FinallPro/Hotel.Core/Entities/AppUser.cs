using Microsoft.AspNetCore.Identity;

namespace Hotel.Core.Entities;

public class AppUser:IdentityUser
{
    public string FullName { get; set; } = null!;
    //public List<Reservation> Reservations { get; set; }
}
