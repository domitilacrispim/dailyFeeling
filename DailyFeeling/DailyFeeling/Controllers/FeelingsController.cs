using DailyFeeling.Models;
using DailyFeeling.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyFeeling.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FeelingsController : ControllerBase
{
    private readonly IFeelingsService _feelingsService;

    public FeelingsController(IFeelingsService feelingsService)
    {
        _feelingsService = feelingsService;
    }

    [HttpGet("GetTodaysFeeling")]
    public async Task<IActionResult> GetTodaysFeeling()
    {
        var username = User.FindFirst("username")?.Value;
        if (string.IsNullOrEmpty(username))
            return StatusCode(ApiResponse<User?>.Unauthorized().StatusCode, ApiResponse<User?>.Unauthorized());
        var response = await _feelingsService.GetTodaysFeelingAsync(username);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("CreateFeeling")]
    public async Task<IActionResult> CreateFeeling([FromBody] Mood mood)
    {
        var username = User.FindFirst("username")?.Value;
        if (string.IsNullOrEmpty(username))
            return StatusCode(ApiResponse<User?>.Unauthorized().StatusCode, ApiResponse<User?>.Unauthorized());
        var response = await _feelingsService.CreateFeelingAsync(username, mood);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("DeleteFeeling")]
    public async Task<IActionResult> DeleteFeeling()
    {
        var username = User.FindFirst("username")?.Value;
        if (string.IsNullOrEmpty(username))
            return StatusCode(ApiResponse<User?>.Unauthorized().StatusCode, ApiResponse<User?>.Unauthorized());
        var response = await _feelingsService.DeleteFeelingAsync(username);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetFeelingHistory")]
    public async Task<IActionResult> GetFeelingHistory()
    {
        var username = User.FindFirst("username")?.Value;
        if (string.IsNullOrEmpty(username))
            return StatusCode(ApiResponse<User?>.Unauthorized().StatusCode, ApiResponse<User?>.Unauthorized());
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