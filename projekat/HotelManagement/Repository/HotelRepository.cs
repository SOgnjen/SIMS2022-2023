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

        private void WriteToJson()
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
      
      public List<Hotel> SortByName()
      {
         throw new NotImplementedException();
      }
      
      public List<Hotel> SortByStars()
      {
         throw new NotImplementedException();
      }
      
      public List<Hotel> SearchByCode(String code)
      {
         throw new NotImplementedException();
      }
      
      public List<Hotel> SearchByName(String name)
      {
         throw new NotImplementedException();
      }
      
      public List<Hotel> SearchByBuiltIn(int buildIn)
      {
         throw new NotImplementedException();
      }
      
      public List<Hotel> SearchByStarts(int stars)
      {
         throw new NotImplementedException();
      }
      
      public List<Hotel> SearchByAppartments(int rooms, int persons)
      {
         throw new NotImplementedException();
      }
      
      public HotelManagement.Model.Hotel GetByCode(String code)
      {
         throw new NotImplementedException();
      }
      
      public HotelManagement.Model.Hotel AddHotel(HotelManagement.Model.Hotel hotel)
      {
         throw new NotImplementedException();
      }
      
      public HotelManagement.Model.Hotel UpdateHotel(HotelManagement.Model.Hotel hotel)
      {
         throw new NotImplementedException();
      }
   
   }
}