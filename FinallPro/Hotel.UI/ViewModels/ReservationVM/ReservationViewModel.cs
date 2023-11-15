using Hotel.Core.Entities;

namespace Hotel.UI.ViewModels.ReservationVM;

public class ReservationViewModel
{
    public DateTime CheckInTime { get; set; }
    public DateTime CheckOutTime { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
}
