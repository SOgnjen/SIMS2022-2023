using HotelManagement.Model;
using HotelManagement.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace HotelManagement.Service
{
    public class UserService
    {

        private UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsJmbgValid(string jmbg)
        {
            throw new NotImplementedException();
        }

        public bool IsEmailValid(string email)
        {
            throw new NotImplementedException();
        }

        public void AddUser(string jmbg, string email, string password, string name, string surname, string phone, UserType type, bool blocked, List<Hotel> hotels, List<Reservation> reservations)
        {
            User newUser = new User(jmbg, email, password, name, surname, phone, type, blocked, hotels, reservations);

            userRepository.AddUser(newUser);
        }

        public User Block(string email)
        {
            throw new NotImplementedException();
        }

        public User Unblock(string email)
        {
            throw new NotImplementedException();
        }

        public User GetByEmail(string email)
        {
            return userRepository.GetByEmail(email);
        }

        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public List<Reservation> GetAllReservationsOfOwner(string ownersJmbg)
        {
            return userRepository.GetAllReservationsOfOwner(ownersJmbg);
        }


    }
}