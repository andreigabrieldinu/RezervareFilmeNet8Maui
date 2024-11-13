using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervareFilmeNet8.API
{
    [Table("rezervations")]
    public class Reservation
    {
        [PrimaryKey]
        [AutoIncrement]    
        [Column("id")]
        public int Id { get; set; }
        [Column("movie_title")]
        public string MovieTitle { get; set; }
        [Column("room_id")]
        public string RoomName { get; set; }
        [Column("date")]
        public string Date { get; set; }
        [Column("PersonsNumber")]
        public int PersonsNumber{ get; set; }
        [Column("email")]
        public string Email { get; set; }
        public Reservation() {}
        public Reservation(string movieTitle, string roomName, string date, int pers, string email)
        {
            MovieTitle = movieTitle;
            RoomName = roomName;
            Date = date;
            PersonsNumber = pers;
            Email = email;
        }
    }
    
}
