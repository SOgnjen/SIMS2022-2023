using System;
using System.Collections.Generic;

namespace HotelManagement.Model
{
    public class Apartment
    {
        public string ApartmentNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rooms { get; set; }
        public int MaxGuests { get; set; }
        public string HotelCode { get; set; }

        public Apartment(string apartmentNumber, string name, string description, int rooms, int maxGuests, string hotelCode)
        {
            ApartmentNumber = apartmentNumber;
            Name = name;
            Description = description;
            Rooms = rooms;
            MaxGuests = maxGuests;
            HotelCode = hotelCode;
        }
    }
}