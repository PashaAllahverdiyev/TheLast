namespace Hotel.Core.Entities;

public class Size : BaseEntity
{
    public string SizeName { get; set; }
    public ICollection<RoomSize> Rooms { get; set;}
}
