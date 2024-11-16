using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervareFilmeNet8.API
{
    public class Revenue
    {
        public string MovieTitle { get; set; }
        public string Poster { get; set; }
        public int Total{ get; set; }

        public Revenue() { }

        public Revenue(string movieTitle, string poster)
        {
            MovieTitle = movieTitle;
            Poster = poster;
            Total = 0;
        }
        public static bool verifyMovie(string MovieTitle, List<Revenue> list)
        {
            if (list.Count == 0)
            {
                return false;
            }
            foreach (Revenue r in list)
            {
                if (r.MovieTitle == MovieTitle)
                {
                    return true;
                }
            }
            return false;
        }
        public static Revenue GetRevenue(string MovieTitle, List<Revenue> list)
        {
            foreach (Revenue r in list)
            {
                if (r.MovieTitle == MovieTitle)
                {
                    return r;
                }
            }
            return null;
        }

    }
}
