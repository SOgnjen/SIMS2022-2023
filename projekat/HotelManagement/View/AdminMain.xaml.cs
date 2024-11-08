﻿using HotelManagement.Model;
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
    /// Interaction logic for AdminMain.xaml
    /// </summary>
    public partial class AdminMain : Window
    {
        App app = (App)Application.Current;

        List<Hotel> AcceptedHotels;

        List<User> AllUsers;

        bool flag = false;

        public AdminMain()
        {
            InitializeComponent();
            AcceptedHotels = app.hotelController.GetByAccepted(true);

            DataContext = this;

            hotelDataGrid.ItemsSource = null;
            hotelDataGrid.ItemsSource = AcceptedHotels;

            AllUsers = app.userController.GetAll();

            userDataGrid.ItemsSource = null;
            userDataGrid.ItemsSource = AllUsers;
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

        private void SearchUsersByType(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedTypeItem = (ComboBoxItem)searchByType.SelectedItem;

            if (selectedTypeItem == null)
            {
                MessageBox.Show("Please select a user type.");
                return;
            }

            UserType selectedType = (UserType)Enum.Parse(typeof(UserType), selectedTypeItem.Tag.ToString());

            List<User> filteredUsers = AllUsers.Where(user => user.Type == selectedType).ToList();

            userDataGrid.ItemsSource = filteredUsers;
        }

        private void BlockUser(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            User user = clickedButton.DataContext as User;

            if (user != null)
            {
                if (user.Type != UserType.Administrator)
                {
                    app.userController.BlockUser(user);
                    userDataGrid.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Cannot block an administrator user.", "Invalid Action", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        private List<User> LoadUsersFromJsonFile()
        {
            string jsonFilePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\users.json";

            if (File.Exists(jsonFilePath))
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonData);
                return users;
            }
            else
            {
                return new List<User>();
            }
        }

        private void SaveUsersToJsonFile(List<User> users)
        {
            string jsonFilePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\users.json";
            string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(jsonFilePath, jsonData);
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            string jmbg = jmbgBox.Text;
            string email = emailBox.Text;
            string password = passwordBox.Text;
            string name = nameBox.Text;
            string surname = surnameBox.Text;
            string phone = phoneBox.Text;

            ComboBoxItem selectedComboBoxItem = userTypeBox.SelectedItem as ComboBoxItem;

            if (string.IsNullOrWhiteSpace(jmbg) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("All fields are required.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UserType type = (UserType)Enum.Parse(typeof(UserType), selectedComboBoxItem.Tag.ToString());

            foreach (var user in AllUsers)
            {
                if (jmbg == user.Jmbg || email == user.Email)
                {
                    MessageBox.Show("User with this email or JMBG already exists!");
                    return;
                }
            }

            app.userController.AddUser(jmbg, email, password, name, surname, phone, type, false);
            MessageBox.Show("User created");

            userDataGrid.Items.Refresh();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void AddHotel(object sender, RoutedEventArgs e)
        {
            string code = codeBox.Text;
            string name = hotelNameBox.Text;
            string builtInText = builtInBox.Text;
            string starsText = starsBox.Text;
            string ownersJmbg = ownersJmbgBox.Text;

            if (!int.TryParse(builtInText, out int builtIn))
            {
                MessageBox.Show("Built In must be a valid number.");
                return;
            }

            if (!int.TryParse(starsText, out int stars))
            {
                MessageBox.Show("Stars must be a valid number.");
                return;
            }

            flag = false;

            foreach (var hotel in AcceptedHotels)
            {
                if (code == hotel.Code)
                {
                    MessageBox.Show("Hotel with this code already exists!");
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                app.hotelController.AddHotel(code, name, builtIn, new Dictionary<string, Apartment>(), stars, ownersJmbg, false, HotelStatus.Waiting);
                MessageBox.Show("Hotel created");
            }

        }
    }
}
