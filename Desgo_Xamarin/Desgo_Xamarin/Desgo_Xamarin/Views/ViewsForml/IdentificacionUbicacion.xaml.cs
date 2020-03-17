using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desgo_Xamarin.ViewModels.ViewsModelsForm;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Desgo_Xamarin.Models;
using Desgo_Xamarin.Models.Db;
using Desgo_Xamarin.Data;
using Desgo_Xamarin.Interfaces;
using System.Collections.ObjectModel;

namespace Desgo_Xamarin.Views.ViewsForml
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IdentificacionUbicacion : ContentPage
	{
        persona personavista = new persona();
        usuario uservista = new usuario();
        formulario formvista = new formulario();
        public IdentificacionUbicacion ()
		{
			InitializeComponent ();
            //BindingContext = new IdentificacionUbicacionlModel();
                var config = DependencyService.Get<IConfig>();
                string aux = System.IO.Path.Combine(config.DirectoryDB, "DesgoXamarin.db3");
                lblmensaje.Text = aux;
           
            try
            {
                using (DataAccess datos = new DataAccess())
                {
                     
                    lblmensaje.Text = datos.GetListPersona().ToString();
                    listapersonas.ItemsSource= datos.GetListPersona();
                }
            }
            catch (Exception e)
            {
                lblmensaje.Text = e.Message+aux+">";
            }
        }

        private void AgregarButton_Clicked(object sender, EventArgs e)
        {
            persona per = new persona()
            {
                PNOMBRE_PERSONA = txtnombre.Text,
                SNOMBRE_PERSONA = "gio",
                PAPELLIDO_PERSONA = "cuichan",
                SAPELLIDO_PERSONA ="tipan",
                TELEFONO_PERSONA = "098446771",
                CORREO_PERSONA ="egct@outlook.es",
                CARGO_PERSONA="Ingeniero",
                PROFESION_PERSONA="Ingeniero",
                CEDULA_PERSONA ="1723953073",
                FOTO_PERSONA="xxx",
                EMPRESA_PERSONA="DESGO"

            };

            using (DataAccess datos = new DataAccess())
            {
                datos.Insert(per);
                lblmensaje.Text = "insertado persona";

            }
        }

        private void ListaButton_Clicked(object sender, EventArgs e)
        {
            using (DataAccess datos = new DataAccess())
            {

                lblmensaje.Text = datos.GetListPersona().ToString();
                listapersonas.ItemsSource = new ObservableCollection<persona>(datos.GetListPersona());
                int aux = Int32.Parse( txtid.Text);
                personavista = datos.getPersona(aux);
                try
                {
                    lblmensaje.Text = personavista.PNOMBRE_PERSONA.ToString();

                }
                catch (Exception ee)
                {
                    lblmensaje.Text = ee.Message;

                }


            }

        }

        private void AgregarUsuarioButton_Clicked(object sender, EventArgs e)
        {
            usuario per = new usuario()
            {
                USUARIO_USUARIO = txtusuario.Text,
                ID_TIPOUSUARIO=1,
                ID_PERSONA=1,
                CONTRASENIA_USUARIO="1234",
                EMPRESA_USUARIO="DESGO",
                SALT_USUARIO=1235

            };

            using (DataAccess datos = new DataAccess())
            {
                datos.Insertusuario(per);
                lblmensaje.Text = "insertado user";

            }
        }

        private void AgregarFormularioButton_Clicked(object sender, EventArgs e)
        {
            formulario per = new formulario()
            {
                CODIGO_FORMULARIO = Int32.Parse(txtformulario.Text),
                ID_USUARIO = 1
                
            };

            using (DataAccess datos = new DataAccess())
            {
                datos.Insertformulario(per);
                lblmensaje.Text = "insertado formulario";

            }
        }

        private void ListaUsuariosButton_Clicked(object sender, EventArgs e)
        {
            using (DataAccess datos = new DataAccess())
            {

                lblmensaje.Text = datos.GetListUsuario().ToString();
                listapersonas.ItemsSource = datos.GetListUsuario();
                int aux = Int32.Parse(txtidUsuario.Text);
                uservista = datos.getUsuario(aux);
                try
                {
                    lblmensaje.Text = uservista.USUARIO_USUARIO.ToString()+uservista.SALT_USUARIO.ToString();
                    datos.updateCambiosEstadoSqlite(false);

                }
                catch (Exception ee)
                {
                    lblmensaje.Text = ee.Message;

                }

            }
        }

        private void ListaFormularioButton_Clicked(object sender, EventArgs e)
        {
            using (DataAccess datos = new DataAccess())
            {
                lblmensaje.Text = datos.GetListFormulario().ToString();
                listapersonas.ItemsSource = datos.GetListFormulario();
                int aux = Int32.Parse(txtidformulario.Text);
                formvista = datos.getFormulario(aux);
                try
                {
                    lblmensaje.Text = formvista.CODIGO_FORMULARIO.ToString();
                    datos.updateCambiosEstadoSqlite(true);

                }
                catch (Exception ee)
                {
                    lblmensaje.Text = ee.Message;

                }
                datos.deleteTable();
            }

        }
    }
}