using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Desgo_Xamarin.Models.Db
{
    [Table("estadoSqlite")]
    public class estadoSqlite
    {
        [PrimaryKey, AutoIncrement]
        public int ID_ESTADOSQLITE { get; set; }
        public string ESTADO_ESTADOSQLITE { get; set; }//existe
        public bool CAMBIOS_ESTADOSQLITE { get; set; }//true o false
        public int ID_USUARIO { get; set; }
        public string USUARIO_USUARIO { get; set; }
        public string CONTRASENIA_USUARIO { get; set; }
        public int SALT_USUARIO { get; set; }

    }
}
