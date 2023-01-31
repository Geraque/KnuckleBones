using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication101.EfCore
{
    [Table("rating")]
    public class Rating
    {
        [Key, Required]
        public long IdUser { get; set; }
        public string UserName { get; set; }
        public int Win { get; set; }
        public int Lose { get; set; }
        public int Draw { get; set; }
        public int Points { get; set; }

    }
}