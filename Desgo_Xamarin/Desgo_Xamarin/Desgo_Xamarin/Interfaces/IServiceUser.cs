using System;
using System.Collections.Generic;
using System.Text;

namespace Desgo_Xamarin.Interfaces
{
    interface IServiceUser
    {
        string validarUsuario(string usuario, string contra);
    }
}
