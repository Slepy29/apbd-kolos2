namespace apbd_kolos2.DTOs;

public class GetCharacterDTO
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public int currentWeight { get; set; }
    public int maxWeight { get; set; }
    public List<CharacterItemDTO> backpackItems { get; set; }
    public List<TitleDTO> titles { get; set; }
}