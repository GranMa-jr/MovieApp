namespace MovieApp.Entity.Entities
{
    public class MovieUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public double Time { get; set; }
        public int Rate { get; set; }
    }
}