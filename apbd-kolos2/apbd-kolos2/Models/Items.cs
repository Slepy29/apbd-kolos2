namespace apbd_kolos2.Models;

public class Items
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Weight { get; set; }
    public ICollection<Backpacks> BackpacksCollection { get; set; } = new HashSet<Backpacks>();
}