using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_40.Models
{
    public class RoomBookings
    {
        [Key]
        public int RoomBookingId { get; set; }
        public int RoomId { get; set; }
        [Required]
        public string NameOfHolder { get; set; }
        [Required]
        public string PhoneOfHolder { get; set; }

    }
}
