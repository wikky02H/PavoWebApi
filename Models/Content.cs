namespace PavoWeb.Models
{
    public class Content
    {   
        public int ContentId { get; set; }
        public string? ContentPage { get; set; }
        public string? ContentTitle { get; set; }
        public string? ContentDescription { get; set; }
        public string? ContentImagePath { get; set; }
        public string? ContentFeedback { get; set; }
        public string? ContentFeedbackUserId { get; set; }
        public int? ContentOrderNo { get; set; }
        public string? Feedback { get; set; }
        public int? OrderNo { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public List<ContentDetail>? ContentDetailsList { get; set; } = new List<ContentDetail>();
    }
}
