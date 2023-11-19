using UndacApp.Data;
using UndacApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services.LocalMedias
{
    public interface ILocalMediaService
    {  /*! <summary>
         * Method responsible for quering database, for entries in LocalMedia table
         * </summary> 
         */
        Task<List<LocalMedia>> GetLocalMediaList();
        /*! <summary>
         * Method responsible for addtion of entry into LocalMedia table.
         * </summary> 
         * <param name="localMedia">LocalMedia class instance that is attempted to be inserted into LocalMedia table.</param>
         */
        Task<int> AddLocalMedia(LocalMedia localMedia);
        /*! <summary>
         * Method responsible for deletion of an entry from LocalMedia table.
         * </summary> 
         * <param name="localMedia">LocalMedia class instance, that is attempted to be deleted from LocalMedia table. (Based on its' primary key)</param>
         */
        Task<int> DeleteLocalMedia(LocalMedia localMedia);
        /*! <summary>
         * Method responsible for updating an entry from LocalMedia table, based on primary key of the element passed.
         * </summary> 
         * <param name="localMedia">LocalMedia class instance that will be attempted to be updated, based on its' primary key value</param>
         */
        Task<int> UpdateLocalMedia(LocalMedia localMedia);
    }
}
