using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace Desgo_Xamarin.Models.Db
{
    [Table("identificacionubicacionlote")]
    class identificacionubicacionlote
    {
        [PrimaryKey, AutoIncrement]
        public int ID_IULOTE { get; set; }
        public int ID_DDPLOTE { get; set; }
        public string CLAVECATASTRALANTIGUO_IULOTE { get; set; }
        public string  NUMEROPREDIO_IULOTE { get; set; }
        public string CLAVECATASTRALNUEVO_IULOTE { get; set; }

    }
}
