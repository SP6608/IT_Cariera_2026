using System.ComponentModel.DataAnnotations;

namespace WebAppHotelFinal.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [StringLength(10, ErrorMessage = "Телефонният номер трябва да е до 10 символа.")]
        public string PhoneNumber { get; set; }= string.Empty;
        public bool IsAdult {  get; set; }
    }
}
