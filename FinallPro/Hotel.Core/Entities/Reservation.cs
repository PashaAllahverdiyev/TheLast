namespace Hotel.Core.Entities;

public class Reservation:BaseEntity
{
    public string UserId { get; set; }
    public AppUser User { get; set; }
    public DateTime CheckInTime { get; set; }
    public DateTime CheckOutTime { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }
}
