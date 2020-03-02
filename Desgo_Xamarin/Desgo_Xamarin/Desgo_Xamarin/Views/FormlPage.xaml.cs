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

namespace Desgo_Xamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FormlPage : CarouselPage
    {
		public FormlPage ()
		{
			InitializeComponent ();
            
        }

        private void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (DataAccess datos = new DataAccess())
                {
                    SQLiteConnection connection=datos.SQLiteConnection();
                //    connection.Query<>
                }
            }
            catch (Exception ex)
            {
              //  lblmensaje.Text = e.Message + aux + ">";
            }
        }
    }
}