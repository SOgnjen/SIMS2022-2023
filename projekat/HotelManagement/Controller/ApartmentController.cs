using HotelManagement.Model;
using HotelManagement.Service;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace HotelManagement.Controller
{
   public class ApartmentController
   {
        private readonly ApartmentService apartmantService;

        public ApartmentController(ApartmentService service)
        {
            apartmantService = service;
        }

        public void AddApartment(Apartment apartment)
        {
            apartmantService.AddApartment(apartment);
        }

        public Boolean IsNameValid(string name)
        {
            return apartmantService.IsNameValid(name);
        }

        public List<Apartment> GetAll()
        {
            return apartmantService.GetAll();
        }
         
   }
}