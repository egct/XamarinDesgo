using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Desgo_Xamarin.Droid.Class
{
    public class usuarioC
    {
        public int ID_USUARIO { get; set; }

        public int ID_TIPOUSUARIO { get; set; }

        public int ID_PERSONA { get; set; }

        public string USUARIO_USUARIO { get; set; }

        public string CONTRASENIA_USUARIO { get; set; }

        public string EMPRESA_USUARIO { get; set; }

        public int SALT_USUARIO { get; set; }

    }
}
