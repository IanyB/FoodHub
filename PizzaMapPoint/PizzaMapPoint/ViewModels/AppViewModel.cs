using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaMapPoint.HttpRequester;
using PizzaMapPoint.Models;
using PizzaMapPoint.Views;

namespace PizzaMapPoint.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        private ObservableCollection<PizzaRestaurantViewModel> pizzaPlaces;

        public IEnumerable<PizzaRestaurantViewModel> PizzaPlaces
        {
            get
            {
                if (this.pizzaPlaces == null)
                {
                    this.pizzaPlaces = new ObservableCollection<PizzaRestaurantViewModel>();
                    this.GetData();

                }
                return this.pizzaPlaces;
            }
            set
            {
                if (this.pizzaPlaces == null)
                {
                    this.pizzaPlaces = new ObservableCollection<PizzaRestaurantViewModel>();
                }
                this.SetObservableValues(this.pizzaPlaces, value);
            }
        }

        private async void GetData()
        {
            try
            {
                this.PizzaPlaces = await HttpRequest.LoadRemoteDataAsync(GlobalConfig.Longitude, GlobalConfig.Latitude);

            }
            catch (Exception)
            {
                Helper.ErrorMessage.MessageBox("Please check your internet connection and try again later.");
            }
        }

    }
}
