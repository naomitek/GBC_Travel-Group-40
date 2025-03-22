using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_40.Models
{
    public class BookingFlights
    {
        [Key]
        public int BookingFlightId {  get; set; }
        public int FlightId { get; set; }
        public int PassengerId {  get; set; }
    }
}
