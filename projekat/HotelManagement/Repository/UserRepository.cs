using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace HotelManagement.Repository
{
   public class UserRepository
   {
        private List<User> users;
        private readonly string fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\users.json";

        public UserRepository()
        {
            users = LoadData();

            foreach (var user in users)
            {
                Console.WriteLine($"Loaded user: {user.Name} {user.Surname}, Email: {user.Email}");
            }
        }

        private List<User> LoadData() 
        {
            if (File.Exists(fileLocation))
            {
                string json = File.ReadAllText(fileLocation);
                return JsonSerializer.Deserialize<List<User>>(json);
            }
            return new List<User>();
        }

        private void SaveData()
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileLocation, json);
        }

        public List<User> GetAll()
        {
            return users;
        }

        public List<User> SortByName()
      {
         throw new NotImplementedException();
      }
      
      public List<User> SortBySurname()
      {
         throw new NotImplementedException();
      }
      
      public List<User> GuestUser()
      {
         throw new NotImplementedException();
      }
      
      public List<User> OwnerUser()
      {
         throw new NotImplementedException();
      }
      
      public User GetByJmbg(String jmbg)
      {
         throw new NotImplementedException();
      }

        public User GetByEmail(string email)
        {
            return users.FirstOrDefault(user => user.Email == email);
        }
      
      public HotelManagement.Model.User AddUser(HotelManagement.Model.User user)
      {
         throw new NotImplementedException();
      }
      
      public HotelManagement.Model.User UpdateUser(HotelManagement.Model.User user)
      {
         throw new NotImplementedException();
      }
   
   }
}