using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Desgo_Xamarin.Services;
using Desgo_Xamarin.Views;

namespace Desgo_Xamarin.ViewModels.ViewsModelsForm
{
    class MapasModel: BaseViewModel
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        private string txtLat;
        private string txtLong;
        private bool btnGetLocationIsEnabled;
        #endregion

        #region Properties
        public string TxtLat
        {
            get { return this.txtLat; }
            set { SetValue(ref this.txtLat, value); }
        }
        public string TxtLong
        {
            get { return this.txtLong; }
            set { SetValue(ref this.txtLong, value); }
        }

        public bool BtnGetLocationIsEnabled
        {
            get
            {
                return btnGetLocationIsEnabled;
            }
            set
            {
                if (btnGetLocationIsEnabled != value)
                {
                    btnGetLocationIsEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(BtnGetLocationIsEnabled)));
                }
            }
        }

        #endregion

        #region Constructors
        public MapasModel()
        {
            this.BtnGetLocationIsEnabled = true;
            this.TxtLat = "*" ;
            this.TxtLong = "*" ;
        }

        #endregion

        #region Commands
        public ICommand BtnGetLocationCommand
        {
            get
            {
                return new RelayCommand(BtnbtnGetLocation_Clicked);
            }
        }
        #endregion

        #region Methods
        private async void BtnbtnGetLocation_Clicked()
        {
           await RetreiveLocation();
           
        }

        private async Task RetreiveLocation()
        {

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 20;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(40), null, true);
                this.TxtLat = position.Latitude.ToString();
                this.TxtLong = position.Longitude.ToString();
            }
            catch (Exception ex) {
            }

        }
        #endregion
    }
}
