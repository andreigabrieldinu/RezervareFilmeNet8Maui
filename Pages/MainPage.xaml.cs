using RezervareFilme;
using RezervareFilmeNet8.API;
using RezervareFilmeNet8.Pages;

namespace RezervareFilmeNet8
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _localDbService=null;
        RestServices restService=null;

        public MainPage(LocalDbService localDbService)
        {
            InitializeComponent();
            _localDbService = localDbService;
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            restService = new RestServices();
            Movies m = await restService.GetMovie("Real Steel 2");
            await _localDbService.InsertMovie(m);
        }
        private async void toMoviePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MoviesPage(_localDbService));
        }
    }

}
