
using UndacApp.Data;
using UndacApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    internal class CalendarEventService : ICalendarEventService
    {
        private SQLiteAsyncConnection _dbConnection;
        
        // Wait for db con
        private async Task SetUpDb()
        {
            if (_dbConnection != null) 
            {
                return;
            }

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<CalendarEvent>();
        }
        // Add a new calendar event
        public async Task<int> AddCalendarEvent(CalendarEvent e)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(e);
        }

        // Delete new calendar event
        public async Task<int> DeleteCalendarEvent(CalendarEvent e)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(e);
        }

        // Get list
        public async Task<List<CalendarEvent>> GetAllCalendarEvents()
        {
            await SetUpDb();
            return await _dbConnection.Table<CalendarEvent>().ToListAsync();
        }

        // Update
        public async Task<int> UpdateCalendarEvent(CalendarEvent e)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(e);
        }

    }
}
