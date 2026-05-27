using DiceService.Application.DTOs;
using DiceService.Application.Interfaces;
using DiceService.Application.QueryParams;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RollDiceAsync()
    {
        var result = await this._diceRollService.RollDiceAsync();
        
        return this.Ok(result);
    }
    
    [HttpGet("rolls")]

    public async Task<ActionResult<IEnumerable<DiceRollVM>>> GetDiceRolls(
        [FromQuery] DiceRollQueryParams queryParams)
    {
        var result = await _diceRollService.GetDiceRollsAsync(queryParams);
        
        return Ok(result);
    }
}