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
      public Boolean Blocked { get; set; } = false;
      public int Unsuccessful { get; set; } = 0;
      public List<Hotel> hotels { get; set; }
      
      
      public User(string jmbg, string email, string password, string name, string surname, string phone, UserType type, bool blocked, int unsuccessful, List<Hotel> hotels)
        {
            Jmbg = jmbg;
            Email = email;
            Password = password;
            Name = name;
            Surname = surname;
            Phone = phone;
            Type = type;
            Blocked = blocked;
            Unsuccessful = unsuccessful;
            this.hotels = hotels;
        }
    }
}