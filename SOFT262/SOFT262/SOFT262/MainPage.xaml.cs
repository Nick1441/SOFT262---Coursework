using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SOFT262
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            CreateJSON();

            //Setting Test Text For Main App. Will Eventualy Be Subject Types For Flash Cards.
            string Sentence = "This Is A Test. Hopefully This Works.";
            //var src = Sentence.Split(" ").ToArray<string>;
            MainPageList.ItemsSource = Sentence;


            //This is Testing Creating JSON File. This Will Be Ran Once For Creating. Normaly It will Load THe Built File.  NOT LOADING WHY!?
            //CreateJSON();
        }

        //This is called when Button is Clicked, Set by Even In XAML Class.
        private async void Button_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page2());
        }

        private void CreateJSON()
        {
            //Creating New List, This Will Store The Cards Inside if a Cards Array.
            List<Cards> CardList = new List<Cards>();

            //Testing Creating Subjects.
            Cards Car1 = new Cards("Maths", "10 + 10 = ??", "20");
            Cards Car2 = new Cards("Maths", "10 - 10 = ??", "0");

            CardList.Add(Car1);
            CardList.Add(Car2);

            Console.WriteLine("WORKED");
        }
    }
}
