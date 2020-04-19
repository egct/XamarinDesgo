using Desgo_Xamarin.Data;
using Desgo_Xamarin.Views.ViewsForml;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Desgo_Xamarin.ViewModels;
using Desgo_Xamarin.ViewModels.ViewsModelsForm;
using Desgo_Xamarin.Models.Class;

namespace Desgo_Xamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FormlPage : CarouselPage
    {
        
        public FormlPage ()
		{
			InitializeComponent ();
         
        }

        private void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            CGuardarBDLocal cGuardarBDLocal = new CGuardarBDLocal();  
            try
            {
                if (cGuardarBDLocal.guardarIdentificacionUbicacion())
                {
                     
                    lblmensaje.Text = "Base de datos local Actualizada";
                }
                else
                {
                    lblmensaje.Text = "Base de datos local no se actualizo";

                }
            }
            catch (Exception ee)
            {
                lblmensaje.Text = ee.Message;
            }
        }

        private void BtnQuitarActualizar_Clicked(object sender, EventArgs e)
        {
            using (DataAccess datos = new DataAccess())
            {
                try
                {
                    datos.updateCambiosEstadoSqlite(false);
                    lblmensaje.Text = "Base de datos local sin Actualizar";
                }
                catch (Exception ee)
                {
                    lblmensaje.Text = ee.Message;
                }
            }
        }
    }
}