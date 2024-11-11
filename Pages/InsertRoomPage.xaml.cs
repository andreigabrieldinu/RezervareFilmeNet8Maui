using RezervareFilmeNet8.API;

namespace RezervareFilmeNet8.Pages;

public partial class InsertRoomPage : ContentPage
{
	private List<RoomsPage> _rooms;
	private LocalDbService _localDbService;

	public InsertRoomPage(LocalDbService lbd)
	{
		InitializeComponent();
		_localDbService = lbd;
	}

    private void SliderCapacity_ValueChanged(object sender, ValueChangedEventArgs e)
    {
		int newValue = (int)e.NewValue;
		LabelCapacity.Text = "Capacity=" + newValue;
    }

    private void SliderRows_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        int newValue = (int)e.NewValue;
        LabelNumberRows.Text= "Number of rows=" + newValue;
    }

    private async void InsertRoomClicked(object sender, EventArgs e)
    {
        string tbRoomName = TbRoomName.Text;
        int capacity = (int)SliderCapacity.Value;
        int rows = (int)SliderRows.Value;
        string status = PickerStatus.SelectedItem.ToString();
        string screenType = PickerScreenType.SelectedItem.ToString();
        string roomType = "";
        if (Rb3D.IsChecked)
        {
            roomType = "3D";
        }
        else if (Rb2D.IsChecked)
        {
            roomType = "2D";
        }
        else if (Rb4DX.IsChecked)
        {
            roomType = "4DX";
        }
        else if (RbIMAX.IsChecked)
        {
            roomType = "IMAX";
        }
        else
        {
            roomType = "VIP";
        }
        if (tbRoomName is null || status is null || screenType is null || roomType is null)
        {
            DisplayAlert("Error", "Please fill in all the fields.", "OK");
            return;
        }
        Room r = new Room(tbRoomName, capacity, rows, status, screenType, roomType);
        Room r2=await _localDbService.GetRoomByName(r.Name);
        if (r2 is not null)
        {
            await DisplayAlert("Error", "Room already exists in the database.", "OK");
            return;
        }
        await _localDbService.CreateRoom(r);
        await DisplayAlert("Success", "Room was added!", "OK");
        TbRoomName.Text = null;
        SliderCapacity.Value = 1;
        SliderRows.Value = 1;
        PickerStatus.SelectedItem = null;
        PickerScreenType.SelectedItem = null;
        Rb3D.IsChecked = false;
        Rb2D.IsChecked = false;
        Rb4DX.IsChecked = false;
        RbIMAX.IsChecked = false;
        RbVIP.IsChecked = false;
    }
}