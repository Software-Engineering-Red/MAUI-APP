using UndacApp.Data;
using UndacApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    public class VolunteerService : IVolunteerService
    {
        private SQLiteAsyncConnection _dbConnection;

        public VolunteerService()
        {
            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            _dbConnection.CreateTableAsync<Volunteer>().Wait();
        }

        public async Task<int> AddVolunteer(Volunteer volunteer)
        {
            return await _dbConnection.InsertAsync(volunteer);
        }

        public async Task<int> DeleteVolunteer(Volunteer volunteer)
        {
            return await _dbConnection.DeleteAsync(volunteer);
        }

        public async Task<List<Volunteer>> GetVolunteerList()
        {
            return await _dbConnection.Table<Volunteer>().ToListAsync();
        }

        public async Task<int> UpdateVolunteer(Volunteer volunteer)
        {
            return await _dbConnection.UpdateAsync(volunteer);
        }

        public async Task<int> FlagVolunteer(Volunteer volunteer)
        {
            if (volunteer != null)
            {
                volunteer.Status = "Flagged";
                return await UpdateVolunteer(volunteer);
            }
            return 0;
        }

        public async Task<int> ClearFlagVolunteer(Volunteer volunteer)
        {
            if (volunteer != null)
            {
               
                volunteer.Status = "Neutral";
                volunteer.DateOfDeparture = null;
                volunteer.DateOfArrival = null;
                return await UpdateVolunteer(volunteer);
            }
            return 0;
        }

        public async Task<int> SendInvitationEmail(Volunteer volunteer, DateTime arrivalDate, DateTime departureDate)
        {
            if (volunteer != null)
            {
                string emailRecipient = volunteer.Email;
                string emailSubject = "Volunteer Invitation";
                string emailBody = $"Hi there {volunteer.Name}, we request your assistance from {arrivalDate} until {departureDate}.";

                try
                {
                    var message = new EmailMessage
                    {
                        Subject = emailSubject,
                        Body = emailBody,
                        To = { emailRecipient }
                    };

                    await Email.ComposeAsync(message);
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0; // Email sending failed
                }
            }

            return 0; // Volunteer not found
        }
    }
}
