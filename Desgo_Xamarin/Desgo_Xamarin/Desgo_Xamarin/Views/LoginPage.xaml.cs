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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (Password.IsPassword)
            {
                Password.IsPassword =false;
                imagenEyes.Source = "eyesTrue.png";
            }
            else
            {
                Password.IsPassword = true;
                imagenEyes.Source = "eyesFalse.png";

            }
            //            Password.IsPassword = Password.IsPassword ? false : true;          
        }
    }
}