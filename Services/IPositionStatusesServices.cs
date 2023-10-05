using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    /// <summary>
    /// this interface is to be used for defining that the classes implemented with this interface have these task function
    /// </summary>
    internal interface IPositionStatusesServices
    {
        /*this task deifines that there should be a class called position_statuses and has a task function called GetPosition_StatusesList() that returns a list*/
        Task<List<position_statuses>> GetPosition_StatusesList();

        /*this task deifnes that there should be a class called position_statuses and has a task function called AddStatus()*/
        /*this task function should have a caller for position_statuses and a parameter called status */
        Task<int> AddStatus(position_statuses status);

        /*this task deifnes that there should be a class called position_statuses and has a task function called DeleteStatus()*/
        /*this task function should have a caller for position_statuses and a parameter called status */
        Task<int> DeleteStatus(position_statuses status);

        /*this task deifnes that there should be a class called position_statuses and has a task function called UpdateStatus()*/
        /*this task function should have a caller for position_statuses and a parameter called status */
        Task<int> UpdateStatus(position_statuses status);
    }
}
