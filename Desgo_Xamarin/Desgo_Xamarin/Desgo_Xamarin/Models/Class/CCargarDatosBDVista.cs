using Desgo_Xamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desgo_Xamarin.Models.Class
{
    public class CCargarDatosBDVista
    {
        public void cargarIdentificacionUbicacion()
        {
            MainViewModel.GetInstance().IdentificacionUbicacionPart.ID_IULOTE = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.ID_IULOTE;
            MainViewModel.GetInstance().IdentificacionUbicacionPart.ID_DDPLOTE = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.ID_DDPLOTE;

            MainViewModel.GetInstance().IdentificacionUbicacionPart.CLAVECATASTRALANTIGUO_IULOTE = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.CLAVECATASTRALANTIGUO_IULOTE;
            MainViewModel.GetInstance().IdentificacionUbicacionPart.CLAVECATASTRALNUEVO_IULOTE = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.CLAVECATASTRALNUEVO_IULOTE;
            MainViewModel.GetInstance().IdentificacionUbicacionPart.NUMEROPREDIO_IULOTE = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.NUMEROPREDIO_IULOTE;

            MainViewModel.GetInstance().IdentificacionUbicacionPart.ID_DDPLOTEDP = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.ID_DDPLOTE;
            MainViewModel.GetInstance().IdentificacionUbicacionPart.ID_DLOTE = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.ID_DLOTE;
            MainViewModel.GetInstance().IdentificacionUbicacionPart.NOMBRESECTOR_DDPLOTE = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.NOMBRESECTOR_DDPLOTE;
            MainViewModel.GetInstance().IdentificacionUbicacionPart.NOMBREEDIFICIO_DDPLOTE = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.NOMBREEDIFICIO_DDPLOTE; ;
            MainViewModel.GetInstance().IdentificacionUbicacionPart.USOPREDIO_DDPLOTE = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.USOPREDIO_DDPLOTE;

            MainViewModel.GetInstance().IdentificacionUbicacionPart.ID_DLOTEDIR = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.dDPLote.ID_DLOTE;
            MainViewModel.GetInstance().IdentificacionUbicacionPart.CALLEP_DLOTE = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.dDPLote.CALLEP_DLOTE;
            MainViewModel.GetInstance().IdentificacionUbicacionPart.NO_DLOTE = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.dDPLote.NO_DLOTE;
            MainViewModel.GetInstance().IdentificacionUbicacionPart.INTERSECCION_DLOTE = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.dDPLote.INTERSECCION_DLOTE;

            MainViewModel.GetInstance().IdentificacionUbicacionPart.MyTipoPredioForm = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.TIPOPREDIO_DDPLOTE;
            MainViewModel.GetInstance().IdentificacionUbicacionPart.MyRegimenTenenciaForm = MainViewModel.GetInstance().Formularioall.identificacionubicacionlote.dDescriptivosPredio_IULote.REGIMENTENECIA_DDPLOTE;           

        }
    }
}
