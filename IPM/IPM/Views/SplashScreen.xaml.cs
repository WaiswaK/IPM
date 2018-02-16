using PDDT.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDDT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // await a new task
            await Task.Factory.StartNew(async () => {

                // delay for a few seconds on the splash screen
                await Task.Delay(3000);

                var navPage = new NavigationPage();
                if (Entities.Database.GetActiveUser() == string.Empty)
                {
                    navPage = new NavigationPage(new Login())
                    {
                        BarBackgroundColor = Color.Green
                    };
                }
                else
                {
                    navPage = new NavigationPage(
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
                // on the main UI thread, set the MainPage to the navPage
                Device.BeginInvokeOnMainThread(() => {
                    Application.Current.MainPage = navPage;
                });
            });
        }
    }
}
