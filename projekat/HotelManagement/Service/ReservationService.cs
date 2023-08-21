using System;

namespace HotelManagement.Service
{
   public class ReservationService
   {
      public HotelManagement.Model.Reservation Cancel(int id)
      {
         throw new NotImplementedException();
      }
      
      public HotelManagement.Model.Reservation Accept(int id)
      {
         throw new NotImplementedException();
      }
      
      public HotelManagement.Model.Reservation Decline(int id)
      {
         throw new NotImplementedException();
      }
      
      public HotelManagement.Model.Reservation CreateReservation(String apartmentName, DateTime date)
      {
         throw new NotImplementedException();
      }
      
      public HotelManagement.Repository.ReservationRepository reservationRepository;
   
   }
}