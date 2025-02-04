using DailyFeeling.Models;
using DailyFeeling.Repositories;

namespace DailyFeeling.Services;

public class FeelingsService : IFeelingsService
{
    private readonly IFeelingsRepository _feelingsRepository;
    private readonly IUserRepository _userRepository;

    public FeelingsService(IFeelingsRepository feelingsRepository, IUserRepository userRepository)
    {
        _feelingsRepository = feelingsRepository;
        _userRepository = userRepository;
    }
    public async Task<ApiResponse<Feeling?>> GetTodaysFeelingAsync(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
            return ApiResponse<Feeling?>.NotFound($"User {username} not found");

        var feeling = await _feelingsRepository.GetTodaysFeelingByUserIdAsync(user.Id);
        if (feeling == null) 
            return ApiResponse<Feeling?>.NotFound($"User {username} has no feeling today");
        
        return ApiResponse<Feeling>.SuccessResponse(feeling, "Feeling retrieved successfully");
    }

    public async Task<ApiResponse<Feeling?>> CreateFeelingAsync(string username, Mood mood)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
            return ApiResponse<Feeling?>.NotFound($"User {username} not found");
        
        var feeling = await _feelingsRepository.CreateFeelingAsync(user.Id, mood);
        if (feeling == null)
            return ApiResponse<Feeling?>.ErrorResponse(409, "Feeling today already exists");
        return ApiResponse<Feeling>.SuccessResponse(feeling, "Created Feeling Successfully");
    }

    public async Task<ApiResponse<Feeling?>> DeleteFeelingAsync(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
            return ApiResponse<Feeling?>.NotFound($"User {username} not found");
        
        var feeling = await _feelingsRepository.DeleteFeelingAsync(user.Id);
        if (feeling == null)
            return ApiResponse<Feeling?>.NotFound($"User {username} has no feeling today");
        return ApiResponse<Feeling?>.SuccessResponse(feeling, "Deleted Feeling Successfully");
    }

    public async Task<ApiResponse<List<Feeling>>> GetFeelingHistoryAsync(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
            return ApiResponse<List<Feeling>>.NotFound($"User {username} not found");

        var feelings = await _feelingsRepository.GetFeelingHistoryAsync(user.Id);
        if (feelings.Count == 0)
            return ApiResponse<List<Feeling>>.NotFound($"User {username} has no feelings registered");

        return ApiResponse<List<Feeling>>.SuccessResponse(feelings, "Feeling history successfully retrieved");
    }

    // public async Task<ApiResponse<Feeling?>> GetFeelingAggregationAsync(string username)
    // {
    // }
}