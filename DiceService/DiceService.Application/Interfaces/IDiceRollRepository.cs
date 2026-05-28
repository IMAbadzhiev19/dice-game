using DiceService.Application.Models;
using DiceService.Domain.Entities;

namespace DiceService.Application.Interfaces;

public interface IDiceRollRepository
{
    Task AddAsync(DiceRoll diceRoll, CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);

    Task<int> CountByFilterAsync(
        DiceRollFilter filter,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DiceRoll>> GetAllAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DiceRoll>> GetByFilterAsync(
        DiceRollFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);
}
