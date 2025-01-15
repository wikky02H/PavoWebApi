using PavoWeb.Models;
using PavoWeb.Database;
using Dapper;
using System.Text.Json;
using System.Data;

namespace PavoWeb.Repository
{
    public class SubscriptionRepository
    {
        private readonly DBConnection _context;

        public SubscriptionRepository(DBConnection context)
        {
            _context = context;
        }

        // This method returns a dictionary of Subscription objects, grouped by PlanName
        public async Task<Dictionary<string, List<Subscription>>> GetAllSubscriptionDetails()
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    // Fetching subscription details with SubscriptionDetails as JSON string
                    var subscriptionList = await connection.QueryAsync<dynamic>(
                        "[dbo].[GetSubscriptionList]", commandType: CommandType.StoredProcedure
                    );
                    //var subscriptionList = await connection.QueryAsync<dynamic>(
                    //    @"SELECT
                    //        s.Id AS SubscriptionId,
                    //        s.PlanName,
                    //        s.PlanDescription,
                    //        s.BillingCycle,
                    //        s.Price AS PlanPrice,
                    //        (
                    //            SELECT 
                    //                Sd.Id AS DetailId,
                    //                Sd.Description AS DetailDescription, 
                    //                Sd.OrderNo AS DetailOrderNo  
                    //            FROM 
                    //                SubscriptionDetails Sd
                    //            WHERE 
                    //                Sd.SubscriptionId = s.Id
                    //                AND Sd.IsDeleted = 0
                    //            ORDER BY 
                    //                Sd.OrderNo ASC
                    //            FOR JSON PATH
                    //        ) AS SubscriptionDetails
                    //    FROM
                    //        Subscription s
                    //    WHERE 
                    //        s.IsDeleted = 0"
                    //);

                    if (subscriptionList == null || !subscriptionList.Any())
                    {
                        return new Dictionary<string, List<Subscription>>();
                    }

                    var mappedSubscriptions = subscriptionList.Select(data => new Subscription
                    {
                        Id = data.SubscriptionId,
                        PlanName = data.PlanName,
                        PlanDescription = data.PlanDescription,
                        PlanPrice = data.PlanPrice,
                        BillingCycle = data.BillingCycle,
                        SubscriptionDetails = data.SubscriptionDetails != null
                            ? JsonSerializer.Deserialize<List<SubscriptionDetail>>(data.SubscriptionDetails)
                            : new List<SubscriptionDetail>()
                    }).ToList();

                    // Grouping the subscriptions by PlanName
                    var groupedSubscriptions = mappedSubscriptions
                        .GroupBy(subscription => subscription.PlanName ?? "Unknown")
                        .ToDictionary(group => group.Key, group => group.ToList());

                    return groupedSubscriptions;
                }
            }
            catch (Exception ex)
            {
                // Log exception details if necessary
                throw new Exception("An error occurred while retrieving subscription details. Please try again later.", ex);
            }
        }
    }
}
