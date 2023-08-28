using HotelManagement.Model;
using HotelManagement.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace HotelManagement.Service
{
    public class HotelService
    {

        private HotelRepository hotelRepository;

        public HotelService(HotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }

        public void AddHotel(string code, string name, int builtIn, Dictionary<string, Apartment> appartments, int stars, string ownersJmbg, bool accepted, HotelStatus status)
        {
            Hotel newHotel = new Hotel(code, name, builtIn, appartments, stars, ownersJmbg, accepted, status);

            hotelRepository.AddHotel(newHotel);
        }

        public List<Hotel> GetAll()
        {
            return hotelRepository.GetAll();
        }

        public List<Hotel> GetByAccepted(bool accepted)
        {
            return hotelRepository.GetByAccepted(accepted);
        }

        public List<Hotel> GetByOwnersJmbg(string ownersJmbg)
        {
            return hotelRepository.GetByOwnersJmbg(ownersJmbg);
        }

        public bool AcceptHotel(string hotelCode)
        {
            return hotelRepository.AcceptHotel(hotelCode);
        }

        public bool DeclineHotel(string hotelCode)
        {
            return hotelRepository.DeclineHotel(hotelCode);
        }


    }
}