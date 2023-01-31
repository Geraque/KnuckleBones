namespace WebApplication101.Model
{
    public class RatingModel
    {
        public long IdUser { get; set; }
        public string UserName { get; set; }
        public int Win { get; set; }
        public int Lose { get; set; }
        public int Draw { get; set; }
        public int Points { get; set; }

    }
}