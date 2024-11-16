using RezervareFilmeNet8.API;

namespace RezervareFilmeNet8.Pages;

public partial class ReservationPage : ContentPage
{
	private LocalDbService _localDbService;
	private List<Reservation> reservations;
	private List<Room> rooms;
    private string roomName;
	public ReservationPage(LocalDbService localDbService)
	{
		InitializeComponent();
		_localDbService = localDbService;
	}

	async Task InitializeRooms()
	{
		rooms= await _localDbService.GetRooms();
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
		await InitializeRooms();
		string[] roomsNames = new string[rooms.Count];
        for (int i = 0; i < rooms.Count; i++)
        {
            roomsNames[i] = rooms[i].Name;
        }
        PickerRoomRezervation.ItemsSource= roomsNames;
    }

    private async void CalendarReservation_SelectedDatesChanged(object sender, Xceed.Maui.Toolkit.ValueChangedEventArgs<System.Collections.ObjectModel.Collection<DateTime>> e)
    {
        DateTime Date = (DateTime)CalendarReservation.SelectedDate;
        reservations = await _localDbService.GetReservationsByDate(Date);
		listViewReservations.ItemsSource = reservations;
			if (reservations.Count > 0)
			{
				BindingContext = reservations;
			}
    }

    private void PickerRoomRezervation_SelectedIndexChanged(object sender, EventArgs e)
    {
        roomName= PickerRoomRezervation.SelectedItem.ToString();
    }

    private void ButtonReservations_Clicked(object sender, EventArgs e)
    {
        List<Reservation> GoodReservations = new List<Reservation>();
        foreach (Reservation r in reservations)
        {
            if (r.RoomName == roomName)
            {
                GoodReservations.Add(r);
            }
        }
        if (GoodReservations.Count > 0) 
        { 
            listViewReservations.ItemsSource = GoodReservations;
            BindingContext = GoodReservations;
        }
    }

    
}
