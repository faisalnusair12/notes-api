namespace Notes_API.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Nots_Title { get; set; }
        public string Nots_Description { get; set; }
        public DateTime Created_Time { get; set; }
        public bool IsImportant { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
