using RezervareFilmeNet8.API;

namespace RezervareFilmeNet8.Pages;

public partial class ModifyRoomPage : ContentPage
{
	private Room _room;
	private LocalDbService _localDbService;


	public ModifyRoomPage(Room r, LocalDbService ldb)
	{
		InitializeComponent();
		_room = r;
		_localDbService = ldb;
	}

	//private async void ModifyRoom(object sender, EventArgs e)
	//{
 //       if (string.IsNullOrEmpty(TextBoxName.Text) || string.IsNullOrEmpty(TextBoxSeats.Text))
	//	{
 //           await DisplayAlert("Error", "Please fill in all the fields.", "OK");
 //           return;
 //       }
 //       if (!int.TryParse(TextBoxSeats.Text, out int seats))
	//	{
 //           await DisplayAlert("Error", "Please introduce a number in the seats field.", "OK");
 //           return;
 //       }
 //       _room.Name = TextBoxName.Text;
 //       _room.Seats = seats;
 //       await _localDbService.UpdateRoom(_room);
 //       await Navigation.PopAsync();
 //   }   

}