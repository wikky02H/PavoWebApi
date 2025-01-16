using PavoWeb.Models;
using PavoWeb.Database;
using Dapper;
using System.Data;
using PavoWeb.Handler;
using Serilog;

namespace PavoWeb.Repository
{
    public class SocialIconRepository
    {
        private readonly DBConnection _context;
        private readonly SocialIconHandler _socialIconHandler;
        public SocialIconRepository(DBConnection context, SocialIconHandler socialIconHandler)
        {
            _context = context;
            _socialIconHandler = socialIconHandler;
        }
        public async Task<Dictionary<string, List<SocialIcon>>> GetAllSocialIcons()
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var iconList = await connection.QueryAsync<SocialIcon>(
                        "[dbo].[GetAllSocialIcons]", commandType: CommandType.StoredProcedure
                    );
                    if (iconList == null || !iconList.Any())
                        return new Dictionary<string, List<SocialIcon>>();
                    var groupedIcons = _socialIconHandler.GroupSocialIconsByType(iconList);
                    return groupedIcons;
                }
            }
            catch (Exception ex) {
                Log.Error("Exception occurred in GetAllSocialIcons method. Error: {ErrorMessage}", ex.Message);
                throw new Exception("An error occurred while retrieving feedback contents. Please try again later.", ex);
            }
        }
    }
}
