namespace apbd_kolos2.Models;

public class Titles
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Character_titles> CharacterTitlesCollection { get; set; } = new HashSet<Character_titles>();
}