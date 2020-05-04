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
using System.Security.Cryptography.X509Certificates;

namespace SOFT262
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Browse : ContentPage
    {
        ObservableCollection<Cards> Test2 = new ObservableCollection<Cards>();
        private ObservableCollection<Cards> Test3 { get { return Test2; } }

        ObservableCollection<Cards> SubList = new ObservableCollection<Cards>();
        ObservableCollection<string> SubjectString;

        //Finding The Path File To Write/Read Data To/From.
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.json");

        //Creating List To Store All The Cards.
        public List<Cards> Test = new List<Cards>();

        public int ListNumber = 0;

        public Browse()
        {
            InitializeComponent();

            //
            //Generate Basic File With Premade Cards, This is Checked As Only Made If Doesnt Exist.
            FileFinder();

            //Loading from JSON Array...
            Test.Clear();
            string text = File.ReadAllText(fileName);
            Test = JsonConvert.DeserializeObject<List<Cards>>(text);

            SubjectString = new ObservableCollection<string>();

            for (int i = 0; i < Test.Count; i++)
            {
                if (Test[i].Card == false)
                {
                    SubjectString.Add(Test[i].Subject);
                }
            }

            SubjectPick.ItemsSource = SubjectString;


            //Adding Subjects To A Collection So Binding Can Work?
            //for (int i = 0; i < Test.Count; i++)
            //{
            //    if (Test[i].Card == false)
            //    {
            //        Test2.Add(Test[i]);
            //    }
            //}

            //THIS CRASHES IT IDK WHY
            //SubjectList.ItemsSource = Test;


            //Finds Subject Cards And Puts Them Into Seperate Array.
            //List<string> Subjects = new List<string>();

            //for (int i = 0; i < Test.Count; i++)
            //{
            //    if (Test[i].Card == false)
            //    {
            //        Subjects.Add(Test[i].Subject);
            //    }
            //}

            //SubjectList.ItemsSource = Subjects;
        }

        public async void SubjectChange(object sender, EventArgs e)
        {
            //Picker picker = sender as Picker;
            string selected = SubjectPick.SelectedItem.ToString();

            SubList.Clear();

            for (int i = 0; i < Test.Count; i++)
            {
                if (Test[i].Subject == selected && Test[i].Card == true)
                {
                    SubList.Add(Test[i]);
                }
            }

            btnReveal.Text = SubList[ListNumber].Question;
        }

        public void ResetQuestion()
        {
            btnReveal.Text = SubList[ListNumber].Question;
        }
        public void UpdateQuestion()
        {
            btnReveal.Text = SubList[ListNumber].Answer;
        }

        public async void btnRevealClick(object sender, EventArgs args)
        {
            if (btnReveal.Text == "")
            {
                await DisplayAlert("Alert", "Please Select A Subject First", "OK");
            }
            else
            {
                UpdateQuestion();
            }
        }

        public async void btnNextClick(object sender, EventArgs args)
        {
            if (btnReveal.Text != "")
            {
                if (ListNumber != SubList.Count - 1)
                {
                    ListNumber = ListNumber + 1;
                    ResetQuestion();
                }
                else
                {
                    await DisplayAlert("Alert", "There is No Next Card.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Alert", "Please Select A Subject First", "OK");
            }

        }

        public async void btnPrevClick(object sender, EventArgs args)
        {
            if (btnReveal.Text != "")
            {
                if (ListNumber != 0)
                {
                    ListNumber = ListNumber - 1;
                    ResetQuestion();
                }
                else
                {
                    await DisplayAlert("Alert", "There is No Previous Card", "OK");
                }
            }
            else
            {
                await DisplayAlert("Alert", "Please Select A Subject First", "OK");
            }
        }
        public async void btnRandomClick(object sender, EventArgs args)
        {
            await DisplayAlert("Alert", SubList.ToString(), "OK");
        }


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