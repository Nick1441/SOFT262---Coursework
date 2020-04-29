using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace SOFT262
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        public List<Cards> Test = new List<Cards>();

        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.json");
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



            Test.Clear();
            string text = File.ReadAllText(fileName);
            Test = JsonConvert.DeserializeObject<List<Cards>>(text);

            InitializeComponent();
        }

        public List<Cards> GetList()
        {
            return Test;
        }
    }
}