using System;

namespace HotelManagement.Model
{
   public class Reservation
   {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ReservationStatus Status { get; set; }
        public string DeclinedBecause { get; set; }
        public string OwnersJmbg { get; set; }
        public string ApartmentName { get; set; }

        public Reservation(int id, DateTime date, ReservationStatus status, string declinedBecause, string ownersJmbg, string apartmentName)
        {
            Id = id;
            Date = date;
            Status = status;
            DeclinedBecause = declinedBecause;
            OwnersJmbg = ownersJmbg;
            ApartmentName = apartmentName;
        }
   
   }
}