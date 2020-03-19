using Desgo_Xamarin.Data;
using Desgo_Xamarin.Models.Db;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desgo_Xamarin.Models.Class
{
    class CPrepararDatosSycn
    {
        public formularioAll convertirFtoFAll(formulario f)
        {
            formularioAll fAll = new formularioAll();

            using (DataAccess datos = new DataAccess())
            {
                //fAll.ID_FORMULARIO = f.ID_FORMULARIO;
                //fAll.CODIGO_FORMULARIO = f.CODIGO_FORMULARIO;
                //fAll.ESTADO_FORMULARIO = f.ESTADO_FORMULARIO;
                //fAll.USUARIO_FORMULARIO = f.ID_USUARIO;
                fAll = datos.getFormularioAllCodigo(f.CODIGO_FORMULARIO);
            }
            return fAll;

        }

    }
}
