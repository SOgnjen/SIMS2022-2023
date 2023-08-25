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

      public void AddApartment(Apartment apartment)
      {
         throw new NotImplementedException();
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