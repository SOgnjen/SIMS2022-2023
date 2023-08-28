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

        public Reservation Cancel(int id)
        {
            return reservationService.Cancel(id);
        }

        public Reservation Accept(int id)
        {
            return reservationService.Accept(id);
        }

        public Reservation Decline(int id)
        {
            return reservationService.Decline(id);
        }

        public void AddReservation(int id, DateTime date, ReservationStatus status, string declinedBecause, string ownersJmbg, string apartmentName)
        {
            reservationService.AddReservation(id, date, status, declinedBecause, ownersJmbg, apartmentName);
        }

        public List<Reservation> GetAll()
        {
            return reservationService.GetAll();
        }
      
   
   }
}