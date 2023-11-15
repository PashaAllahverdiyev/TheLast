namespace Hotel.Core.Entities;

public class Review:BaseEntity
{
    public int Rating { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }
    public int CommentId { get; set; }
    public Comment Comment { get; set; }
}
