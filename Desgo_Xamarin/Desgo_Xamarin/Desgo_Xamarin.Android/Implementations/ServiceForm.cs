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
using Desgo_Xamarin.Droid.AmazonDesgoWebServiceForm1;
using Desgo_Xamarin.Interfaces;
using Desgo_Xamarin.Models.Db;
using Xamarin.Forms;

[assembly: Dependency(typeof(Desgo_Xamarin.Droid.Implementations.ServiceForm))]
namespace Desgo_Xamarin.Droid.Implementations
{
    class ServiceForm : IServiceForm
    {
        public List<Models.Db.formulario> listarFormularios(int usuar)
        {
            var serviciof = new WSGestionFormulario();            
            formularioIds formIds= new formularioIds();
            Models.Db.formulario form = new Models.Db.formulario(); ;
            List<Models.Db.formulario> formlist = new List<Models.Db.formulario>();
            try
            {
                //var b = serviciof.listarFormulariosId(1);
                //List<formularioIds> bc = b.ToList();
                int longitud = 0;
                longitud = serviciof.listarFormulariosId(usuar).Length;
                formularioIds[] listFormIds = new formularioIds[longitud];
                listFormIds = serviciof.listarFormulariosId(usuar);   
                foreach (formularioIds i in listFormIds)
                {
            
                    form.ID_FORMULARIO = i.idFormulario;
                    form.ID_IULOTE = i.identificacionU_F;
                    form.ID_USUARIO = i.idUsuario_F;
                    form.CODIGO_FORMULARIO = i.codigo_F;
                    if (i.estado_F == 0)
                    {
                        form.ESTADO_FORMULARIO = false;
                    }
                    else
                    {
                        form.ESTADO_FORMULARIO = true;
                    }
                    formlist.Add(form);
                    form = new Models.Db.formulario();
                }

            }
            catch (Exception e)
            {

            }

            return formlist;
        }

        public formularioAll listarFormulariosAll(int codigo,int codigo3,int codigo2)
        {
            var serviciof = new WSGestionFormulario();
            formularioAll formAll = new formularioAll();
            /***/
            identificacionubicacionlote iul = new identificacionubicacionlote();
            DDescriptivosPredio_IULote dDescriptivosPredio_IULote = new DDescriptivosPredio_IULote();
            Direccion_DDPLote direccion_DDPLote = new Direccion_DDPLote();
            /***/
            AmazonDesgoWebServiceForm1.formulario f = new AmazonDesgoWebServiceForm1.formulario();
            AmazonDesgoWebServiceForm1.formularioIds fid = new AmazonDesgoWebServiceForm1.formularioIds()
            {
                idFormulario = codigo,
                identificacionU_F = codigo2,
            };
            AmazonDesgoWebServiceForm1.user us = new AmazonDesgoWebServiceForm1.user()
            {
                ID_USUARIO = 1,
            };
            try
            {
                f = serviciof.listarFormularioInt(codigo,codigo2);
                
                formAll.ID_FORMULARIO = f.idFormulario;
                formAll.CODIGO_FORMULARIO = codigo3;
                /****/
                iul.ID_IULOTE = f.identificacionU_F.ID_IULOTE;
                iul.ID_DDPLOTE = f.identificacionU_F.ID_DDPLOTE;
                iul.CLAVECATASTRALNUEVO_IULOTE = f.identificacionU_F.CLAVECATASTRALNUEVO_IULOTE;
                iul.CLAVECATASTRALANTIGUO_IULOTE = f.identificacionU_F.CLAVECATASTRALANTIGUO_IULOTE;
                iul.NUMEROPREDIO_IULOTE = f.identificacionU_F.NUMEROPREDIO_IULOTE;

                dDescriptivosPredio_IULote.ID_DDPLOTE = f.identificacionU_F.dDescriptivosPredio_IULote.ID_DDPLOTE;
                dDescriptivosPredio_IULote.ID_DLOTE = f.identificacionU_F.dDescriptivosPredio_IULote.ID_DLOTE;
                dDescriptivosPredio_IULote.NOMBREEDIFICIO_DDPLOTE = f.identificacionU_F.dDescriptivosPredio_IULote.NOMBREEDIFICIO_DDPLOTE;
                dDescriptivosPredio_IULote.NOMBRESECTOR_DDPLOTE = f.identificacionU_F.dDescriptivosPredio_IULote.NOMBRESECTOR_DDPLOTE;
                dDescriptivosPredio_IULote.USOPREDIO_DDPLOTE = f.identificacionU_F.dDescriptivosPredio_IULote.USOPREDIO_DDPLOTE;
                dDescriptivosPredio_IULote.TIPOPREDIO_DDPLOTE = f.identificacionU_F.dDescriptivosPredio_IULote.TIPOPREDIO_DDPLOTE;
                dDescriptivosPredio_IULote.REGIMENTENECIA_DDPLOTE = f.identificacionU_F.dDescriptivosPredio_IULote.REGIMENTENECIA_DDPLOTE;

                direccion_DDPLote.ID_DLOTE = f.identificacionU_F.dDescriptivosPredio_IULote.dDPLote.ID_DLOTE;
                direccion_DDPLote.INTERSECCION_DLOTE = f.identificacionU_F.dDescriptivosPredio_IULote.dDPLote.INTERSECCION_DLOTE;
                direccion_DDPLote.NO_DLOTE = f.identificacionU_F.dDescriptivosPredio_IULote.dDPLote.NO_DLOTE;
                direccion_DDPLote.CALLEP_DLOTE = f.identificacionU_F.dDescriptivosPredio_IULote.dDPLote.CALLEP_DLOTE;

                formAll.identificacionubicacionlote = iul;
                formAll.identificacionubicacionlote.dDescriptivosPredio_IULote = dDescriptivosPredio_IULote;
                formAll.identificacionubicacionlote.dDescriptivosPredio_IULote.dDPLote = direccion_DDPLote;

                /***/
                formAll.ID_USUARIO = f.idUsuario_F;
                //formAll.CODIGO_FORMULARIO = f.codigo_F;
                if (f.estado_F == 0)
                {
                    formAll.ESTADO_FORMULARIO = false;
                }
                else
                {
                    formAll.ESTADO_FORMULARIO = true;
                }
                return formAll;


            }
            catch (Exception e)
            {

            }

            return formAll;
        }
    }
}