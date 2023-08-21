using HotelManagement.Model;
using HotelManagement.Service;
using System;

namespace HotelManagement.Controller
{
   public class HotelController
   {

        private readonly HotelService _service;

        public HotelController(HotelService service)
        {
            _service = service;
        }

        public Hotel Accept(string code)
        {
            return _service.Accept(code);
        }

        public Hotel Decline(string code)
        {
            return _service.Decline(code);
        }

        public Hotel CreateHotel(Hotel hotel)
        {
            return _service.CreateHotel(hotel);
        }

        public Boolean IsCodeValid(string code)
        {
            return _service.IsCodeValid(code);
        }
      
   
   }
}