using System;
using System.Collections.Generic;
using System.Text;

namespace ET31_XamarinMVVM.Model
{
    public class Listado
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Avatar { get; set; }
        public string Website { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }
    }
}
