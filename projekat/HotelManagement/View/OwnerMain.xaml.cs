using HotelManagement.Model;
using HotelManagement.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        List<Hotel> OwnersHotels;

        private User LoggedUser;

        List<Apartment> AllApartments;

        List<Reservation> OwnersReservations;

        public OwnerMain(User loggedUser)
        {
            InitializeComponent();
            this.LoggedUser = loggedUser;
            AcceptedHotels = app.hotelController.GetByAccepted(true);

            string ownersJmbg = loggedUser.Jmbg;

            OwnersHotels = app.hotelController.GetByOwnersJmbg(LoggedUser.Jmbg);


            DataContext = this;

            hotelDataGrid.ItemsSource = null;
            hotelDataGrid.ItemsSource = AcceptedHotels;

            myWaitingHotelsDataGrid.ItemsSource = null;
            myWaitingHotelsDataGrid.ItemsSource = OwnersHotels;

            ownersAcceptedHotelsComboBox.ItemsSource = OwnersHotels.Where(hotel => hotel.Status == HotelStatus.Accepted);

            AllApartments = app.apartmentController.GetAll();

            OwnersReservations = app.userController.GetAllReservationsOfOwner(LoggedUser.Jmbg);

            reservationDataGrid.ItemsSource = null;
            reservationDataGrid.ItemsSource = OwnersReservations;

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
                            break;
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
                            break;
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

        private void AcceptHotel(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            Hotel hotel = clickedButton.DataContext as Hotel;

            if (hotel != null)
            {
                bool accepted = app.hotelController.AcceptHotel(hotel.Code);

                if (accepted)
                {
                    MessageBox.Show("Hotel Accepted");
                    RefreshAcceptedHotels();
                    myWaitingHotelsDataGrid.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Hotel cannot be accepted");
                }
            }
        }


        private void RefreshAcceptedHotels()
        {
            AcceptedHotels = app.hotelController.GetByAccepted(true);
            hotelDataGrid.ItemsSource = null;
            hotelDataGrid.ItemsSource = AcceptedHotels;

            hotelDataGrid.Items.Refresh();
        }

        private void RefreshAllAppartments()
        {
            AllApartments = app.apartmentController.GetAll();
        }

        private List<Hotel> LoadHotelsFromJsonFile()
        {
            string jsonFilePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\hotels.json";

            if (File.Exists(jsonFilePath))
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                List<Hotel> hotels = JsonConvert.DeserializeObject<List<Hotel>>(jsonData);
                return hotels;
            }
            else
            {
                return new List<Hotel>();
            }
        }

        private void SaveHotelsToJsonFile(List<Hotel> hotels)
        {
            string jsonFilePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\hotels.json";
            string jsonData = JsonConvert.SerializeObject(hotels, Formatting.Indented);
            File.WriteAllText(jsonFilePath, jsonData);
        }

        private void DeclineHotel(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            Hotel hotel = clickedButton.DataContext as Hotel;

            if (hotel != null)
            {

                bool declined = app.hotelController.DeclineHotel(hotel.Code);

                if (declined)
                {
                    MessageBox.Show("Hotel Declined");
                    RefreshAcceptedHotels();
                    myWaitingHotelsDataGrid.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Hotel cannot be declined");
                }
            }
        }

        private void RefreshOwnersHotels(User loggedUser)
        {
            OwnersHotels = app.hotelController.GetByOwnersJmbg(loggedUser.Jmbg);
            myWaitingHotelsDataGrid.ItemsSource = null;
            myWaitingHotelsDataGrid.ItemsSource = OwnersHotels;

            myWaitingHotelsDataGrid.Items.Refresh();
        }

        private void AddApartment(object sender, RoutedEventArgs e)
        {
            string hotelCode = (string)ownersAcceptedHotelsComboBox.SelectedValue;
            string apartmentNumber = apartmentNumberBox.Text;
            string name = apartmentNameBox.Text;
            string description = descriptionBox.Text;
            string roomsText = roomsBox.Text;
            string maxGuestsText = maxGuestsBox.Text;

            if (string.IsNullOrWhiteSpace(apartmentNumber) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(roomsText) ||
                string.IsNullOrWhiteSpace(maxGuestsText) || hotelCode == null)
            {
                MessageBox.Show("All fields are required.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool apartmentExists = AllApartments.Any(apartment =>
                apartment.ApartmentNumber == apartmentNumber || apartment.Name == name);

            if (apartmentExists)
            {
                MessageBox.Show("An apartment with the same name or apartment number already exists.");
                return;
            }

            if (!int.TryParse(roomsText, out int rooms) || rooms <= 0)
            {
                MessageBox.Show("Invalid value for rooms.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(maxGuestsText, out int maxGuests) || maxGuests <= 0)
            {
                MessageBox.Show("Invalid value for max guests.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            app.apartmentController.CreateApartment(apartmentNumber, name, description, rooms, maxGuests, hotelCode);

            RefreshAcceptedHotels();
            hotelDataGrid.Items.Refresh();

            RefreshOwnersHotels(LoggedUser);
            myWaitingHotelsDataGrid.Items.Refresh();

            MessageBox.Show("Apartment Created");
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedStatusItem = (ComboBoxItem)searchByReservationStatus.SelectedItem;

            if (selectedStatusItem == null)
            {
                MessageBox.Show("Please select a reservation status.");
                return;
            }

            ReservationStatus selectedStatus = (ReservationStatus)Enum.Parse(typeof(ReservationStatus), selectedStatusItem.Tag.ToString());

            List<Reservation> filteredReservations = OwnersReservations.Where(reservation => reservation.Status == selectedStatus).ToList();

            reservationDataGrid.ItemsSource = filteredReservations;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Hotel selectedHotel = button.DataContext as Hotel;

            HotelsReservations hotelDetailsWindow = new HotelsReservations(selectedHotel);

            List<Reservation> filteredReservations = new List<Reservation>();

            foreach (var apartment in selectedHotel.Apartments.Values)
            {
                filteredReservations.AddRange(OwnersReservations
                    .Where(reservation => reservation.ApartmentName == apartment.Name));
            }

            hotelDetailsWindow.reservationsDataGrid.ItemsSource = filteredReservations;

            hotelDetailsWindow.Owner = this;
            hotelDetailsWindow.ShowDialog();
        }

    }
}
