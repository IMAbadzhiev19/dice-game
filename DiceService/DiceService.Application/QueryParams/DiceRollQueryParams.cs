using System.ComponentModel.DataAnnotations;
using DiceService.Application.Enums;

namespace DiceService.Application.QueryParams;

public class DiceRollQueryParams : IValidatableObject
{
    [Range(1, 9999)]
    public int? Year { get; set; }

    [Range(1, 12)]
    public int? Month { get; set; }

    [Range(1, 31)]
    public int? Day { get; set; }

    public SortDirection? DateSortDirection { get; set; }

    public SortDirection? SumSortDirection { get; set; }

    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    [Range(1, 100)]
    public int PageSize { get; set; } = 10;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Month.HasValue && !Year.HasValue)
        {
            yield return new ValidationResult(
                "Month filter requires Year to be provided.",
                [nameof(Month), nameof(Year)]);
        }

        if (Day.HasValue && (!Year.HasValue || !Month.HasValue))
        {
            yield return new ValidationResult(
                "Day filter requires both Month and Year to be provided.",
                [nameof(Day), nameof(Month), nameof(Year)]);
        }

        if (Year.HasValue && Month.HasValue && Day.HasValue)
        {
            var maxDay = DateTime.DaysInMonth(Year.Value, Month.Value);
            if (Day.Value > maxDay)
            {
                yield return new ValidationResult(
                    $"Day must be between 1 and {maxDay} for the supplied month and year.",
                    [nameof(Day), nameof(Month), nameof(Year)]);
            }
        }
    }
}
