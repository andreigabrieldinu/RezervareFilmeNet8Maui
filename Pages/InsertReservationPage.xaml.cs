using Microsoft.Maui.ApplicationModel.Communication;
using RezervareFilmeNet8.API;
using System.Net.Mail;
using Xceed.Maui.Toolkit.Themes.Brushes;

namespace RezervareFilmeNet8.Pages;

public partial class InsertReservationPage : ContentPage
{
	private LocalDbService _localDbService;
    private List<Movies> movies;
    private List<Room> rooms;
    private List<Reservation> reservations;
    private Dictionary<string,List<Reservation>> reservationsMap=new Dictionary<string, List<Reservation>>();

    public InsertReservationPage(LocalDbService localDbService)
	{
		InitializeComponent();
		_localDbService = localDbService;
	}
    async Task InitializeMovies()
    {
        movies = await _localDbService.GetMovies();
    }
    async Task InitializeRooms()
    {
        rooms = await _localDbService.GetRooms();
    }
    async Task InitializeReservation()
    {
        reservations = await _localDbService.GetReservations();
    }
    Dictionary<string, List<Reservation>> InsertRoom(Dictionary<string, List<Reservation>> dict, Room room)
    {
        if(!dict.ContainsKey(room.Name))
        {
            List<Reservation> list = new List<Reservation>();
            dict.Add(room.Name, list);
        }
        return dict;
    }
    Dictionary<string, List<Reservation>> InsertReservationsInDict(Dictionary<string, List<Reservation>> dict, Reservation r)
    {
        dict[r.RoomName].Add(r);
        return dict;
    }

    async Task PopulateDictionary() 
    {
        Dictionary<string, List<Reservation>> dict = new Dictionary<string, List<Reservation>>();
        foreach (Room r in rooms)
        {
            List<Reservation> list = new List<Reservation>();
            foreach (Reservation rez in reservations)
            {
                if (rez.RoomName == r.Name)
                {
                    list.Add(rez);
                }
            }
            dict.Add(r.Name,list);
        }
        reservationsMap=dict;
    }


    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await InitializeMovies();
        await InitializeRooms();
        await InitializeReservation();
        await PopulateDictionary();
        string[] moviesTitles= new string[movies.Count];
        string[] roomsNames = new string[rooms.Count];
        for (int i = 0; i < movies.Count; i++)
        {
            moviesTitles[i] = movies[i].Title;
        }
        for (int i = 0; i < rooms.Count; i++)
        {
            roomsNames[i] = rooms[i].Name;
        }
        PickerMovie.ItemsSource = moviesTitles;
        PickerRoom.ItemsSource = roomsNames;
    }
    private bool validateMail(string email)
    {
        try
        {
            MailAddress emailAddress = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }

    async Task<bool[]> EnoughSeats(Dictionary<string, List<Reservation>> dict, Reservation r, Room room)
    {
        bool[] array=new bool[2];
        array[0]=false;
        array[1] = false;
        if (room.Status != "Available")
        {
            array[0]=false;
        }
        else
        {
            int suma = 0;
            foreach (Reservation rez in dict[room.Name])
            {
                suma += rez.PersonsNumber;
            }
            if(suma + r.PersonsNumber <= room.Capacity)
            {
                if (suma + r.PersonsNumber == room.Capacity)
                {
                    array[1] = true;
                }
                array[0]=true;
            }
            else
            {
                array[0]=false;
            }
        }
        return array;
    }
    async Task<bool> ValidateReservation(Dictionary<string, List<Reservation>> dict, Reservation r,Room room, bool[] bools, List<int> durations)
    {
        if (!bools[0])
        {
            return false;
        }
        else
        {
            for (int i = 0; i < durations.Count; i++)
            {
                DateTime date = DateTime.Parse(dict[room.Name][i].Date);
                DateTime date2 = DateTime.Parse(r.Date);
                TimeSpan ts;
                if (date.CompareTo(date2) > 0)
                {
                    ts = date - date2;
                }
                else
                {
                    ts = date2 - date;
                }
                if (ts.TotalMinutes >= durations[i])
                {
                    return true;
                }

            }    
               
        }
        return false;
    }
    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        int newValue = (int)e.NewValue;
        LabelNumberTickets.Text = "Number of reserved tickets=" + newValue;
    }
    private async void InsertReservation(object sender, EventArgs e)
    {
        string movieTitle = PickerMovie.SelectedItem.ToString();
        string roomName = PickerRoom.SelectedItem.ToString();
        DateTime date = DateUpDown.Value.Value;
        string email=TbEmail.Text;
        int personsNumber = (int)SliderTickets.Value;
        bool emailValid = validateMail(email);
        if(!emailValid)
        {
            await DisplayAlert("Error", "Please fill the email field correctly.", "OK");
            return;
        };

        Reservation r = new Reservation(movieTitle, roomName, date.ToString(), personsNumber, email);
        Room room = await _localDbService.GetRoomByName(r.RoomName);

        reservationsMap = InsertRoom(reservationsMap, room);
        bool[] array= EnoughSeats(reservationsMap, r, room).Result;

        if (!array[0])
        {
            await DisplayAlert("Error", "The room is not available at this time.", "OK");
            return;
        }
        List<Reservation> reservations = new List<Reservation>();
        reservations= reservationsMap[room.Name];
        List<Movies> movies = new List<Movies>();
        List<int> durationsForMovies=new List<int>();
        foreach (Reservation reservation in reservations)
        {
            Movies m = await _localDbService.GetMovieByTitle(reservation.MovieTitle);
            movies.Add(m);
            durationsForMovies.Add(int.Parse(m.Runtime.Substring(0, 2).Trim()) + 30);
        }
        if(!ValidateReservation(reservationsMap, r, room,array,durationsForMovies).Result)
        {
            await DisplayAlert("Error", "The room is not available at this time.", "OK");
            return;
        }
        else
        {
            reservationsMap = InsertReservationsInDict(reservationsMap, r);
            await _localDbService.CreateReservation(r);
            if (array[1])
            {
                await _localDbService.UpdateRoomFull(room);
            }
        }
    }    
}