using Microsoft.Maui.Controls;

namespace SmartClinic.Mobile
{
    public partial class App : IApplication
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
