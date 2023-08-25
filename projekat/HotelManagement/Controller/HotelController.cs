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

        public void AddHotel(string code, string name, int builtIn, Dictionary<string, Apartment> appartments, int stars, string ownersJmbg, bool accepted, HotelStatus status)
        {
            hotelService.AddHotel(code, name, builtIn, appartments, stars, ownersJmbg, accepted, status);
        }

        public Boolean IsCodeValid(string code)
        {
            return hotelService.IsCodeValid(code);
        }

        public List<Hotel> GetAll()
        {
            return hotelService.GetAll();
        }

        public List<Hotel> GetByAccepted(bool accepted)
        {
            return hotelService.GetByAccepted(accepted);
        }

        public List<Hotel> GetByOwnersJmbg(string ownersJmbg)
        {
            return hotelService.GetByOwnersJmbg(ownersJmbg);
        }
      
   
   }
}