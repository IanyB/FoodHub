using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMapPoint.ViewModels
{
    public class PizzaRestaurantViewModel
    {
        private string name;
        private string phone;
        private string address;
        private string distance;
        private string city;
        private string country;
        private readonly double pizzaLongitude;
        private readonly double pizzaLatitude;
        private string site;

        public PizzaRestaurantViewModel()
        {
        }

        public PizzaRestaurantViewModel(string name, string phone, string address, string distance, string country, 
            double pizzaLongitude, double pizzaLatitude, string site, string city)
        {
            this.site = site; 
            this.name = name;
            this.phone = phone;
            this.address = address;
            this.distance = distance;
            this.city = city;
            this.country = country;
            this.site = site;
            this.pizzaLongitude = pizzaLongitude;
            this.pizzaLatitude = pizzaLatitude;
        }

        public string Name
        {
            get
            {
                if (this.name == null)
                {
                    this.name = "No Information";
                }
                return this.name;
            }
        }

        public string Phone
        {
            get
            {
                if (this.phone == null)
                {
                    this.phone = "No Information";
                }
                return this.phone;
            }
        }

        public string Address
        {
            get
            {
                if (this.address == null)
                {
                    this.address = "No Information";
                }
                return this.address;
            }
        }

        public string Distance
        {
            get
            {
                if (this.distance == null)
                {
                    this.distance = "No Information";
                }
                return this.distance;
            }
        }

        public string City
        {
            get
            {
                if (this.city == null)
                {
                    this.city = "No Information";               
                }
                return this.city;
            }
        }

        public string Country
        {
            get
            {
                if (this.country == null)
                {
                    this.country = "No Information";
                }
                return this.country;
            }
        }

        public string Site
        {
            get
            {
                if (this.site == null)
                {
                    this.site = "No Information";               
                }
                return this.site;
            }
        }

        public double PizzaLongitude
        {
            get { return pizzaLongitude; }
        }

        public double PizzaLatitude
        {
            get { return pizzaLatitude; }
        }
    }
}
