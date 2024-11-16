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
                Movies m2=await _localDbService.GetMovieByTitle(m.Title);
                if(m2 is not null)
                {
                    await DisplayAlert("Error", "Movie already exists in the database.", "OK");
                    TextBoxMovie.Text = null;
                    return;
                }
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

        private async void ToAddRoomPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InsertRoomPage(_localDbService));
        }

        private async void ToAddResevationPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InsertReservationPage(_localDbService));
        }

        private async void ToReservationPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReservationPage(_localDbService));
        }

        private async void ToChartPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChartPage(_localDbService));
        }
    }

}
