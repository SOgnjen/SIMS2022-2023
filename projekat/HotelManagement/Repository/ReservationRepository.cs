using HotelManagement.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HotelManagement.Repository
{
    public class ReservationRepository
    {
        private List<Reservation> reservations;
        private string fileLocation;

        public ReservationRepository()
        {
            fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\reservations.json";
            ReadJson();
        }

        private void ReadJson()
        {
            if (!File.Exists(fileLocation))
            {
                File.Create(fileLocation).Close();
            }

            StreamReader r = new StreamReader(fileLocation);

            string json = r.ReadToEnd();
            if (json != "")
            {
                reservations = JsonConvert.DeserializeObject<List<Reservation>>(json);
            }
        }

        private void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(reservations, Formatting.Indented);
            File.WriteAllText(fileLocation, json);
        }

        public List<Reservation> GetAll()
        {
            return reservations;
        }


        public Reservation GetById(int id)
        {
            return reservations.Find(reservation => reservation.Id == id);
        }

        public void AddReservation(Reservation reservation)
        {
            reservations.Add(reservation);
            WriteToJson();
        }

        public void CancelReservation(int reservationId, string cancellationReason)
        {
            Reservation reservation = reservations.FirstOrDefault(r => r.Id == reservationId);

            if (reservation != null)
            {
                if (reservation.Status == ReservationStatus.Waiting || reservation.Status == ReservationStatus.Accepted)
                {
                    reservation.Status = ReservationStatus.Cancelled;
                    reservation.DeclinedBecause = cancellationReason;
                    WriteToJson();
                }
            }
        }

        public bool AcceptReservation(int reservationId)
        {
            Reservation reservation = GetById(reservationId);

            if (reservation != null)
            {
                if (reservation.Status != ReservationStatus.Waiting)
                {
                    return false;
                }

                reservation.Status = ReservationStatus.Accepted;
                WriteToJson();
                return true;
            }

            return false;
        }

        public bool DeclineReservation(int reservationId, string declinedBecause)
        {
            Reservation reservation = GetById(reservationId);

            if (reservation != null)
            {
                if (reservation.Status != ReservationStatus.Waiting)
                {
                    return false;
                }

                reservation.Status = ReservationStatus.Declined;
                reservation.DeclinedBecause = declinedBecause;
                WriteToJson();
                return true;
            }

            return false;
        }

    }
}