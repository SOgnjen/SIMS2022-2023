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


        public void AddReservation(int id, DateTime date, ReservationStatus status, string declinedBecause, string ownerJmbg, string apartmentName)
        {
            Reservation newReservation = new Reservation(id, date, status, declinedBecause, ownerJmbg, apartmentName);
            reservationRepository.AddReservation(newReservation);
        }

        public void CancelReservation(int reservationId, string cancellationReason)
        {
            reservationRepository.CancelReservation(reservationId, cancellationReason);
        }

        public List<Reservation> GetAll()
        {
            return reservationRepository.GetAll();
        }

        public bool AcceptReservation(int reservationId)
        {
            return reservationRepository.AcceptReservation(reservationId);
        }

        public bool DeclineReservation(int reservationId, string declinedBecause)
        {
            return reservationRepository.DeclineReservation(reservationId, declinedBecause);
        }

    }
}