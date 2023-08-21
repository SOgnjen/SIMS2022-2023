using System;

namespace HotelManagement.Controller
{
   public class UserController
   {
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
      
      public HotelManagement.Service.UserService userService;
   
   }
}