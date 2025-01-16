using PavoWeb.Models;
using PavoWeb.Database;
using Dapper;
using System.Data;
using PavoWeb.Handler;
using Serilog;

namespace PavoWeb.Repository
{
    public class SubscriptionRepository
    {
        private readonly DBConnection _context;
        private readonly SubscriptionHandler _subscriptionHandler;
        public SubscriptionRepository(DBConnection context, SubscriptionHandler subscriptionHandler)
        {
            _context = context;
            _subscriptionHandler = subscriptionHandler;
        }
        public async Task<Dictionary<string, List<Subscription>>> GetAllSubscriptionDetails()
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<dynamic>(
                        "[dbo].[GetSubscriptionList]", commandType: CommandType.StoredProcedure);

                    if (result == null || !result.Any())
                        return new Dictionary<string, List<Subscription>>();
                    
                    var groupedSubscriptions = _subscriptionHandler.ProcessAndGroupSubscriptions(result);
                    return groupedSubscriptions;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred in GetAllSubscriptionDetails method. Error: {ErrorMessage}", ex.Message);
                throw new Exception("An error occurred while retrieving subscription details. Please try again later.", ex);
            }
        }
    }
}
