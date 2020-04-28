using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SOFT262
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        public TabbedPage1()
        {
            //NavigationPage NavyPage = new NavigationPage(new MainPage());
            //NavyPage.Title = "Test!";
            ////Set Icons Here?

            //Children.Add(new Edit());
            //Children.Add(NavyPage);


            //var TabPages = new TabbedPage();

            //TabPages.Children.Add(new Browse { Title = "SubjectBrowse" });
            //TabPages.Children.Add(new Edit { Title = "EditCards" });

            InitializeComponent();
        }
    }
}