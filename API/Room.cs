using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervareFilmeNet8.API
{
    [Table("rooms")]
    public class Room
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("capacity")]
        public int Capacity { get; set; }
        [Column("rowNumbers")]
        public int RowNumbers { get; set; }
        [Column("status")]
        public string Status { get; set; }
        [Column("screenType")]
        public string ScreenType { get; set; }
        [Column("roomType")]
        public string RoomType { get; set; }
    }
}
