using ET31_XamarinMVVM.Model;
using ET31_XamarinMVVM.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ET31_XamarinMVVM.ViewModel
{
    public class ListadoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool busy;
        public ObservableCollection<Listado> ListadoPersonal { get; set; }
        public Command RecuperaListado { get; set; }


        public ListadoViewModel()
        {
            RecuperaListado = new Command(
                async () => await GetLista(), 
                () => !EstaOcupado);

            ListadoPersonal = new ObservableCollection<Listado>();
        }

        private void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public bool EstaOcupado
        {
            get { return busy; }
            set {
                busy = value;
                OnPropertyChanged();
                RecuperaListado.ChangeCanExecute();
            }
        }

        private async Task GetLista()
        {
            if (EstaOcupado)
            {
                return;
            }

            Exception error;
            try
            {
                EstaOcupado = true;
                // Peticion del listado

                // Usando Azure
                var servicio = DependencyService.Get<ServicioAzure>();
                var items = await servicio.GetListado();
                ListadoPersonal.Clear();
                foreach (var item in items)
                {
                    ListadoPersonal.Add(item);
                }

                // Usando listado TXT
                //using(var cliente = new HttpClient())
                //{
                //    var jsontxt = await cliente.GetStringAsync("http://kona2.alc.upv.es/Listado_jc.txt");
                //    var items = JsonConvert.DeserializeObject<List<Persona>>(jsontxt);
                //    ListadoPersonal.Clear();
                //    foreach(var item in items)
                //    {
                //        ListadoPersonal.Add(item);
                //    }
                //}
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                EstaOcupado = false;
            }

        }

        public async Task UpdateListado(Listado persona)
        {
            var service = DependencyService.Get<ServicioAzure>();
            service.ActualizaListado(persona);
            await GetLista();

        }
    }
}
