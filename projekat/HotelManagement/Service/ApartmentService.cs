using HotelManagement.Model;
using HotelManagement.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace HotelManagement.Service
{
    public class ApartmentService
    {
        private ApartmentRepository apartmentRespository;

        public ApartmentService(ApartmentRepository apartmentRespository)
        {
            this.apartmentRespository = apartmentRespository;
        }

        public void CreateApartment(string apartmentNumber, string name, string description, int rooms, int maxGuests, string hotelCode)
        {
            Apartment newApartment = new Apartment(apartmentNumber, name, description, rooms, maxGuests, hotelCode);
            apartmentRespository.AddApartment(newApartment);
        }

        public bool IsNameValid(string name)
        {
            throw new NotImplementedException();
        }

        public List<Apartment> GetAll()
        {
            return apartmentRespository.GetAll();
        }


    }
}