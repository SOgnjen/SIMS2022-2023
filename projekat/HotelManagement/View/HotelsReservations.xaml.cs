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
        App app = (App)Application.Current;
        private List<Reservation> reservations;
        private string fileLocation;

        public HotelsReservations(Hotel selectedHotel)
        {
            InitializeComponent();

            Title = "Hotel Details - " + selectedHotel.Name;

        }

        private void AcceptReservation(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            Reservation reservation = clickedButton.DataContext as Reservation;

            if (reservation != null)
            {
                bool accepted = app.reservationController.AcceptReservation(reservation.Id);

                if (accepted)
                {
                    MessageBox.Show("Reservation Accepted");
                    reservationsDataGrid.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Reservation cannot be accepted");
                }
            }
        }

        private void DeclineReservation(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            Reservation reservation = clickedButton.DataContext as Reservation;

            if (reservation != null)
            {
                StackPanel stackPanel = clickedButton.Parent as StackPanel;
                TextBox declinedBecauseTextBox = stackPanel.FindName("declinedBecauseTextBox") as TextBox;

                if (declinedBecauseTextBox != null)
                {
                    string declinedBecause = declinedBecauseTextBox.Text.Trim();

                    bool declined = app.reservationController.DeclineReservation(reservation.Id, declinedBecause);

                    if (declined)
                    {
                        MessageBox.Show("Reservation Declined");
                        reservationsDataGrid.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Reservation cannot be declined");
                    }
                }
            }
        }

    }
}
