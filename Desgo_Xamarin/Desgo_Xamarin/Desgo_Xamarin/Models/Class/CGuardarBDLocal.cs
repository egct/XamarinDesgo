using Desgo_Xamarin.Data;
using Desgo_Xamarin.Models.Db;
using Desgo_Xamarin.ViewModels;
using Desgo_Xamarin.ViewModels.ViewsModelsForm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desgo_Xamarin.Models.Class
{
    public class CGuardarBDLocal
    {
        public bool guardarIdentificacionUbicacion()
        {
            
            using (DataAccess datos = new DataAccess())
            {
            
                try
                {
                    IdentificacionUbicacionlModel identificacionUbicacionModel = MainViewModel.GetInstance().IdentificacionUbicacionPart;
                    /***/
                    identificacionubicacionlote iulote = new identificacionubicacionlote();
                    iulote.ID_IULOTE = identificacionUbicacionModel.ID_IULOTE;
                    iulote.ID_DDPLOTE = identificacionUbicacionModel.ID_DDPLOTE;

                    iulote.CLAVECATASTRALANTIGUO_IULOTE = identificacionUbicacionModel.CLAVECATASTRALANTIGUO_IULOTE;
                    iulote.CLAVECATASTRALNUEVO_IULOTE = identificacionUbicacionModel.CLAVECATASTRALNUEVO_IULOTE;
                    iulote.NUMEROPREDIO_IULOTE = identificacionUbicacionModel.NUMEROPREDIO_IULOTE;

                    DDescriptivosPredio_IULote dDescriptivos = new DDescriptivosPredio_IULote();
                    dDescriptivos.ID_DDPLOTE = identificacionUbicacionModel.ID_DDPLOTEDP;
                    dDescriptivos.ID_DLOTE = identificacionUbicacionModel.ID_DLOTE;
                    dDescriptivos.NOMBRESECTOR_DDPLOTE = identificacionUbicacionModel.NOMBRESECTOR_DDPLOTE;
                    dDescriptivos.NOMBREEDIFICIO_DDPLOTE = identificacionUbicacionModel.NOMBREEDIFICIO_DDPLOTE;
                    dDescriptivos.USOPREDIO_DDPLOTE = identificacionUbicacionModel.USOPREDIO_DDPLOTE;
                    dDescriptivos.TIPOPREDIO_DDPLOTE = identificacionUbicacionModel.MyTipoPredio;
                    dDescriptivos.REGIMENTENECIA_DDPLOTE = identificacionUbicacionModel.MyRegimenTenencia;

                    Direccion_DDPLote dDPLote = new Direccion_DDPLote();
                    dDPLote.ID_DLOTE = identificacionUbicacionModel.ID_DLOTEDIR;
                    dDPLote.CALLEP_DLOTE = identificacionUbicacionModel.CALLEP_DLOTE;
                    dDPLote.NO_DLOTE = identificacionUbicacionModel.NO_DLOTE;
                    dDPLote.INTERSECCION_DLOTE = identificacionUbicacionModel.INTERSECCION_DLOTE;

                    /****/
                    datos.updateIdentificacionUbicacion(iulote);
                    datos.updateDDescriptivosPredio_IULote(dDescriptivos);
                    datos.updateDireccion_DDPLote(dDPLote);
                    datos.updateCambiosEstadoSqlite(true);
                    return true;    
                    
                }
                catch (Exception ee)
                {
                    
                }
            }
            return false;
        }
    }
}
