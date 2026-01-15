using System.ComponentModel.DataAnnotations;

namespace WebAppHotel.Data.Domain
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }=string.Empty;
        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }= string.Empty;
        [Required]
        public bool IsAdult {  get; set; }
    }
}
