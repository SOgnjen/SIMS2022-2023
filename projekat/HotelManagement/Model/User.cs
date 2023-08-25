using System;
using System.Collections.Generic;

namespace HotelManagement.Model
{
   public class User
   {
        public string Jmbg { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set;  }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public UserType Type { get; set; }
        public bool Blocked { get; set; } = false;
        public List<Hotel> Hotels { get; set; }
        public List<Reservation> Reservations { get; set; }
      
      
      public User(string jmbg, string email, string password, string name, string surname, string phone, UserType type, bool blocked, List<Hotel> hotels, List<Reservation> reservations)
        {
            Jmbg = jmbg;
            Email = email;
            Password = password;
            Name = name;
            Surname = surname;
            Phone = phone;
            Type = type;
            Blocked = blocked;
            Hotels = hotels;
            Reservations = reservations;
        }
    }
}