using UndacApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    /*! <summary>
       Interface that exposes methods of EquipmentService
   </summary> */
    public interface IEquipmentService
    {
        /*! <summary>
  Method responsible for quering database, for entries in Equipment table
 </summary> 
  <returns>Returns Task containing List of Equipment present in the database.</returns>*/
        Task<List<Equipment>> GetEquipmentList();
        /*! <summary>
     Method responsible for addtion of entry into Equipment table.
 </summary> 
  <param name="org">Equipment class instance that is attempted to be inserted into Equipment table.</param>
  <returns>Returns Task containing number of rows inserted into Equipment table.</returns> */
        Task<int> AddEquipment(Equipment equipment);

        /*! <summary>
  Method responsible for deletion of an entry from Equipment table.
 </summary> 
  <param name="org">Equipment class instance, that is attempted to be deleted from Equipment table. (Based on its' primary key)</param>
  <returns>Returns Task containing number of rows deleted from Equipment table.</returns>*/
        Task<int> DeleteEquipment(Equipment equipment);

        /*! <summary>
 Method responsible for updating an entry from Equipment table, based on primary key of the element passed.
</summary> 
 <param name="org">Equipment class instance that will be attempted to be updated, based on its' primary key value</param>
 <returns>Returns Task containing number of rows updated in Equipment table.</returns>*/
        Task<int> UpdateEquipment(Equipment equipment);
    }
}