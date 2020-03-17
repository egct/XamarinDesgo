using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Desgo_Xamarin.ViewModels.ViewsModelsForm
{
    class GraficosPredioModel : BaseViewModel
    {
        #region Attribute
        private MediaFile filePlano;
        private MediaFile fileFachada;
        private ImageSource imageSourcePlano;
        private ImageSource imageSourceFachada;
        #endregion

        #region Propierties
        public ImageSource ImageSourcePlano
        {
            get { return this.imageSourcePlano; }
            set { SetValue(ref this.imageSourcePlano, value); }
        }
        public ImageSource ImageSourceFachada
        {
            get { return this.imageSourceFachada; }
            set { SetValue(ref this.imageSourceFachada, value); }
        }

        #endregion
        #region Constructors
        public GraficosPredioModel()
        {
            this.imageSourcePlano = "Desgo";
            this.imageSourceFachada = "Desgo";

        }
        #endregion
        #region Command
        public ICommand ChangeImagePlanoCommand
        {
            get
            {
                return new RelayCommand(ChangeImagePlano);
            }
        }
        public ICommand ChangeImageFachadaCommand
        {
            get
            {
                return new RelayCommand(ChangeImageFachada);
            }
        }


        private async void ChangeImagePlano()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                "¿De donde desea tomar la Imagen?",
                "Cancelar",
                null,
                "Galeria",
                "Nueva Foto");
            if(source == "Cancelar")
            {
                this.filePlano = null;
                return;
            }
            if(source=="Nueva Foto")
            {
                this.filePlano = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "testPlano.jpg",
                        PhotoSize = PhotoSize.Small
                    });
            }
            else
            {
                this.filePlano = await CrossMedia.Current.PickPhotoAsync();
            }
            if (this.filePlano != null)
            {
                this.ImageSourcePlano = ImageSource.FromStream(
                    ()=> {
                        var stream = filePlano.GetStream();
                        return stream;
                    });
            }
        }


        private async void ChangeImageFachada()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                "¿De donde desea tomar la Imagen?",
                "Cancelar",
                null,
                "Galeria",
                "Nueva Foto");
            if (source == "Cancelar")
            {
                this.fileFachada = null;
                return;
            }
            if (source == "Nueva Foto")
            {
                this.fileFachada = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "testFachada.jpg",
                        PhotoSize = PhotoSize.Small
                    });
            }
            else
            {
                this.fileFachada = await CrossMedia.Current.PickPhotoAsync();
            }
            if (this.fileFachada != null)
            {
                this.ImageSourceFachada = ImageSource.FromStream(
                    () => {
                        var stream = fileFachada.GetStream();
                        return stream;
                    });
            }
        }
        #endregion
    }
}
