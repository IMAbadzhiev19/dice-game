using DiceService.Application.DTOs;
using DiceService.Application.Interfaces;
using DiceService.Application.QueryParams;
using DiceService.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DiceService.Presentation.Controllers;

[ApiController]
[Route("api/dice")]
public class DiceController(
    IDiceRollService diceRollService
    ) : ControllerBase
{
    private readonly IDiceRollService _diceRollService = diceRollService;

    [HttpPost("roll")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<DiceRollVM>> RollDiceAsync(CancellationToken cancellationToken)
    {
        var result = await this._diceRollService.RollDiceAsync(cancellationToken);

        return this.StatusCode(StatusCodes.Status201Created, result);
    }
    
    [HttpGet("rolls")]
    public async Task<ActionResult<PagedResult<DiceRollVM>>> GetDiceRolls(
        [FromQuery] DiceRollQueryParams queryParams,
        CancellationToken cancellationToken)
    {
        var request = new GetDiceRollsRequest
        {
            Year = queryParams.Year,
            Month = queryParams.Month,
            Day = queryParams.Day,
            DateSortDirection = queryParams.DateSortDirection,
            SumSortDirection = queryParams.SumSortDirection,
            PageNumber = queryParams.PageNumber,
            PageSize = queryParams.PageSize
        };

        var result = await _diceRollService.GetDiceRollsAsync(request, cancellationToken);
        
        return Ok(result);
    }
}
