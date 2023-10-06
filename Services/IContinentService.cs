using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    /*! <summary>
        Interface that exposes methods of ContinentService
    </summary> */
    public interface IContinentService
    {
        /*! <summary>
         Method responsible for quering database, for entries in Continent table
        </summary> 
         <returns>Returns Task containing List of Continents present in the database.</returns>*/
        Task<List<Continent>> GetContinentList();

        /*! <summary>
            Method responsible for addtion of entry into Continent table.
        </summary> 
         <param name="org">Continent class instance that is attempted to be inserted into Continent table.</param>
         <returns>Returns Task containing number of rows inserted into Continent table.</returns> */
        Task<int> AddContinent(Continent continent);

        /*! <summary>
         Method responsible for deletion of an entry from Continent table.
        </summary> 
         <param name="org">Continent class instance, that is attempted to be deleted from Continent table. (Based on its' primary key)</param>
         <returns>Returns Task containing number of rows deleted from Continent table.</returns>*/
        Task<int> DeleteContinent(Continent continent);

        /*! <summary>
         Method responsible for updating an entry from Continent table, based on primary key of the element passed.
        </summary> 
         <param name="org">Continent class instance that will be attempted to be updated, based on its' primary key value</param>
         <returns>Returns Task containing number of rows updated in Continent table.</returns>*/
        Task<int> UpdateContinent(Continent continent);
    }
}
