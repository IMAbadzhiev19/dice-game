using DiceService.Application.Enums;

namespace DiceService.Application.Models;

public class DiceRollFilter
{
    public int? Year { get; init; }

    public int? Month { get; init; }

    public int? Day { get; init; }

    public SortDirection? DateSortDirection { get; init; }

    public SortDirection? SumSortDirection { get; init; }
}
