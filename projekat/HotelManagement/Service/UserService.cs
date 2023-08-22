using HotelManagement.Model;
using HotelManagement.Repository;
using System;

namespace HotelManagement.Service
{
   public class UserService
   {

        private UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

      public HotelManagement.Model.User Login(String email, String password)
      {
         throw new NotImplementedException();
      }
      
      public Boolean IsJmbgValid(String jmbg)
      {
         throw new NotImplementedException();
      }
      
      public Boolean IsEmailValid(String email)
      {
         throw new NotImplementedException();
      }
      
      public HotelManagement.Model.User Register(HotelManagement.Model.User user)
      {
         throw new NotImplementedException();
      }
      
      public HotelManagement.Model.User Block(String email)
      {
         throw new NotImplementedException();
      }
      
      public HotelManagement.Model.User Unblock(String email)
      {
         throw new NotImplementedException();
      }

        public User GetByEmail(string email)
        {
            return userRepository.GetByEmail(email);
        }
      
   
   }
}