using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace SOFT262
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Setting Main Load Up Page As Main Tabbed Page.
            MainPage = new TabbedPage1();
        }


        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
