using RezervareFilmeNet8.API;
using RezervareFilmeNet8.Pages;
using System.Text.RegularExpressions;

namespace RezervareFilmeNet8
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _localDbService=null;
        RestServices restService=null;
        private List<Reservation> reservations;
        private List<Movies> movies;
        private List<Room> rooms;

        public MainPage(LocalDbService localDbService)
        {
            InitializeComponent();
            _localDbService = localDbService;
        }
        async Task InitializeReservations()
        {
            reservations = await _localDbService.GetReservations();
        }
        async Task InitializeMovies()
        {
            movies= await _localDbService.GetMovies();
        }
        async Task InitializeRooms()
        {
            rooms = await _localDbService.GetRooms();
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
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await InitializeReservations();
            await InitializeMovies();
            await InitializeRooms();
            
            if (reservations.Count > 0)
            {
                List<Revenue> RevenueList = new List<Revenue>();
                foreach (Reservation reservation in reservations)
                {
                    Room r=await _localDbService.GetRoomByName(reservation.RoomName);
                    if(!Revenue.verifyMovie(reservation.MovieTitle, RevenueList))
                    {
                        Movies m=await _localDbService.GetMovieByTitle(reservation.MovieTitle);
                        Revenue rev = new Revenue(reservation.MovieTitle, m.Poster);
                        rev.Total+= reservation.PersonsNumber * r.Price;
                        RevenueList.Add(rev);
                    }
                    else
                    {
                        Revenue rev = Revenue.GetRevenue(reservation.MovieTitle, RevenueList);
                        rev.Total+= reservation.PersonsNumber * r.Price;
                    }
                }
                RevenueList.Sort((x, y) => y.Total.CompareTo(x.Total));
                listViewRevenueMovies.ItemsSource = RevenueList;
                BindingContext = RevenueList;

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
