using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Desgo_Xamarin.Models.Db
{
    [Table("DDescriptivosPredio_IULote")]
    public class DDescriptivosPredio_IULote
    {
        [PrimaryKey]
        public int ID_DDPLOTE { get; set; }
        public int ID_DLOTE { get; set; }
        public string NOMBRESECTOR_DDPLOTE { get; set; }
        public string NOMBREEDIFICIO_DDPLOTE { get; set; }
        public string USOPREDIO_DDPLOTE { get; set; }
        public string TIPOPREDIO_DDPLOTE { get; set; }
        public string REGIMENTENECIA_DDPLOTE { get; set; }
        [OneToOne]
        public Direccion_DDPLote dDPLote { get; set; }
    }
}
