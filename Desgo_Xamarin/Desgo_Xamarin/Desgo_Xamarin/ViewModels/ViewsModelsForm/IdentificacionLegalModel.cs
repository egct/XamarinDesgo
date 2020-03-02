using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Desgo_Xamarin.ViewModels.ViewsModelsForm
{
    class IdentificacionLegalModel: INotifyPropertyChanged
    {

        public List<City> CitiesList { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _myCity;
        public string MyCity
        {
            get { return _myCity; }
            set
            {
                if (_myCity != value)
                {
                    _myCity = value;
                    OnPropertyChanged();
                }
            }
        }

        private City _selectedCity { get; set; }
        public City SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                if (_selectedCity != value)
                {
                    _selectedCity = value;
                    // Do whatever functionality you want...When a selectedItem is changed..
                    // write code here..
                    MyCity = "Selected : " + _selectedCity.Value;
                }
            }
        }

        public IdentificacionLegalModel()
        {
            CitiesList = GetCities().OrderBy(t => t.Value).ToList();
            MyCity = "Selected City : ";
        }
        public List<City> GetCities()
        {
            var cities = new List<City>()
            {
                new City(){Key =  1, Value= "Soltero/a"},
                new City(){Key =  2, Value= "Casado/a"},
                new City(){Key =  3, Value= "Divorciado/a"},
                new City(){Key =  4, Value= "Viudo/a"},
                new City(){Key =  5, Value= "Union de hecho"}
            };

            return cities;
        }

    }

    public class City
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}
