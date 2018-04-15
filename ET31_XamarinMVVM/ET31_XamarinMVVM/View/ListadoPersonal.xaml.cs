using ET31_XamarinMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ET31_XamarinMVVM.Model;
using System.Reflection;

namespace ET31_XamarinMVVM.View
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null) return null;
            string prefijo = Device.OnPlatform<string>("iOS.Resources", "Android.Assets", "UWP.Assets");
            string sufijo = Device.OnPlatform<string>("iOS.jpg", "And.jpg", "Win.jpg");
            string nombre = "ET31_XamarinMVVM." + prefijo + "." + Source + sufijo;
          
            return ImageSource.FromResource(Source, Assembly.GetExecutingAssembly());
        }
    }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListadoPersonal : ContentPage
	{
        ListadoViewModel listadoViewModel;

        public ListadoPersonal ()
		{
			InitializeComponent ();
            listadoViewModel = new ListadoViewModel();
            BindingContext = listadoViewModel;

            ListadoView.ItemSelected += ListadoView_ItemSelected;

            //imagenComun.Source = ImageSource.FromResource("ET31_XamarinMVVM.Assets.BannerXam.jpg",
            //    Assembly.GetExecutingAssembly()); // solo para W10 Fall Creators

            //string extension = Device.OnPlatform<string>(
            //    "iOS.Resources.BanneriOS", 
            //    "Android.Assets.BannerAnd", 
            //    "UWP.Assets.BannerWin.jpg");

            string nombre = "";
            switch (Device.RuntimePlatform)
            {
                case "iOS":
                    nombre = "BanneriOS.jpg";
                    break;
                case "Android":
                    nombre = "BannerAnd.jpg";
                    break;
                case "UWP":
                    nombre = "Assets/BannerWin.jpg";
                    break;
            }

            //imagenPlataforma.Source = ImageSource.FromResource("ET31_XamarinMVVM." + extension);
            imagenPlataforma.Source = ImageSource.FromFile(nombre);

        }

        private async void ListadoView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Listado persona = e.SelectedItem as Listado;
            if (persona != null)
            {
                PaginaDetalle paginaDetalle = new PaginaDetalle(persona, listadoViewModel);
                await Navigation.PushModalAsync(paginaDetalle);
                ListadoView.SelectedItem = null;
            }
        }
	}
}