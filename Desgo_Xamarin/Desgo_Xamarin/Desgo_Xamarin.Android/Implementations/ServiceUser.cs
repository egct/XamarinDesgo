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
using Desgo_Xamarin.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(Desgo_Xamarin.Droid.Implementations.ServiceUser))]

namespace Desgo_Xamarin.Droid.Implementations
{
    class ServiceUser : IServiceUser
    {
        public string validarUsuario(string usuario, string contra)
        {
            var servicio = new AmazonDesgoWebService.WSGestionUsuario();
            AmazonDesgoWebService.user usuarioaux = new AmazonDesgoWebService.user();
            usuarioaux = servicio.login(usuario, contra);
            return "Amazon"+usuarioaux.EMPRESA_USUARIO +"-"+ usuarioaux.USUARIO_USUARIO+"-"+usuarioaux.persona.CEDULA_PERSONA + "-" + usuarioaux.persona.PNOMBRE_PERSONA;
        }
    }
}