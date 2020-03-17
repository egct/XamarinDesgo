using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Desgo_Xamarin.Interfaces;
using Xamarin.Forms;
using Desgo_Xamarin.Models.Db;

[assembly: Dependency(typeof(Desgo_Xamarin.iOS.Implementations.ServiceUser))]
namespace Desgo_Xamarin.iOS.Implementations
{
    class ServiceUser: IServiceUser
    {
        public string validarUsuario(string usuario, string contra)
        {
            var servicio = new AmazonDesgoWebService.WSGestionUsuario();
            AmazonDesgoWebService.user usuarioaux = new AmazonDesgoWebService.user();
            usuarioaux = servicio.login(usuario, contra);
            return "Amazon" + usuarioaux.EMPRESA_USUARIO + "-" + usuarioaux.USUARIO_USUARIO + "-" + usuarioaux.persona.CEDULA_PERSONA + "-" + usuarioaux.persona.PNOMBRE_PERSONA;
        }
        public usuario validarUsuarioAmazon(string usuario, string contra)
        {
            var servicio = new AmazonDesgoWebService.WSGestionUsuario();
            AmazonDesgoWebService.user usuarioaux = new AmazonDesgoWebService.user();
            usuarioaux = servicio.login(usuario, contra);
            usuario us = new usuario();
            us.ID_PERSONA = usuarioaux.ID_PERSONA;
            us.ID_TIPOUSUARIO = usuarioaux.ID_TIPOUSUARIO;
            us.ID_USUARIO = usuarioaux.ID_USUARIO;
            us.CONTRASENIA_USUARIO = usuarioaux.CONTRASENIA_USUARIO;
            us.USUARIO_USUARIO = usuarioaux.USUARIO_USUARIO;
            us.EMPRESA_USUARIO = usuarioaux.EMPRESA_USUARIO;
            us.SALT_USUARIO = usuarioaux.SALT_USUARIO;
            return us;

        }
    }
}