using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication101.EfCore
{
    [Table("game")]
    public class Game
    {
        [Key, Required]
        public int IdGame { get; set; }
        public long IdUser1 { get; set; }
        public long IdUser2 { get; set; }
        public string Field1 { get; set; } = string.Empty;
        public string Field2 { get; set; } = string.Empty;
        public int Dice { get; set; }
        public int Size { get; set; }
        public int Move { get; set; }
    }
}
