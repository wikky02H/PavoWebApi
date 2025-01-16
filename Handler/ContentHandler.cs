using PavoWeb.Models;

namespace PavoWeb.Handler
{
    public class ContentHandler
    {
        public List<Content> ProcessContentList(IEnumerable<dynamic> contentList)
        {
            var contentDictionary = new Dictionary<int, Content>();

            foreach (var item in contentList)
            {
                int contentId = item.ContentId;

                if (!contentDictionary.ContainsKey(contentId))
                {
                    contentDictionary[contentId] = new Content
                    {
                        ContentId = contentId,
                        ContentPage = item.ContentPage,
                        ContentTitle = item.ContentTitle,
                        ContentDescription = item.ContentDescription,
                        ContentImagePath = item.ContentImagePath,
                        ContentFeedback = item.ContentFeedback,
                        ContentFeedbackUserId = item.ContentFeedbackUserId,
                        ContentOrderNo = item.ContentOrderNo,
                        ContentDetailsList = new List<ContentDetail>()
                    };
                }
                if(item.ContentDetailId != null)
                {
                    var contentDetail = new ContentDetail
                    {
                        ContentDetailId = item.ContentDetailId ?? 0,
                        ContentDetailName = item.ContentDetailName ?? "default",
                        ContentDetailOrderNo = item.ContentDetailOrderNo ?? 0
                    };

                    contentDictionary[contentId].ContentDetailsList.Add(contentDetail);
                }
            }

            return contentDictionary.Values.ToList();
        }

        public List<Content> ProcessFeedbackList(IEnumerable<dynamic> feedbackList)
        {
            return feedbackList.Select(data => new Content
            {
                ContentId = data.ContentId,
                Feedback = data.Feedback,
                OrderNo = data.OrderNo,
                UserId = data.UserId,
                UserName = data.UserName,
                ContentImagePath = data.contentImagePath
            }).ToList();
        }
    }
}
