using DiceService.Application.Interfaces;
using DiceService.Domain.Entities;
using DiceService.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;

namespace DiceService.Infrastructure.Repositories;

public class DiceRollRepository(
    ApplicationDbContext applicationDbContext
    ) : IDiceRollRepository
{
    private readonly ApplicationDbContext _dbContext = applicationDbContext;
    
    public async Task AddAsync(DiceRoll diceRoll)
    {
        await this._dbContext.DiceRolls.AddAsync(diceRoll);
        await this._dbContext.SaveChangesAsync();
    }

    public IQueryable<DiceRoll> GetAll()
    {
        return this._dbContext.DiceRolls.AsQueryable();
    }
}