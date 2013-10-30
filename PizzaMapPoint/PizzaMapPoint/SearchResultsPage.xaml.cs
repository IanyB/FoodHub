using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The Search Contract item template is documented at http://go.microsoft.com/fwlink/?LinkId=234240
using PizzaMapPoint.HttpRequester;
using PizzaMapPoint.ViewModels;
using PizzaMapPoint.Views;

namespace PizzaMapPoint
{
    // TODO: Edit the manifest to enable searching
    //
    // The package manifest could not be automatically updated.  Open the package manifest
    // file and ensure that support for activation for searching is enabled.
    /// <summary>
    /// This page displays search results when a global search is directed to this application.
    /// </summary>
    public sealed partial class SearchResultsPage : PizzaMapPoint.Common.LayoutAwarePage
    {
        public SearchResultsPage()
        {
            this.InitializeComponent();

            try
            {
                Task.Delay(19000).ContinueWith(async _ =>
                {
                    DataContext = await HttpRequest.LoadRemoteDataAsync(GlobalConfig.SearchLonngitude, GlobalConfig.SearchLatitude);
                }
    );

                queryText.Text = GlobalConfig.QueryText;

                var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
                roamingSettings.Values["lastSearch"] = queryText.Text;
            }
            catch (Exception)
            {
                Helper.ErrorMessage.MessageBox("Please check your internet connection and try again later.");
            }

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
                    ResultsSearch.Name = (string)pageState["currentInput"];
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
            pageState["currentInput"] = ResultsSearch.Name;
        }

        private void PizzaMapSearchTapped(object sender, TappedRoutedEventArgs tappedRoutedEventArgs)
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
