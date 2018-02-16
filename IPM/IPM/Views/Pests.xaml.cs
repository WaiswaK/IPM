using PDDT.Entities;
using PDDT.Models;
using PDDT.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDDT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pests : ContentPage
    {
        public Pests()
        {
            InitializeComponent();
        }

        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var pest = ((ListView)sender).SelectedItem as Pest;
            if (pest == null)
                return; //Move to nextpage
            else
            {
                await Navigation.PushAsync(new DiseasePestDetail()
                {
                    BindingContext = new PestDiseaseViewModel(Json.PestDetails(await Json.PestObject(pest.ID, pest.Name)))
                });
            }
        }
    }
}
