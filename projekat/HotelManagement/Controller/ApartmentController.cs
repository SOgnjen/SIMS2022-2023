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

        public HotelManagement.Model.Apartment CreateApartment(HotelManagement.Model.Apartment apartment)
        {
            return _service.CreateApartment(apartment);
        }

        public Boolean IsNameValid(String name)
        {
            return _service.IsNameValid(name);
        }
         
   }
}