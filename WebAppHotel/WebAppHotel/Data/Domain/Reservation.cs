using System.ComponentModel.DataAnnotations;

namespace WebAppHotel.Data.Domain
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        
        public string UserId { get; set; }=string.Empty;
        public AppUser? User { get; set; }
            
        public DateTime DateIn { get; set; }
        
        public DateTime DateOut { get; set; }
     
        public decimal TotalSum {  get; set; }
    }
}
