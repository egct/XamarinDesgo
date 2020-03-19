using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Desgo_Xamarin.Models;
using Desgo_Xamarin.Models.Db;
using Desgo_Xamarin.ViewModels.ViewsModelsForm;
using Desgo_Xamarin.ViewModels.ViewsModelsLoggedin;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;
using Menu = Desgo_Xamarin.Models.Menu;
using Desgo_Xamarin.Data;
using Desgo_Xamarin.Interfaces;
using Desgo_Xamarin.Models.Class;

namespace Desgo_Xamarin.ViewModels
{
    class MainViewModel:BaseViewModel
    {
        #region variables
        public usuario user;
        public formularioAll formularioall;
        public formulario formulario;
        public Boolean estadoConnection;
        public String messageConnection;
        private string estadoSincronizacion;
        private bool isEnabledSincronizacion;

        #endregion 
        #region ViewModels
        public usuario User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        public formularioAll Formularioall
        {
            get { return this.formularioall; }
            set { SetValue(ref this.formularioall, value); }
        }
        public formulario Formulario
        {
            get { return this.formulario; }
            set { SetValue(ref this.formulario, value); }
        }

        public Boolean EstadoConnection
        {
            get { return this.estadoConnection; }
            set { SetValue(ref this.estadoConnection, value); }
        }
        public String MessageTypeConnection
        {
            get { return this.messageConnection; }
            set { SetValue(ref this.messageConnection, value); }
        }
        public string EstadoSincronizacion
        {
            get { return this.estadoSincronizacion; }
            set { SetValue(ref this.estadoSincronizacion, value); }
        }
        public bool IsEnabledSincronizacion
        {
            get { return this.isEnabledSincronizacion; }
            set { SetValue(ref this.isEnabledSincronizacion, value); }
        }
        public ICommand SincronizacionCommand
        {
            get
            {
                return new RelayCommand(SincronizarBDAws);
            }
        }

        private async void SincronizarBDAws()
        {
            var action = await Application.Current.MainPage.DisplayAlert("Alert",
                           "Comenzar la sincronizacióon?",
                           "Accept", "Cancel");
            if (action)
            {
                using (DataAccess datos = new DataAccess())
                {
                    estadoSqlite eqlite = new estadoSqlite();
                    eqlite = datos.getEstadoSqlite();
                    if (eqlite.CAMBIOS_ESTADOSQLITE == false)
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert",
                           "No existe modificación almacenada",
                           "Accept");
                        this.IsEnabledSincronizacion = true;
                        this.EstadoSincronizacion = "Sincronizar";
                        return;
                    }
                    else
                    {
                        try
                        {
                            /*Enviar datos a AWS*/
                            this.IsEnabledSincronizacion = false;
                            this.EstadoSincronizacion = "Sincronizando...";
                            /***/
                            var servicioForm = DependencyService.Get<IServiceForm>();
                            CPrepararDatosSycn cPrepararDatosSycn = new CPrepararDatosSycn();
                            List<formularioAll> fAll = new List<formularioAll>();
                            List<formulario> f = new List<formulario>();
                            f= datos.GetListFormulario();
                            foreach (formulario i in f)
                            {
                                fAll.Add(cPrepararDatosSycn.convertirFtoFAll(i));
                            }
                            bool bandera = servicioForm.sincronizarBD(fAll);
                            if (bandera)
                            {
                                /**si sincronizo*/
                                this.IsEnabledSincronizacion = true;
                                this.EstadoSincronizacion = "Sincronizar";
                                datos.updateCambiosEstadoSqlite(false);
                                
                            }
                            else
                            {
                                /**no sincronizo*/
                                this.IsEnabledSincronizacion = true;
                                this.EstadoSincronizacion = "Sincronizar";
                                datos.updateCambiosEstadoSqlite(true);

                            }
                            /***/
                            return;
                        }
                        catch (Exception ee)
                        {
                            return;
                        }
                    }
                }
            }
            else
            {
                this.IsEnabledSincronizacion = true;
                this.EstadoSincronizacion = "Sincronizar";
            }
        }
        public LoginViewModel Login
        {
            get;
            set;
        }

        public ObservableCollection<Menu> MyMenu
        {
            get;
            set;
        }

        public ObservableCollection<MenuLoggedin> MyMenuLoggedin
        {
            get;
            set;
        }

        public HomePageModel HomePage
        {
            get;
            set;
        }
        public HomePageModelLoggedin HomePageLoggedin
        {
            get;
            set;
        }

        public HelpPageModel HelpPage
        {
            get;
            set;
        }
        public FormPageModel FormPage
        {
            get;
            set;
        }

        public MapasModel Ubications
        {
            get;
            set;
        }

        public GraficosPredioModel GraficosPredio
        {
            get;
            set;
        }
        public ElementosConstructivosModel ElementosConstructivos
        {
            get;
            set;
        }

        public MyFormsPageLoggedinModel MyFormsPage
        {
            get;
            set;
        }

        public IdentificacionUbicacionlModel IdentificacionUbicacionPart
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MainViewModel(){
            instance = this;
           this.Login = new LoginViewModel();
           LoadMenu();
           //LoadMenuLoggedin();
           // LoadMap();
        }


        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance==null)
            {
                return new MainViewModel();
            }
            return instance;

        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            MyMenu = new ObservableCollection<Menu>();
            MyMenu.Add(new Menu
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginPage",
                Title = "Iniciar sesion",
            });

       /*     MyMenu.Add(new Menu
            {
                Icon = "ic_playlist_add",
                PageName = "FormlPage",
                Title = "Formulario",
            });
            */
            
            MyMenu.Add(new Menu
            {
                Icon = "ic_exit_to_app",
                PageName = "HelpPage",
                Title = "Ayuda",
            });

        }
        public void LoadMenuLoggedin()
        {
            MyMenuLoggedin = new ObservableCollection<MenuLoggedin>();

            MyMenuLoggedin.Add(new MenuLoggedin
            {
                Icon = "ic_playlist_add",
                PageName = "FormlPage",
                Title = "Avances Formulario",
            });

            MyMenuLoggedin.Add(new MenuLoggedin
            {
                Icon = "ic_playlist_add",
                PageName = "MyFormsPage",
                Title = "Mis Formularios",
            });

            MyMenuLoggedin.Add(new MenuLoggedin
            {
                Icon = "ic_exit_to_app",
                PageName = "MasterPage1",
                Title = "Cerrar sesion",
            });

            MyMenuLoggedin.Add(new MenuLoggedin
            {
                Icon = "ic_exit_to_app",
                PageName = "HelpPage",
                Title = "Ayuda",
            });

        }
        public void LoadMap()
        {
            Ubications = new MapasModel();
        }
        #endregion
    }
}
