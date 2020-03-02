using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Desgo_Xamarin.Interfaces;
using Foundation;
using UIKit;

using Xamarin.Forms;

[assembly: Dependency(typeof(Desgo_Xamarin.iOS.Implementations.Config))]

namespace Desgo_Xamarin.iOS.Implementations
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
                    var directory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    directoryDB = System.IO.Path.Combine(directory, "..", "Library");
                }

                return directoryDB;
            }
        }
    }
}