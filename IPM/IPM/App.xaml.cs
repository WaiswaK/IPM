
using PDDT.Entities;
using PDDT.ViewModels;
using PDDT.Views;
using Xamarin.Forms;

namespace PDDT
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
               MainPage = new SplashScreen();
            }
            else
            {
                MainPage = new NavigationPage(
                                               new Dashboard()
                                               {
                                                   BindingContext = new MasterViewModel(),
                                                   IsPresented = true
                                               })
                {
                    BarBackgroundColor = Color.Green,
                    BarTextColor = Color.White
                };
            } 
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Constants.InitializeDatabase();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
