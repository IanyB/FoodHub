using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaMapPoint.HttpRequester;
using PizzaMapPoint.Views;

namespace PizzaMapPoint.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private ObservableCollection<PizzaRestaurantViewModel> seachItem;

        public IEnumerable<PizzaRestaurantViewModel> SeachItemPlaces
        {
            get
            {
                if (this.seachItem == null)
                {
                    this.seachItem = new ObservableCollection<PizzaRestaurantViewModel>();
                    this.GetData();

                }
                return this.seachItem;
            }
            set
            {
                if (this.seachItem == null)
                {
                    this.seachItem = new ObservableCollection<PizzaRestaurantViewModel>();
                }
                this.SetObservableValues(this.seachItem, value);
            }
        }

        private async void GetData()
        {
            try
            {
                this.SeachItemPlaces = await HttpRequest.LoadRemoteDataAsync(GlobalConfig.SearchLonngitude, GlobalConfig.SearchLatitude);

            }
            catch (Exception)
            {
                Helper.ErrorMessage.MessageBox("Please check your internet connection and try again later.");
            }
        }
    }
}
