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

        List<Hotel> AllHotels;

        public OwnerMain()
        {
            InitializeComponent();
            AllHotels = app.hotelController.GetAll();

            DataContext = this;

            hotelDataGrid.ItemsSource = null;
            hotelDataGrid.ItemsSource = AllHotels;
        }
    }
}
