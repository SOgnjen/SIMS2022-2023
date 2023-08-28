using HotelManagement.Model;
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
    /// Interaction logic for HotelsReservations.xaml
    /// </summary>
    public partial class HotelsReservations : Window
    {
        private List<Reservation> reservations;
        private string fileLocation;

        public HotelsReservations(Hotel selectedHotel)
        {
            InitializeComponent();

            Title = "Hotel Details - " + selectedHotel.Name;

            // Set the file location for reservations
            fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\reservations.json";

            // Load reservations from JSON file
            LoadReservations();
        }

        private void LoadReservations()
        {
            // Read reservations from the JSON file
            ReadJson();

            // Bind reservations to the data grid
            reservationsDataGrid.ItemsSource = reservations;
        }

        private void ReadJson()
        {
            if (!File.Exists(fileLocation))
            {
                File.Create(fileLocation).Close();
            }

            StreamReader r = new StreamReader(fileLocation);
            string json = r.ReadToEnd();
            r.Close();

            if (!string.IsNullOrEmpty(json))
            {
                reservations = JsonConvert.DeserializeObject<List<Reservation>>(json);
            }
            else
            {
                reservations = new List<Reservation>();
            }
        }

        private void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(reservations, Formatting.Indented);
            File.WriteAllText(fileLocation, json);
        }

        private void AcceptReservation(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Reservation reservation = button.Tag as Reservation;

            if (reservation.Status == ReservationStatus.Waiting)
            {
                reservation.Status = ReservationStatus.Accepted;

                // Refresh the data grid
                reservationsDataGrid.Items.Refresh();

                // Save changes to JSON file
                WriteToJson();
            }
            else
            {
                MessageBox.Show("Cannot accept reservation that is not in Waiting status.", "Invalid Action", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
