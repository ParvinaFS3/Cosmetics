namespace Cosmetics.Areas.Admin.ViewModels.Message
{
    public class UpdateMessageVM
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }

        public string MessageInfo { get; set; }
    }
}
