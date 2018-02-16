using PDDT.Entities;

namespace PDDT.ViewModels
{
    class AboutViewModel
    {
        private int _port;
        public int Port
        {
            get => _port; set => _port = value;
        }
        private string _host;
        public string Host
        {
            get => _host; set => _host = value;
        }
        public AboutViewModel()
        {
            Port = Constants.HostPort();
            Host = Constants.HostIP();
        }
    }
}
