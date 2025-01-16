namespace PavoWeb.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string? PlanName { get; set; }
        public string? PlanDescription { get; set; }
        public Decimal? PlanPrice { get; set; }
        public string? BillingCycle { get; set; }
        public List<SubscriptionDetail>? SubscriptionDetails { get; set; } = new List<SubscriptionDetail>();
    }
}
