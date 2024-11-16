using RezervareFilmeNet8.API;

namespace RezervareFilmeNet8.Pages;

public partial class ModifyRoomPage : ContentPage
{
	private Room _room;
	private LocalDbService _localDbService;
	private string status;

	public ModifyRoomPage(Room r, LocalDbService ldb)
	{
		InitializeComponent();
		_room = r;
		_localDbService = ldb;
	}

    private async void ModifyRoom(object sender, EventArgs e)
    {
		if (RbAvailable.IsChecked)
		{
			status="Available";
		}
		else if (RbNotAvailable.IsChecked)
		{
			status="Not Available";
		}
		else if (RbFull.IsChecked)
		{
			status = "Full";  
		}
		else
		{
			status = "In Repairs";
		}
		await _localDbService.UpdateRoom(_room, status);
        await Navigation.PopAsync();
    }

}