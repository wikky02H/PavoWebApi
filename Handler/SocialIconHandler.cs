using PavoWeb.Models;
using PavoWeb.Repository;

namespace PavoWeb.Handler
{
    public class SocialIconHandler
    {         
        public Dictionary<string, List<SocialIcon>> GroupSocialIconsByType(IEnumerable<SocialIcon> iconList)
        {
            var enumerable = iconList.Where(icon => icon.Type != null); 
            var groupedIcons = enumerable
                .GroupBy(icon => icon.Type) 
                .ToDictionary(g => g.Key, g => g.ToList()); 

            return groupedIcons;
        }   
    }
}
