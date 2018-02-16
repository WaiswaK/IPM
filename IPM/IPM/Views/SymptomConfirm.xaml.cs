
using PDDT.Entities;
using PDDT.Models;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDDT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SymptomConfirm : ContentPage
    {
        List<Symptom> selected_symptoms = new List<Symptom>();
        public SymptomConfirm()
        {
            InitializeComponent();
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if ((sender as ListView).SelectedItem == null)
                return;
            (sender as ListView).SelectedItem = null;

            var item = e.Item as Symptom;
            if (item.IsSelected)
            {
                item.IsSelected = false;
                selected_symptoms.Remove(item);
                //SelectionLabel.Text = "";
                
            }
            else
            {
                item.IsSelected = true;
                selected_symptoms.Add(item);
                //SelectionLabel.Text = "●";
            }

        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            string connected = await Plugin.Connectivity.CrossConnectivity.Current.
                IsRemoteReachable(Constants.baseUrl, Constants.port) ? "Reachable" : "Not reachable";
            if (connected == "Reachable")
            {
                string selected = string.Empty;
                StringBuilder sb = new StringBuilder();
                StringBuilder sbFinal = new StringBuilder();
                foreach (var item in selected_symptoms)
                {
                    if (selected == string.Empty)
                    {
                        selected = item.S_ID;
                    }
                    else
                    {
                        sb.Append(".");
                        sb.Append(item.S_ID);
                    }
                }
                sbFinal.Append(selected);
                sbFinal.Append(sb.ToString());
                selected = sbFinal.ToString();

                List<Models.Diagnosis> diag = new List<Models.Diagnosis>();
                if (selected == string.Empty || selected == null)
                {

                }
                else
                {
                    diag = await Json.GetDiagnosis(selected);
                }
                if (diag == null || diag.Count == 0)
                {
                    await DisplayAlert(Message.Diagnosis_Header, Message.Diagnosis_Inconclusive, Message.Ok);
                }
                else
                {
                    List<Control> chemicals = Json.Controls(await Json.ChemicalsArray());
                    await Navigation.PushAsync(new Questionaire
                    {
                        BindingContext = new ViewModels.DiagnosisViewModel(selected, chemicals)
                    });
                }
            }
            else
            {
                await DisplayAlert(Message.Symptom_Header, Message.Symptom_Unclear_Message, Message.Ok);
            }
        }
    }
}
