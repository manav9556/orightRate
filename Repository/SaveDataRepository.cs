using OrightApi.Db;
using OrightApi.Model;
using Dapper;

namespace OrightApi.Repository
{
    public class SaveDataRepository
    {
        private readonly DbContext _context;
        public SaveDataRepository(DbContext dbContext) { 
        _context = dbContext;
        }

        public async Task InsertRateChartsAsync(IEnumerable<Rate> rateCharts)
        {
            try
            {
                using (var connection = _context.GetConnection())
                {
                    await connection.ExecuteAsync(
                        "INSERT INTO RateCharts (ClientId, SNF, FAT, Price) " +
                        "VALUES (@ClientId, @SNF, @FAT, @Price)",
                        rateCharts);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in InsertRateChartsAsync: {ex.Message}");
                throw;
            }
        }

    }
}
