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
	public partial class MasterPage1 : MasterDetailPage
	{
		public MasterPage1 ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Navigator = Navigator;
            App.Master = this;
        }
    }
}