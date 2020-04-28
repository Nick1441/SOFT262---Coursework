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

            MainPage = new TabbedPage1();
            ////Creating New List, This Will Store The Car1ds Inside if a Cards Array.
            //List<Cards> CardList = new List<Cards>();

            ////Testing Creating Subjects.
            //Cards Car1 = new Cards("Maths", "10 + 10 = ??", "20");
            //Cards Car2 = new Cards("Maths", "10 - 10 = ??", "0");

            //CardList.Add(Car1);
            //CardList.Add(Car2);

            //Console.WriteLine("WORKED");

            //string json = JsonConvert.SerializeObject(CardList, Formatting.Indented);
            //File.WriteAllText(@"C:\Text.json", json);

            //string json = JsonConvert.SerializeObject(CardList.ToArray());
            //System.IO.File.WriteAllText(@"C:\Text.json", json);



            //MainPage = new NavigationPage(new MainPage());
        }

        private void SaveData(List<Cards> cardList)
        {
            var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "SettingData.txt");
            using (var writer = File.CreateText(backingFile))
            {
                writer.WriteLineAsync(cardList.ToString());
            }
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
