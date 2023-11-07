﻿using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    internal interface IOperation_Records
    {
        #region get data
        /*tasks that get the list of  data from the databas for each column in the table*/
        /*task that  return list of ID of the record, operational teamID and name of the the reqest*/
        Task<List<OperationRecords>> GetOperationRecordsIDTable();
        Task<List<int>> GetOperationRecordsID();

        Task<List<OperationRecords>> GetOperationRecordsOperationalTeamIDTable();
        Task<List<int>> GetOperationRecordsOpperationalTeamID();

        Task<List<OperationRecords>> GetOperationRecordsRequested_ByTable();

        /*tasks that return the list of details of the request, date it was requested and status of the request*/
        Task<List<OperationRecords>> GetOperationRecordsRequest_DetailsTable();
        Task<List<string>> GetOperationRequest_Details();

        Task<List<OperationRecords>> GetOperationRecordsRequest_DateTable();
        Task<List<DateTime>> GetOperationRecordsRequest_Date();

        Task<List<OperationRecords>> GetOperationRecordsStatusTable();
        Task<List<string>> GetOperationsRecordsStatus();

        /*tasks returns lists of data from other table to be used for UI only*/
        /*Task<List<ResourceTable>> GetResourceID()*//*place holder for when Resource table created*/

        /*tasks return lists with data that will later be used to add to the Operationrecords table from other tables*/
        Task<List<OperationalTeamStatus>> GetOperationalTeamStatusesTable();
        Task<List<int>> GetOperationalTeamStatusesID();
        /*Task<List<RequestTable>> GetRequestTableRequest_By();*/ /*placeholder for when Request table added*/
        /*Task<List<RequestTable>> GetRequestTable_Details();*/
        /*Task<List<RequestTable>> GetRequestTables_Date();*/


        /*tasks that reutrn the list  of names confiriming the status and the date the confirmations was on as well as the time */
        Task<List<OperationRecords>> GetOperationRecordsConfirmed_ByTable();
        Task<List<string>> GetOperationRecordsConfirmed_By();

        Task<List<OperationRecords>> GetOperationRecordsConfirmed_DateTable();
        Task<List<DateTime>> GetOperationRecordsConfirmed_Date();
        #endregion
        #region add data
        /*task that add data from seperate tables to the OperationRecords table*//* these are place holders untill the tables are added*/
        Task<int> AddOperationRecords_Operational_TeamID(OperationRecords OperationRecords, OperationalTeamStatus operational_team_status);/* operational_team_status is a place holder untill there is a operational team table*/
        /*Task<int> AddOperationRecords_Requested_By(OperationRecords OperationRecords, RequestTable requesttable); to be used for adding the request name to the OperationRecords table is a placeholder  untill RequestTable is added*/
        /*Task<int> AddOperationsRecords_Request_Details(OperationRecords OperationRecords, RequestTable requesttable); to be used for adding request details to the OperationRecords table*/
        /*Task<int> AddOperationsRecords_Request_Date(OperationRecords OperationRecords, RequestTable requesttable); to be used for adding request dates to the OperationRecords table*/

        /*tasks that add data from the UI to the table*/
        Task<int> AddOperationsRecords_Status(OperationRecords operationRecords);
        Task<int> AddOperationsRecords_Confirmed_By(OperationRecords operationRecords);
        Task<int> AddOperationsRecprds_Confirmed_Date(OperationRecords operationRecords);
        #endregion

        #region extra used methods
        /*task for combined lists of ID of the table, Opperational Team ID, and requested By. name */
        Task<int> addOperationRecordrquest(OperationRecords operationRecords);

        Task<List<string>> OperationRecordsRquest();
        #endregion
    }
}