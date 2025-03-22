using System.ComponentModel.DataAnnotations;


namespace GBC_Travel_Group_40.Models
{
    public class CarBookings
    {
        [Key]
        public int CarBookingId { get; set; }

        // Foreign key for Car
        public int CarId { get; set; }

        [Required]
        public string NameOfHolder { get; set; }
        [Required]
        public string PhoneOfHolder { get; set; }

    }
}
