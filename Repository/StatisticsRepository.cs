using PavoWeb.Models;
using PavoWeb.Database;
using Dapper;
using System.Data;

namespace PavoWeb.Repository
{
    public class StatisticsRepository
    {
        private readonly DBConnection _context;

        public StatisticsRepository(DBConnection context)
        {
            _context = context;
        }
        public async Task<Dictionary<string, List<Statistics>>> GetAllStatistics()
        {
            using (var connection = _context.CreateConnection())
            {
                var statisticsList = await connection.QueryAsync<Statistics>(
                    "[dbo].[GetAllStatistics]", commandType: CommandType.StoredProcedure
                );
                var groupedStatistics = statisticsList
                   .GroupBy(s => s.Name)  
                   .ToDictionary(
                       g => g.Key,  
                       g => g.ToList()  
                   );
                return groupedStatistics;
            }
        }

    }
}
