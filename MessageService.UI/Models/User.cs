namespace MessageService.UI.Models
{
    public class User :IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string Mail { get; set; }
    }
}
