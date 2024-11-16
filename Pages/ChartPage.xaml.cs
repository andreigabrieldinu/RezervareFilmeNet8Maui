using CommunityToolkit.Maui.Markup;
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
		await InitializeRoom();
        
		Xceed.Maui.Toolkit.Series series = new Xceed.Maui.Toolkit.Series();
        Dictionary<string, int> roomReservationsDict = new Dictionary<string, int>();
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
            roomReservationsDict.Add(r.Name, roomReservations.Count);
		}
        for(int i = 0; i < roomReservationsDict.Count; i++)
		{
			Xceed.Maui.Toolkit.DataPoint dataPoint = new Xceed.Maui.Toolkit.DataPoint();

			dataPoint.Text= roomReservationsDict.FirstOrDefault(x=>x.Key==roomReservationsDict.ElementAt(i).Key).Key;
			dataPoint.Y=roomReservationsDict.ElementAt(i).Value;
            series.DataPoints.Add(dataPoint);
        }
		Chart1.Series.Add(series);
		Chart1.BackgroundColor=new Color(0,0,0,0);
		// transform the chart to bar chart
    }

    private async void ChangeChart(object sender, EventArgs e)
    {
        DateTime Date;
        if (CalendarRez.SelectedDate.HasValue)
        {
            Date = (DateTime)CalendarRez.SelectedDate;
        }
        else
        {
            Date = DateTime.Now;
        }
        Chart1.Series.Clear();
        List<Reservation> reservationByDate = await _localDbService.GetReservationsByDate(Date);
        Xceed.Maui.Toolkit.Series series = new Xceed.Maui.Toolkit.Series();
        Dictionary<string, int> roomReservationsDict = new Dictionary<string, int>();
        foreach (Room r in rooms)
        {
            List<Reservation> roomReservations = new List<Reservation>();
            foreach (Reservation rez in reservationByDate)
            {
                if (rez.RoomName == r.Name)
                {
                    roomReservations.Add(rez);
                }

            }
            roomReservationsDict.Add(r.Name, roomReservations.Count);
        }
        for (int i = 0; i < roomReservationsDict.Count; i++)
        {
            Xceed.Maui.Toolkit.DataPoint dataPoint = new Xceed.Maui.Toolkit.DataPoint();

            dataPoint.Text = roomReservationsDict.FirstOrDefault(x => x.Key == roomReservationsDict.ElementAt(i).Key).Key;
            dataPoint.Y = roomReservationsDict.ElementAt(i).Value;
            series.DataPoints.Add(dataPoint);
        }
        Chart1.Series.Add(series);
    }
}