using MauiApp1.Data;
using MauiApp1.Models;
using SQLite;

namespace MauiApp1.Services
{
    /*! <summary>
        OrderStatusService extending IOrderStatusService Interface
    </summary> */
    public class OrderStatusService : IOrderStatusService
    {
        /*! <summary>
            Variable storing connection with db.
        </summary> */
        private SQLiteAsyncConnection _dbConn;
        /*! <summary>
        Method that initiates connection to database. Will create the OrderStatus table if connected.
        </summary> */
        private async Task ConnectToDB()
        {
            if (_dbConn != null)
                return;


            _dbConn = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConn.CreateTableAsync<OrderStatus>();
        }
        /*! <summary>
            Method that allows adding a status into the DB.
        </summary>
        <param name="status">OrderStatus to add to DB</param>
        <returns>Returns Task containing number of rows inserted into table</returns> 
        */
        public async Task<int> AddStatus(OrderStatus status)
        {
            await ConnectToDB();
            return await _dbConn.InsertAsync(status);
        }

        /*! <summary>
            Method that allows removing a status from the DB.
        </summary>
        <param name="status">OrderStatus to remove from DB</param>
        <returns>Returns Task containing number of rows deleted from table</returns> 
        */
        public async Task<int> DeleteStatus(OrderStatus status)
        {
            await ConnectToDB();
            return await _dbConn.DeleteAsync(status);
        }

        /*! <summary>
            Method that allows querying list of order status from the DB.
        </summary>
        <returns>Returns Task with list of order status in the db.</returns> 
        */
        public async Task<List<OrderStatus>> GetOrderStatusList()
        {
            await ConnectToDB();
            return await _dbConn.Table<OrderStatus>().ToListAsync();
        }

        /*! <summary>
            Method that allows updating a status from the DB.
        </summary>
        <param name="status">OrderStatus to update from DB</param>
        <returns>Returns Task containing number of rows updated from table</returns> 
        */
        public async Task<int> UpdateStatus(OrderStatus status)
        {
            await ConnectToDB();
            return await _dbConn.UpdateAsync(status);
        }
    }
}
