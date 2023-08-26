using HotelManagement.Model;
using HotelManagement.Service;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace HotelManagement.Controller
{
   public class ApartmentController
   {
        private readonly ApartmentService apartmentService;

        public ApartmentController(ApartmentService service)
        {
            apartmentService = service;
        }

        public void CreateApartment(string hotelCode, string apartmentNumber, string name, string description, int rooms, int maxGuests, List<Reservation> reservations)
        {
            apartmentService.CreateApartment(hotelCode, apartmentNumber, name, description, rooms, maxGuests, reservations);
        }


        public Boolean IsNameValid(string name)
        {
            return apartmentService.IsNameValid(name);
        }

        public List<Apartment> GetAll()
        {
            return apartmentService.GetAll();
        }
         
   }
}