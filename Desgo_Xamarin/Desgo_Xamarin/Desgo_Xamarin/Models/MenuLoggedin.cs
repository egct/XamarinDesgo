﻿using Desgo_Xamarin.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Desgo_Xamarin.Services;
using Desgo_Xamarin.ViewModels;
using Desgo_Xamarin.ViewModels.ViewsModelsLoggedin;

namespace Desgo_Xamarin.Models
{
    class MenuLoggedin
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
        public MenuLoggedin()
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
                
                case "MasterPage1":
                    MainViewModel.GetInstance().HomePage = new HomePageModel();
                    navigationService.SetMainPage("MasterPage1");
                    break;
                case "AvanceForms":
                    MainViewModel.GetInstance().AvancesFormsLoggedin = new AvancesFormsLoggedin();
                    await navigationService.NavigateOnMasterLoggedin("AvanceForms");
                    break;
                case "HelpPage":
                    MainViewModel.GetInstance().HelpPage = new HelpPageModel();
                    await navigationService.NavigateOnMasterLoggedin("HelpPage");
                    break;
                case "FormlPage":
                    MainViewModel.GetInstance().FormPage = new FormPageModel();
                    await navigationService.NavigateOnMasterLoggedin("FormlPage");
                    break; 
                case "MyFormsPage":
                    MainViewModel.GetInstance().MyFormsPage = new MyFormsPageLoggedinModel();
                    await navigationService.NavigateOnMasterLoggedin("MyFormsPage");
                    break; 

            }
        }
        #endregion

    }

}
