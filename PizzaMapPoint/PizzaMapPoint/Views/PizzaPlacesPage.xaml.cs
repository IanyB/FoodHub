// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using PizzaMapPoint.Common;
using PizzaMapPoint.ViewModels;

namespace PizzaMapPoint.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class PizzaPlacesPage : PizzaMapPoint.Common.LayoutAwarePage
    {
        public PizzaPlacesPage()
        {
            //var erer = GlobalConfig.Latitude;
            this.InitializeComponent();

            var statusText = Windows.Storage.ApplicationData.Current.RoamingSettings.Values["lastSearch"];
            LastSearch.Text = "Last Search: " + statusText;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            if (pageState != null)
            {
                if (pageState.ContainsKey("currentInput"))
                {
                    ResultPlaces.Name = (string)pageState["currentInput"];
                }
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            pageState["currentInput"] = ResultPlaces.Name;
        }

        private void PizzaMapTapped(object sender, TappedRoutedEventArgs tappedRoutedEventArgs)
        {
            var listView = sender as ListView;
            if (listView != null)
            {
                PizzaRestaurantViewModel data = listView.SelectedItem as PizzaRestaurantViewModel;
                GlobalConfig.SelectedItem = data;
                if (data != null)
                {
                    GlobalConfig.PizzaPlaceLatitude = data.PizzaLatitude;
                    GlobalConfig.PizzaPlaceLongitude = data.PizzaLongitude;
                    //this.Frame.Navigate(typeof(PizzaMapPoint.PizzaMap));
                    this.Frame.Navigate(typeof(PizzaMap), data);
                }
            }
        }
    }
}
