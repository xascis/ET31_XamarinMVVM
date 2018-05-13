using ET31_XamarinMVVM.Model;
using ET31_XamarinMVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ET31_XamarinMVVM
{
	public partial class MainPage : MasterDetailPage
	{
		public MainPage()
		{
			InitializeComponent();

            MasterPage.listView.ItemSelected += listView_ItemSelected;

        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Listado persona = e.SelectedItem as Listado;
            if (persona != null)
            {
                PaginaDetalle paginaDetalle = new PaginaDetalle(persona, MasterPage.listadoViewModel);
                Detail = new NavigationPage(paginaDetalle);

                IsPresented = false; // hide master page

                MasterPage.listView.SelectedItem = null;
            }
        }
    }
}
