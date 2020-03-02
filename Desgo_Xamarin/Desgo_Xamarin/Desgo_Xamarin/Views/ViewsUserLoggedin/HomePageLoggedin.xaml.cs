using Desgo_Xamarin.Data;
using Desgo_Xamarin.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Desgo_Xamarin.Views.ViewsUserLoggedin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePageLoggedin : ContentPage
	{
		public HomePageLoggedin ()
		{
			InitializeComponent ();
            try
            {
                persona per = new persona()
                {
                    PNOMBRE_PERSONA = "edwin",
                    SNOMBRE_PERSONA = "gio",
                    PAPELLIDO_PERSONA = "cuichan",
                    SAPELLIDO_PERSONA = "tipan",
                    TELEFONO_PERSONA = "098446771",
                    CORREO_PERSONA = "egct@outlook.es",
                    CARGO_PERSONA = "Ingeniero",
                    PROFESION_PERSONA = "Ingeniero",
                    CEDULA_PERSONA = "1723953073",
                    FOTO_PERSONA = "xxx",
                    EMPRESA_PERSONA = "DESGO"
                };
                usuario us = new usuario()
                {
                    USUARIO_USUARIO = "edwindesgo",
                    ID_TIPOUSUARIO = 1,
                    ID_PERSONA = 1,
                    CONTRASENIA_USUARIO = "1234",
                    EMPRESA_USUARIO = "DESGO",
                    SALT_USUARIO = 1235
                };

                usuario us2 = new usuario()
                {
                    USUARIO_USUARIO = "egct",
                    ID_TIPOUSUARIO = 1,
                    ID_PERSONA = 1,
                    CONTRASENIA_USUARIO = "1234",
                    EMPRESA_USUARIO = "DESGO",
                    SALT_USUARIO = 1235
                };
                formulario form = new formulario()
                {
                    CODIGO_FORMULARIO = Int32.Parse("1723953053"),
                    ID_USUARIO = 1
                };
                using (DataAccess datos = new DataAccess())
                {
                    if (datos.getPersona(per.CEDULA_PERSONA) == null)
                    {
                        datos.Insert(per);
                        if (datos.getUsuario(us.USUARIO_USUARIO) == null)
                        {
                            datos.Insertusuario(us);
                            if (datos.getFormularioCodigo(form.CODIGO_FORMULARIO) == null)
                            {
                                datos.Insertformulario(form);
                                lblmensaje.Text = "agregado";
                            }
                            lblmensaje.Text = "existe el form";
                        }
                        lblmensaje.Text = "existe el usuario 1";
                    }
                    lblmensaje.Text = "existe la persona";
                }
                using (DataAccess datos = new DataAccess())
                {
                    
                        if (datos.getUsuario(us2.USUARIO_USUARIO) == null)
                        {
                            datos.Insertusuario(us2);
                            
                        }
                        lblmensaje.Text = "existe el usuario 2";

                }

            }
            catch (Exception e)
            {
                lblmensaje.Text = "error: "+e;
            }
        }
	}
}