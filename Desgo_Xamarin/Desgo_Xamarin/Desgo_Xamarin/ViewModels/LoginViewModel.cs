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
using Desgo_Xamarin.ViewModels.ViewsModelsForm;

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
        Sha256Service sha256;
        #endregion

        #region Attributes
        private string usuario;
        private string password;
        private bool isRunning;
        private bool isEnable;
        private string status;
        usuario usuar;
        estadoSqlite estadosqlite=new estadoSqlite();
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
            sha256 = new Sha256Service();

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
            this.Status += "Estado:"+connection.Message;
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
            this.IsRunning = true;
            this.IsEnabled = false;
            if (this.IsRemembered!=true)//online = true
            {
                /**Sin internet***/
                await LoadLoginLocal();
            }
            else
            {
                if (!connection.IsSuccess)
                {
                    using (DataAccess datos = new DataAccess())
                    {
                        try
                        {

                            estadosqlite = datos.getEstadoSqlite();
                            if (estadosqlite != null)
                            {
                                await Application.Current.MainPage.DisplayAlert("Alert",
                              "Sin conexión a internet, se sugiere activar el modo offline para iniciar sesión",
                              "Accept");
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("Alert",
                                "Verifique su conexión a Internet.",
                                "Accept");
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("Error",
                           ">>" + usuar.CONTRASENIA_USUARIO + ">" + usuar.USUARIO_USUARIO + "..." + ex,
                           "Accept");
                        }
                    }
                }
                else
                {
                    /**Con internet**/
                    using (DataAccess datos = new DataAccess())
                    {
                        try
                        {
                            estadosqlite = datos.getEstadoSqlite();
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("Error",
                           "In Command SQLITE: "+ex,
                           "Accept");
                        }
                    }
                    if (estadosqlite==null)
                    {
                        await LoadLoginAPIempty();
                    }
                    else
                    {

                        if (estadosqlite.USUARIO_USUARIO==this.Usuario)
                        {
                            await LoadLoginNormalInternet();
                        }
                        else
                        {
                            var action=await Application.Current.MainPage.DisplayAlert("Alert",
                            "Desea ingresar con un nuevo usuario?, los datos almacenados del usuario <<" + estadosqlite.USUARIO_USUARIO + ">> se perderan. ",
                            "Accept","Cancel");
                            if (action)
                            {
                                //  Navigate to first page
                                //                                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                                
                                if (estadosqlite.CAMBIOS_ESTADOSQLITE == false)
                                {
                                    using (DataAccess datos = new DataAccess())
                                    {
                                        datos.deleteTable();
                                    }
                                    await LoadLoginAPIempty();
                                }
                                else
                                {
                                    var action2 = await Application.Current.MainPage.DisplayAlert("Alert",
                                                        "Existen cambios que no se han sincronizado del usuario <<" + estadosqlite.USUARIO_USUARIO + ">>, Esta seguro que desea iniciar sesión?",
                                                        "Accept", "Cancel");
                                    if (action2)
                                    {
                                        using (DataAccess datos = new DataAccess())
                                        {
                                            datos.deleteTable();
                                        }
                                        await LoadLoginAPIempty();
                                    }
                                    else
                                    {
                                        this.IsRunning = false;
                                        return;
                                    }
                                }
                                
                            }
                            else
                            {

                            }
                            this.IsRunning = false;
                            return;
                        }
                       
                    }
                }
            }
            this.IsRunning = false;
            this.IsEnabled = true;
            this.Usuario = string.Empty;
            this.Password = string.Empty;         
        }

        async Task LoadLoginLocal()
        {
            this.IsRunning = true;

            using (DataAccess datos = new DataAccess())
            {
                try
                {
                    
                    estadosqlite = datos.getEstadoSqlite();
                    if (estadosqlite != null)
                    {
                        int saltus = estadosqlite.SALT_USUARIO;//datos.getUsuarioSalt(this.Usuario);
                                                               //await Application.Current.MainPage.DisplayAlert("Error",
                                                               //"sha256>" + sha256.ComputeSha256Hash(saltus + this.Password)+ "sal>"+saltus +"pass>"+ this.Password,
                                                               //"Accept");
                        usuar = datos.getUsuario(sha256.ComputeSha256Hash(saltus + this.Password), this.Usuario);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error, no existe usuarios almacenados",
                        "Deber iniciar sesion en modo online y cargar un usuario primero.",
                        "Accept");
                        return;
                    }
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
                this.IsRunning = false;
                this.IsEnabled = true;

                    await Application.Current.MainPage.DisplayAlert("Error",
                    "Usuario o Contraseña incorrecto",
                    "Accept");
                this.Password = string.Empty;
                this.Usuario = string.Empty;
                return;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Bienvenido",
                  ">>" + usuar.USUARIO_USUARIO,
                  "Accept");
                MainViewModel.GetInstance().IsEnabledSincronizacion = true;
                MainViewModel.GetInstance().EstadoSincronizacion = "Sincronizar";
                MainViewModel.GetInstance().User = usuar;
                MainViewModel.GetInstance().Formularioall = new formularioAll();
                MainViewModel.GetInstance().EstadoConnection = true;
                MainViewModel.GetInstance().MessageTypeConnection = "Offline";
                MainViewModel.GetInstance().LoadMenuLoggedin();
                MainViewModel.GetInstance().LoadMap();
                MainViewModel.GetInstance().GraficosPredio = new ViewsModelsForm.GraficosPredioModel();
                MainViewModel.GetInstance().ElementosConstructivos = new ViewsModelsForm.ElementosConstructivosModel();
                MainViewModel.GetInstance().HomePageLoggedin = new HomePageModelLoggedin();
                MainViewModel.GetInstance().IdentificacionUbicacionPart = new IdentificacionUbicacionlModel();

                navigationService.SetMainPage("MasterPage1Loggedin");
            }
        }
        async Task LoadLoginAPIempty()
        {
            this.IsRunning = true;

            try
            {
                var servicio = DependencyService.Get<IServiceUser>();
                var resultado = servicio.validarUsuarioAmazon(this.Usuario, this.Password);
                if (resultado == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error",
                       "No existe el usuario",
                       "Accept");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Bienvenido",
                   ">>"+resultado.USUARIO_USUARIO,
                   "Accept");
                    using (DataAccess datos = new DataAccess())
                    {
                        estadoSqlite eqlite = new estadoSqlite();
                        eqlite.ESTADO_ESTADOSQLITE = "Datos";
                        eqlite.CAMBIOS_ESTADOSQLITE = false;
                        eqlite.USUARIO_USUARIO = resultado.USUARIO_USUARIO;
                        eqlite.CONTRASENIA_USUARIO = resultado.CONTRASENIA_USUARIO;
                        eqlite.SALT_USUARIO = resultado.SALT_USUARIO;
                        eqlite.ID_USUARIO = resultado.ID_USUARIO;

                        datos.setEstadoSqlite(eqlite);
                        datos.Insert(resultado.PERSONA_PERSONA);
                        datos.Insertusuario(resultado);
                        datos.InsertipoUser(resultado.TipoUsuario_PERSONA);
                        //formulario form = new formulario()
                        //{
                        //    ID_FORMULARIO = 3,
                        //    CODIGO_FORMULARIO = Int32.Parse("1723953051"),
                        //    ID_USUARIO = resultado.ID_USUARIO
                        //};
                        //datos.Insertformulario(form);
                        usuario usresult = new usuario()
                        {
                            ID_USUARIO=resultado.ID_USUARIO,
                            USUARIO_USUARIO = resultado.USUARIO_USUARIO,
                            ID_TIPOUSUARIO = resultado.ID_TIPOUSUARIO,
                            ID_PERSONA = resultado.ID_PERSONA,
                            CONTRASENIA_USUARIO = resultado.CONTRASENIA_USUARIO,
                            EMPRESA_USUARIO = resultado.EMPRESA_USUARIO,
                            SALT_USUARIO = resultado.SALT_USUARIO

                        };
                        var servicioForm = DependencyService.Get<IServiceForm>();
                        await Application.Current.MainPage.DisplayAlert("AWS",
                       "IdU" + resultado.ID_USUARIO + "idP" + resultado.ID_PERSONA + "idTp" + resultado.ID_TIPOUSUARIO,
                       "Accept");
                        List<formulario> resultadoForms = servicioForm.listarFormularios(usresult.ID_USUARIO);
                        await Application.Current.MainPage.DisplayAlert("AWS",
                       "form" + resultadoForms.ToString()+"iduser"+ usresult.ID_USUARIO,
                       "Accept");
                        if (resultadoForms != null)
                        {
                            List<formularioAll> resultadoFormsAll = new List<formularioAll>();

                            foreach (formulario i in resultadoForms)
                            {
                                datos.Insertformulario(i);
                                resultadoFormsAll.Add(servicioForm.listarFormulariosAll(i.ID_FORMULARIO,i.CODIGO_FORMULARIO,i.ID_IULOTE));
                                await Application.Current.MainPage.DisplayAlert("result all form>",
                                   i + ">"+resultadoFormsAll.ToString()+"codigo form para all"+i.CODIGO_FORMULARIO,
                                   "Accept");
                            }
                            foreach (formularioAll i in resultadoFormsAll)
                            {
                                await Application.Current.MainPage.DisplayAlert("All form >",
                                      "cvnew"+ i + ">" + i.identificacionubicacionlote.CLAVECATASTRALNUEVO_IULOTE,
                                      "Accept");
                                datos.Insertformularioall(i);
                            }
                        }

                        //var persona = servicio.validarUsuarioAmazon(this.Usuario, this.Password);
                        //  await Application.Current.MainPage.DisplayAlert("Error",
                        // "IdU"+resultado.ID_USUARIO+"idP"+resultado.ID_PERSONA+"idTp"+resultado.ID_TIPOUSUARIO,
                        // "Accept");
                        //  await Application.Current.MainPage.DisplayAlert("Error",
                        //">>>>>>>"+datos.getUsuario("egct").CONTRASENIA_USUARIO+">>"+ datos.getUsuario("egct").ID_USUARIO,
                        //"Accept");

                    }
                    usuar = resultado;

                    MainViewModel.GetInstance().IsEnabledSincronizacion = true;
                    MainViewModel.GetInstance().EstadoSincronizacion = "Sincronizar";
                    MainViewModel.GetInstance().User = usuar;
                    MainViewModel.GetInstance().Formularioall = new formularioAll();
                    MainViewModel.GetInstance().EstadoConnection = true;
                    MainViewModel.GetInstance().MessageTypeConnection = "Online";
                    MainViewModel.GetInstance().LoadMenuLoggedin();
                    MainViewModel.GetInstance().LoadMap();
                    MainViewModel.GetInstance().GraficosPredio= new ViewsModelsForm.GraficosPredioModel();
                    MainViewModel.GetInstance().ElementosConstructivos = new ViewsModelsForm.ElementosConstructivosModel();
                    MainViewModel.GetInstance().HomePageLoggedin = new HomePageModelLoggedin();
                    MainViewModel.GetInstance().IdentificacionUbicacionPart = new IdentificacionUbicacionlModel();
                    navigationService.SetMainPage("MasterPage1Loggedin");
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
               "Amazon services" + e,
               "Accept");
            }
        }
        async Task LoadLoginNormalInternet()
        {
            this.IsRunning = true;

            try
            {
                estadoSqlite eqlite = new estadoSqlite();
                using (DataAccess datos = new DataAccess())
                {
                    eqlite = datos.getEstadoSqlite();
                    if (eqlite.CAMBIOS_ESTADOSQLITE == false)
                    {
                        datos.deleteTable();
                        await LoadLoginAPIempty();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error",
                           "Existen cambios que no se han sincronizado, Inicie sesión en modo offline y envie los cambios.",
                           "Accept");
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
               "Amazon services" + e,
               "Accept");
            }

        }
        async Task LoadLoginAPIfull()
        {
            this.IsRunning = true;

            try
            {
                var servicio = DependencyService.Get<IServiceUser>();
                var resultado = servicio.validarUsuarioAmazon(this.Usuario, this.Password);
                if (resultado == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error",
                       "No existe el usuario",
                       "Accept");

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Bienvenido",
                   ">>" + resultado.USUARIO_USUARIO,
                   "Accept");
                    MainViewModel.GetInstance().IsEnabledSincronizacion = true;
                    MainViewModel.GetInstance().EstadoSincronizacion = "Sincronizar";
                    MainViewModel.GetInstance().GraficosPredio = new ViewsModelsForm.GraficosPredioModel();
                    MainViewModel.GetInstance().ElementosConstructivos = new ViewsModelsForm.ElementosConstructivosModel();
                    usuar = resultado;
                    MainViewModel.GetInstance().Formularioall = new formularioAll();
                    MainViewModel.GetInstance().HomePageLoggedin = new HomePageModelLoggedin();
                    MainViewModel.GetInstance().User = usuar;
                    MainViewModel.GetInstance().EstadoConnection = true;
                    MainViewModel.GetInstance().MessageTypeConnection = "Online";
                    MainViewModel.GetInstance().IdentificacionUbicacionPart = new IdentificacionUbicacionlModel();

                    navigationService.SetMainPage("MasterPage1Loggedin");
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
               "Amazon services" + e,
               "Accept");
            }
        }
        #endregion
    }
}
