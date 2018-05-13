using ET31_XamarinMVVM.Model;
using ET31_XamarinMVVM.ViewModel;
using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ET31_XamarinMVVM.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaginaDetalle : ContentPage
	{
        Listado persona;
        ListadoViewModel listadoViewModel;

        public PaginaDetalle ()
        {
            InitializeComponent();

            persona = null;
            listadoViewModel = null;

            BotonHablar.IsEnabled = false;
            BotonWeb.IsEnabled = false;
            BotonGuardar.IsEnabled = false;
        }
		public PaginaDetalle (Listado p, ListadoViewModel viewModel)
		{
			InitializeComponent ();

            persona = p;
            BindingContext = persona;

            listadoViewModel = viewModel;

            BotonHablar.Clicked += BotonHablar_Clicked;
            BotonWeb.Clicked += BotonWeb_Clicked;
            BotonGuardar.Clicked += BotonGuardar_Clicked;
		}

        private async void BotonGuardar_Clicked(object sender, EventArgs e)
        {
            persona.Title = TituloEditable.Text;
            await listadoViewModel.UpdateListado(persona);
            await Navigation.PopModalAsync();
        }

        private void BotonHablar_Clicked(object sender, EventArgs e)
        {
            CrossTextToSpeech.Current.Speak(this.persona.Description);
        }

        private void BotonWeb_Clicked(object sender, EventArgs e)
        {
            if (persona.Website.StartsWith("http"))
            {
                Device.OpenUri(new Uri(persona.Website));
            }
        }
    }
}