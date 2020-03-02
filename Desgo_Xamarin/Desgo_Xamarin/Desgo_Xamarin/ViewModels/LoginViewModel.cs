using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Desgo_Xamarin.Services;
using Desgo_Xamarin.Views;
using Desgo_Xamarin.ViewModels.ViewsModelsLoggedin;
using System.Threading.Tasks;
using Desgo_Xamarin.Models.Db;
using persona = Desgo_Xamarin.Models.Db.persona;
using Desgo_Xamarin.Data;
using Desgo_Xamarin.Interfaces;

namespace Desgo_Xamarin.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        NavigationService navigationService;
        ApiService apiService;
        DataService dataService;
        DialogService dialogService;
        #endregion

        #region Attributes
        private string usuario;
        private string password;
        private bool isRunning;
        private bool isEnable;
        private string status;
        usuario usuar;
        //private usuario us;
        //private tipousuario tp;
        //private persona p;
        //private user user;
       
        #endregion

        #region Properties
        public string Usuario
        {
            get { return this.usuario; }
            set { SetValue(ref this.usuario, value); }
        }
        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsRemembered
        {
            get;
            set;
        }
        public bool IsEnabled
        {
            get
            {
                return isEnable;
            }
            set
            {
                if (isEnable != value)
                {
                    isEnable = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status != value)
                {
                    status = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Status)));
                }
            }
        }

        #endregion

        #region Constructors
        public LoginViewModel(){
            apiService = new ApiService();
            navigationService = new NavigationService();
            dataService = new DataService();
            dialogService = new DialogService();

            this.IsRemembered = true;
            this.IsEnabled = true;
            this.isRunning = false;
            InternetStatus();

        }

        #endregion

        #region Commands

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }
        private async void InternetStatus()
        {
            var connection = await apiService.CheckConnection();
            this.Status += connection.Message;
        }

        private async void Login()
        {
            var connection = await apiService.CheckConnection();
            
            if (string.IsNullOrEmpty(this.Usuario))
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                    "Ingrese un usuario",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                    "Ingrese una contraseña",
                    "Accept");
                return;
            }
            this.isRunning = true;
            this.IsEnabled = false;
            if (!connection.IsSuccess)
            {
                await LoadLoginLocal();
            }
            else
            {
                //await Application.Current.MainPage.DisplayAlert("Error",
                //    "0",
                //    "Accept");
                //await LoadLoginAPI();
                await LoadLoginLocal();

            }
            this.isRunning = false;
            this.IsEnabled = true;

            this.Usuario = string.Empty;
            this.Password = string.Empty;
            
           

        }

        async Task LoadLoginLocal()
        {
            /*string hashpass = this.Password;
            us = dataService.Find<usuario>(this.Usuario,hashpass,false);
            */
            using (DataAccess datos = new DataAccess())
            {
                try
                {
                    usuar = datos.getUsuario(this.Password, this.Usuario);
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error",
                   ">>" + usuar.CONTRASENIA_USUARIO + ">" + usuar.USUARIO_USUARIO+"..."+ex,
                   "Accept");
                }
            }

            if (usuar==null)
            {
                this.isRunning = false;
                this.IsEnabled = true;

                    await Application.Current.MainPage.DisplayAlert("Error",
                    "Usuario o Contraseña incorrecto",
                    "Accept");
                this.Password = string.Empty;

                return;
            }
            else
            {
                /*
                MainViewModel.GetInstance().HomePageLoggedin = new HomePageModelLoggedin();
                navigationService.SetMainPage("MasterPage1Loggedin");
                 */
                MainViewModel.GetInstance().HomePageLoggedin = new HomePageModelLoggedin();
                MainViewModel.GetInstance().User = usuar;
                navigationService.SetMainPage("MasterPage1Loggedin");
            }


        }

        async Task LoadLoginAPI()
        {
            this.isRunning = true;

            try
            {
                var servicio = DependencyService.Get<IServiceUser>();
                string resultado = servicio.validarUsuario(this.Usuario, this.Password);
                if (resultado == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error",
                       "noooooo",
                       "Accept");

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert",
                   ">>"+resultado,
                   "Accept");
                    /***
                    MainViewModel.GetInstance().HomePageLoggedin = new HomePageModelLoggedin();
                    navigationService.SetMainPage("MasterPage1Loggedin");
                    */
                    MainViewModel.GetInstance().HomePageLoggedin = new HomePageModelLoggedin();
                    MainViewModel.GetInstance().User = usuar;
                    navigationService.SetMainPage("MasterPage1Loggedin");
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
               "service nooo" + e,
               "Accept");
            }

            //WSGestionUsuario loginS = new WSGestionUsuario();
            //try
            //{
            //    user = loginS.login(this.Usuario, this.Password);
            //}
            //catch (Exception e)
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error",
            //   "service nooo" + e,
            //   "Accept");
            //}
            //await Application.Current.MainPage.DisplayAlert("Error",
            //       this.Usuario + ">>" + this.Password,
            //       "Accept");
            

            // var url = "http://apiexchangerates.azurewebsites.net"; 
            //Application.Current.Resources["URLAPI"].ToString();
        }
        #endregion
    }
}
