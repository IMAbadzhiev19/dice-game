using DiceService.Application.DTOs;
using DiceService.Application.QueryParams;

namespace DiceService.Application.Interfaces;

public interface IDiceRollService
{
    Task<DiceRollVM> RollDiceAsync();
    
    Task<IEnumerable<DiceRollVM>> GetDiceRollsAsync(DiceRollQueryParams queryParams);
}