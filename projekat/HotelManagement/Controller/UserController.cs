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

        public User Login(string email, string password)
        {
            return userService.Login(email, password);
        }

        public bool IsJmbgValid(string jmbg)
        {
            return userService.IsJmbgValid(jmbg);
        }
      
        public bool IsEmailValid(string email)
        {
            return userService.IsEmailValid(email);
        }
      
        public void AddUser(string jmbg, string email, string password, string name, string surname, string phone, UserType type, bool blocked, List<Hotel> hotels, List<Reservation> reservations)
        {
            userService.AddUser(jmbg, email, password, name, surname, phone, type, blocked, hotels, reservations);
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

        public List<User> GetAll()
        {
            return userService.GetAll();
        }

        public List<Reservation> GetAllGetAllReservationsOfOwner(string ownersJmbg)
        {
            return userService.GetAllReservationsOfOwner(ownersJmbg);
        }



   }
}