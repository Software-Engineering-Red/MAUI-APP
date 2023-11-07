using UndacApp.Models;

namespace UndacApp.Services
{
    /*! <summary>
        Interface that exposes methods of OrderStatusService
    </summary> */
    internal interface IOrderStatusService
    {
        /*! <summary>
        Method responsible for quering list of order status in the database
        </summary> 
        <returns>Returns Task containing List of Order Status present in the database.</returns>
         */
        Task<List<OrderStatus>> GetOrderStatusList();
        /*! <summary>
        Method responsible for creating new order status in db
        </summary> 
        <param name="status">status to add into the db</param>
        <returns>Returns task containing rows added to the db</returns>
         */
        Task<int> AddStatus(OrderStatus status);
        /*! <summary>
        Method responsible for deleting order status in db
        </summary> 
        <param name="status">status to delete from the db</param>
        <returns>Returns task containing rows removed from the db</returns>
         */
        Task<int> DeleteStatus(OrderStatus status);
        /*! <summary>
        Method responsible for updating order status in db
        </summary> 
        <param name="status">status to update from the db</param>
        <returns>Returns task containing rows updated from the db</returns>
         */
        Task<int> UpdateStatus(OrderStatus status);
    }
}
