using System;

namespace HotelManagement.Model
{
    public class Apartment
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public int Rooms { get; set; }
        public int MaxGuests { get; set; }
      
      public System.Collections.Generic.List<Reservation> reservation;
      
      public System.Collections.Generic.List<Reservation> Reservation
      {
         get
         {
            if (reservation == null)
               reservation = new System.Collections.Generic.List<Reservation>();
            return reservation;
         }
         set
         {
            RemoveAllReservation();
            if (value != null)
            {
               foreach (Reservation oReservation in value)
                  AddReservation(oReservation);
            }
         }
      }
      
      
      public void AddReservation(Reservation newReservation)
      {
         if (newReservation == null)
            return;
         if (this.reservation == null)
            this.reservation = new System.Collections.Generic.List<Reservation>();
         if (!this.reservation.Contains(newReservation))
            this.reservation.Add(newReservation);
      }
      
      
      public void RemoveReservation(Reservation oldReservation)
      {
         if (oldReservation == null)
            return;
         if (this.reservation != null)
            if (this.reservation.Contains(oldReservation))
               this.reservation.Remove(oldReservation);
      }
      
      
      public void RemoveAllReservation()
      {
         if (reservation != null)
            reservation.Clear();
      }
   
   }
}