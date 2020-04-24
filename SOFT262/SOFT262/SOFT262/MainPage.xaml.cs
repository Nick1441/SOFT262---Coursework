using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Newtonsoft.Json;

namespace SOFT262
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.json");
        Button loadButton, saveButton;

        public MainPage()
        {
            InitializeComponent();
            //CreateJSON();

            List<Cards> Test;


            var input = new Entry { Text = "" };
            var output = new Label { Text = "" };
            saveButton = new Button { Text = "Save" };


            Test = StartupQuestions();

            saveButton.Clicked += (sender, e) =>
            {
                loadButton.IsEnabled = saveButton.IsEnabled = false;
                
                var json = JsonConvert.SerializeObject(Test);
                File.WriteAllText(fileName, json);
                //System.Diagnostics.Debug.WriteLine(Car2.ToString());
                loadButton.IsEnabled = saveButton.IsEnabled = true;
            };

            loadButton = new Button { Text = "Load" };
            loadButton.Clicked += (sender, e) =>
            {
                loadButton.IsEnabled = saveButton.IsEnabled = false;
                //output.Text = File.ReadAllText(fileName);

                //
                //WANT TO LOAD EVERYTHING BACK FROM THE FILE INTO AN ARRAY LIST FROM JSON FILE.
                //

                //Experementing Getting SUbjects Name and Put into A Lable. Will Turn Into View Model.
                //This Works. It Gets All Subjects and puts them into a string.
                string SUBJECTS = "";
                for (int i = 0; i < Test.Count; i++)
                {
                    if (Test[i].Card == false)
                    {
                        SUBJECTS = SUBJECTS + Test[i].Subject + "\n";
                    }
                }

                output.Text = SUBJECTS;
                loadButton.IsEnabled = saveButton.IsEnabled = true;
            };
            loadButton.IsEnabled = File.Exists(fileName);
            var buttonLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children = { saveButton, loadButton }
            };

            Content = new StackLayout
            {
                Margin = new Thickness(20),
                Children =
                {
                    new Label
                    {
                        Text = "Save and Load Text",
                        FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
                        FontAttributes = FontAttributes.Bold
                    },
                    new Label { Text = "DOES THIS WORK?" },
                    input,
                    buttonLayout,
                    output
                }
            };








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
            //Cards Car1 = new Cards("Maths", "10 + 10 = ??", "20");
            //Cards Car2 = new Cards("Maths", "10 - 10 = ??", "0");

            //CardList.Add(Car1);
            //CardList.Add(Car2);

            //Console.WriteLine("WORKED");
        }

        //This Class Is For First Time Use. This Will Create The Basic Questions For The Database.
        //This will Eventualy Be Checked So it is Only Ran Once? Somehow...
        private List<Cards> StartupQuestions()
        {
            //Creating A List To Store All Cards.
            List<Cards> CardList = new List<Cards>();

            //Creating Cards For Each Subject.
            Cards Car = new Cards("Maths");
            Cards Car1 = new Cards("Maths", true, "10 + 10 = ??", "20");
            Cards Car2 = new Cards("Maths", true, "10 - 10 = ??", "0");

            Cards Car3 = new Cards("Art");
            Cards Car4 = new Cards("Art", true, "What Colour Is Red?", "Red");
            Cards Car5 = new Cards("Art", true, "How Many Paint Brushes Do We Have?", "0");

            CardList.Add(Car);
            CardList.Add(Car1);
            CardList.Add(Car2);

            CardList.Add(Car3);
            CardList.Add(Car4);
            CardList.Add(Car5);


            return CardList;
        }
    }
}
