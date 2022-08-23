namespace MvcLibPavel.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BookYear { get; set; }
        public int AuthorId { get; set; }
    }
}
