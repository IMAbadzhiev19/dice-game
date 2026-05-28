using DiceService.Application.DTOs;
using DiceService.Application.Requests;

namespace DiceService.Application.Interfaces;

public interface IDiceRollService
{
    Task<DiceRollVM> RollDiceAsync(CancellationToken cancellationToken = default);
    
    Task<PagedResult<DiceRollVM>> GetDiceRollsAsync(
        GetDiceRollsRequest request,
        CancellationToken cancellationToken = default);
}
