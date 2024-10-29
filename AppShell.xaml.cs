using RezervareFilmeNet8.Pages;

namespace RezervareFilmeNet8
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MoviesPage), typeof(MoviesPage));
        }
    }
}
