using System;

namespace HotelManagement.Model
{
   public class User
   {
      public String Jmbg { get; set; }
      public String Email { get; set; }
      public String Password { get; set; }
      public String Name { get; set;  }
      public String Surname { get; set; }
      public String Phone { get; set; }
      public UserType Type { get; set; }
      public Boolean Blocked { get; set; } = false;
      public int Unsuccessful { get; set; } = 0;
      
      public System.Collections.Generic.List<Hotel> hotel;
      
      public System.Collections.Generic.List<Hotel> Hotel
      {
         get
         {
            if (hotel == null)
               hotel = new System.Collections.Generic.List<Hotel>();
            return hotel;
         }
         set
         {
            RemoveAllHotel();
            if (value != null)
            {
               foreach (Hotel oHotel in value)
                  AddHotel(oHotel);
            }
         }
      }
      
      
      public void AddHotel(Hotel newHotel)
      {
         if (newHotel == null)
            return;
         if (this.hotel == null)
            this.hotel = new System.Collections.Generic.List<Hotel>();
         if (!this.hotel.Contains(newHotel))
         {
            this.hotel.Add(newHotel);
            newHotel.User = this;
         }
      }
      
      
      public void RemoveHotel(Hotel oldHotel)
      {
         if (oldHotel == null)
            return;
         if (this.hotel != null)
            if (this.hotel.Contains(oldHotel))
            {
               this.hotel.Remove(oldHotel);
               oldHotel.User = null;
            }
      }
      
      
      public void RemoveAllHotel()
      {
         if (hotel != null)
         {
            System.Collections.ArrayList tmpHotel = new System.Collections.ArrayList();
            foreach (Hotel oldHotel in hotel)
               tmpHotel.Add(oldHotel);
            hotel.Clear();
            foreach (Hotel oldHotel in tmpHotel)
               oldHotel.User = null;
            tmpHotel.Clear();
         }
      }
   
   }
}