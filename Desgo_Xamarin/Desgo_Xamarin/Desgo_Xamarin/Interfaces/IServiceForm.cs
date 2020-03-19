using System;
using System.Collections.Generic;
using System.Text;
using Desgo_Xamarin.Models.Db;

namespace Desgo_Xamarin.Interfaces
{
    interface IServiceForm
    {
         List<formulario> listarFormularios(int usuario);
         formularioAll listarFormulariosAll(int codigo, int codigo2 ,int codigo3);
        bool sincronizarBD(List<formularioAll> form);
         
    }
}
