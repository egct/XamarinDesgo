namespace Desgo_Xamarin
{
    using Xamarin.Forms;
    using Desgo_Xamarin.Services;

    using Desgo_Xamarin.Views;
    using Desgo_Xamarin.Views.ViewsUserLoggedin;
    using Desgo_Xamarin.Data;
    using Desgo_Xamarin.Models.Db;

    public partial class App : Application
    {
        #region Services
        ApiService apiService;
        NavigationService navigationService;
        #endregion

        #region Properties
       
        public static NavigationPage Navigator
        {
            get;
            internal set;
        }

        public static MasterPage1 Master
        {
            get;
            internal set;
        }

        public static MasterPage1Loggedin Masterloggedin
        {
            get;
            internal set;
        }
        #endregion

        #region Constructors
        public App()
        {
            InitializeComponent();

            //            this.MainPage = new NavigationPage (new LoginPage());
            this.MainPage =new MasterPage1();

            //           this.MainPage = new NavigationPage(new MenuView());

            //this.MainPage = new MenuView();
        }
        #endregion 

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
