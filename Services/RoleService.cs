using UndacApp.Data;
using UndacApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*! The RoleService class defines all the basic CRUD 
 *  methods for the role
 */

namespace UndacApp.Services
{
    public class RoleService : IRoleService
    {

        private SQLiteAsyncConnection _dbConnection;

        //! Basic method to instantiate the database
        private async Task SetUpDb()
        {
            if (_dbConnection != null)
                return;


            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Role>();
        }

        /*!
        * Adds role to database
        * @param role (Object) the role object to be used in the method
        */
        public async Task<int> AddRole(Role role)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(role);
        }

        /*!
        * Deletes role from database
        * @param role (Object) the role object to be used in the method
        */
        public async Task<int> DeleteRole(Role role)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(role);
        }

        /*!
        * Gets list of roles from database
        */
        public async Task<List<Role>> GetRoleList()
        {
            await SetUpDb();
            return await _dbConnection.Table<Role>().ToListAsync();
        }

        /*!
        * Updates a single role in the database
        * @param role (Object) the role object to be used in the method
        */
        public async Task<int> UpdateRole(Role role)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(role);
        }
    }
}
