using HotelManagement.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;

namespace HotelManagement.Repository
{
   public class UserRepository
   {
        private List<User> users;
        private string fileLocation;

        public UserRepository()
        {
            fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\users.json";
            ReadJson();
        }

        private void ReadJson()
        {
            if (!File.Exists(fileLocation))
            {
                File.Create(fileLocation).Close();
            }

            StreamReader r = new StreamReader(fileLocation);

            string json = r.ReadToEnd();
            if (json != "")
            {
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }
        }

        private void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
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

        public void AddUser(User user)
        {
            users.Add(user);
            WriteToJson();
        }
      
      public HotelManagement.Model.User UpdateUser(HotelManagement.Model.User user)
      {
         throw new NotImplementedException();
      }
   
   }
}