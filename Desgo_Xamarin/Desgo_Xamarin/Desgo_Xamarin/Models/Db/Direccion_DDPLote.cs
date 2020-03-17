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
    [Table("Direccion_DDPLote")]
    public class Direccion_DDPLote
    {
        [PrimaryKey]
        public int ID_DLOTE { get; set; }
        public string CALLEP_DLOTE { get; set; }
        public string NO_DLOTE { get; set; }
        public string INTERSECCION_DLOTE { get; set; }
    }
}
