using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SOFT262
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //CreatingData manager;

        public MainPage()
        {
            InitializeComponent();
    
            //Setting Test Text For Main App. Will Eventualy Be Subject Types For Flash Cards.
            string Sentence = "This Is A Test. Hopefully This Works.";
            //var src = Sentence.Split(" ").ToArray<string>;
            MainPageList.ItemsSource = Sentence;

            //TRYING TO UPLOAD TO AZURE. IGNORE FOR NOW
            //manager = CreatingData.DefaultManager;
        }

        private async void Button_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page2());
        }
    }
}
