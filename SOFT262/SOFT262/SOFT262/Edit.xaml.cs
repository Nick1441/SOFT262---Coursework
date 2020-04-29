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
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.json");
        public List<Cards> Test = new List<Cards>();

        public Edit()
        {
            InitializeComponent();

            string text = File.ReadAllText(fileName);
            Test = JsonConvert.DeserializeObject<List<Cards>>(text);

            SubjectPicker.ItemsSource = Test;
        }
    }
}