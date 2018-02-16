using PDDT.Entities;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PDDT.Database;
using PDDT.Models;
using System.Linq;
using IPM.Models;

namespace PDDT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }
        void OnLoginButtonClicked(object sender, EventArgs e)
        {
            IsBusy = true;
            Constants.InitializeDatabase();
            string username = Constants.NullRemove(email_tb.Text);
            string code = Constants.NullRemove(password_tb.Text);
            string confirm = Constants.NullRemove(conpassword_tb.Text);

            if (conpassword_tb.IsVisible == false)
            {
                LoginAction(username, code);
            }
            else
            {
                RegisterAction(username, code, confirm);
            }
        }
        #region Login Methods
        private async void LoginAction(string username, string password)
        {
            if (username == string.Empty || password == string.Empty)
            {
                await DisplayAlert(Message.Login_Header, Message.NoLoginDetails, Message.Ok);
                LoadingMsg.IsVisible = false;
                IsBusy = false;
            }
            else
            {
                LoadingMsg.IsVisible = true;
                LoadingMsg.Text = Message.Checking_Connection;
                string connected = await Plugin.Connectivity.CrossConnectivity.Current.
                IsRemoteReachable(Constants.baseUrl, Constants.port) ? "Reachable" : "Not reachable";
                if (connected == "Reachable")
                {
                    LoadingMsg.Text = Message.Connection_Established;
                    Authenticate(username, password);
                }
                else
                {
                    await DisplayAlert(Message.Login_Header, Message.Login_Message_Fail, Message.Ok);
                    IsBusy = false;
                    LoadingMsg.IsVisible = false;
                }
            }
        }
        private async void Authenticate(string username, string code)
        {
            LoadingMsg.Text = Message.User_Validation;
            List<User> users = Entities.Database.SelectAllUsers();
            LoginTokenResult Token = new LoginTokenResult();
            string error = string.Empty;
            string Token_Result = string.Empty;
            if (users == null)
            {
                Token = Json.GetLoginToken(username, code);
                error = Constants.NullRemove(Token.ErrorDescription);
                Token_Result = Constants.NullRemove(Token.AccessToken);
                if (Token_Result == string.Empty)
                {
                    await DisplayAlert(Message.Login_Header, error, Message.Ok);
                    IsBusy = false;
                    LoadingMsg.IsVisible = false;
                }
                else
                {
                    User user = new User()
                    {
                        User_name = username,
                        Code = code
                    };
                    Entities.Database.InsertUser(user);
                    FinalNavigation(user.User_name);
                }
            }
            else
            {
                foreach (var user in users)
                {
                    if (user.User_name.Equals(username) && user.Code.Equals(code))
                    {
                        Token = Json.GetLoginToken(username, code);
                        error = "You have entered an old password, Please enter the current password";
                        Token_Result = Constants.NullRemove(Token.AccessToken);
                        if (Token_Result == string.Empty)
                        {
                            await DisplayAlert(Message.Login_Header, error, Message.Ok);
                            IsBusy = false;
                            LoadingMsg.IsVisible = false;
                        }
                        else
                        {
                            FinalNavigation(user.User_name);
                        }
                    }
                }
            }
        }
        private async void FinalNavigation(string user)
        {
            Entities.Database.UpdateUser(true, user);
            Navigation.InsertPageBefore(new Dashboard()
            {
                BindingContext = new ViewModels.MasterViewModel()
            }
                , this);
            await Navigation.PopAsync();
        }
        #endregion
        private async void Register_btn_Clicked(object sender, EventArgs e)
        {
            bool selected = await DisplayAlert(Message.RegisterUser, Message.RegisterHeader, Message.RegisterUser, Message.SetServer);
            if (selected == true)
            {
                conpassword_tb.IsVisible = true;
                register_btn.IsVisible = false;
            }
            else
            {
                host_field.IsVisible = true;
                port_field.IsVisible = true;
                SaveBtn.IsVisible = true;
                email_tb.IsVisible = false;
                password_tb.IsVisible = false;
                login_btn.IsVisible = false;
                register_btn.IsVisible = false;
            }

        }
        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            if (host_field.Text == null || port_field.Text == null || host_field.Text == string.Empty || port_field.Text == string.Empty)
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
                host_field.IsVisible = false;
                port_field.IsVisible = false;
                SaveBtn.IsVisible = false;
                email_tb.IsVisible = true;
                password_tb.IsVisible = true;
                login_btn.IsVisible = true;
                register_btn.IsVisible = true;
            }
        }
        private async void RegisterAction(string username, string code, string confirm)
        {
            LoginTokenResult Token = new LoginTokenResult();
            string error = string.Empty;
            string Token_Result = string.Empty;
            if (username == string.Empty || code == string.Empty || confirm == string.Empty)
            {
                await DisplayAlert(Message.RegisterUser, Message.NoRegisterdetails, Message.Ok);
                LoadingMsg.IsVisible = false;
                IsBusy = false;
            }
            else
            {
                LoadingMsg.IsVisible = true;
                LoadingMsg.Text = Message.Checking_Connection;

                string connected = await Plugin.Connectivity.CrossConnectivity.Current.
                IsRemoteReachable(Constants.baseUrl, Constants.port) ? "Reachable" : "Not reachable";
                if (connected == "Reachable")
                {
                    LoadingMsg.Text = Message.Connection_Established;
                    IPM.Models.Registration reg = await Json.GetLoginToken(username, code, confirm);
                    if (reg == null)
                    {
                        Token = Json.GetLoginToken(username, code);
                        error = "You have entered an old password, Please enter the current password";
                        Token_Result = Constants.NullRemove(Token.AccessToken);
                        if (Token_Result == string.Empty)
                        {
                            await DisplayAlert(Message.RegisterUser, Message.Registration_wrong, Message.Ok);
                            IsBusy = false;
                            LoadingMsg.IsVisible = false;
                        }
                        else
                        {
                            User user = new User()
                            {
                                User_name = username,
                                Code = code
                            };
                            Entities.Database.InsertUser(user);
                            FinalNavigation(user.User_name);
                        }
                    }
                    else
                    {
                        await DisplayAlert(Message.RegisterUser, ModelStateError(reg), Message.Ok);
                        IsBusy = false;
                        LoadingMsg.IsVisible = false;
                    }
                }
                else
                {
                    await DisplayAlert(Message.RegisterUser, Message.Register_Message_Fail, Message.Ok);
                    IsBusy = false;
                    LoadingMsg.IsVisible = false;
                }
            }
        }
        private string ModelStateError(Registration reg)
        {
            if (reg.Modelstate.Empty != string.Empty)
            {
                return reg.Modelstate.Empty;
            }
            else
            {
                return reg.Modelstate.Password;
            }
        }
    }
}
