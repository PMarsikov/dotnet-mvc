namespace MvcLibPavel.Models
{
    public class BooksSummary
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BookYear { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorMiddleName { get; set; }
        public string AuthorLastName { get; set; }
        public DateTime AuthorBirthDay { get; set; }
    }
}
