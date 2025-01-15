namespace PavoWeb.Models
{
    public class Menu
    {
        public int Id { get; set; }          
        public string Name { get; set; }     
        public string Type { get; set; }    
        public int? ParentId { get; set; }   
        public int OrderNo { get; set; } 
        public int IsDeleted { get; set; }
    }
}
