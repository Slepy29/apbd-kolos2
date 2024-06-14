using apbd_kolos2.Data;
using apbd_kolos2.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_kolos2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<Characters?> GetCharacterById(int characterId)
    {
        return await _context.characters
            .Include(e => e.BackpacksCollection)
            .ThenInclude(e => e.Item)
            .Include(e => e.CharacterTitlesCollection)
            .ThenInclude(e => e.Title)
            .FirstOrDefaultAsync(e => e.Id == characterId);
    }

    public async Task<bool> DoesCharacterExist(int characterId)
    {
        return await _context.characters.AnyAsync(e => e.Id == characterId);
    }

    public async Task<bool> DoesItemExist(int itemId)
    {
        return await _context.items.AnyAsync(e => e.Id == itemId);
    }

    public async Task<int> GetCharacterSpace(int characterId)
    {
        var character = await _context.characters
            .FirstAsync(e => e.Id == characterId);
        return character.MaxWeight - character.CurrentWei;
    }

    public async Task<int> GetItemWeight(int itemID)
    {
        return (await _context.items.FirstAsync(e => e.Id == itemID)).Weight;
    }

    public async Task<ICollection<Backpacks>> GetBackpack(int characterId)
    {
        return await _context.backpacks
            .Where(e => e.CharacterId == characterId)
            .ToListAsync();
    }

    public async Task AddItems(int[] itemIds, int characterId)
    {
        foreach (var id in itemIds)
        {
            if (await _context.backpacks.AnyAsync(e => e.ItemId == id && e.CharacterId == characterId))
            {
                await UpdateBackpack(characterId, id);
                await _context.SaveChangesAsync();  
            }
            else
            {
              await _context.AddAsync(new Backpacks()
              {
                  ItemId = id,
                  CharacterId = characterId,
                  Amount = 1
              });
              await _context.SaveChangesAsync();  
            }
        }
    }

    public async Task UpdateBackpack(int characterId, int itemId)
    {
        var backpack = await _context.backpacks
            .FirstAsync(e => e.CharacterId == characterId && e.ItemId == itemId);
        backpack.Amount ++;
        _context.Update(backpack);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCharacterCurrentWeight(int characterId, int weight)
    {
        var character = await _context.characters
            .FirstAsync(e => e.Id == characterId);
        character.CurrentWei += weight;
        _context.Update(character);
        await _context.SaveChangesAsync();
    }
}