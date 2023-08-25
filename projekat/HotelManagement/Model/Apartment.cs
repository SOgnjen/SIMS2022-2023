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
        public List<Reservation> Reservations { get; set; }

        public Apartment(string apartmentNumber, string name, string description, int rooms, int maxGuests, List<Reservation> reservations)
        {
            ApartmentNumber = apartmentNumber;
            Name = name;
            Description = description;
            Rooms = rooms;
            MaxGuests = maxGuests;
            Reservations = reservations;
        }
    }
}