using HotelManagement.Controller;
using HotelManagement.Repository;
using HotelManagement.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly UserRepository userRepository = new UserRepository();
        private static readonly HotelRepository hotelRepository = new HotelRepository();
        private static readonly ApartmentRepository apartmentRepository = new ApartmentRepository();
        private static readonly ReservationRepository reservationRepository = new ReservationRepository();

        private static readonly UserService userService = new UserService(userRepository);
        private static readonly HotelService hotelService = new HotelService(hotelRepository);
        private static readonly ApartmentService apartmentService = new ApartmentService(apartmentRepository);
        private static readonly ReservationService reservationService = new ReservationService(reservationRepository);

        public readonly UserController userController = new UserController(userService);
        public readonly HotelController hotelController = new HotelController(hotelService);
        public readonly ApartmentController apartmentController = new ApartmentController(apartmentService);
        public readonly ReservationController reservationController = new ReservationController(reservationService);

    }
}
