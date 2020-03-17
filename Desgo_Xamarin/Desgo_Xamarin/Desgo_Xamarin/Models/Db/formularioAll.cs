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
    [Table("formularioAll")]
    public class formularioAll
    {
        [PrimaryKey]
        public int ID_FORMULARIO { get; set; }
        public int ID_ELOTE { get; set; }
        public int ID_RLOTE { get; set; }
        public int ID_LLOTE { get; set; }
        public int ID_GPLOTE { get; set; }
        [OneToOne]
        public identificacionubicacionlote identificacionubicacionlote { get; set; }
        public int ID_M { get; set; }
        public int ID_CLOTE { get; set; }
        public int ID_VCLOTE { get; set; }
        public int ID_MLOTE { get; set; }
        public int ID_ILLOTE { get; set; }
        public int ID_EC { get; set; }
        public int ID_CCLOTE { get; set; }

        [MaxLength(20)]
        public int CODIGO_FORMULARIO { get; set; }

        public bool ESTADO_FORMULARIO { get; set; }

        public int ID_USUARIO { get; set; }

        public int USUARIO_FORMULARIO { get; set; }
    }
}
