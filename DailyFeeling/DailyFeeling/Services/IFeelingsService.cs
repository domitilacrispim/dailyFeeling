using DailyFeeling.Models;

namespace DailyFeeling.Services;

public interface IFeelingsService
{
    Task<ApiResponse<Feeling?>> GetTodaysFeelingAsync(string username);
    Task<ApiResponse<Feeling?>> CreateFeelingAsync(string username, Mood mood);
    Task<ApiResponse<Feeling?>> DeleteFeelingAsync(string username);
    Task<ApiResponse<List<Feeling>>> GetFeelingHistoryAsync(string username);
    // Task<ApiResponse<Feeling?>> GetFeelingAggregationAsync(string username);
}