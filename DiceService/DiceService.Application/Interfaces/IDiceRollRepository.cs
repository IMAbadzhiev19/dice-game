using DiceService.Domain.Entities;

namespace DiceService.Application.Interfaces;

public interface IDiceRollRepository
{
    Task AddAsync(DiceRoll diceRoll);
    
    IQueryable<DiceRoll> GetAll();
}