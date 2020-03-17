using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Desgo_Xamarin.Models.Db
{
    [Table("usuario")]
    public class usuario
    {
        [PrimaryKey]
        public int ID_USUARIO { get; set; }

        [ForeignKey(typeof(tipousuario))]
        public int ID_TIPOUSUARIO { get; set; }

        [OneToOne]
        public tipousuario TipoUsuario_PERSONA { get; set; }


        [ForeignKey(typeof(persona))]
        public int ID_PERSONA { get; set; }

        [ManyToOne]
        public persona PERSONA_PERSONA { get; set; }

        [MaxLength(70), Unique]
        public string USUARIO_USUARIO { get; set; }

        public string CONTRASENIA_USUARIO { get; set; }

        [MaxLength(70)]
        public string EMPRESA_USUARIO { get; set; }

        [MaxLength(50)]
        public int SALT_USUARIO { get; set; }


        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<formulario> FORMULARIO_USUARIO { get; set; }

        public override int GetHashCode()
        {
            return ID_USUARIO;
        }

    }
}
