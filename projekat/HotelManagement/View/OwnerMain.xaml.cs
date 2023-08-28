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

        List<Hotel> acceptedHotels;

        List<Hotel> ownersHotels;

        private User loggedUser;

        List<Apartment> allApartments;

        List<Reservation> ownersReservations;

        public OwnerMain(User loggedUser)
        {
            InitializeComponent();
            this.loggedUser = loggedUser;
            acceptedHotels = app.hotelController.GetByAccepted(true);

            string ownersJmbg = loggedUser.Jmbg;

            ownersHotels = app.hotelController.GetByOwnersJmbg(ownersJmbg);


            DataContext = this;

            hotelDataGrid.ItemsSource = null;
            hotelDataGrid.ItemsSource = acceptedHotels;

            myWaitingHotelsDataGrid.ItemsSource = null;
            myWaitingHotelsDataGrid.ItemsSource = ownersHotels;

            ownersAcceptedHotelsComboBox.ItemsSource = ownersHotels.Where(hotel => hotel.Status == HotelStatus.Accepted);

            allApartments = app.apartmentController.GetAll();

            ownersReservations = app.userController.GetAllGetAllReservationsOfOwner(loggedUser.Jmbg);

            reservationDataGrid.ItemsSource = null;
            reservationDataGrid.ItemsSource = ownersReservations;

            ShowOwnersReservations();
        }

        private void SearchHotelsByCode(object sender, RoutedEventArgs e)
        {
            if (searchByCode.Text == "")
            {
                MessageBox.Show("You must enter a valid value.");
            }

            List<Hotel> acceptedHotelsByCode = new List<Hotel>();
            foreach (var h in acceptedHotels)
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
            foreach (var h in acceptedHotels)
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
            foreach (var h in acceptedHotels)
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
            foreach (var h in acceptedHotels)
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

            foreach (var hotel in acceptedHotels)
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

            foreach (var hotel in acceptedHotels)
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

                foreach (var hotel in acceptedHotels)
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

                foreach (var hotel in acceptedHotels)
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

        private void AcceptHotel(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            Hotel hotel = clickedButton.DataContext as Hotel;

            if(hotel != null)
            {
                if(hotel.Status == HotelStatus.Accepted)
                {
                    MessageBox.Show("Hotel already accepted");
                    return;
                }
                hotel.Accepted = true;
                hotel.Status = HotelStatus.Accepted;

                List<Hotel> hotels = LoadHotelsFromJsonFile();

                Hotel existingHotel = hotels.Find(h => h.Code == hotel.Code);
                if(existingHotel != null)
                {
                    existingHotel.Accepted = hotel.Accepted;
                    existingHotel.Status = hotel.Status;

                    SaveHotelsToJsonFile(hotels);

                    MessageBox.Show("Hotel Accepted");

                    RefreshAcceptedHotels();

                    myWaitingHotelsDataGrid.Items.Refresh();
                }
            }
        }

        private void RefreshAcceptedHotels()
        {
            acceptedHotels = app.hotelController.GetByAccepted(true);
            hotelDataGrid.ItemsSource = null;
            hotelDataGrid.ItemsSource = acceptedHotels;

            hotelDataGrid.Items.Refresh();
        }

        private void RefreshAllAppartments()
        {
            allApartments = app.apartmentController.GetAll();
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
                if (hotel.Status == HotelStatus.Accepted)
                {
                    MessageBox.Show("Hotel already accepted");
                    return;
                }
                hotel.Accepted = false;
                hotel.Status = HotelStatus.Declined;

                List<Hotel> hotels = LoadHotelsFromJsonFile();

                Hotel existingHotel = hotels.Find(h => h.Code == hotel.Code);
                if (existingHotel != null)
                {
                    existingHotel.Accepted = hotel.Accepted;
                    existingHotel.Status = hotel.Status;

                    SaveHotelsToJsonFile(hotels);

                    MessageBox.Show("Hotel Declined");

                    RefreshOwnersHotels(loggedUser);
                }
            }
        }

        private void RefreshOwnersHotels(User loggedUser)
        {
            ownersHotels = app.hotelController.GetByOwnersJmbg(loggedUser.Jmbg);
            myWaitingHotelsDataGrid.ItemsSource = null;
            myWaitingHotelsDataGrid.ItemsSource = ownersHotels;

            myWaitingHotelsDataGrid.Items.Refresh();
        }

        private void AddApartment(object sender, RoutedEventArgs e)
        {
            string hotelCode = (string)ownersAcceptedHotelsComboBox.SelectedValue;
            string apartmentNumber = apartmentNumberBox.Text;
            string name = apartmentNameBox.Text;
            string description = descriptionBox.Text;

            bool apartmentExists = allApartments.Any(apartment =>
                apartment.ApartmentNumber == apartmentNumber || apartment.Name == name);

            if (apartmentExists)
            {
                MessageBox.Show("An apartment with the same name or apartment number already exists.");
                return;
            }

            if (!int.TryParse(roomsBox.Text, out int rooms))
            {
                MessageBox.Show("Invalid value for rooms.");
                return;
            }

            if (!int.TryParse(maxGuestsBox.Text, out int maxGuests))
            {
                MessageBox.Show("Invalid value for max guests.");
                return;
            }

            app.apartmentController.CreateApartment(apartmentNumber, name, description, rooms, maxGuests, hotelCode);

            RefreshAcceptedHotels();
            RefreshOwnersHotels(loggedUser);

            MessageBox.Show("Apartment Created");
        }

        private void ShowOwnersReservations()
        {
            StringBuilder reservationInfo = new StringBuilder();

            foreach (var reservation in ownersReservations)
            {
                string reservationStatus = reservation.Status == ReservationStatus.Accepted ? "Accepted" : "Declined";
                string reservationDetails = $"Reservation ID: {reservation.Id}\nDate: {reservation.Date}\nStatus: {reservationStatus}\nDeclined Because: {reservation.DeclinedBecause}\n\n";

                reservationInfo.Append(reservationDetails);
            }

            if (reservationInfo.Length == 0)
            {
                MessageBox.Show("You don't have any reservations.");
            }
            else
            {
                MessageBox.Show($"Your reservations:\n\n{reservationInfo.ToString()}");
            }
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

            List<Reservation> filteredReservations = ownersReservations.Where(reservation => reservation.Status == selectedStatus).ToList();

            reservationDataGrid.ItemsSource = filteredReservations;
        }
    }
}
