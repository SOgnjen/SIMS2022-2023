using HotelManagement.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace HotelManagement.Repository
{
    public class HotelRepository
    {
        private List<Hotel> hotels;
        private string fileLocation;

        public HotelRepository()
        {
            fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\hotels.json";
            ReadJson();
        }

        private void ReadJson()
        {
            if (!File.Exists(fileLocation))
            {
                File.Create(fileLocation).Close();
            }

            StreamReader r = new StreamReader(fileLocation);

            string json = r.ReadToEnd();
            if (json != "")
            {
                hotels = JsonConvert.DeserializeObject<List<Hotel>>(json);
            }
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(hotels, Formatting.Indented);
            File.WriteAllText(fileLocation, json);
        }

        public List<Hotel> GetAll()
        {
            return hotels;
        }

        public List<Hotel> GetByAccepted(bool accepted)
        {
            List<Hotel> result = new List<Hotel>();

            foreach (Hotel hotel in hotels)
            {
                if (hotel.Accepted == accepted)
                {
                    result.Add(hotel);
                }
            }

            return result;
        }

        public List<Hotel> GetByOwnersJmbg(string ownersJmbg)
        {
            List<Hotel> result = new List<Hotel>();

            foreach (Hotel hotel in hotels)
            {
                if (hotel.OwnersJmbg == ownersJmbg && hotel.Status != HotelStatus.Declined)
                {
                    result.Add(hotel);
                }
            }

            return result;
        }

        public Hotel GetByCode(string code)
        {
            return hotels.Find(hotel => hotel.Code == code);
        }

        public void AddHotel(Hotel hotel)
        {
            hotels.Add(hotel);
            WriteToJson();
        }

        public bool AcceptHotel(string hotelCode)
        {
            Hotel hotel = hotels.Find(h => h.Code == hotelCode);

            if (hotel != null)
            {
                if (hotel.Status != HotelStatus.Waiting)
                {
                    return false;
                }

                hotel.Accepted = true;
                hotel.Status = HotelStatus.Accepted;
                WriteToJson();
                return true;
            }

            return false;
        }

        public bool DeclineHotel(string hotelCode)
        {
            Hotel hotel = hotels.Find(h => h.Code == hotelCode);

            if (hotel != null)
            {
                if (hotel.Status != HotelStatus.Waiting)
                {
                    return false;
                }

                hotel.Accepted = false;
                hotel.Status = HotelStatus.Declined;
                WriteToJson();
                return true;
            }

            return false;
        }


    }
}