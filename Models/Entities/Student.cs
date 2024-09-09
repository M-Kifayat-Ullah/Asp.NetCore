namespace StudentPortal.web.Models.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public int Phone { get; set; }
        public bool Subscribed { get; set; }
    }
}
