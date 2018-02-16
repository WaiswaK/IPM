using PDDT.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDDT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardMaster : ContentPage
    {
        public ListView ListView => ListViewMenuItems;

        public DashboardMaster()
        {
            InitializeComponent();
            BindingContext = new MasterViewModel();
        }
    }
}
