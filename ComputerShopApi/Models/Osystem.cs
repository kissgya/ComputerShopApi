using System.ComponentModel.DataAnnotations;

namespace ComputerShopApi.Models
{
    public class Osystem
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(27)]
        public string Name { get; set; }
        public int Version { get; set; }
        public DateTime RegisterTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}