using HotelManagement.Model;
using HotelManagement.Service;
using System;

namespace HotelManagement.Controller
{
   public class ReservationController
   {

        private readonly ReservationService _service;

        public ReservationController(ReservationService service)
        {
            _service = service;
        }

        public Reservation Cancel(int id)
        {
            return _service.Cancel(id);
        }

        public Reservation Accept(int id)
        {
            return _service.Accept(id);
        }

        public Reservation Decline(int id)
        {
            return _service.Decline(id);
        }

        public Reservation CreateReservation(string apartmentName, DateTime date)
        {
            return _service.CreateReservation(apartmentName, date);
        }
      
   
   }
}