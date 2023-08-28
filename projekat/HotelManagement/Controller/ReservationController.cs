using HotelManagement.Model;
using HotelManagement.Service;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace HotelManagement.Controller
{
   public class ReservationController
   {

        private readonly ReservationService reservationService;

        public ReservationController(ReservationService service)
        {
            reservationService = service;
        }


        public void AddReservation(int id, DateTime date, ReservationStatus status, string declinedBecause, string ownersJmbg, string apartmentName)
        {
            reservationService.AddReservation(id, date, status, declinedBecause, ownersJmbg, apartmentName);
        }

        public void CancelReservation(int reservationId, string cancellationReason)
        {
            reservationService.CancelReservation(reservationId, cancellationReason);
        }

        public List<Reservation> GetAll()
        {
            return reservationService.GetAll();
        }

        public bool AcceptReservation(int reservationId)
        {
            return reservationService.AcceptReservation(reservationId);
        }

        public bool DeclineReservation(int reservationId, string declinedBecause)
        {
            return reservationService.DeclineReservation(reservationId, declinedBecause);
        }
      
   
   }
}