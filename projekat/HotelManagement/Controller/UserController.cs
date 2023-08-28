using HotelManagement.Model;
using HotelManagement.Service;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace HotelManagement.Controller
{
   public class UserController
   {

        private readonly UserService userService;

        public UserController(UserService service)
        {
            userService = service;
        }
      
        public void AddUser(string jmbg, string email, string password, string name, string surname, string phone, UserType type, bool blocked)
        {
            userService.AddUser(jmbg, email, password, name, surname, phone, type, blocked);
        }
      

        public User getByEmail(string email)
        {
            return userService.GetByEmail(email);
        }

        public List<User> GetAll()
        {
            return userService.GetAll();
        }

        public List<Reservation> GetAllReservationsOfOwner(string ownersJmbg)
        {
            return userService.GetAllReservationsOfOwner(ownersJmbg);
        }

        public void BlockUser(User user)
        {
            userService.BlockUser(user);
        }



   }
}