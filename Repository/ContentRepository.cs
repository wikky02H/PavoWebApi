using PavoWeb.Models;
using PavoWeb.Database;
using Dapper;
using System.Text.Json;
using System.Data;

namespace PavoWeb.Repository
{
    public class ContentRepository
    {
        private readonly DBConnection _context;

        public ContentRepository(DBConnection context)
        {
            _context = context;
        }
        public async Task<Dictionary<string, List<Content>>> GetAllContent()
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    // Fetching content list with ContentDetails as JSON string
                    //  var contentList = await connection.QueryAsync<dynamic>(
                    //      @"SELECT 
                    //  c.Id AS ContentId, 
                    //  c.Page AS ContentPage, 
                    //  c.Title AS ContentTitle, 
                    //  c.Description AS ContentDescription, 
                    //  c.ImagePath AS ContentImagePath, 
                    //  c.Feedback AS ContentFeedback, 
                    //  c.FeedbackUserId AS ContentFeedbackUserId, 
                    //  c.OrderNo AS ContentOrderNo,
                    //  (SELECT cd.Id AS ContentDetailId, cd.OrderNo AS ContentDetailOrderNo, cd.Name AS ContentDetailName 
                    //   FROM ContentDetails cd 
                    //   WHERE cd.ContentId = c.Id AND cd.IsDeleted = 0 
                    //   ORDER BY cd.OrderNo ASC FOR JSON PATH) AS ContentDetailsList 
                    //FROM Contents c 
                    //WHERE c.IsDeleted = 0
                    //AND c.Page != 'feedback'"

                    //  );
                    var contentList = await connection.QueryAsync<dynamic>("[dbo].[GetAllContent]", commandType: CommandType.StoredProcedure);

                    if (contentList == null || !contentList.Any())
                    {
                        return new Dictionary<string, List<Content>>();
                    }

                    var mappedContents = contentList.Select(data => new Content
                    {
                        ContentId = data.ContentId,
                        ContentPage = data.ContentPage,
                        ContentTitle = data.ContentTitle,
                        ContentDescription = data.ContentDescription,
                        ContentImagePath = data.ContentImagePath,
                        ContentFeedback = data.ContentFeedback,
                        ContentFeedbackUserId = data.ContentFeedbackUserId,
                        ContentOrderNo = data.ContentOrderNo,
                        ContentDetailsList = data.ContentDetailsList != null
                            ? JsonSerializer.Deserialize<List<ContentDetail>>(data.ContentDetailsList)
                            : new List<ContentDetail>()
                    }).ToList();

                    var groupedContents = mappedContents
                        .GroupBy(content => content.ContentPage ?? "Unknown")
                        .ToDictionary(group => group.Key, group => group.ToList());

                    return groupedContents;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while retrieving contents. Please try again later.");
            }
        }

        public async Task<List<Content>> GetFeedbackContent()
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var feedbackList = await connection.QueryAsync<dynamic>(
                       "[dbo].[GetFeedbackList]", commandType: CommandType.StoredProcedure
                    );
                //    var feedbackList = await connection.QueryAsync<dynamic>(
                //        @"SELECT
                //    c.id AS ContentId,
                //    c.Feedback AS Feedback,
                //    c.OrderNo AS OrderNo,
                //    c.ImagePath AS contentImagePath,
                //    u.Id AS UserId,
                //    (u.Name + ' ' + u.Designation) AS UserName
                //FROM 
                //    Contents c
                //JOIN 
                //    Users u ON u.Id = c.FeedbackUserId"
                //    );

                    // Check if feedbackList is null or empty
                    if (feedbackList == null || !feedbackList.Any())
                    {
                        return new List<Content>();
                    }

                    // Map feedbackList to Content objects
                    var mappedContents = feedbackList.Select(data => new Content
                    {
                        ContentId = data.ContentId,
                        Feedback = data.Feedback,
                        OrderNo = data.OrderNo,
                        UserId = data.UserId,
                        UserName = data.UserName,
                        ContentImagePath = data.contentImagePath
                    }).ToList();

                    // Log the mappedContents after mapping
                    Console.WriteLine("Logging mappedContents after mapping:");
                    foreach (var feedback in mappedContents)
                    {
                        Console.WriteLine($"mappedContents ContentId: {feedback.ContentId}, Feedback: {feedback.Feedback}, OrderNo: {feedback.OrderNo}, UserId: {feedback.UserId}, UserName: {feedback.UserName}");
                    }

                    return mappedContents;
                }
            }
            catch (Exception ex)
            {
                // Log exception details before rethrowing it (this is optional but helps during debugging)
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new Exception("An error occurred while retrieving feedback contents. Please try again later.", ex);
            }
        }

    }
}
