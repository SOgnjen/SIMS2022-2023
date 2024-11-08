using HotelManagement.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Xml.Linq;

namespace HotelManagement.Repository
{
    public class UserRepository
    {
        private List<User> users;
        private string fileLocation;
        private HotelRepository hotelRepository = new HotelRepository();
        private ReservationRepository reservationRepository = new ReservationRepository();

        public UserRepository()
        {
            fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\users.json";
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
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }
        }

        private void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(fileLocation, json);
        }

        public List<User> GetAll()
        {
            return users;
        }

        public User GetByJmbg(string jmbg)
        {
            return users.FirstOrDefault(user => user.Jmbg == jmbg);
        }

        public User GetByEmail(string email)
        {
            return users.FirstOrDefault(user => user.Email == email);
        }

        public List<Reservation> GetAllReservationsOfOwner(string ownersJmbg)
        {
            List<Reservation> ownerReservations = new List<Reservation>();

            List<Hotel> ownerHotels = hotelRepository.GetByOwnersJmbg(ownersJmbg);

            foreach (var hotel in ownerHotels)
            {
                foreach (var apartment in hotel.Apartments.Values)
                {
                    foreach (var reservation in reservationRepository.GetAll())
                    {
                        if ((reservation.Status == ReservationStatus.Waiting || reservation.Status == ReservationStatus.Accepted)
                            && reservation.ApartmentName == apartment.Name)
                        {
                            ownerReservations.Add(reservation);
                        }
                    }
                }
            }

            return ownerReservations;
        }


        public void AddUser(User user)
        {
            users.Add(user);
            WriteToJson();
        }

        public void BlockUser(User user)
        {
            if (user.Type == UserType.Administrator)
            {
                MessageBox.Show("Cannot block an administrator user.", "Invalid Action", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            User existingUser = users.Find(u => u.Jmbg == user.Jmbg);
            if (existingUser != null)
            {
                existingUser.Blocked = !existingUser.Blocked;
                WriteToJson();
            }
        }


    }
}