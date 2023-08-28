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


        public Reservation Cancel(int id)
        {
            throw new NotImplementedException();
        }

        public Reservation Accept(int id)
        {
            throw new NotImplementedException();
        }

        public Reservation Decline(int id)
        {
            throw new NotImplementedException();
        }

        public void AddReservation(int id, DateTime date, ReservationStatus status, string declinedBecause, string ownerJmbg, string apartmentName)
        {
            Reservation newReservation = new Reservation(id, date, status, declinedBecause, ownerJmbg, apartmentName);
            reservationRepository.AddReservation(newReservation);
        }

        public List<Reservation> GetAll()
        {
            return reservationRepository.GetAll();
        }

    }
}