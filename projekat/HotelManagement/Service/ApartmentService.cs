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

      public void CreateApartment(string hotelCode, string apartmentNumber, string name, string description, int rooms, int maxGuests, List<Reservation> reservations)
      {
            Apartment newApartment = new Apartment(apartmentNumber, name, description, rooms, maxGuests, reservations);
            apartmentRespository.CreateApartment(hotelCode, newApartment);
      }
      
      public Boolean IsNameValid(String name)
      {
         throw new NotImplementedException();
      }
      
        public List<Apartment> GetAll()
        {
            return apartmentRespository.GetAll();
        }
        
   
   }
}