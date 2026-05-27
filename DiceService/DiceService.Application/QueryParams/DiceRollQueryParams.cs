using DiceService.Application.Enums;

namespace DiceService.Application.QueryParams;

public class DiceRollQueryParams
{
    public int? Year { get; set; }

    public int? Month { get; set; }

    public int? Day { get; set; }

    public SortDirection? DateSortDirection { get; set; }

    public SortDirection? SumSortDirection { get; set; }
}