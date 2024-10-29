using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervareFilmeNet8.API
{
    [Table("movies")]
    public class Movies
    {
        [PrimaryKey]
        [Column("id")]
        public string imdbID { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("year")]
        public string Year { get; set; }
        [Column("rated")]
        public string Rated { get; set; }
        [Column("released")]
        public string Released { get; set; }
        [Column("runtime")]
        public string Runtime { get; set; }
        [Column("genre")]
        public string Genre { get; set; }
        [Column("director")]
        public string Director { get; set; }
        [Column("writer")]
        public string Writer { get; set; }
        [Column("actors")]
        public string Actors { get; set; }
        [Column("plot")]
        public string Plot { get; set; }
        [Column("language")]
        public string Language { get; set; }
        [Column("country")]
        public string Country { get; set; }
        [Column("awards")]
        public string Awards { get; set; }
        [Column("poster")]
        public string Poster { get; set; }
        [Column("metascore")]
        public string Metascore { get; set; }
        [Column("imdbRating")]
        public string imdbRating { get; set; }
        [Column("imdbVotes")]
        public string imdbVotes { get; set; }
        [Column("type")]
        public string Type { get; set; }
        [Column("dvd")]
        public string DVD { get; set; }
        [Column("boxOffice")]
        public string BoxOffice { get; set; }
        [Column("production")]
        public string Production { get; set; }
        [Column("website")]
        public string Website { get; set; }
        [Column("response")]
        public string Response { get; set; }
        public Movies() { }
        public Movies(string title, string year, string rated, string released, string runtime, string genre, string director, string writer, string actors, string plot, string language, string country, string awards, string poster, string metascore, string imdbRating, string imdbVotes, string imdbID, string type, string dVD, string boxOffice, string production, string website, string response)
        {
            Title = title;
            Year = year;
            Rated = rated;
            Released = released;
            Runtime = runtime;
            Genre = genre;
            Director = director;
            Writer = writer;
            Actors = actors;
            Plot = plot;
            Language = language;
            Country = country;
            Awards = awards;
            Poster = poster;
            Metascore = metascore;
            this.imdbRating = imdbRating;
            this.imdbVotes = imdbVotes;
            this.imdbID = imdbID;
            Type = type;
            DVD = dVD;
            BoxOffice = boxOffice;
            Production = production;
            Website = website;
            Response = response;
        }

        

    }
}