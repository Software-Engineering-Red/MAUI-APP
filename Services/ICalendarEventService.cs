using UndacApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    internal interface ICalendarEventService
    {
        Task<List<CalendarEvent>> GetAllCalendarEvents();
        Task<int> AddCalendarEvent(CalendarEvent e);
        Task<int> DeleteCalendarEvent(CalendarEvent e);
        Task<int> UpdateCalendarEvent(CalendarEvent e);
    }
}










