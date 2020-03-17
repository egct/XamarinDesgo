using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Desgo_Xamarin.Models.Db
{
    [Table("persona")]
    public class persona
    {
        [PrimaryKey]
        public int ID_PERSONA { get; set; }

        [MaxLength(50)]
        public string PNOMBRE_PERSONA { get; set; }

        [MaxLength(50)]
        public string SNOMBRE_PERSONA { get; set; }

        [MaxLength(50)]
        public string PAPELLIDO_PERSONA { get; set; }

        [MaxLength(50)]
        public string SAPELLIDO_PERSONA { get; set; }

        [MaxLength(10)]
        public string TELEFONO_PERSONA { get; set; }

        [MaxLength(100)]
        public string CORREO_PERSONA { get; set; }

        [MaxLength(50)]
        public string CARGO_PERSONA { get; set; }

        [MaxLength(50)]
        public string PROFESION_PERSONA { get; set; }

        [MaxLength(50), Unique]
        public string CEDULA_PERSONA { get; set; }

        [MaxLength(1000)]
        public string FOTO_PERSONA { get; set; }

        [MaxLength(100)]
        public string EMPRESA_PERSONA { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<usuario> USUARIOS_PERSONA { get; set; }

        public override int GetHashCode()
        {
            return ID_PERSONA;
        }
    }
}
