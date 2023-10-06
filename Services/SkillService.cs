using MauiApp1.Data;
using MauiApp1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class SkillService : ISkillService
    {

        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection != null)
                return;


            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Skill>();
        }

        public async Task<int> AddSkill(Skill skill)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(skill);
        }

        public async Task<int> DeleteSkill(Skill skill)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(skill);
        }

        public async Task<List<Skill>> GetSkillList()
        {
            await SetUpDb();
            return await _dbConnection.Table<Skill>().ToListAsync();
        }

        public async Task<int> UpdateSkill(Skill skill)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(skill);
        }
    }
}
