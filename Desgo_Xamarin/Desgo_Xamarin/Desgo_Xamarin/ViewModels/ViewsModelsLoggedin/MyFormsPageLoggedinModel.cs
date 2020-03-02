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
        ObservableCollection<formulario> _formularios;
        #endregion

        #region Service

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
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        private string _filter;
        public string Filter
        {
            get { return this._filter; }
            set { SetValue(ref this._filter, value); }
        }

        #region Constructors
        public MyFormsPageLoggedinModel()
        {
            LoadFormularios();
        }


        #endregion

        #region Methods
        public void LoadFormularios()
        {
            try
            {
                using (DataAccess datos = new DataAccess())
                {

                    var formulariosAux = datos.GetListFormulario();
                    Formularios = new ObservableCollection<formulario>(formulariosAux.OrderBy(c => c.ID_FORMULARIO));
                }
            }
            catch (Exception e)
            {
                // lblmensaje.Text = e.Message + ">";
            }
        }
        #endregion


    }
}
