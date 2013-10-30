using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PizzaMapPoint.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void SetObservableValues<T>(
            ObservableCollection<T> observables, 
            IEnumerable<T> values)
        {
            if (observables != values)
            {
                observables.Clear();
                foreach (var value in values)
                {
                    observables.Add(value);
                }
            }
        }
    }
}