namespace apbd_kolos2.Models;

public class Characters
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CurrentWei { get; set; }
    public int MaxWeight { get; set; }
    public ICollection<Backpacks> BackpacksCollection { get; set; } = new HashSet<Backpacks>();
    public ICollection<Character_titles> CharacterTitlesCollection { get; set; } = new HashSet<Character_titles>();
    
}