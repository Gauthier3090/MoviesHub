namespace MoviesHub.Models
{
    public class Publication
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int User { get; set; }
    }

    public class PublicationForm
    {
        public string? Title { get; set; }
    }
}
