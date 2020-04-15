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
            this.IsRunning = true;
            var connection = await apiService.CheckConnection();
            
            if (string.IsNullOrEmpty(this.Usuario))
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                    "Ingrese un usuario",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                    "Ingrese una contraseña",
                    "Aceptar");
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
                /**Con internet***/
                if (!connection.IsSuccess)//verifico si existe en verdad internet
                {
                    /**Se quizo inciar sesion con la opcion online pero No existe internet**/
                    //verificamos si existe un usuario almacenado 
                    using (DataAccess datos = new DataAccess())
                    {
                        try
                        {

                            estadosqlite = datos.getEstadoSqlite();
                            if (estadosqlite != null)
                            {
                                await Application.Current.MainPage.DisplayAlert("Alerta",
                              "Sin conexión a internet, se sugiere activar el modo offline para iniciar sesión",
                              "Aceptar");
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("Alerta",
                                "Verifique su conexión a Internet.",
                                "Aceptar");
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("Error",
                           "No se inicio sesión, Intentelo de nuevo",
                           "Aceptar");
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
                           "No se inicio sesión, Intentelo de nuevo",
                           "Aceptar");
                        }
                    }
                    if (estadosqlite==null)
                    {
                        /**cuando no existe usuario almacenado, se carga toda la info del usuario desde 0 sincroniza*/
                        await LoadLoginAPIempty();
                    }
                    else
                    {
                        /**Existe usuario almacenado, verifica si es el mismo usuario o si quiere ingresar con otro usuario*/
                        if (estadosqlite.USUARIO_USUARIO==this.Usuario)
                        {
                            //usuarios iguales carga de nuevo toda la base
                            await LoadLoginNormalInternet();
                        }
                        else
                        {
                            //pregunta si desea ingresar con otro usuario.
                            var action=await Application.Current.MainPage.DisplayAlert("Alerta",
                            "Desea ingresar con un nuevo usuario?, los datos almacenados del Usuario: " + estadosqlite.USUARIO_USUARIO + " se perderan. ",
                            "Aceptar","Cancelar");
                            if (action)
                            {
                                //Si acepta ingresar con otro usuario elimina los datos almacenados del usuario anterior
                                if (estadosqlite.CAMBIOS_ESTADOSQLITE == false)//si no existe cambios sin sicronizar elimina todos los datos y lo carga del  nuevo usuario
                                {
                                    using (DataAccess datos = new DataAccess())
                                    {
                                        datos.deleteTable();
                                    }
                                    await LoadLoginAPIempty();
                                }
                                else
                                {
                                    //si existen cambios sin sicronizar pregunta si desea perder esos datos
                                    var action2 = await Application.Current.MainPage.DisplayAlert("Alerta",
                                                        "Existen cambios que no se han sincronizado del Usuario:" + estadosqlite.USUARIO_USUARIO + ", esta seguro que desea iniciar sesión?",
                                                        "Aceptar", "Cancelar");
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
                        int saltus = estadosqlite.SALT_USUARIO;
                        usuar = datos.getUsuario(sha256.ComputeSha256Hash(saltus + this.Password), this.Usuario);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error, no existe usuarios almacenados",
                        "Deber iniciar sesion en modo online y cargar un usuario primero.",
                        "Aceptar");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error",
                   "No se inicio sesión, Intentelo de nuevo",
                   "Aceptar");
                }
            }

            if (usuar==null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                    await Application.Current.MainPage.DisplayAlert("Error",
                    "Usuario o Contraseña incorrectos",
                    "Aceptar");
                this.Password = string.Empty;
                this.Usuario = string.Empty;
                return;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Bienvenido",
                  "Usuario:" + usuar.USUARIO_USUARIO,
                  "Aceptar");
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
                //se conecta a AWS para verificar si el usuario existe en la base de datos de Amazon
                var servicio = DependencyService.Get<IServiceUser>();
                var resultado = servicio.validarUsuarioAmazon(this.Usuario, this.Password);
                if (resultado == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error",
                       "No existe el usuario",
                       "Aceptar");
                }
                else
                {
                    //comienza a almacenar los datos del AWS en la base sqlite
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
                       // await Application.Current.MainPage.DisplayAlert("AWS",
                       //"IdU" + resultado.ID_USUARIO + "idP" + resultado.ID_PERSONA + "idTp" + resultado.ID_TIPOUSUARIO,
                       //"Accept");
                        List<formulario> resultadoForms = servicioForm.listarFormularios(usresult.ID_USUARIO);
                       // await Application.Current.MainPage.DisplayAlert("AWS",
                       //"form" + resultadoForms.ToString()+"iduser"+ usresult.ID_USUARIO,
                       //"Accept");
                        if (resultadoForms != null)
                        {
                            List<formularioAll> resultadoFormsAll = new List<formularioAll>();

                            foreach (formulario i in resultadoForms)
                            {
                                datos.Insertformulario(i);
                                resultadoFormsAll.Add(servicioForm.listarFormulariosAll(i.ID_FORMULARIO,i.CODIGO_FORMULARIO,i.ID_IULOTE));
                                //await Application.Current.MainPage.DisplayAlert("result all form>",
                                //   i + ">"+resultadoFormsAll.ToString()+"codigo form para all"+i.CODIGO_FORMULARIO,
                                //   "Accept");
                            }
                            foreach (formularioAll i in resultadoFormsAll)
                            {
                                //await Application.Current.MainPage.DisplayAlert("All form >",
                                //      "cvnew"+ i + ">" + i.identificacionubicacionlote.CLAVECATASTRALNUEVO_IULOTE,
                                //      "Accept");
                                datos.Insertformularioall(i);
                            }
                        }
                        

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
                    MainViewModel.GetInstance().AvancesFormsLoggedin = new AvancesFormsLoggedin();
                    MainViewModel.GetInstance().IdentificacionUbicacionPart = new IdentificacionUbicacionlModel();

                    await Application.Current.MainPage.DisplayAlert("Bienvenido",
                   "Usuario:" + resultado.USUARIO_USUARIO,
                   "Aceptar");

                    navigationService.SetMainPage("MasterPage1Loggedin");

                }
           
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
               "No se inicio sesión, Intentelo de nuevo",
               "Aceptar");
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
                           "Aceptar");
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
               "No se inicio sesión, Intentelo de nuevo",
               "Aceptar");
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
                       "Aceptar");

                }
                else
                {
                    
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
                    MainViewModel.GetInstance().AvancesFormsLoggedin = new AvancesFormsLoggedin();
                    MainViewModel.GetInstance().IdentificacionUbicacionPart = new IdentificacionUbicacionlModel();
                    await Application.Current.MainPage.DisplayAlert("Bienvenido",
                   "Usuario:" + resultado.USUARIO_USUARIO,
                   "Aceptar");
                    navigationService.SetMainPage("MasterPage1Loggedin");
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
               "No se inicio sesión, Intentelo de nuevo",
               "Aceptar");
            }
        }
        #endregion
        //meotodo para verificar si existe envio de clases entre xamarin y Java Web services
        private async void LoginPrueba() {
            var servicio = DependencyService.Get<IServiceForm>();
            var resultado = servicio.pruebaConAws();
            await Application.Current.MainPage.DisplayAlert("AWS",
            "Resultado"+resultado.ToString(),
            "Accept");
            return;

        }
    }
}
