using DailyFeeling.Models;
using DailyFeeling.Services;
using Microsoft.AspNetCore.Mvc;

namespace DailyFeeling.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeelingsController : ControllerBase
{
    private readonly IFeelingsService _feelingsService;

    public FeelingsController(IFeelingsService feelingsService)
    {
        _feelingsService = feelingsService;
    }

    [HttpGet("GetTodaysFeeling/{username}")]
    public async Task<IActionResult> GetTodaysFeeling(string username)
    {
        var response = await _feelingsService.GetTodaysFeelingAsync(username);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("CreateFeeling/{username}/{mood}")]
    public async Task<IActionResult> CreateFeeling(string username, Mood mood)
    {
        var response = await _feelingsService.CreateFeelingAsync(username, mood);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("DeleteFeeling/{username}")]
    public async Task<IActionResult> DeleteFeeling(string username)
    {
        var response = await _feelingsService.DeleteFeelingAsync(username);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetFeelingHistory/{username}")]
    public async Task<IActionResult> GetFeelingHistory(string username)
    {
        var response = await _feelingsService.GetFeelingHistoryAsync(username);
        return StatusCode(response.StatusCode, response);
    }

    // [HttpGet("GetFeelingAggregation/{username}")]
    // public async Task<IActionResult> GetFeelingAggregation(string username)
    // {
    //     var response = await _feelingsService.GetFeelingAggregationAsync(username);
    //     return StatusCode(response.StatusCode, response);
    // }
}