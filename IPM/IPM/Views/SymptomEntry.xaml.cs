
using PDDT.Entities;
using PDDT.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDDT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SymptomEntry : ContentPage
    {
        public SymptomEntry()
        {
            InitializeComponent();
        }
        private async void SaveBtn_Clicked(object sender, System.EventArgs e)
        {
            Disable();
            List<Symptom> symptoms = new List<Symptom>();
            if (fruit_field.Text == null && malebud_field.Text == null &&
                leaves_location.Text == null && stem_field.Text == null &&
                corm_field.Text == null && root_location.Text == null &&
                Whole_location.Text == null)
            {
                await DisplayAlert(Message.Diagnosis_Header, Message.Symptom_Empty, Message.Ok);
                Enable();
            }
            else
            {
                string connected = await Plugin.Connectivity.CrossConnectivity.Current.
                IsRemoteReachable(Constants.baseUrl, Constants.port) ? "Reachable" : "Not reachable";
                if (connected == "Reachable")
                {
                    try
                    {
                        ProcessLabel.Text = "Diagonising........";
                        ProcessLabel.TextColor = Color.DarkOliveGreen;
                        symptoms = await Json.GetConfirmSymptoms(fruit_field.Text, malebud_field.Text,
                                            leaves_location.Text, stem_field.Text, corm_field.Text, root_location.Text,
                                            Whole_location.Text);
                        ProcessLabel.Text = "Almost done";
                        ProcessLabel.TextColor = Color.Green;
                    }
                    catch
                    {
                        ProcessLabel.Text = "Something Failed";
                        ProcessLabel.TextColor = Color.Red;
                    }

                    if (symptoms.Count > 0)
                    {                   
                        await Navigation.PushAsync(new SymptomConfirm
                        {
                            BindingContext = new ViewModels.SymptomConfirmViewModel(symptoms)
                        });
                    }
                    else
                    {
                        await DisplayAlert(Message.Symptom_Header, Message.Symptom_Unclear_Message, Message.Ok);                        
                    }
                }
                else
                {
                    await DisplayAlert(Message.Internet_Header, Message.Connection_Fail, Message.Ok);
                }
                Enable();
            }
        }
        private void Disable()
        {
            fruit_field.IsEnabled = false;
            malebud_field.IsEnabled = false;
            corm_field.IsEnabled = false;
            leaves_location.IsEnabled = false;
            Whole_location.IsEnabled = false;
            root_location.IsEnabled = false;
            stem_field.IsEnabled = false;
            SaveBtn.IsEnabled = false;
        }
        private void Enable()
        {
            fruit_field.IsEnabled = true;
            malebud_field.IsEnabled = true;
            corm_field.IsEnabled = true;
            leaves_location.IsEnabled = true;
            Whole_location.IsEnabled = true;
            root_location.IsEnabled = true;
            stem_field.IsEnabled = true;
            SaveBtn.IsEnabled = true;
            ProcessLabel.IsVisible = false;
        }
    }
}
