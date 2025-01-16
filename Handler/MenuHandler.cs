using PavoWeb.Models;

namespace PavoWeb.Handler
{
    public class MenuHandler
    {
        public Dictionary<string, List<Menu>> GroupMenusByType(IEnumerable<Menu> menuList)
        {
            var enumerable = menuList.Where(m => m.Type != null);
            var groupedMenus = enumerable
                .GroupBy(m => m.Type)
                .ToDictionary<IGrouping<string, Menu>, string, List<Menu>>(g => g.Key, g => g.ToList());

            return groupedMenus;
        }
    }
}
