namespace DiceService.Application.DTOs;

/// <summary>
/// The view model of the DiceRoll entity
/// </summary>
public class DiceRollVM
{
    public Guid Id { get; set; }
    
    public int FirstDice { get; set; }
    
    public int SecondDice { get; set; }
    
    public int Sum { get; set; }
    
    public DateTime RolledAtUtc { get; set; }
}