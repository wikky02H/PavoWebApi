using PavoWeb.Models;

namespace PavoWeb.Handler
{
    public class SubscriptionHandler
    {
        public Dictionary<string, List<Subscription>> ProcessAndGroupSubscriptions(IEnumerable<dynamic> result)
        {
            var groupedData = result
                .GroupBy(data => new
                {
                    data.SubscriptionId,
                    data.PlanName,
                    data.PlanDescription,
                    data.PlanPrice,
                    data.BillingCycle
                })
                .Select(group => new Subscription
                {
                    Id = group.Key.SubscriptionId,
                    PlanName = group.Key.PlanName,
                    PlanDescription = group.Key.PlanDescription,
                    PlanPrice = group.Key.PlanPrice,
                    BillingCycle = group.Key.BillingCycle,
                    SubscriptionDetails = group
                        .Where(detail => detail.DetailId != null)
                        .Select(detail => new SubscriptionDetail
                        {
                            DetailId = detail.DetailId,
                            DetailDescription = detail.DetailDescription,
                            DetailOrderNo = detail.DetailOrderNo
                        })
                        .OrderBy(detail => detail.DetailOrderNo)
                        .ToList()
                })
                .ToList();

            var groupedSubscriptions = groupedData
                .GroupBy(sub => sub.PlanName ?? "Unknown")
                .ToDictionary(group => group.Key, group => group.ToList());

            return groupedSubscriptions;
        }
    }
}
