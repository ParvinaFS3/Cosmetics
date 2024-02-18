using Cosmetics.Models.Customer;

namespace Cosmetics.Models
{
    public class Message:BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }

        public string MessageInfo { get; set;}
    }
}
