using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NekoSpot
{
    public class SQLiteCatDatabase : ICatDatabase
    {
        SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;
        string dbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "cat.db3");
        SQLiteAsyncConnection database;

        public SQLiteCatDatabase()
        {
            database = new SQLiteAsyncConnection(dbPath, Flags);
            database.CreateTableAsync<Cat>().Wait();

        }

        public async Task<List<Cat>> GetCatsAsync() => await database.Table<Cat>().ToListAsync();
        public async Task<Cat> GetCatAsync(int id) => await database.Table<Cat>().FirstOrDefaultAsync(x => x.Id == id);
        public async Task DeleteCatAsync(Cat cat) => await database.DeleteAsync(cat);
        public async Task UpdateCatAsync(Cat cat) => await database.UpdateAsync(cat);
        public async Task CreateCatAsync(Cat cat) => await database.InsertAsync(cat);

    }
}
