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
	public partial class MyFormsPageLoggedin : ContentPage
	{
		public MyFormsPageLoggedin ()
		{
			InitializeComponent ();
            //try
            //{
            //    using (DataAccess datos = new DataAccess())
            //    {

            //        listapersonas.ItemsSource = datos.GetListPersona();
            //    }
            //}
            //catch (Exception e)
            //{
            //   // lblmensaje.Text = e.Message + ">";
            //}
        }

    }
}