using System.Data;
using PavoWeb.Models;
using PavoWeb.Database;
using Dapper;
using PavoWeb.Handler;
using Serilog;

namespace PavoWeb.Repository
{
    public class MenuRepository
    {
        private readonly DBConnection _context;
        private readonly MenuHandler _menuHandler;
        public MenuRepository(DBConnection context, MenuHandler menuHandler)
        {
            _context = context;
            _menuHandler = menuHandler;
        }
        public async Task<Dictionary<string, List<Menu>>> GetAllMenu()
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var menuList = await connection.QueryAsync<Menu>("[dbo].[GetMenuList]", commandType: CommandType.StoredProcedure);
                    var groupedMenus = _menuHandler.GroupMenusByType(menuList);
                    return groupedMenus;
                }
            }
            catch (Exception ex) {
                Log.Error("Exception occurred in GetFeedbackContent method. Error: {ErrorMessage}", ex.Message);
                throw new Exception("An error occurred while retrieving feedback contents. Please try again later.", ex);
            }
        }
    }
}
