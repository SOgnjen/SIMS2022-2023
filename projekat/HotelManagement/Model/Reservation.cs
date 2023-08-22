using System;

namespace HotelManagement.Model
{
   public class Reservation
   {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ReservationStatus Status { get; set; }
        public string DeclinedBecause { get; set; }

        public Reservation(int id, DateTime date, ReservationStatus status, string declinedBecause)
        {
            Id = id;
            Date = date;
            Status = status;
            DeclinedBecause = declinedBecause;
        }
   
   }
}