using System.Windows;
using HotelManagement.Model;
using HotelManagement.View;
using MaterialDesignThemes.Wpf;

namespace HotelManagement
{
    public partial class MainWindow : Window
    {
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        public App app = (App)Application.Current;

        public User loggedUser;
        public int counter;

        public MainWindow()
        {
            InitializeComponent();
            counter = 0;
        }

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if(IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            loggedUser = app.userController.getByEmail(email);

            if(counter == 2)
            {
                if (loggedUser == null || loggedUser.Password != password)
                {
                    MessageBox.Show("Too many unsuccessful login attempts. Closing the application.");
                    Application.Current.Shutdown();
                    return;
                }
            }

            if(loggedUser == null || loggedUser.Blocked) 
            {
                MessageBox.Show("Invalid email or user blocked");
                counter++;
            }
            else if(loggedUser.Password != password)
            {
                MessageBox.Show("Invalid password");
                counter++;
            }
            else
            {
                if(loggedUser.Type == 0)
                {
                    this.Hide();
                    AdminMain adminMain = new AdminMain();
                    adminMain.Show();
                }
                else if(loggedUser.Type == (UserType)1)
                {
                    this.Hide();
                    GuestMain guestMain = new GuestMain(loggedUser);
                    guestMain.Show();
                }
                else
                {
                    this.Hide();
                    OwnerMain ownerMain = new OwnerMain(loggedUser);
                    ownerMain.Show();
                }
            }
            
        }
    }
}
