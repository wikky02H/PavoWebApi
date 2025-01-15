using System.Data;
using PavoWeb.Models;
using PavoWeb.Database;
using Dapper;
using System.Data;

namespace PavoWeb.Repository
{
    public class MenuRepository
    {

        private readonly DBConnection _context;
        public MenuRepository(DBConnection context)
        {
            _context = context;
        }
        public async Task<Dictionary<string, List<Menu>>> GetAllMenu()
        {
            using (var connection = _context.CreateConnection())
            {
                //var menuList = await connection.QueryAsync<Menu>(
                //    "SELECT Id, Name, Type, ParentId, OrderNo, IsDeleted FROM Menus WHERE IsDeleted = 0"
                //);
                var menuList = await connection.QueryAsync<Menu>("[dbo].[GetMenuList]", commandType: CommandType.StoredProcedure);

                IEnumerable<Menu> enumerable = menuList
                                    .Where(m => m.Type != null);
                var groupedMenus = enumerable
                    .GroupBy(m => m.Type)
                    .ToDictionary<IGrouping<string, Menu>, string, List<Menu>>(g => g.Key, g => g.ToList());

                return groupedMenus;
            }
        }
    }
}
