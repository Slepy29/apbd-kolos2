namespace apbd_kolos2.Models;

public class Character_titles
{
    public int CharacterId { get; set; }
    public int TitleId { get; set; }
    public DateTime AcquireAt { get; set; }
    public Characters Character { get; set; }
    public Titles Title { get; set; }
}