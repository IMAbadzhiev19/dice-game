using DiceService.Application.Interfaces;
using DiceService.Application.Enums;
using DiceService.Application.Models;
using DiceService.Domain.Entities;
using DiceService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DiceService.Infrastructure.Repositories;

public class DiceRollRepository(
    ApplicationDbContext applicationDbContext
    ) : IDiceRollRepository
{
    private readonly ApplicationDbContext _dbContext = applicationDbContext;
    
    public async Task AddAsync(DiceRoll diceRoll, CancellationToken cancellationToken = default)
    {
        await this._dbContext.DiceRolls.AddAsync(diceRoll, cancellationToken);
        await this._dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await this._dbContext.DiceRolls.CountAsync(cancellationToken);
    }

    public async Task<int> CountByFilterAsync(
        DiceRollFilter filter,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter(this._dbContext.DiceRolls.AsNoTracking(), filter);

        return await query.CountAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<DiceRoll>> GetAllAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        return await this._dbContext.DiceRolls
            .AsNoTracking()
            .OrderByDescending(x => x.RolledAtUtc)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<DiceRoll>> GetByFilterAsync(
        DiceRollFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        IQueryable<DiceRoll> query = ApplyFilter(this._dbContext.DiceRolls.AsNoTracking(), filter);
        query = ApplySorting(query, filter);

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    private static IQueryable<DiceRoll> ApplyFilter(IQueryable<DiceRoll> query, DiceRollFilter filter)
    {
        if (filter.Year.HasValue)
        {
            query = query.Where(x => x.RolledAtUtc.Year == filter.Year.Value);
        }

        if (filter.Month.HasValue)
        {
            query = query.Where(x => x.RolledAtUtc.Month == filter.Month.Value);
        }

        if (filter.Day.HasValue)
        {
            query = query.Where(x => x.RolledAtUtc.Day == filter.Day.Value);
        }

        return query;
    }

    private static IQueryable<DiceRoll> ApplySorting(IQueryable<DiceRoll> query, DiceRollFilter filter)
    {
        if (filter.SumSortDirection.HasValue)
        {
            query = filter.SumSortDirection == SortDirection.Desc
                ? query.OrderByDescending(x => x.FirstDice + x.SecondDice)
                : query.OrderBy(x => x.FirstDice + x.SecondDice);
        }

        if (filter.DateSortDirection.HasValue)
        {
            if (query is IOrderedQueryable<DiceRoll> orderedQuery)
            {
                query = filter.DateSortDirection == SortDirection.Desc
                    ? orderedQuery.ThenByDescending(x => x.RolledAtUtc)
                    : orderedQuery.ThenBy(x => x.RolledAtUtc);
            }
            else
            {
                query = filter.DateSortDirection == SortDirection.Desc
                    ? query.OrderByDescending(x => x.RolledAtUtc)
                    : query.OrderBy(x => x.RolledAtUtc);
            }
        }
        else if (query is not IOrderedQueryable<DiceRoll>)
        {
            query = query.OrderByDescending(x => x.RolledAtUtc);
        }

        return query;
    }
}
