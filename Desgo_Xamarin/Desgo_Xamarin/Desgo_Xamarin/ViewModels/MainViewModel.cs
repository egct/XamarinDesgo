﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Desgo_Xamarin.Models;
using Desgo_Xamarin.Models.Db;
using Desgo_Xamarin.ViewModels.ViewsModelsForm;
using Desgo_Xamarin.ViewModels.ViewsModelsLoggedin;

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
