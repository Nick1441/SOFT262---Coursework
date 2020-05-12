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
    public partial class Edit : ContentPage
    {
        //Finding File Name Of Data.
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.json");

        //Creating Observable Collections Used Throughout.
        public ObservableCollection<Cards> MainList = new ObservableCollection<Cards>();
        ObservableCollection<string> SubjectString;

        public Edit()
        {
            InitializeComponent();

            //Getting Data From File.
            string text = File.ReadAllText(fileName);
            MainList = JsonConvert.DeserializeObject<ObservableCollection<Cards>>(text);

            //Main Startup setting Pickers.
            StartUp();
        }

        //Created a new Card Based of Editors Text Field. Adds To Collection Then Saves To JSON.
        async void btnCreateCard(object sender, EventArgs e)
        {
            await DisplayAlert("Alert", "Successfully Created Card.", "OK");
            MainList.Add(new Cards(SubjectPicker.SelectedItem.ToString(), true, EditorQuestion.Text, EditorAnswer.Text));


            EditorQuestion.Text = "";
            EditorAnswer.Text = "";

            WriteAllToFile();
        }

        //Creates A new Subject Using Differnt Constructor. Saves to Collection, saves everything Back To JSON.
        async void btnCreateSubject(object sender, EventArgs e)
        {
            await DisplayAlert("Alert", "Successfully Created Subject.", "OK");
            MainList.Add(new Cards(EditorNewSub.Text));

            EditorNewSub.Text = "";

            WriteAllToFile();
        }

        //Saves Collection back To JSON File.
        public void WriteAllToFile()
        {
            var json = JsonConvert.SerializeObject(MainList);
            File.WriteAllText(fileName, json);

            StartUp();
        }

        //Setting up Basic Pickers Item Source.
        public void StartUp()
        {
            SubjectString = new ObservableCollection<string>();

            for (int i = 0; i < MainList.Count; i++)
            {
                if (MainList[i].Card == false)
                {
                    SubjectString.Add(MainList[i].Subject);
                }
            }

            SubjectPicker.ItemsSource = SubjectString;
        }

        //Reads JSON File For Changes. Re Calls Startup top update Pickers.
        async void btnRefreshClick(object sender, EventArgs e)
        {
            string text = File.ReadAllText(fileName);
            MainList = JsonConvert.DeserializeObject<ObservableCollection<Cards>>(text);

            StartUp();
        }
    }
}