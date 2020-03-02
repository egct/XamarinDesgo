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
	public partial class MasterPage1Loggedin : MasterDetailPage
    {
        public MasterPage1Loggedin ()
		{
			InitializeComponent ();
		}
        /******/
        
        /*****/
        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Navigator = Navigator;
            App.Masterloggedin = this;
        }
    }
}