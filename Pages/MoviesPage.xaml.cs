using RezervareFilme;
using RezervareFilmeNet8.API;
using RezervareFilmeNet8.Pages;

namespace RezervareFilmeNet8.Pages;

public partial class MoviesPage : ContentPage
{
    List<Movies> movies = new List<Movies>(); 
    private LocalDbService _localDbService;
    public MoviesPage(LocalDbService localDbService)
	{
        InitializeComponent();
        _localDbService = localDbService;

        
	}
    async Task InitializeMovies()
    {
        movies=await _localDbService.GetMovies();
    }
	override protected async void OnAppearing()
    {
        base.OnAppearing();
        await InitializeMovies();
        listViewMovies.ItemsSource= movies;
        if (movies.Count != 0)
        {
            BindingContext = movies;
        }
        
    }
}