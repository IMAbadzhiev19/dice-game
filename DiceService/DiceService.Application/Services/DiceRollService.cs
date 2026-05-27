using DiceService.Application.DTOs;
using DiceService.Application.Enums;
using DiceService.Application.Interfaces;
using DiceService.Application.QueryParams;
using DiceService.Domain.Entities;

namespace DiceService.Application.Services;

public class DiceRollService(
    IDiceRollRepository diceRollRepository
    ) : IDiceRollService
{
    private readonly IDiceRollRepository _diceRollRepository = diceRollRepository;

    public async Task<DiceRollVM> RollDiceAsync()
    {
        var diceRoll = new DiceRoll
        {
            Id = Guid.NewGuid(),
            FirstDice = Random.Shared.Next(1, 7),
            SecondDice = Random.Shared.Next(1, 7),
            RolledAtUtc = DateTime.UtcNow,
        };

        await this._diceRollRepository.AddAsync(diceRoll);

        return new DiceRollVM
        {
            Id = diceRoll.Id,
            FirstDice = diceRoll.FirstDice,
            SecondDice = diceRoll.SecondDice,
            RolledAtUtc = diceRoll.RolledAtUtc,
            Sum = diceRoll.Sum
        };
    }

    public Task<IEnumerable<DiceRollVM>> GetDiceRollsAsync(DiceRollQueryParams queryParams)
    {
        var query = this._diceRollRepository.GetAll();

        // Filtering
        if (queryParams.Year.HasValue)
        {
            query = query.Where(x => x.RolledAtUtc.Year == queryParams.Year);
        }

        if (queryParams.Month.HasValue)
        {
            query = query.Where(x => x.RolledAtUtc.Month == queryParams.Month);
        }

        if (queryParams.Day.HasValue)
        {
            query = query.Where(x => x.RolledAtUtc.Day == queryParams.Day);
        }
        
        // Sorting
        if (queryParams.DateSortDirection.HasValue)
        {
            if (query is IOrderedQueryable<DiceRoll> orderedQuery)
            {
                query = queryParams.DateSortDirection == SortDirection.Desc
                    ? orderedQuery.ThenByDescending(x => x.RolledAtUtc)
                    : orderedQuery.ThenBy(x => x.RolledAtUtc);
            }
            else
            {
                query = queryParams.DateSortDirection == SortDirection.Desc
                    ? query.OrderByDescending(x => x.RolledAtUtc)
                    : query.OrderBy(x => x.RolledAtUtc);
            }
        }

        var diceRolls = query
            .Select(x => new DiceRollVM
            {
                Id = x.Id,
                FirstDice = x.FirstDice,
                SecondDice = x.SecondDice,
                Sum = x.Sum,
                RolledAtUtc = x.RolledAtUtc,
            }).ToList();

        return Task.FromResult<IEnumerable<DiceRollVM>>(diceRolls);
    }
}