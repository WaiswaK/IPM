using PDDT.Entities;
using PDDT.Models;
using PDDT.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDDT.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : MasterDetailPage
    {
        public Dashboard()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as DashboardMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;
            if (page.Title == "Pests"|| page.Title == "Diseases")
            {
                string connected = await Plugin.Connectivity.CrossConnectivity.Current.
                IsRemoteReachable(Constants.baseUrl, Constants.port) ? "Reachable" : "Not reachable";
                if (connected == "Reachable")
                {
                    if(page.Title == "Pests")
                    {
                        List<Pest> pests = Json.PestList(await Json.PestsArray());
                        Detail = new NavigationPage(page)
                        {
                            BindingContext = new PestsDiseasesViewModel(pests),
                            BarBackgroundColor = Color.Green,
                            BarTextColor = Color.White
                        };
                    }
                    if (page.Title == "Diseases")
                    {
                        List<Disease> diseases = Json.DiseaseList(await Json.DiseasesArray());
                        Detail = new NavigationPage(page)
                        {
                            BindingContext = new PestsDiseasesViewModel(diseases),
                            BarBackgroundColor = Color.Green,
                            BarTextColor = Color.White
                        };
                    }                  
                }
                else
                {
                    await DisplayAlert(Message.Internet_Header, Message.Connection_Fail, Message.Ok);
                }
            }
            else
            {
                Detail = new NavigationPage(page)
                {
                    BarBackgroundColor = Color.Green,
                    BarTextColor = Color.White
                };
            }       
            MasterPage.ListView.SelectedItem = null;
            IsPresented = false;
        }
    }

}
