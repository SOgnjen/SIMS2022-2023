using HotelManagement.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace HotelManagement.Repository
{
    public class ApartmentRepository
    {
        private List<Apartment> apartments;
        private string fileLocation;
        private HotelRepository hotelRepository = new HotelRepository();

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

        public Apartment GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void AddApartment(Apartment apartment)
        {
            apartments.Add(apartment);
            WriteToJson();
        }


        public void CreateApartment(Apartment apartment)
        {
            apartments.Add(apartment);
            WriteToJson();

            Hotel hotel = hotelRepository.GetByCode(apartment.HotelCode);

            if (hotel != null)
            {
                hotel.Apartments[apartment.Name] = apartment;
                hotelRepository.WriteToJson();
            }
            else
            {
                MessageBox.Show("Hotel not found for the given apartment.");
            }
        }

    }
}