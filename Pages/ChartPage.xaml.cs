using RezervareFilmeNet8.API;

namespace RezervareFilmeNet8.Pages;

public partial class ChartPage : ContentPage
{
	private LocalDbService _localDbService;
	private List<Reservation> reservations;
	private List<Room> rooms;
	public ChartPage(LocalDbService localDb)
	{
		InitializeComponent();
		_localDbService = localDb;
	}
    async Task InitializeReservation()
    {
        reservations = await _localDbService.GetReservations();
    }
	async Task InitializeRoom()
	{
		rooms=await _localDbService.GetRooms();
	}
	override async protected void OnAppearing()
	{
        base.OnAppearing();
        await InitializeReservation();

		foreach (Room r in rooms)
		{
			List<Reservation> roomReservations = new List<Reservation>();
			foreach (Reservation rez in reservations)
			{
				if (rez.RoomName == r.Name)
				{
					roomReservations.Add(rez);
				}
			}
		}		
    }
}