namespace WebApplication101.Model
{
    public class LobbyModel
    {
        public int IdGame { get; set; }
        public long IdUser1 { get; set; }
        public long? IdUser2 { get; set; }
        public string Status { get; set; }
        public string LobbyName { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }
        public int Size { get; set; }
    }
}
