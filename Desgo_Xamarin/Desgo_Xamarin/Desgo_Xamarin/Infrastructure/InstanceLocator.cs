using System;
using System.Collections.Generic;
using System.Text;
using Desgo_Xamarin.ViewModels;

namespace Desgo_Xamarin.Infrastructure
{
    class InstanceLocator
    {
        #region Properties
            public MainViewModel Main
            {
                get;
                set;
            }
        #endregion
        #region Constructors
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }


        #endregion
    }
}
