using PavoWeb.Models;
using PavoWeb.Database;
using Dapper;
using System.Data;
using PavoWeb.Handler;
using Serilog;

namespace PavoWeb.Repository
{
    public class StatisticsRepository
    {
        private readonly DBConnection _context;
        private readonly StatisticsHandler _statisticsHandler;

        public StatisticsRepository(DBConnection context, StatisticsHandler statisticsHandler)
        {
            _context = context;
            _statisticsHandler = statisticsHandler;
        }
        public async Task<Dictionary<string, List<Statistics>>> GetAllStatistics()
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var statisticsList = await connection.QueryAsync<Statistics>(
                        "[dbo].[GetAllStatistics]", commandType: CommandType.StoredProcedure
                    );
                    if (statisticsList == null || !statisticsList.Any())
                        return new Dictionary<string, List<Statistics>>();

                    var groupedStatistics = _statisticsHandler.GroupStatisticsByName(statisticsList);
                    return groupedStatistics;
                }
            }
            catch (Exception ex) {
                Log.Error("Exception occurred in GetAllStatistics method. Error: {ErrorMessage}", ex.Message);
                throw new Exception("An error occurred while retrieving feedback contents. Please try again later.", ex);
            }   
        }
    }
}
