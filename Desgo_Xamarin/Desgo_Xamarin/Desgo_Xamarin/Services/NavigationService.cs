using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Desgo_Xamarin.Models.Db;
using Desgo_Xamarin.Views;
using Desgo_Xamarin.Views.ViewsUserLoggedin;
using Xamarin.Forms;

namespace Desgo_Xamarin.Services
{
    class NavigationService
    {
        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
                case "LoginPage":
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
                case "MasterPage1":
                    Application.Current.MainPage = new MasterPage1();
                    break;
                case "MasterPage1Loggedin":
                    Application.Current.MainPage = new MasterPage1Loggedin();
                    break;
                
            }
        }

        public async Task NavigateOnMaster(string pageName)
        {
            App.Master.IsPresented = false;

            switch (pageName)
            {
                case "HelpPage":
                    await App.Navigator.PushAsync(
                        new HelpPage());
                    break;
                case "HomePage":
                    await App.Navigator.PushAsync(
                        new HomePage());
                    break;
                case "LoginPage":
                    await App.Navigator.PushAsync(
                        new LoginPage());
                    break;
            }
        }
        public async Task NavigateOnMasterLoggedin(string pageName)
        {
            App.Masterloggedin.IsPresented = false;

            switch (pageName)
            {
                case "AvanceForms":
                    await App.Navigator.PushAsync(
                        new AvancesFormLoggedin());
                    break;
                case "HelpPage":
                    await App.Navigator.PushAsync(
                        new HelpPage());
                    break;
                case "HomePage":
                    await App.Navigator.PushAsync(
                        new HomePage());
                    break;
                case "LoginPage":
                    await App.Navigator.PushAsync(
                        new LoginPage());
                    break;
                case "FormlPage":
                    await App.Navigator.PushAsync(
                        new FormlPage());
                    break; 
                case "MyFormsPage":
                    await App.Navigator.PushAsync(
                        new MyFormsPageLoggedin());
                    break; 
            }
        }

       
    }
}
