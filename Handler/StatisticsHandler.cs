using PavoWeb.Models;

namespace PavoWeb.Handler
{
    public class StatisticsHandler
    {
        public Dictionary<string, List<Statistics>> GroupStatisticsByName(IEnumerable<Statistics> statisticsList)
        {
            var groupedStatistics = statisticsList
                .GroupBy(stat => stat.Name) 
                .ToDictionary(g => g.Key, g => g.ToList()); 

            return groupedStatistics;
        }   
    }
}
