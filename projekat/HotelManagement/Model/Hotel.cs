using System;
using System.Collections.Generic;


namespace HotelManagement.Model
{
   public class Hotel
   {
      public string Code { get; set; }
      public string Name { get; set; }
      public int BuiltIn { get; set; }
      public int Stars { get; set; }
      public string OwnersJmbg { get; set; }
      public Boolean Accepted { get; set; } = false;
      public HotelStatus Status { get; set; }
      public Dictionary<string, Apartment> Appartments { get; set; }
      
      public System.Collections.Generic.List<Apartment> apartment;
      
      public System.Collections.Generic.List<Apartment> Apartment
      {
         get
         {
            if (apartment == null)
               apartment = new System.Collections.Generic.List<Apartment>();
            return apartment;
         }
         set
         {
            RemoveAllApartment();
            if (value != null)
            {
               foreach (Apartment oApartment in value)
                  AddApartment(oApartment);
            }
         }
      }
      
      
      public void AddApartment(Apartment newApartment)
      {
         if (newApartment == null)
            return;
         if (this.apartment == null)
            this.apartment = new System.Collections.Generic.List<Apartment>();
         if (!this.apartment.Contains(newApartment))
            this.apartment.Add(newApartment);
      }
      
      
      public void RemoveApartment(Apartment oldApartment)
      {
         if (oldApartment == null)
            return;
         if (this.apartment != null)
            if (this.apartment.Contains(oldApartment))
               this.apartment.Remove(oldApartment);
      }
      
      
      public void RemoveAllApartment()
      {
         if (apartment != null)
            apartment.Clear();
      }
      public User user;
      
      
      public User User
      {
         get
         {
            return user;
         }
         set
         {
            if (this.user == null || !this.user.Equals(value))
            {
               if (this.user != null)
               {
                  User oldUser = this.user;
                  this.user = null;
                  oldUser.RemoveHotel(this);
               }
               if (value != null)
               {
                  this.user = value;
                  this.user.AddHotel(this);
               }
            }
         }
      }
   
   }
}