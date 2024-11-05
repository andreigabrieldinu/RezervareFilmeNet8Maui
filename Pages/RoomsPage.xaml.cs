using RezervareFilme;
using RezervareFilmeNet8.API;

namespace RezervareFilmeNet8.Pages;

public partial class RoomsPage : ContentPage
{

    private List<Room> rooms = [];
    private LocalDbService _localDbService;
    public RoomsPage(LocalDbService localDbService)
    {
        InitializeComponent();
        _localDbService = localDbService;

    }
    async Task InitializeRooms()
    {
      rooms= await _localDbService.GetRooms();
    }
    override protected async void OnAppearing()
    {
        base.OnAppearing();
        await InitializeRooms();
        CollectionViewRooms.ItemsSource= rooms;
        if(rooms.Count!=0)
        {
            BindingContext = rooms;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}