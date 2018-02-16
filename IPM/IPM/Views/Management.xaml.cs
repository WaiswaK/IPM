
using PDDT.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDDT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Management : ContentPage
    {
        public Management()
        {
            InitializeComponent();
        }
        private void OnDateSelected(object sender, DateChangedEventArgs e) { }
        private async void SaveBtn_Clicked(object sender, System.EventArgs e)
        {
            string msg = await Json.ActivityUpload(toDatePicker.Date, Bunches_lb.Text, 
                Weavils_Noticed_lb.Text, Sales_lb.Text, TP_lb.Text);
            await DisplayAlert(Message.ManagementHeader, msg, Message.Ok);
        }
    }
}
