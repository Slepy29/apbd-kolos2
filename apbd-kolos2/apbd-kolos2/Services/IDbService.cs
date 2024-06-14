using apbd_kolos2.Models;

namespace apbd_kolos2.Services;

public interface IDbService
{
    Task<Characters> GetCharacterById(int characterId);
    Task<bool> DoesCharacterExist(int characterId);
    Task<bool> DoesItemExist(int itemID);
    Task<int> GetCharacterSpace(int characterId);
    Task<int> GetItemWeight(int itemID);
    Task<ICollection<Backpacks>> GetBackpack(int characterId);
    Task AddItems(int[] itemIds, int characterId);
    Task UpdateBackpack(int characterId, int itemId);
    Task UpdateWeight(int characterId, int weight);
}