using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class Manage : ContentPage
    {
        //Creating Observable COllections Used Throughout Page.
        public ObservableCollection<Cards> MainCards = new ObservableCollection<Cards>();
        public ObservableCollection<Cards> SubList = new ObservableCollection<Cards>();


        //Finding Path String For Data File.
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.json");

        int SelectedCard = 0;
        public Manage()
        {
            InitializeComponent();

            //Setting Up Listview Item Source.
            string text = File.ReadAllText(fileName);
            MainCards = JsonConvert.DeserializeObject<ObservableCollection<Cards>>(text);

            for (int i = 0; i < MainCards.Count; i++)
            {
                if (MainCards[i].Card == true)
                {
                    SubList.Add(MainCards[i]);
                }
            }

            EditCardList.ItemsSource = SubList;
        }

        //Startup Setting Of ListView itemsource.
        //Do not need to reset item source as It is Observable Collection.
        public void StartUp()
        {
            string text = File.ReadAllText(fileName);
            MainCards = JsonConvert.DeserializeObject<ObservableCollection<Cards>>(text);

            SubList.Clear();

            for (int i = 0; i < MainCards.Count; i++)
            {
                if (MainCards[i].Card == true)
                {
                    SubList.Add(MainCards[i]);
                }
            }
        }

        //Save all Collection Back to JSON File.
        public void WriteAllToFile()
        {
            var json = JsonConvert.SerializeObject(MainCards);
            File.WriteAllText(fileName, json);
        }

        //Changes Editors Based on what is Clicked.
        async void EditCardList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            int  Selected = e.SelectedItemIndex;

            int Count = 0;

            for (int i = 0; i < MainCards.Count; i++)
            {
                if (MainCards[i].Card == true)
                {
                    if (Count == Selected)
                    {
                        EditSubject.Text = MainCards[i].Subject;
                        EditQuestion.Text = MainCards[i].Question;
                        EditAnswer.Text = MainCards[i].Answer;
                        SelectedCard = i;
                        break;
                    }
                    else
                    {
                        Count++;
                    }
                }
            }
        }

        //Saves Current Updates For Edits on Cards.
        async void btnSave(object sender, EventArgs e)
        {
            MainCards[SelectedCard].Subject = EditSubject.Text;
            MainCards[SelectedCard].Question = EditQuestion.Text;
            MainCards[SelectedCard].Answer = EditAnswer.Text;

            WriteAllToFile();

            StartUp();
        }

        //Finds and delets selected card. Saves all Back to JSON File.
        async void btnDelete(object sender, EventArgs e)
        {
            string LastCard = EditSubject.Text; ;
            int CardAmount = 0;

            for (int i = 0; i < MainCards.Count; i++)
            {
                if (MainCards[i].Subject == LastCard)
                {
                    CardAmount++;
                }
            }

            if (CardAmount == 2)
            {
                DisplayAlert("Alert", "This Is The Last Card In The Subject. Consider Using Delete Subject. (May Have To Reclick List)", "OK");
            }
            else
            {
                MainCards.RemoveAt(SelectedCard);

                WriteAllToFile();
                StartUp();
            }


        }

        //Finds all Cards of that Subject Type. Delets them, then Saves Back to JSON File.
        async void btnDeleteSub(object sender, EventArgs e)
        {
            string SelectedSub = EditSubject.Text;

            for (int i = 0; i < MainCards.Count; i++)
            {
                if (MainCards[i].Subject == SelectedSub)
                {
                    MainCards.RemoveAt(i);
                }
            }

            WriteAllToFile();
            StartUp();
        }

        //Refreshes Any Card Changes/Creations.
        async void btnRefreshClick(object sender, EventArgs e)
        {
            string text = File.ReadAllText(fileName);
            MainCards = JsonConvert.DeserializeObject<ObservableCollection<Cards>>(text);
            StartUp();
        }
    }
}