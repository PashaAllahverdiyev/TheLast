using Hotel.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Hotel.Business.ViewModels.RoomDetailVM;

public class RoomDetailCreateViewModel
{
    public string Description { get; set; }
    public  decimal Price { get; set; }
    public bool Service { get; set; }
    public bool MiniBar { get; set; }
    public bool Tv { get; set; }
    public bool AirController { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }



}
