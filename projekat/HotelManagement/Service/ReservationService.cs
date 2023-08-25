using HotelManagement.Model;
using HotelManagement.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace HotelManagement.Service
{
   public class ReservationService
   {
        private ReservationRepository reservationRepository;

        public ReservationService(ReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }


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
      
      public void AddReservation(int id, DateTime date, ReservationStatus status, string declinedBecause)
      {
            Reservation newReservation = new Reservation(id, date, status, declinedBecause);
            reservationRepository.AddReservation(newReservation);
      }

        public List<Reservation> GetAll()
        {
            return reservationRepository.GetAll();
        }
         
   }
}