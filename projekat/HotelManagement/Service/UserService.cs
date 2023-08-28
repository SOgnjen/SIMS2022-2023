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


        public void AddUser(string jmbg, string email, string password, string name, string surname, string phone, UserType type, bool blocked)
        {
            User newUser = new User(jmbg, email, password, name, surname, phone, type, blocked);

            userRepository.AddUser(newUser);
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

        public void BlockUser(User user)
        {
            userRepository.BlockUser(user);
        }


    }
}