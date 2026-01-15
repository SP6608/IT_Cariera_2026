using System.ComponentModel.DataAnnotations;

namespace EventuresApp.Models
{
    public class EventCreateBindingModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Place")]
        public string Place { get; set; } = string.Empty;

        [Display(Name = "Start")]
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }

        [Display(Name = "End")]
        [DataType(DataType.DateTime)]
        public DateTime End { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Total tickets")]
        public int TotalTickets { get; set; }

        [Range(0.01, double.MaxValue)]
        [Display(Name = "Price per ticket")]
        public decimal PricePerTicket { get; set; }
    }
}
