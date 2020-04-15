using Desgo_Xamarin.Models.Db;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using Desgo_Xamarin.Data;

namespace Desgo_Xamarin.ViewModels.ViewsModelsLoggedin
{
    class MyFormsPageLoggedinModel:BaseViewModel
    {
        
        #region Attribute
        List<formulario> formulariosModel;
        ObservableCollection<formulario> _formularios;
        string _filter;
        bool _isRefreshing;
        #endregion

        #region Service
        DataAccess datos = new DataAccess();
        #endregion

        #region Propierties
        public ObservableCollection <formulario> Formularios
        {
            get
            {
                return _formularios;
            }
            set
            {
                if (_formularios != value)
                {
                    _formularios = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Formularios)));
                }
            }
        }
        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    Search();
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Filter)));
                }
            }

        }
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRefreshing)));
                }
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        void Search()
        {
            IsRefreshing = true;
            _formularios = null;
            Formularios = null;
            try
            {
                //using (DataAccess datos = new DataAccess())
                //{
                    formulariosModel = datos.GetListFormulario();
                    if (string.IsNullOrEmpty(Filter))
                    {
                        Formularios = new ObservableCollection<formulario>(formulariosModel.OrderBy(c => c.ID_FORMULARIO));
                    }
                    else
                    {
                        Formularios = new ObservableCollection<formulario>(formulariosModel.Where(c => c.CODIGO_FORMULARIO.ToString().ToLower().Contains(Filter.ToLower())).OrderBy(c => c.ID_FORMULARIO));
                    }
                //}
            } 
            catch (Exception e)
            {
            }
            IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadFormularios);
            }
        }
        void LoadFormularios()
        {
            IsRefreshing = true;
            _formularios = null;
            Formularios = null;
            try
            {
                Formularios = new ObservableCollection<formulario>(formulariosModel.OrderBy(c => c.ID_FORMULARIO));
                IsRefreshing = false;
                return;
            }
            catch (Exception e)
            {
                
            }
            IsRefreshing = false;
        }
        #endregion



        #region Constructors
        public MyFormsPageLoggedinModel()
        {
            //    LoadFormularios();
            instance = this;
            Search();
        }
        #endregion

        #region Sigleton
        private static MyFormsPageLoggedinModel instance;

        public static MyFormsPageLoggedinModel GetInstance()
        {
            if (instance == null)
            {
                return new MyFormsPageLoggedinModel();
            }

            return instance;
        }
        #endregion
        
        #region Methods
        
        #endregion


    }
}
