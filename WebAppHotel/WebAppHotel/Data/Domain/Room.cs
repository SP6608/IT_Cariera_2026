using System.ComponentModel.DataAnnotations;

namespace WebAppHotel.Data.Domain
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Number { get; set; } = string.Empty;

        [Required]
        public int Capacity { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        [Required]
        public decimal PricePerNight { get; set; }
    }

    public enum RoomType
    {
        Single = 1,
        Double = 2,
        Apartment = 3
    }
}
