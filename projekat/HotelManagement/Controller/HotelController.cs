using HotelManagement.Model;
using HotelManagement.Service;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace HotelManagement.Controller
{
   public class HotelController
   {

        private readonly HotelService hotelService;

        public HotelController(HotelService service)
        {
            hotelService = service;
        }

        public Hotel Accept(string code)
        {
            return hotelService.Accept(code);
        }

        public Hotel Decline(string code)
        {
            return hotelService.Decline(code);
        }

        public Hotel CreateHotel(Hotel hotel)
        {
            return hotelService.CreateHotel(hotel);
        }

        public Boolean IsCodeValid(string code)
        {
            return hotelService.IsCodeValid(code);
        }

        public List<Hotel> GetAll()
        {
            return hotelService.GetAll();
        }
      
   
   }
}