using DiceService.Application.Enums;

namespace DiceService.Application.Requests;

public class GetDiceRollsRequest
{
    public int? Year { get; init; }

    public int? Month { get; init; }

    public int? Day { get; init; }

    public SortDirection? DateSortDirection { get; init; }

    public SortDirection? SumSortDirection { get; init; }

    public int PageNumber { get; init; }

    public int PageSize { get; init; }
}
