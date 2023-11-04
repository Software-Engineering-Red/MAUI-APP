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
    /*! <summary>
     * Interface that exposes methods of SystemTypeService
     * </summary> 
     */
    public interface ISystemTypeService
    {
        /*! <summary>
         * Method responsible for quering database, for entries in System Type table
         * </summary> 
         */
        Task<List<SystemType>> GetSystemTypeList();
        /*! <summary>
         * Method responsible for addtion of entry into System Type table.
         * </summary> 
         * <param name="systemType">SystemType class instance that is attempted to be inserted into System Type table.</param>
         */
        Task<int> AddSystemType(SystemType systemType);
        /*! <summary>
         * Method responsible for deletion of an entry from System Type table.
         * </summary> 
         * <param name="systemType">SystemType class instance, that is attempted to be deleted from System Type table. (Based on its' primary key)</param>
         */
        Task<int> DeleteSystemType(SystemType systemType);
        /*! <summary>
         * Method responsible for updating an entry from System Type table, based on primary key of the element passed.
         * </summary> 
         * <param name="systemType">SystemType class instance that will be attempted to be updated, based on its' primary key value</param>
         */
        Task<int> UpdateSystemType(SystemType systemType);
    }
}
