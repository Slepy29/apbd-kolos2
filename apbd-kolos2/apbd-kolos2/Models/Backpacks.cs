namespace apbd_kolos2.Models;

public class Backpacks
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }
    public Items Item { get; set; }
    public Characters Character { get; set; }
}