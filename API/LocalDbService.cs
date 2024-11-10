using RezervareFilmeNet8.API;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervareFilmeNet8
{
    public class LocalDbService
    {
        public static string DB_NAME="movies_db.db3";
        public SQLiteAsyncConnection connection;

        public LocalDbService()
        {

        }

        async Task Init()
        {
            if (connection is not null)
            {
                return;
            }
            //aici puneti calea absoluta catre baza de 
            connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            await connection.CreateTableAsync<Movies>();
            await connection.CreateTableAsync<Room>();
            await connection.CreateTableAsync<Reservation>();
        }

        public async Task <List<Movies>> GetMovies()
        {
            await Init();
            return await connection.Table<Movies>().ToListAsync();

        }
        public async Task<Movies> GetByTitle(string title)
        {
            await Init();
            return await connection.Table<Movies>().Where(x => x.Title == title).FirstOrDefaultAsync();
        }
        public async Task<Movies> GetByIdImdb(string idIMDB)
        {
            await Init();
            return await connection.Table<Movies>().Where(x => x.imdbID == idIMDB).FirstOrDefaultAsync();
        }
        public async Task DeleteMovie(string title)
        {
            await Init();
            await connection.Table<Movies>().Where(x => x.Title == title).DeleteAsync();
        }
        public async Task InsertMovie(Movies movie)
        {
            await Init();
            if (movie.imdbID is not null)
            {
                Movies m= await GetByIdImdb(movie.imdbID);
                if (m is null)
                {
                    await connection.InsertAsync(movie);
                }
            }
            
        }
        public async Task CreateRoom(Room room)
        {
            await Init();
            if (room.Name is not null)
            {
                Room r = await connection.Table<Room>().Where(x => x.Name == room.Name).FirstOrDefaultAsync();
                if (r is null)
                {
                    await connection.InsertAsync(room);
                }
            }
        }
        public async Task<List<Room>> GetRooms()
        {
            await Init();
            return await connection.Table<Room>().ToListAsync();
        }
        public async Task<Room> GetRoomByName(string name)
        {
            await Init();
            return await connection.Table<Room>().Where(x => x.Name == name).FirstOrDefaultAsync();
        }
        public async Task<Room> GetRoomByCapacity(int capacity)
        {
            await Init();
            return await connection.Table<Room>().Where(x => x.Capacity == capacity).FirstOrDefaultAsync();
        }
        public async Task DeleteRoom(string name)
        {
            await Init();
            await connection.Table<Room>().Where(x => x.Name == name).DeleteAsync();
        }
        public async Task InsertReservation(Reservation reservation)
        {
            await Init();
            
        }   

    }
}
