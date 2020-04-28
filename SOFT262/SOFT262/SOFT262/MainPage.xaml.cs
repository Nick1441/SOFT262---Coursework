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
    //[DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //Finding The File Path Where Everything Will Be Saved.
        //
        //Create Checking Bool. This Will Check to See if the File Exists or If the File is Blank. If So Will Create Data.
        //
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.json");

        List<Cards> Test = new List<Cards>();


        Button loadButton, saveButton;

        public void MainPages()
        {
            //
            //Creating Tabs For Pages.
            //

            InitializeComponent();
            //CreateJSON();

            


            var input = new Entry { Text = "" };
            var output = new Label { Text = "" };
            saveButton = new Button { Text = "Save" };

            //Checks To See If Initial File Exists.
            FileFinder();
            //Make It Save to JSON File.


            //Load In Data From JSON File. Back Into List Array.
            //Reset Array. Clear All Data (Incase Needed To Create File)

            //Loading from JSON Array...
            Test.Clear();
            string text = File.ReadAllText(fileName);
            Test = JsonConvert.DeserializeObject<List<Cards>>(text);


            //Finding The Key Cards

            List<string> Subjects = new List<string>();

            for (int i = 0; i < Test.Count; i++)
            {
                if (Test[i].Card == false)
                {
                    Subjects.Add(Test[i].Subject);
                }
            }

            SubjectList.ItemsSource = Subjects;
            //Create Bindings To Allow Specific Parts to Be Shown in list View.
            //SubjectList.ItemsSource = Test;

            //Test = StartupQuestions();

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

            //Content = new StackLayout
            //{
            //    Margin = new Thickness(20),
            //    Children =
            //    {
            //        new Label
            //        {
            //            Text = "Save and Load Text",
            //            FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
            //            FontAttributes = FontAttributes.Bold
            //        },
            //        new Label { Text = "DOES THIS WORK?" },
            //        input,
            //        buttonLayout,
            //        output
            //    }
            //};








            //Setting Test Text For Main App. Will Eventualy Be Subject Types For Flash Cards.
            //string Sentence = "This Is A Test. Hopefully This Works.";
            ////var src = Sentence.Split(" ").ToArray<string>;
            //SubjectList.ItemsSource = Sentence;


            //This is Testing Creating JSON File. This Will Be Ran Once For Creating. Normaly It will Load THe Built File.  NOT LOADING WHY!?
            //CreateJSON();
        }

        //This is called when Button is Clicked, Set by Even In XAML Class.
        //private async void Button_Clicked(Object sender, EventArgs e)
        //{
        //   // await Navigation.PushAsync(new Page2());
        //}

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
        
        //This Method Checks To See if the File Is Created. Mainly For First Time Use as Will Only Create Once.
        //Either Will Load File. Or Create New File With info Inside.
        private void FileFinder()
        {
            if (File.Exists(fileName))
            {
                System.Diagnostics.Debug.WriteLine("File Found");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No File");
                Test = StartupQuestions();
            }
        }

        //This Method Is For First Time Use. This Will Create The Basic Questions For The Database.
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

            //Cards Car3 = new Cards("Art");
            //Cards Car4 = new Cards("Art", true, "What Colour Is Red?", "Red");
            //Cards Car5 = new Cards("Art", true, "How Many Paint Brushes Do We Have?", "0");

            CardList.Add(Car);
            CardList.Add(Car1);
            CardList.Add(Car2);

            CardList.Add(Car3);
            CardList.Add(Car4);
            CardList.Add(Car5);

            //Creates File.
            var json = JsonConvert.SerializeObject(CardList);
            File.WriteAllText(fileName, json);

            return CardList;
        }
    }
}

//
//      TO DO
//
//Create A Template for Items In List View.
//Have Button On Each Items Will Will move onto a differnt Page Based Off That Subject.
//
//Create List veiws Etc in XAML based off CS File.
//
//Figure Out Top Thingy to Move around Edit And Deleting Stuff Which Could Be Good.
//
//
//