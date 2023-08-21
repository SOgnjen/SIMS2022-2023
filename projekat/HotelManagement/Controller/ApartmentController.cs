using HotelManagement.Model;
using HotelManagement.Service;
using System;

namespace HotelManagement.Controller
{
   public class ApartmentController
   {
        private readonly ApartmentService _service;

        public ApartmentController(ApartmentService service)
        {
            _service = service;
        }

        public Apartment CreateApartment(Apartment apartment)
        {
            return _service.CreateApartment(apartment);
        }

        public Boolean IsNameValid(string name)
        {
            return _service.IsNameValid(name);
        }
         
   }
}