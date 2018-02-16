
using PDDT.Database;
using PDDT.Entities;
using PDDT.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDDT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();
        }

        private void EditBtn_Clicked(object sender, EventArgs e)
        {
            hostLabel.IsVisible = false;
            portLabel.IsVisible = false;
            host_field.IsVisible = true;
            port_field.IsVisible = true;
            SaveBtn.IsVisible = true;
            EditBtn.IsVisible = false;
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            if(host_field.Text== null || port_field.Text == null || host_field.Text == string.Empty || port_field.Text == string.Empty)
            {
                DisplayAlert(Message.Server_Header, Message.SettingsIncomplete, Message.Ok);
            }
            else
            {
                using (var db = DependencyService.Get<DependencyInterface.IClientDatabase>().GetConnection())
                {
                    var query = db.Table<Server>().ToList();
                    if (query.Count > 0)
                    {
                        foreach (var result in query)
                        {
                            db.Delete(result);
                        }
                    }
                    db.Insert(new Server()
                    {
                        Host = host_field.Text,
                        Port = Int32.Parse(port_field.Text)
                    });
                }
                DisplayAlert(Message.Server_Header, Message.Server_Message, Message.Ok);
                hostLabel.Text = host_field.Text;
                portLabel.Text = port_field.Text;
                hostLabel.IsVisible = true;
                portLabel.IsVisible = true;
                host_field.IsVisible = false;
                port_field.IsVisible = false;
                SaveBtn.IsVisible = false;
                EditBtn.IsVisible = true;
            }           
        }
    }
}
