using HotelManagement.Model;
using HotelManagement.Service;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

        public void CreateApartment(string apartmentNumber, string name, string description, int rooms, int maxGuests, string hotelCode)
        {
            apartmentService.CreateApartment(apartmentNumber, name, description, rooms, maxGuests, hotelCode);
        }


        public List<Apartment> GetAll()
        {
            return apartmentService.GetAll();
        }
         
   }
}