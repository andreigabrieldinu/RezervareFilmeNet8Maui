using RezervareFilme;
using RezervareFilmeNet8.API;
using RezervareFilmeNet8.Pages;
using System.Text.RegularExpressions;

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

        private async void OnClicked(object sender, EventArgs e)
        {
            restService = new RestServices();
            if (TextBoxMovie.Text is null)
            {
                await DisplayAlert("Error", "Please introduce a movie name.", "OK");
                return;
            }
            Movies m = await restService.GetMovie(TextBoxMovie.Text);
            if (m.Response == "False")
            {
                
                await DisplayAlert("Error", "Movie not found. Please type another movie name.", "OK");
                TextBoxMovie.Text = null ;
                return;
            }
            else
            {
                await _localDbService.InsertMovie(m);
                await DisplayAlert("Success", "Movie was added!", "OK");
                TextBoxMovie.Text = null;
            }
        }
        private async void ToMoviePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MoviesPage(_localDbService));
        }

        private async void ToRoomsPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RoomsPage(_localDbService));
        }
    }

}
