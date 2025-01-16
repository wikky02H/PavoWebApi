using PavoWeb.Models;
using PavoWeb.Database;
using Dapper;
using System.Data;
using PavoWeb.Handler;
using Serilog;

namespace PavoWeb.Repository
{
    public class ContentRepository
    {
        private readonly DBConnection _context;
        private readonly ContentHandler _contentHandler;

        public ContentRepository(DBConnection context, ContentHandler contentHandler)
        {
            _context = context;
            _contentHandler = contentHandler;
        }
        public async Task<List<Content>> GetAllContent()
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
 
                    var contentList = await connection.QueryAsync<dynamic>(
                        "[dbo].[GetAllContent]", commandType: CommandType.StoredProcedure
                    );

                    if (contentList == null || !contentList.Any())      
                        return new List<Content>();
                  
                    var processedContent = _contentHandler.ProcessContentList(contentList);
                    return processedContent;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred in GetAllContent method. Error: {ErrorMessage}", ex.Message);
                throw new Exception("An error occurred while retrieving content. Please try again later.", ex);
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
          
                    if (feedbackList == null || !feedbackList.Any())
                        return new List<Content>();
                 
                    var processedFeedback = _contentHandler.ProcessFeedbackList(feedbackList);
                    return processedFeedback;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred in GetFeedbackContent method. Error: {ErrorMessage}", ex.Message);
                throw new Exception("An error occurred while retrieving feedback contents. Please try again later.", ex);
            }
        }

    }
}
