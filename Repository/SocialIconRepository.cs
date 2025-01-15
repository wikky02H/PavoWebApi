using PavoWeb.Models;
using PavoWeb.Database;
using Dapper;
using System.Data;

namespace PavoWeb.Repository
{
    public class SocialIconRepository
    {
        private readonly DBConnection _context;

        public SocialIconRepository(DBConnection context)
        {
            _context = context;
        }
        public async Task<Dictionary<string, List<SocialIcon>>> GetAllSocialIcons()
        {
            using (var connection = _context.CreateConnection())
            {
                var iconList = await connection.QueryAsync<SocialIcon>(
                    "[dbo].[GetAllSocialIcons]", commandType: CommandType.StoredProcedure
                );
               // var iconList = await connection.QueryAsync<SocialIcon>(
               //    "SELECT Id, Name, Type, Icon, OrderNo FROM SocialIcons WHERE IsDeleted = 0"
               //);
                var groupedIcons = iconList
                    .Where(icon => icon.Type != null)
                    .GroupBy(icon => icon.Type!)
                    .ToDictionary(g => g.Key, g => g.ToList());

                return groupedIcons;
            }
        }
    }
}
