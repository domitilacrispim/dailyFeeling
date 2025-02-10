using DailyFeeling.Models;

namespace DailyFeeling.Repositories;

public interface IFeelingsRepository
{
    Task<Feeling?> GetTodaysFeelingByUserIdAsync(int userId);
    Task<Feeling?> CreateFeelingAsync(int userId, Mood mood);
    Task<Feeling?> DeleteFeelingAsync(int userId);
    Task<List<Feeling>> GetFeelingHistoryAsync(int userId);
    Task<Feeling?> GetFeelingAggregationAsync(int userId);
}