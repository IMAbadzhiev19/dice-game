using DiceService.Domain.Entities.Common;

namespace DiceService.Domain.Entities;

public class DiceRoll : BaseEntity
{
    public int FirstDice { get; set; }
    
    public int SecondDice { get; set; }
    
    public DateTime RolledAtUtc { get; set; }

    public int Sum => FirstDice + SecondDice;
}