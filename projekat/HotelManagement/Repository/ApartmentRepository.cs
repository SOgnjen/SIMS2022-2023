using HotelManagement.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace HotelManagement.Repository
{
   public class ApartmentRepository
   {
        private List<Apartment> apartments;
        private string fileLocation;

        public ApartmentRepository()
        {
            fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\apartments.json";
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
                apartments = JsonConvert.DeserializeObject<List<Apartment>>(json);
            }
        }

        private void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(apartments, Formatting.Indented);
            File.WriteAllText(fileLocation, json);
        }

        public List<Apartment> GetAll()
        {
            return apartments;
        }
      
      public HotelManagement.Model.Apartment GetByName(string name)
      {
         throw new NotImplementedException();
      }
      
      public void AddApartment(Apartment apartment)
      {
         apartments.Add(apartment);
            WriteToJson();
      }
      
      public HotelManagement.Model.Apartment UpdateApartment(HotelManagement.Model.Apartment apartment)
      {
         throw new NotImplementedException();
      }
   
   }
}