using Desgo_Xamarin.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Desgo_Xamarin.Services;
using Desgo_Xamarin.ViewModels;

namespace Desgo_Xamarin.Models
{
    class Menu
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Properties

        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Constructors
        public Menu()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        private async void Navigate()
        {
            switch (PageName)
            {
                case "LoginPage":
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    await navigationService.NavigateOnMaster("LoginPage");
                    break;
                case "HelpPage":
                    MainViewModel.GetInstance().HelpPage = new HelpPageModel();
                    await navigationService.NavigateOnMaster("HelpPage");
                    break;
            }
        }
        #endregion

    }
}
