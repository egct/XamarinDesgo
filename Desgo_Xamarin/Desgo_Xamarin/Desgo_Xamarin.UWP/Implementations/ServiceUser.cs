using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;
using Desgo_Xamarin.Interfaces;

[assembly: Dependency(typeof(Desgo_Xamarin.UWP.Implementations.ServiceUser))]
namespace Desgo_Xamarin.UWP.Implementations
{
    class ServiceUser : IServiceUser
    {
        public string validarUsuario(string usuario, string contra)
        {
            //var servicio = new AmazonDesgoWebService.WSGestionUsuario();
            //AmazonDesgoWebService.user usuarioaux = new AmazonDesgoWebService.user();
            //usuarioaux = servicio.login(usuario, contra);
            //return "Amazon" + usuarioaux.EMPRESA_USUARIO + "-" + usuarioaux.USUARIO_USUARIO + "-" + usuarioaux.persona.CEDULA_PERSONA + "-" + usuarioaux.persona.PNOMBRE_PERSONA;
            return "windowsegct";
        }
    }
}
