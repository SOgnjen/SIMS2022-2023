using HotelManagement.Model;
using HotelManagement.Service;
using System;

namespace HotelManagement.Controller
{
   public class UserController
   {

        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        public User Login(string email, string password)
        {
            return _service.Login(email, password);
        }

        public Boolean IsJmbgValid(string jmbg)
        {
            return _service.IsJmbgValid(jmbg);
        }
      
        public Boolean IsEmailValid(string email)
        {
            return _service.IsEmailValid(email);
        }
      
        public User Register( User user)
        {
            return _service.Register(user);
        }
      
        public User Block(string email)
        {
            return _service.Block(email);
        }
      
        public User Unblock(string email)
        {
            return _service.Unblock(email);
        }
      
   
   }
}