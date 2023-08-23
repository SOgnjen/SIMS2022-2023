using HotelManagement.Model;
using HotelManagement.Service;
using System;

namespace HotelManagement.Controller
{
   public class UserController
   {

        private readonly UserService userService;

        public UserController(UserService service)
        {
            userService = service;
        }

        public User Login(string email, string password)
        {
            return userService.Login(email, password);
        }

        public Boolean IsJmbgValid(string jmbg)
        {
            return userService.IsJmbgValid(jmbg);
        }
      
        public Boolean IsEmailValid(string email)
        {
            return userService.IsEmailValid(email);
        }
      
        public User Register( User user)
        {
            return userService.Register(user);
        }
      
        public User Block(string email)
        {
            return userService.Block(email);
        }
      
        public User Unblock(string email)
        {
            return userService.Unblock(email);
        }

        public User getByEmail(string email)
        {
            return userService.GetByEmail(email);
        }
      
   
   }
}