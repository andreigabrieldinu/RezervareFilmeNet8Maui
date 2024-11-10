using RezervareFilmeNet8;

namespace RezervareFilmeNet8
{
    public partial class App : Application
    {
        public App()
        {
            LocalDbService localDbService = new LocalDbService();
            InitializeComponent();

            MainPage = new AppShell();
            MainPage = new NavigationPage(new MainPage(localDbService));

        }
    }
}
