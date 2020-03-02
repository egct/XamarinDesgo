using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Desgo_Xamarin.Models.Db
{
    public class tipousuario
    {
        [PrimaryKey, AutoIncrement]
        public int ID_TIPOUSUARIO { get; set; }

        [MaxLength(50)]
        public string NIVEL_TIPOUSUARIO { get; set; }
        public override int GetHashCode()
        {
            return ID_TIPOUSUARIO;
        }
    }
}
