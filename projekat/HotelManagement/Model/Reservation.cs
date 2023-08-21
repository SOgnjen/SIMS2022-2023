using System;

namespace HotelManagement.Model
{
   public class Reservation
   {
      public DateTime Date { get; set; }
      public ReservationStatus Status { get; set; }
      public string DeclinedBecause { get; set; }
      public int Id { get; set; }
   
   }
}