// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Bing.Maps;

namespace PizzaMapPoint.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class PizzaMap : PizzaMapPoint.Common.LayoutAwarePage
    {
        Location location;
        PositionLocator pos;
        private DataTransferManager dataTransferManager;

        public PizzaMap() 
        {
            this.InitializeComponent();
            DataContext = GlobalConfig.SelectedItem;
            pos = new PositionLocator();
            myMap.Children.Add(pos);
            location = new Location(GlobalConfig.PizzaPlaceLatitude, GlobalConfig.PizzaPlaceLongitude);
            MapLayer.SetPosition(pos, location);
            myMap.SetView(location, 18.0f);
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            //S1: Set the Default display for the MAp Type
            myMap.MapType = MapType.Aerial;
            //S2: Set the properties for the Map
            myMap.ShowBuildings = true;
            myMap.ShowTraffic = true;
            // myMap.ViewRestriction = MapViewRestriction.ZoomOutToWholeWorld;
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
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private async void Save(object sender, RoutedEventArgs e)
        {
            var title = Title.Text;
            var name = Name.Text;
            var phone = Phone.Text;
            var address = Address.Text;
            var url = Site.Text;
            var distance = Distance.Text;
            var city = City.Text;
            var country = Country.Text;

            var sb = new StringBuilder();
            sb.AppendLine(title + "; ");
            sb.AppendLine("Name: " + name + "; ");
            sb.AppendLine("Phone: " + phone + "; ");
            sb.AppendLine("Address: " + address + "; ");
            sb.AppendLine("Site: " + url + "; ");
            sb.AppendLine("Distance: " + distance + "; ");
            sb.AppendLine("City: " + city + "; ");
            sb.AppendLine("Country: " + country + "; ");

            var savePicker = new Windows.Storage.Pickers.FileSavePicker();

            var plainTextFileTypes = new List<string>(new string[]{".txt"});

            var webPageFileTypes = new List<string>(new string[]{".html", ".htm"});

            savePicker.FileTypeChoices.Add(
                new KeyValuePair<string, IList<string>>("Plain Text", plainTextFileTypes)
                );
            savePicker.FileTypeChoices.Add(
                new KeyValuePair<string, IList<string>>("Web Page", webPageFileTypes)
                );

            var saveFile = await savePicker.PickSaveFileAsync();

            if (saveFile != null)
            {
                if (saveFile.FileType == ".html" || saveFile.FileType == ".htm")
                {
                    var text = new StringBuilder();
                    text.Append("<html><head><title>Saved By FileSavePicker</title></head><meta charset=\"utf-8\"><body><p>" + sb + "</p></body>");
                    await Windows.Storage.FileIO.WriteTextAsync(saveFile, text.ToString());
                    await new Windows.UI.Popups.MessageDialog("File Saved!").ShowAsync();
                }
                else
                {
                    await Windows.Storage.FileIO.WriteTextAsync(saveFile, sb.ToString());
                    await new Windows.UI.Popups.MessageDialog("File Saved!").ShowAsync();
                }

            }
        }

        private void GetShareContent(DataTransferManager sender, DataRequestedEventArgs e)
        {
            var htmlExample = string.Format(
                "<p>Name: {0}<br />Phone: {1}<br />Address: {2}<br />Site: {3}<br />Distance: {4}<br />City: {5}<br />Country: {6}</p>",
                Name.Text, Phone.Text,
                Address.Text, Site.Text,
                Distance.Text,
                City.Text, Country.Text);
            var request = e.Request;
            request.Data.Properties.Title = Title.Text;
            request.Data.Properties.Description = "Try FoodHub is Great!";
            var htmlFormat = Windows.ApplicationModel.DataTransfer.HtmlFormatHelper.CreateHtmlFormat(htmlExample);
            request.Data.SetHtmlFormat(htmlFormat);
        }

        //private void share_DataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        //{
        //    var localImage = "ms-appx:///Assets/dog/dog.png";
        //    var htmlExample = string.Format(
        //        "<img src=\"{0}\"><br /><p>Name: {1}<br />Age: {2}<br />Hunger: {3}<br />Energy: {4}<br />Hygiene: {5}<br />Happiness: {6}<br />Love: {7}</p>",
        //        localImage, this.Model.Tamagotchi.Name, this.Model.Tamagotchi.Age,
        //        this.Model.Tamagotchi.Hunger, this.Model.Tamagotchi.Energy,
        //        this.Model.Tamagotchi.Hygiene, this.Model.Tamagotchi.Happiness, this.Model.Tamagotchi.Love);
        //    var request = e.Request;
        //    request.Data.Properties.Title = string.Format("{0}'s pet.", DataPersister.Username);
        //    request.Data.Properties.Description = "Share you're current pet.";
        //    var htmlFormat = Windows.ApplicationModel.DataTransfer.HtmlFormatHelper.CreateHtmlFormat(htmlExample);
        //    request.Data.SetHtmlFormat(htmlFormat);
        //    var streamRef = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromUri(new Uri(localImage));
        //    request.Data.ResourceMap[localImage] = streamRef;
        //}

        //private bool GetShareContent(DataRequest request)
        //{
        //    bool succeeded = false;

        //    var text = Name.Text + " at " + Address.Text + ", " + City.Text + "Tel: " + Phone.Text;

        //    string dataPackageText = text;
        //    if (!String.IsNullOrEmpty(dataPackageText))
        //    {
        //        DataPackage requestData = request.Data;
        //        requestData.Properties.Title = "Check out that new Place!";
        //        requestData.Properties.Description = "Try FoodHub is Great!"; // The description is optional.
        //        requestData.SetText(dataPackageText);
        //        succeeded = true;
        //    }
        //    else
        //    {
        //        request.FailWithDisplayText("Enter the text you would like to share and try again.");
        //    }
        //    return succeeded;
        //}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.dataTransferManager = DataTransferManager.GetForCurrentView();
            this.dataTransferManager.DataRequested += GetShareContent;
       }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.dataTransferManager.DataRequested -= GetShareContent;
        }

        private void Hover(object sender, PointerRoutedEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(Colors.CornflowerBlue);
        }

        private void BackHover(object sender, PointerRoutedEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(new Color() { R = 0xDB, G = 0xEA, B = 0xF9, A = 0xff });
        }
    }
}
