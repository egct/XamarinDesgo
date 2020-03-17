using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;


namespace Desgo_Xamarin.ViewModels.ViewsModelsForm
{
    class ElementosConstructivosModel : BaseViewModel
    {
        #region Attribute
        private StackLayout stacklayout;
        private string mensaje;
        #endregion

        public StackLayout Stacklayout
        {
            get { return this.stacklayout; }
            set { SetValue(ref this.stacklayout, value); }
        }
        public string Mensaje
        {
            get { return this.mensaje; }
            set { SetValue(ref this.mensaje, value); }
        }


        #region Constructors
        public ElementosConstructivosModel()
        {
            
        }

        private void BtnCliente_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Commands

        public ICommand AgregarECCommand
        {
            get
            {
                return new RelayCommand(AgregarEC);
            }
        }

        private void AgregarEC()
        {
            StackLayout stack = new StackLayout { Orientation = StackOrientation.Vertical };
            Entry entry1 = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand };
            Entry entry2 = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand };
            stack.Children.Add(entry1);
            stack.Children.Add(entry2);
            this.Stacklayout = stack;
            this.Mensaje = "Agregando....";
            Button btnCliente = new Button();
            btnCliente.Text = "Desgo";
            btnCliente.Clicked += BtnCliente_Click;
            btnCliente.BackgroundColor = Color.DarkGreen;
            btnCliente.TextColor = Color.Black;
            btnCliente.HeightRequest = 50;
            btnCliente.WidthRequest = 50;
            this.Stacklayout.Children.Add(btnCliente);
            this.Mensaje = btnCliente.Text;

        }
        #endregion
    }
}
