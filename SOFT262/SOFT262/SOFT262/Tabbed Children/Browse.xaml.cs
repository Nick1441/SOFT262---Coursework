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
        //Creating Observable Collections To Auto Update When they are changed On Pages.
        ObservableCollection<Cards> MainList = new ObservableCollection<Cards>();
        ObservableCollection<Cards> SubList = new ObservableCollection<Cards>();
        ObservableCollection<string> SubjectString = new ObservableCollection<string>();

        //Finding The Path File To Write/Read Data To/From.
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.json");

        public int ListNumber = 0;

        public Browse()
        {
            InitializeComponent();

            //
            //Generate Basic File With Premade Cards, This is Checked As Only Made If Doesnt Exist.
            FileFinder();

            //Loading from JSON Array...
            MainList.Clear();
            string text = File.ReadAllText(fileName);
            MainList = JsonConvert.DeserializeObject<ObservableCollection<Cards>>(text);

            //First Time Set up For Picker.
            for (int i = 0; i < MainList.Count; i++)
            {
                if (MainList[i].Card == false)
                {
                    SubjectString.Add(MainList[i].Subject);
                }
            }

            SubjectPick.ItemsSource = SubjectString;
        }

        //Refresh Method To Update Observaable collections when Changes on other tabbed pages.
        public void Refresh()
        {
            MainList.Clear();

            string text = File.ReadAllText(fileName);
            MainList = JsonConvert.DeserializeObject<ObservableCollection<Cards>>(text);

            SubjectString.Clear();

            for (int i = 0; i < MainList.Count; i++)
            {
                if (MainList[i].Card == false)
                {
                    SubjectString.Add(MainList[i].Subject);
                }
            }

            SubjectPick.SelectedIndex = 0;
        }

        //When a change occours in picker. method is called to change what cards are being shown.
        public async void SubjectChange(object sender, EventArgs e)
        {
            if (SubjectPick.SelectedIndex != -1)
            {
                //Picker picker = sender as Picker;
                string selected = SubjectPick.SelectedItem.ToString();
                ListNumber = 0;

                SubList.Clear();

                int ListAmount = 0;
                for (int i = 0; i < MainList.Count; i++)
                {
                    if (MainList[i].Subject == selected && MainList[i].Card == true)
                    {
                        ListAmount++;
                    }
                }

                //If a Subject Contains No Cards.
                if (ListAmount == 0)
                {
                    await DisplayAlert("Alert", "There Are No Cards For That Subject. Please Create Card For This Subject.", "OK");
                }
                else
                {
                    for (int i = 0; i < MainList.Count; i++)
                    {
                        if (MainList[i].Subject == selected && MainList[i].Card == true)
                        {
                            SubList.Add(MainList[i]);
                        }
                    }

                    btnReveal.Text = SubList[ListNumber].Question;
                }
            }


        }

        //Resets Question Button to Show Question Text.
        public void ResetQuestion()
        {
            btnReveal.Text = SubList[ListNumber].Question;
        }

        //Updates Question Button to Show Answer Text.
        public void UpdateQuestion()
        {
            btnReveal.Text = SubList[ListNumber].Answer;
        }

        //Reveals Text, Checks If User has Selected a subject first.
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

        //Moves the cards on By One. Checks to see if there is a next card to show first.
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

        //Moves cards back by one, checks to see if subject is chosen first.
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

        //Randomly picks a question card to show. Checks to see if subject is selected first.
        public async void btnRandomClick(object sender, EventArgs args)
        {
            if (btnReveal.Text != "")
            {
                if (SubList.Count != 0 || SubList.Count != 1)
                {
                    Random r = new Random();
                    ListNumber = r.Next(0, SubList.Count);
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

        //Event For Refresh. Method is called as also used elsewhere.
        async void btnRefreshClick(object sender, EventArgs e)
        {
            Refresh();
        }

        //Checks to see if a file is already created. If Not it will create the file. (First time setup of app)
        private void FileFinder()
        {
            if (File.Exists(fileName))
            {
                System.Diagnostics.Debug.WriteLine("File Found");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No File");
                MainList = StartupQuestions();
            }
        }

        //Startup Base questions App creates on first time setup.
        private ObservableCollection<Cards> StartupQuestions()
        {
            //This Creates A New File
            //Creating A List To Store All Cards.
            ObservableCollection<Cards> CardList = new ObservableCollection<Cards>();

            //Creating Cards For Each Subject. And Adding Them Into List.
            //These are only Starting Cards. They Can Be Edited Etc.
            CardList.Add(new Cards("Maths"));
            CardList.Add(new Cards("Maths", true, "10 + 7 = ??", "17"));
            CardList.Add(new Cards("Maths", true, "7 x 7 = ??", "49"));
            CardList.Add(new Cards("Maths", true, "11 - 4 = ??", "7"));
            CardList.Add(new Cards("Maths", true, "3 - 12 = ??", "-9"));
            CardList.Add(new Cards("Maths", true, "11 x 4 = ??", "44"));

            CardList.Add(new Cards("Biology"));
            CardList.Add(new Cards("Biology", true, "What are all living things made up of?", "Cells"));
            CardList.Add(new Cards("Biology", true, "What if the difference between a prokaryotic cell and a eukaryotic cell?", "The prokaryotic cell has no neucleusProkaryote is simpler"));
            CardList.Add(new Cards("Biology", true, "What is the function of the nucleus?", "To contain genetic material that controlling the activities the cell"));
            CardList.Add(new Cards("Biology", true, "What is the function of the cytoplasm?", "The site where most chemical reactions happen"));
            CardList.Add(new Cards("Biology", true, "What is the function of the cell membrane??", "To hold the cell together and controls what goes in and out"));

            CardList.Add(new Cards("Chemistry"));
            CardList.Add(new Cards("Chemistry", true, "What subatomic particles are atoms made up of", "Protons, neutrons and electrons"));
            CardList.Add(new Cards("Chemistry", true, "What is the charge, mass and location of a proton?", "Charge: +1 Mass: 1 Location: nucleus"));
            CardList.Add(new Cards("Chemistry", true, "Atomic number definition", "Number of protons in the nucleus of an atom"));
            CardList.Add(new Cards("Chemistry", true, "Mass number definition", "Number of protons and neutrons in the nucleus of an atom"));
            CardList.Add(new Cards("Chemistry", true, "Isotope", "Atoms of the same element with a different number of neutrons"));

            CardList.Add(new Cards("Physics"));
            CardList.Add(new Cards("Physics", true, "What is energy?", "energy is the ability to do work on objects and it converts from one form to another"));
            CardList.Add(new Cards("Physics", true, "What is work?", "work is done when force is applied to an object and the object is moved through a distance"));
            CardList.Add(new Cards("Physics", true, "When a ball is held above the ground what form of energy does it have?", "gravitational potential energy (gpe)"));
            CardList.Add(new Cards("Physics", true, "What happens in terms of energy as a ball falls?", "gpe is converted into kinetic energy"));
            CardList.Add(new Cards("Physics", true, "What happens in terms of energy as a ball is during the bounce stage?", "kinetic energy is converted into elastic potential energy, thermal energy and sound energy"));

            //Creates File.
            var json = JsonConvert.SerializeObject(CardList);
            File.WriteAllText(fileName, json);

            return CardList;
        }

        //USED WHEN NEEDING TO REMAKE JSON FILE. ADDS BUTTON THAT RESETS FILE.
        //async void btnReset_Clicked(object sender, EventArgs e)
        //{
        //    //this is for the purpose of reseting the JSON File With New Info From Devolper
        //    ObservableCollection<Cards> NotRequired = new ObservableCollection<Cards>();
        //    NotRequired = StartupQuestions();
        //}
    }
}