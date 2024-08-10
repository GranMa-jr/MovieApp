namespace MovieApp.Entity.Entities
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public int Time { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}