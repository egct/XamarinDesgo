using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Desgo_Xamarin.Interfaces;

using Xamarin.Forms;

[assembly: Dependency(typeof(Desgo_Xamarin.Droid.Implementations.Config))]

namespace Desgo_Xamarin.Droid.Implementations
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
                    directoryDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }

                return directoryDB;
            }
        }


    }
}