using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelManagement.View
{
    /// <summary>
    /// Interaction logic for OwnerMain.xaml
    /// </summary>
    public partial class OwnerMain : Window
    {
        App app = (App)Application.Current;

        List<Hotel> AcceptedHotels;

        public OwnerMain()
        {
            InitializeComponent();
            AcceptedHotels = app.hotelController.GetByAccepted(true);

            DataContext = this;

            hotelDataGrid.ItemsSource = null;
            hotelDataGrid.ItemsSource = AcceptedHotels;
        }

        private void SearchHotelsByCode(object sender, RoutedEventArgs e)
        {
            if (searchByCode.Text == "")
            {
                MessageBox.Show("You must enter a valid value.");
            }

            List<Hotel> acceptedHotelsByCode = new List<Hotel>();
            foreach (var h in AcceptedHotels)
            {
                if (h.Code.ToLower().Contains(searchByCode.Text.ToLower()))
                {
                    acceptedHotelsByCode.Add(h);
                }
            }
            hotelDataGrid.ItemsSource = acceptedHotelsByCode;
        }

        private void SearchHotelsByName(object sender, RoutedEventArgs e)
        {
            if (searchByName.Text == "")
            {
                MessageBox.Show("You must enter a valid value.");
            }

            List<Hotel> AcceptedHotelsByName = new List<Hotel>();
            foreach (var h in AcceptedHotels)
            {
                if (h.Name.ToLower().Contains(searchByName.Text.ToLower()))
                {
                    AcceptedHotelsByName.Add(h);
                }
            }
            hotelDataGrid.ItemsSource = AcceptedHotelsByName;
        }

        private void SearchHotelByBuiltIn(object sender, RoutedEventArgs e)
        {
            if (searchByBuiltIn.Text == "")
            {
                MessageBox.Show("You must enter a valid value.");
            }

            List<Hotel> acceptedHotelsByBuiltIn = new List<Hotel>();
            foreach (var h in AcceptedHotels)
            {
                try
                {
                    if (h.BuiltIn == Int32.Parse(searchByBuiltIn.Text))
                    {
                        acceptedHotelsByBuiltIn.Add(h);
                    }
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("You must enter a valid value.");
                }
            }
            hotelDataGrid.ItemsSource = acceptedHotelsByBuiltIn;
        }

        private void SearchHotelByStars(object sender, RoutedEventArgs e)
        {
            if (searchByStars.Text == "")
            {
                MessageBox.Show("You must enter a valid value.");
            }

            List<Hotel> acceptedHotelsByStars = new List<Hotel>();
            foreach (var h in AcceptedHotels)
            {
                try
                {
                    if (h.Stars == Int32.Parse(searchByStars.Text))
                    {
                        acceptedHotelsByStars.Add(h);
                    }
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("You must enter a valid value.");
                }
            }
            hotelDataGrid.ItemsSource = acceptedHotelsByStars;
        }

        private void SearchHotelByRoomNumber(object sender, RoutedEventArgs e)
        {
            if (searchByRoomNumber.Text == "")
            {
                MessageBox.Show("You must enter a valid value.");
                return;
            }

            List<Hotel> acceptedHotelsByRoomNumber = new List<Hotel>();
            int desiredRoomNumber;

            if (!Int32.TryParse(searchByRoomNumber.Text, out desiredRoomNumber))
            {
                MessageBox.Show("You must enter a valid value.");
                return;
            }

            foreach (var hotel in AcceptedHotels)
            {
                foreach (var apartment in hotel.Apartments.Values)
                {
                    if (apartment.Rooms == desiredRoomNumber)
                    {
                        acceptedHotelsByRoomNumber.Add(hotel);
                        break;
                    }
                }
            }

            hotelDataGrid.ItemsSource = acceptedHotelsByRoomNumber;
        }

        private void SearchHotelByMaxGuestNumber(object sender, RoutedEventArgs e)
        {
            if (searchByMaxGuestNumber.Text == "")
            {
                MessageBox.Show("You must enter a valid value.");
                return;
            }

            List<Hotel> acceptedHotelsByMaxGuestNumber = new List<Hotel>();
            int desiredMaxGuestNumber;

            if (!Int32.TryParse(searchByMaxGuestNumber.Text, out desiredMaxGuestNumber))
            {
                MessageBox.Show("You must enter a valid value.");
                return;
            }

            foreach (var hotel in AcceptedHotels)
            {
                foreach (var apartment in hotel.Apartments.Values)
                {
                    if (apartment.MaxGuests == desiredMaxGuestNumber)
                    {
                        acceptedHotelsByMaxGuestNumber.Add(hotel);
                        break;
                    }
                }
            }

            hotelDataGrid.ItemsSource = acceptedHotelsByMaxGuestNumber;
        }

        private void SearchHotelByRoomsAndMaxGuests(object sender, RoutedEventArgs e)
        {
            string searchText = searchByRoomsAndMaxGuests.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("You must enter a valid value.");
                return;
            }

            string[] orParts = searchText.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            string[] andParts = searchText.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            List<Hotel> hotelsWithMatchingApartments = new List<Hotel>();

            if (orParts.Length == 2)
            {
                int roomCondition;
                int maxGuestsCondition;

                if (!int.TryParse(orParts[0].Trim(), out roomCondition) || roomCondition <= 0)
                {
                    MessageBox.Show("Invalid room number condition.");
                    return;
                }

                if (!int.TryParse(orParts[1].Trim(), out maxGuestsCondition) || maxGuestsCondition <= 0)
                {
                    MessageBox.Show("Invalid max guests condition.");
                    return;
                }

                foreach (var hotel in AcceptedHotels)
                {
                    foreach (var apartment in hotel.Apartments.Values)
                    {
                        if (apartment.Rooms == roomCondition ||
                            apartment.MaxGuests == maxGuestsCondition)
                        {
                            hotelsWithMatchingApartments.Add(hotel);
                            break; // Break once we find a matching apartment in this hotel
                        }
                    }
                }
            }
            else if (andParts.Length == 2)
            {
                int roomAndCondition;
                int maxGuestsAndCondition;

                if (!int.TryParse(andParts[0].Trim(), out roomAndCondition) || roomAndCondition <= 0)
                {
                    MessageBox.Show("Invalid room number condition.");
                    return;
                }

                if (!int.TryParse(andParts[1].Trim(), out maxGuestsAndCondition) || maxGuestsAndCondition <= 0)
                {
                    MessageBox.Show("Invalid max guests condition.");
                    return;
                }

                foreach (var hotel in AcceptedHotels)
                {
                    foreach (var apartment in hotel.Apartments.Values)
                    {
                        if (apartment.Rooms == roomAndCondition &&
                            apartment.MaxGuests == maxGuestsAndCondition)
                        {
                            hotelsWithMatchingApartments.Add(hotel);
                            break; // Break once we find a matching apartment in this hotel
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid expression format.");
                return;
            }

            hotelDataGrid.ItemsSource = hotelsWithMatchingApartments;
        }












    }
}
