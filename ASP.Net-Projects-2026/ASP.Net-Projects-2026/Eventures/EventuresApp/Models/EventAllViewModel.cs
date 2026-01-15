namespace EventuresApp.Models
{
    public class EventAllViewModel
    {
        //Tova e modela ot koito shte se vzemat dannite
        public string Name { get; set; } =string.Empty;
        public string Start { get; set; }=string.Empty;
        public string End { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public string Owner { get; set; } =string.Empty;
        public decimal PricePerTicket { get; set; }
        public int TotalTickets { get; set; }

    }
}
