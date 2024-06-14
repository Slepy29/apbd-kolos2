using apbd_kolos2.DTOs;
using apbd_kolos2.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_kolos2.Controllers;

[Route("api/characters")]
[ApiController]
public class CharacterController : ControllerBase
{
    private readonly IDbService _dbService;
    public CharacterController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{characterID}/")]
    public async Task<IActionResult> GetCharacter(int characterID)
    {
        if (!await _dbService.DoesCharacterExist(characterID))
            return NotFound($"Character with given ID - {characterID} doesn't exist");
        
        var ch = await _dbService.GetCharacterById(characterID);
        
        return Ok(new GetCharacterDTO()
        {
            firstName = ch.FirstName,
            lastName = ch.LastName,
            currentWeight = ch.CurrentWei,
            maxWeight = ch.MaxWeight,
            backpackItems = ch.BackpacksCollection.Select(e => new CharacterItemDTO()
            {
                itemName = e.Item.Name,
                itemWeight = e.Item.Weight,
                amount = e.Amount
            }).ToList(),
            titles = ch.CharacterTitlesCollection.Select(e => new TitleDTO()
            {
                title = e.Title.Name,
                aquiredAt = e.AcquireAt
            }).ToList()
        });
    }
    
    [HttpPost("{characterID}/backpacks")]
    public async Task<IActionResult> PostItems(int characterID, int[] itemIds)
    {
        if (!await _dbService.DoesCharacterExist(characterID))
            return NotFound($"Character with given ID - {characterID} doesn't exist");

        int itemsWeightSum = 0;
        
        foreach (var itemId in itemIds)
        {
            if (!await _dbService.DoesItemExist(itemId))
                return NotFound($"Item with given ID - {itemId} doesn't exist");
            itemsWeightSum += await _dbService.GetItemWeight(itemId);
        }

        int avaliableSpace = await _dbService.GetCharacterSpace(characterID);

        if (itemsWeightSum > avaliableSpace)
            return BadRequest(
                $"Items are to heavy, items weight - {itemsWeightSum}, avaliableSpace - {avaliableSpace}");

        await _dbService.AddItems(itemIds, characterID);
        await _dbService.UpdateCharacterCurrentWeight(characterID, itemsWeightSum);

        var bList = await _dbService.GetBackpack(characterID);
        
        return Ok(bList.Select(e => new BackpackItemDTO()
        {
            amount = e.Amount,
            itemId = e.ItemId,
            characterId = e.CharacterId
        }));
    }
}