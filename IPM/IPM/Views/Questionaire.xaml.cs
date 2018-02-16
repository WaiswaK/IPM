using PDDT.Entities;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDDT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Questionaire : ContentPage
    {
        public Questionaire()
        {
            InitializeComponent();
        }
        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            List<Models.Diagnosis> diag = await Json.GetDiagnosis(selected_label.Text);
            if (diagnosis_switch.On == true)
            {
                if (Chemical_Picker.SelectedIndex != -1)
                {
                    await Navigation.PushAsync(new Diagnosis
                    {
                        BindingContext = new ViewModels.DiagnosisViewModel(diag, Chemical_Picker.SelectedValue.ToString())
                    });
                }
                else
                {
                    await Navigation.PushAsync(new Diagnosis
                    {
                        BindingContext = new ViewModels.DiagnosisViewModel(diag, null)
                    });
                }       
            }
            else
            {
                await Navigation.PushAsync(new Diagnosis
                {
                    BindingContext = new ViewModels.DiagnosisViewModel(diag, null)
                });
            }
        }
        private void Diagnosis_switch_OnChanged(object sender, ToggledEventArgs e)
        {
            if (diagnosis_switch.On == false)
            {
                Chemical_Picker.IsVisible = false;
                ChemicalDatePicker.IsVisible = false;
                Chemical_label.IsVisible = false;
            }
            else
            {
                Chemical_Picker.IsVisible = true;
                ChemicalDatePicker.IsVisible = true;
                Chemical_label.IsVisible = true;
            }
        }
    }
}
