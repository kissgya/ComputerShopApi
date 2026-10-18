using System.ComponentModel.DataAnnotations;

namespace ComputerShopApi.Models
{
    public class Computer
    {
        [Key]
        public int Id { get; set; } // Az int elsődleges kulcs automatikusan Auto_Increment lesz az EF Core-ban

        [MaxLength(37)]
        public string Brand { get; set; }

        [MaxLength(30)]
        public string Type { get; set; }

        public double Display { get; set; }

        public int Memory { get; set; }

        public DateTime CreatedTime { get; set; }

        public Guid OsId { get; set; } // Idegen kulcs az Osystem táblához (char(36))
    }
}