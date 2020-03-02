using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Desgo_Xamarin.Services;
using Desgo_Xamarin.ViewModels;
using GalaSoft.MvvmLight.Command;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Desgo_Xamarin.Models.Db
{
    
    [Table("formulario")]
    public class formulario
    {
        [PrimaryKey, AutoIncrement]
        public int ID_FORMULARIO { get; set; }
        public int ID_ELOTE { get; set; }
        public int ID_RLOTE { get; set; }
        public int ID_LLOTE { get; set; }
        public int ID_GPLOTE { get; set; }
        public int ID_IULOTE { get; set; }
        public int ID_M { get; set; }
        public int  ID_CLOTE { get; set; }
        public int  ID_VCLOTE { get; set; }
        public int  ID_MLOTE { get; set; }
        public int  ID_ILLOTE { get; set; }
        public int  ID_EC { get; set; }
        public int  ID_CCLOTE { get; set; }

        [MaxLength(20), Unique]
        public int CODIGO_FORMULARIO { get; set; }

        public bool ESTADO_FORMULARIO { get; set; }

        [ForeignKey(typeof(usuario))]
        public int  ID_USUARIO { get; set; }

        [ManyToOne]
        public usuario USUARIO_FORMULARIO { get; set; }

        #region Services
        NavigationService navigationService = new NavigationService();
        ApiService apiService;
        DataService dataService;
        DialogService dialogService;
        #endregion


        #region Commands
        public ICommand SelectFormCommand
        {
            get
            {
                return new RelayCommand(SelectFormulario);
            }
        }

        async void SelectFormulario()
        {
            MainViewModel.GetInstance().FormPage = new FormPageModel();
            await navigationService.NavigateOnMasterLoggedin("FormlPage");
        } 
        #endregion
    }
}
