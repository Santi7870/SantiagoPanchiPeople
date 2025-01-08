using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SantiagoPanchiPeople.Models;
using SQLite;

namespace SantiagoPanchiPeople.Data
{
    public class PersonRepository
    {
        private SQLiteAsyncConnection conn;
        private string _dbPath;
        public string StatusMessage { get; set; }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        private async Task InitAsync()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<Person>();
        }

        public async Task AddNewPersonAsync(string name)
        {
            try
            {
                await InitAsync();

                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                int result = await conn.InsertAsync(new Person { Name = name });
                StatusMessage = $"Inserted {result} record(s) [Name: {name}]";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to add {name}. Error: {ex.Message}";
            }
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            try
            {
                await InitAsync();
                return await conn.Table<Person>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
                return new List<Person>();
            }
        }
    }
}

