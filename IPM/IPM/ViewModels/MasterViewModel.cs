using PDDT.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PDDT.ViewModels
{
    class MasterViewModel : INotifyPropertyChanged
    {
        private string _userfullname;
        public string UserFullName
        {
            get => _userfullname; set => _userfullname = value;
        }
        public ObservableCollection<DashboardMenuItem> MenuItems { get; }
        public MasterViewModel()
        {
            UserFullName = Entities.Database.UserDetails(Entities.Database.GetActiveUser()).User_name;
            MenuItems = new ObservableCollection<DashboardMenuItem>(new[]
            {
                    new DashboardMenuItem { Id = 0, Title = "Home", IconSource = "home.png", TargetType = typeof(DashboardDetail) },
                    new DashboardMenuItem { Id = 1, Title = "Symptoms", IconSource = "farm.png", TargetType = typeof(SymptomEntry) },
                    new DashboardMenuItem { Id = 2, Title = "Pests", IconSource = "bug.png", TargetType = typeof(Pests)  },
                    new DashboardMenuItem { Id = 3, Title = "Diseases", IconSource = "tree.png", TargetType = typeof(Diseases) },
                    new DashboardMenuItem { Id = 4, Title = "Management", IconSource = "management.png", TargetType = typeof(Management) },
                    new DashboardMenuItem { Id = 5, Title = "Settings", IconSource = "settings.png", TargetType = typeof(Settings) }
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
