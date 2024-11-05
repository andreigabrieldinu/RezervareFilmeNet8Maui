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
        [Column("price")]
        public double Price { get; set; }
        [Column("status")]
        public string Status { get; set; }
        [Column("email")]
        public string Email { get; set; }
    }
}
