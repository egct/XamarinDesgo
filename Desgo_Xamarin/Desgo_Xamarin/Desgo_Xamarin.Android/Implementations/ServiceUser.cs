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
using Desgo_Xamarin.Models.Db;
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
            persona per = new persona();
            us.PERSONA_PERSONA = per;
            us.PERSONA_PERSONA.ID_PERSONA = usuarioaux.persona.ID_PERSONA;
            us.PERSONA_PERSONA.PNOMBRE_PERSONA = usuarioaux.persona.PNOMBRE_PERSONA;
            us.PERSONA_PERSONA.SNOMBRE_PERSONA = usuarioaux.persona.SNOMBRE_PERSONA;
            us.PERSONA_PERSONA.PAPELLIDO_PERSONA = usuarioaux.persona.PAPELLIDO_PERSONA;
            us.PERSONA_PERSONA.SAPELLIDO_PERSONA = usuarioaux.persona.SAPELLIDO_PERSONA;
            us.PERSONA_PERSONA.TELEFONO_PERSONA = usuarioaux.persona.TELEFONO_PERSONA;
            us.PERSONA_PERSONA.CEDULA_PERSONA = usuarioaux.persona.CEDULA_PERSONA;
            us.PERSONA_PERSONA.CARGO_PERSONA = usuarioaux.persona.CARGO_PERSONA;
            us.PERSONA_PERSONA.CORREO_PERSONA = usuarioaux.persona.CORREO_PERSONA;
            us.PERSONA_PERSONA.EMPRESA_PERSONA = usuarioaux.persona.EMPRESA_PERSONA;
            us.PERSONA_PERSONA.FOTO_PERSONA = usuarioaux.persona.FOTO_PERSONA;
            us.PERSONA_PERSONA.PROFESION_PERSONA = usuarioaux.persona.PROFESION_PERSONA;
            tipousuario tp = new tipousuario();
            us.TipoUsuario_PERSONA = tp;
            us.TipoUsuario_PERSONA.ID_TIPOUSUARIO = usuarioaux.tipoUsuario.ID_TIPOUSUARIO;
            us.TipoUsuario_PERSONA.NIVEL_TIPOUSUARIO = usuarioaux.tipoUsuario.NIVEL_TIPOUSUARIO;
            return us;

        }


    }
}