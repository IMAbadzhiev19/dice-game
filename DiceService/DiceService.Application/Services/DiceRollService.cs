using DiceService.Application.DTOs;
using DiceService.Application.Interfaces;
using DiceService.Application.Models;
using DiceService.Application.Requests;
using DiceService.Domain.Entities;

namespace DiceService.Application.Services;

public class DiceRollService(
    IDiceRollRepository diceRollRepository
    ) : IDiceRollService
{
    private readonly IDiceRollRepository _diceRollRepository = diceRollRepository;

    public async Task<DiceRollVM> RollDiceAsync(CancellationToken cancellationToken = default)
    {
        var diceRoll = new DiceRoll
        {
            Id = Guid.NewGuid(),
            FirstDice = Random.Shared.Next(1, 7),
            SecondDice = Random.Shared.Next(1, 7),
            RolledAtUtc = DateTime.UtcNow,
        };

        await this._diceRollRepository.AddAsync(diceRoll, cancellationToken);

        return new DiceRollVM
        {
            Id = diceRoll.Id,
            FirstDice = diceRoll.FirstDice,
            SecondDice = diceRoll.SecondDice,
            RolledAtUtc = diceRoll.RolledAtUtc,
            Sum = diceRoll.Sum
        };
    }

    public async Task<PagedResult<DiceRollVM>> GetDiceRollsAsync(
        GetDiceRollsRequest request,
        CancellationToken cancellationToken = default)
    {
        var hasFiltersOrSorting =
            request.Year.HasValue ||
            request.Month.HasValue ||
            request.Day.HasValue ||
            request.DateSortDirection.HasValue ||
            request.SumSortDirection.HasValue;

        int totalCount;
        IReadOnlyList<DiceRoll> diceRolls;

        if (!hasFiltersOrSorting)
        {
            totalCount = await this._diceRollRepository.CountAsync(cancellationToken);
            diceRolls = await this._diceRollRepository.GetAllAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);
        }
        else
        {
            var filter = new DiceRollFilter
            {
                Year = request.Year,
                Month = request.Month,
                Day = request.Day,
                DateSortDirection = request.DateSortDirection,
                SumSortDirection = request.SumSortDirection
            };

            totalCount = await this._diceRollRepository.CountByFilterAsync(filter, cancellationToken);
            diceRolls = await this._diceRollRepository.GetByFilterAsync(
                filter,
                request.PageNumber,
                request.PageSize,
                cancellationToken);
        }

        return new PagedResult<DiceRollVM>
        {
            Items = diceRolls
                .Select(x => new DiceRollVM
                {
                    Id = x.Id,
                    FirstDice = x.FirstDice,
                    SecondDice = x.SecondDice,
                    Sum = x.Sum,
                    RolledAtUtc = x.RolledAtUtc,
                })
                .ToList(),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}
