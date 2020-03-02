using Desgo_Xamarin.ViewModels.ViewsModelsForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Desgo_Xamarin.Views.ViewsForml
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IdentificacionLegal : ContentPage
	{
		public IdentificacionLegal ()
		{
			InitializeComponent ();
            BindingContext = new IdentificacionLegalModel();
        }
	}
}