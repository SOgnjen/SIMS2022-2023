using System;
using System.Collections.Generic;


namespace HotelManagement.Model
{
   public class Hotel
   {
        public string Code { get; set; }
        public string Name { get; set; }
        public int BuiltIn { get; set; }
        public Dictionary<string, Apartment> Apartments { get; set; }
        public int Stars { get; set; }
        public string OwnersJmbg { get; set; }
        public Boolean Accepted { get; set; } = false;
        public HotelStatus Status { get; set; }
      
        public Hotel(string code, string name, int builtIn, Dictionary<string, Apartment> appartments, int stars, string ownersJmbg, bool accepted, HotelStatus status)
        {
            Code = code;
            Name = name;
            BuiltIn = builtIn;
            Apartments = appartments;
            Stars = stars;
            OwnersJmbg = ownersJmbg;
            Accepted = accepted;
            Status = status;
        }
    }
}