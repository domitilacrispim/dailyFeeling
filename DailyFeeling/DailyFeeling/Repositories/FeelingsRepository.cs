using DailyFeeling.Database;
using DailyFeeling.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyFeeling.Repositories;

public class FeelingsRepository : IFeelingsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FeelingsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Feeling?> GetTodaysFeelingByUserIdAsync(int userId)
    {
        return await _dbContext.Feelings
            .Where(f => f.UserId == userId && f.Date == DateTime.Today)
            .FirstOrDefaultAsync();
    }

    public async Task<Feeling?> CreateFeelingAsync(int userId, Mood mood)
    {
        if (await UserHasFeelingTodayAsync(userId))
            return null; // Indica que o sentimento já foi registrado
        
        var feeling = new Feeling
        {
            UserId = userId,
            Mood = mood,
            Date = DateTime.Today
        };

        _dbContext.Feelings.Add(feeling);
        await _dbContext.SaveChangesAsync();

        return feeling;
    }

    public async Task<Feeling?> DeleteFeelingAsync(int userId)
    {
        var feeling = await GetTodaysFeelingByUserIdAsync(userId);
        if (feeling == null)
            return null;
        _dbContext.Feelings.Remove(feeling);
        await _dbContext.SaveChangesAsync();
        return feeling;
    }

    public async Task<List<Feeling>> GetFeelingHistoryAsync(int userId)
    {
        var list = await _dbContext.Feelings
            .Where(f => f.UserId == userId)
            .OrderBy(f => f.Date)
            .ToListAsync();
        return list;
    }

    public async Task<Feeling?> GetFeelingAggregationAsync(int userId)
    {
        throw new NotImplementedException();
    }
    
    private async Task<bool> UserHasFeelingTodayAsync(int userId)
    {
        return await _dbContext.Feelings
            .AnyAsync(f => f.UserId == userId && f.Date == DateTime.Today);
    }

}