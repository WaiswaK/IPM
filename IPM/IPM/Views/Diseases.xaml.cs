using PDDT.Entities;
using PDDT.Models;
using PDDT.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDDT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Diseases : ContentPage
    {
        public Diseases()
        {
            InitializeComponent();
        }
        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var disease = ((ListView)sender).SelectedItem as Disease;
            if (disease == null)
                return; //Move to nextpage
            else
            {
                await Navigation.PushAsync(new DiseasePestDetail()
                {
                    BindingContext = new PestDiseaseViewModel(Json.DiseaseDetails(await Json.DiseaseObject(disease.ID, disease.Name)))
                });
            }
        }
    }
}
