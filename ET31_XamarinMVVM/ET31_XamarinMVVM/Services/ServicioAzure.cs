using ET31_XamarinMVVM.Model;
using ET31_XamarinMVVM.Services;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ServicioAzure))]

namespace ET31_XamarinMVVM.Services
{
	class ServicioAzure : ContentPage
	{
        public MobileServiceClient MobileService { get; set; } = null;
        IMobileServiceSyncTable<Listado> table;
        private Listado pesona;

        public async Task Initialize()
        {
            if (MobileService?.SyncContext?.IsInitialized ?? false)
            {
                return;
            }

            MobileService = new MobileServiceClient("http://eucmuxamarin.azurewebsites.net");

            var path = "syncstore.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);
            var store = new MobileServiceSQLiteStore(path);

            store.DefineTable<Listado>();
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            
            table = MobileService.GetSyncTable<Listado>();
        }

        public async Task SyncListado()
        {
            try
            {
                await MobileService.SyncContext.PushAsync();
                await table.PullAsync("Listado", table.CreateQuery());
            }
            catch (Exception e)
            {
                Debug.WriteLine("No puedo sincronizar con Listado. Trabajando Offline: " + e);
            }
        }

        public async Task<IEnumerable<Listado>> GetListado()
        {
            await Initialize();
            await SyncListado();
            return await table.OrderBy(s => s.Name).ToEnumerableAsync();
        }

        public async Task ActualizaListado(Listado persona)
        {
            await Initialize();
            await table.UpdateAsync(pesona);
            await SyncListado();
        }

	}
}