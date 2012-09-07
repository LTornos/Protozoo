namespace Protozoo.Core.Entities
{
    public class Entity 
    {
        public Entity() { }

        public Entity(string name, string account)
        {
            this.Name = name;
            this.Account = account;
        }

        public string Name { get; set; }
        
        public string Account { get; set; }
    }
}


