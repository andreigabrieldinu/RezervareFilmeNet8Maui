using RezervareFilmeNet8.API;
using System.Windows.Input;

namespace RezervareFilmeNet8.Pages;

public partial class RoomsPage : ContentPage
{

    private List<Room> Rooms = [];
    private LocalDbService _localDbService;
    public RoomsPage(LocalDbService localDbService)
    {
        InitializeComponent();
        _localDbService = localDbService;

    }
    async Task InitializeRooms()
    {
        Rooms= await _localDbService.GetRooms();
    }
    override protected async void OnAppearing()
    {
        base.OnAppearing();
        await InitializeRooms();
        CollectionViewRooms.ItemsSource= Rooms;
        if(Rooms.Count!=0)
        {
            BindingContext = Rooms;
        }
    }
    private void ModifyRoomInUI(object sender, EventArgs e)
    {
        Xceed.Maui.Toolkit.Button b = (Xceed.Maui.Toolkit.Button)sender;
        Room r = (Room)b.BindingContext;
        Navigation.PushAsync(new ModifyRoomPage(r, _localDbService));
    }
    private async void DeleteRoomInUI(object sender, EventArgs e)
    {
        Xceed.Maui.Toolkit.Button b = (Xceed.Maui.Toolkit.Button)sender;
        Room r = (Room)b.BindingContext;
        await _localDbService.DeleteRoom(r.Name);
        OnAppearing();
    }
}