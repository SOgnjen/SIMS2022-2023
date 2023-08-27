using HotelManagement.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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

        public List<Reservation> WaitingReservations()
        {
            throw new NotImplementedException();
        }

        public List<Reservation> AcceptedReservations()
        {
            throw new NotImplementedException();
        }

        public List<Reservation> DeclinedReservations()
        {
            throw new NotImplementedException();
        }

        public Reservation GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddReservation(Reservation reservation)
        {
            reservations.Add(reservation);
            WriteToJson();
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

    }
}