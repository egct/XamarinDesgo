using Desgo_Xamarin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(Desgo_Xamarin.UWP.Implementations.Config))]
namespace Desgo_Xamarin.UWP.Implementations
{
    class Config : IConfig
    {
        string directoryDB;

        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDB))
                {
                    directoryDB = ApplicationData.Current.LocalFolder.Path;
                }

                return directoryDB;
            }
        }
    }

}
