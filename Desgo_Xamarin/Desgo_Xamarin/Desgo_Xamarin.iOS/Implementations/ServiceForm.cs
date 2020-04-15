﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Desgo_Xamarin.Interfaces;
using Xamarin.Forms;
using Desgo_Xamarin.Models.Db;
using Desgo_Xamarin.iOS.AmazonDesgoWebServiceForm;

[assembly: Dependency(typeof(Desgo_Xamarin.iOS.Implementations.ServiceForm))]
namespace Desgo_Xamarin.iOS.Implementations
{
    class ServiceForm : IServiceForm
    {
        public List<Models.Db.formulario> listarFormularios(int usuar)
        {
            var serviciof = new WSGestionFormulario();
            formularioIds formIds = new formularioIds();
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

        public formularioAll listarFormulariosAll(int codigo, int codigo2, int codigo3)
        {
            var serviciof = new WSGestionFormulario();
            formularioAll formAll = new formularioAll();
            /***/
            identificacionubicacionlote iul = new identificacionubicacionlote();
            DDescriptivosPredio_IULote dDescriptivosPredio_IULote = new DDescriptivosPredio_IULote();
            Direccion_DDPLote direccion_DDPLote = new Direccion_DDPLote();
            /***/
            AmazonDesgoWebServiceForm.formulario f = new AmazonDesgoWebServiceForm.formulario();
            AmazonDesgoWebServiceForm.formularioIds fid = new AmazonDesgoWebServiceForm.formularioIds()
            {
                idFormulario = codigo,
                identificacionU_F = codigo2,
            };
            AmazonDesgoWebServiceForm.user us = new AmazonDesgoWebServiceForm.user()
            {
                ID_USUARIO = 1,
            };
            try
            {
                f = serviciof.listarFormularioInt(codigo, codigo2);

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

        public string pruebaConAws()
        {
            var serviciof = new WSGestionFormulario();
            pruebaAws p = new pruebaAws();
            p.idPrueba = 1;
            p.idPruebaSpecified = true;
            p.hora = 1343;
            p.horaSpecified = true;
            p.nombre = "EGCT";
            string res = "";
            res = serviciof.cambiopruebaAWS(p);
            return res;
            
        }

        public bool sincronizarBD(List<formularioAll> form)
        {
            List<AmazonDesgoWebServiceForm.formulario> f = new List<AmazonDesgoWebServiceForm.formulario>();
            var serviciof = new WSGestionFormulario();
            bool banderaAws = false;
            try
            {
                foreach (formularioAll i in form)
                {
                    f.Add(convertirFAlltoFAws(i));
                }
                //envia datos a AWS
                foreach (AmazonDesgoWebServiceForm.formulario i in f)
                {
                    banderaAws = serviciof.editarformulario(i);
                    if (!banderaAws)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public AmazonDesgoWebServiceForm.formulario convertirFAlltoFAws(formularioAll formAll)
        {
            AmazonDesgoWebServiceForm.formulario f = new AmazonDesgoWebServiceForm.formulario();
            /***/
            //SQlite
            identificacionubicacionlote iul = formAll.identificacionubicacionlote;
            DDescriptivosPredio_IULote dDescriptivosPredio_IULote = formAll.identificacionubicacionlote.dDescriptivosPredio_IULote;
            Direccion_DDPLote direccion_DDPLote = formAll.identificacionubicacionlote.dDescriptivosPredio_IULote.dDPLote;
            //AWS
            identificacionUF uF = new identificacionUF();
            dDescriptivosPredioIULote dDescriptivosPredio = new dDescriptivosPredioIULote();
            direccionDDPLote direccion = new direccionDDPLote();
            dDescriptivosPredio.dDPLote = direccion;
            uF.dDescriptivosPredio_IULote = dDescriptivosPredio;
            f.identificacionU_F = uF;
            /***/

            f.idFormulario = formAll.ID_FORMULARIO;
            f.idFormularioSpecified = true;
            f.codigo_F = formAll.CODIGO_FORMULARIO;
            f.codigo_FSpecified = true;
            /****/
            f.identificacionU_F.ID_IULOTE = iul.ID_IULOTE;
            f.identificacionU_F.ID_IULOTESpecified = true;
            f.identificacionU_F.ID_DDPLOTE = iul.ID_DDPLOTE;
            f.identificacionU_F.ID_DDPLOTESpecified = true;
            f.identificacionU_F.CLAVECATASTRALNUEVO_IULOTE = iul.CLAVECATASTRALNUEVO_IULOTE;
            f.identificacionU_F.CLAVECATASTRALANTIGUO_IULOTE = iul.CLAVECATASTRALANTIGUO_IULOTE;
            f.identificacionU_F.NUMEROPREDIO_IULOTE = iul.NUMEROPREDIO_IULOTE;

            f.identificacionU_F.dDescriptivosPredio_IULote.ID_DDPLOTE = dDescriptivosPredio_IULote.ID_DDPLOTE;
            f.identificacionU_F.dDescriptivosPredio_IULote.ID_DDPLOTESpecified = true;
            f.identificacionU_F.dDescriptivosPredio_IULote.ID_DLOTE = dDescriptivosPredio_IULote.ID_DLOTE;
            f.identificacionU_F.dDescriptivosPredio_IULote.ID_DLOTESpecified = true;
            f.identificacionU_F.dDescriptivosPredio_IULote.NOMBREEDIFICIO_DDPLOTE = dDescriptivosPredio_IULote.NOMBREEDIFICIO_DDPLOTE;
            f.identificacionU_F.dDescriptivosPredio_IULote.NOMBRESECTOR_DDPLOTE = dDescriptivosPredio_IULote.NOMBRESECTOR_DDPLOTE;
            f.identificacionU_F.dDescriptivosPredio_IULote.USOPREDIO_DDPLOTE = dDescriptivosPredio_IULote.USOPREDIO_DDPLOTE;
            f.identificacionU_F.dDescriptivosPredio_IULote.TIPOPREDIO_DDPLOTE = dDescriptivosPredio_IULote.TIPOPREDIO_DDPLOTE;
            f.identificacionU_F.dDescriptivosPredio_IULote.REGIMENTENECIA_DDPLOTE = dDescriptivosPredio_IULote.REGIMENTENECIA_DDPLOTE;

            f.identificacionU_F.dDescriptivosPredio_IULote.dDPLote.ID_DLOTE = direccion_DDPLote.ID_DLOTE;
            f.identificacionU_F.dDescriptivosPredio_IULote.dDPLote.ID_DLOTESpecified = true;
            f.identificacionU_F.dDescriptivosPredio_IULote.dDPLote.INTERSECCION_DLOTE = direccion_DDPLote.INTERSECCION_DLOTE;
            f.identificacionU_F.dDescriptivosPredio_IULote.dDPLote.NO_DLOTE = direccion_DDPLote.NO_DLOTE;
            f.identificacionU_F.dDescriptivosPredio_IULote.dDPLote.CALLEP_DLOTE = direccion_DDPLote.CALLEP_DLOTE;


            /***/
            f.idUsuario_F = formAll.ID_USUARIO;
            f.idUsuario_FSpecified = true;
            f.estado_FSpecified = true;
            if (formAll.ESTADO_FORMULARIO == true)
            {
                f.estado_F = 1;
            }
            else
            {
                f.estado_F = 0;
            }
            return f;
        }

    }
}