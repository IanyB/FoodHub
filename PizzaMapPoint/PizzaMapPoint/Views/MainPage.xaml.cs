// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using System;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PizzaMapPoint.ViewModels;

namespace PizzaMapPoint.Views
{
    public static class GlobalConfig
    {
        public static PizzaRestaurantViewModel SelectedItem;
        public static Geolocator Geolocator = null;
        public static double Longitude;
        public static double Latitude;
        public static double PizzaPlaceLongitude;
        public static double PizzaPlaceLatitude;
        public static double SearchLonngitude;
        public static double SearchLatitude;
        public static string QueryText;
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            GlobalConfig.Geolocator = new Geolocator();

            GetGeolocation(null, null);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        async private void GetGeolocation(object sender, RoutedEventArgs e)
        {
            try
            {
                Geoposition pos = await GlobalConfig.Geolocator.GetGeopositionAsync();

                GlobalConfig.Latitude = pos.Coordinate.Latitude;
                GlobalConfig.Longitude = pos.Coordinate.Longitude;
            }
            catch (Exception)
            {
                Helper.ErrorMessage.MessageBox("Please check your internet connection and try again later.");
            }


            //ScenarioOutput_Accuracy.Text = pos.Coordinate.Accuracy.ToString();
        }

        private async void GeolocationTapped(object sender, TappedRoutedEventArgs e)
        {
            //await PizzaPlace.ViewModels.DataSource.LoadData();
            this.Frame.Navigate(typeof(PizzaPlacesPage));
        }

        private void Hover(object sender, PointerRoutedEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(Colors.CornflowerBlue);
        }

        private void BackHover(object sender, PointerRoutedEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(new Color(){R = 0xDB, G = 0xEA, B = 0xF9, A = 0xff});
        }
    }
}
