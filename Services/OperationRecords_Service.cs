using MauiApp1.Data;
using MauiApp1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    internal class OperationRecords_Service : IOperation_Records
    {
        private SQLiteAsyncConnection _dbConnection;
        #region database initilaisation
        async Task SetUpDB()
        {
            if (_dbConnection != null)
                return;

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<OperationRecords>();

        }

        async Task SetUpDBOppTeamStatus()
        { if (_dbConnection != null)
                return;


            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<OperationalTeamStatus>();
        }
        #endregion
        public OperationRecords_Service() { }

        #region getting data
        public async Task<List<OperationalTeamStatus>> GetOperationalTeamStatusesTable()
        {
            await SetUpDBOppTeamStatus();
            return await _dbConnection.Table<OperationalTeamStatus>().ToListAsync();

        }

        public async Task<List<int>> GetOperationalTeamStatusesID()
        {
            List<OperationalTeamStatus> oppTeam = await GetOperationalTeamStatusesTable();
            List<int> TeamID = new List<int>();
            TeamID = oppTeam.Select(name => name.ID).ToList();
            return TeamID;
        }

        public async Task<List<OperationRecords>> GetOperationRecordsConfirmed_ByTable()
        {
            await SetUpDB();

            return await _dbConnection.Table<OperationRecords>().ToListAsync();

        }

        public async Task<List<string>> GetOperationRecordsConfirmed_By()
        {
            List<OperationRecords> oppRecords = await GetOperationRecordsConfirmed_ByTable();
            List<string> c_By = new List<string>();
            c_By = oppRecords.Select(confirmed_by => confirmed_by.Confirmed_By).ToList();
            return c_By;
        }

        public async Task<List<OperationRecords>> GetOperationRecordsConfirmed_DateTable()
        {
            await SetUpDB();
            return await _dbConnection.Table<OperationRecords>().ToListAsync();
        }

        public async Task<List<DateTime>> GetOperationRecordsConfirmed_Date()
        {
            List<OperationRecords> oppRecords = await GetOperationRecordsConfirmed_DateTable();
            List<DateTime> c_Date = new List<DateTime>();
            c_Date = oppRecords.Select(confirmed_Date => confirmed_Date.Confirmed_Date).ToList();
            return c_Date;
        }

        public async Task<List<OperationRecords>> GetOperationRecordsIDTable()
        {
            await SetUpDB();
            return await _dbConnection.Table<OperationRecords>().ToListAsync();
        }

        public async Task<List<int>> GetOperationRecordsID()
        {
            List<OperationRecords> oppRecords = await GetOperationRecordsIDTable();
            List<int> oppRecordID = new List<int>();
            oppRecordID = oppRecords.Select(operationRecordsID => operationRecordsID.Id).ToList();
            return oppRecordID;
        }

        public async Task<List<OperationRecords>> GetOperationRecordsOperationalTeamIDTable()
        {
            await SetUpDB();
            return await _dbConnection.Table<OperationRecords>().ToListAsync();
        }

        public async Task<List<int>> GetOperationRecordsOpperationalTeamID()
        {
            List<OperationRecords> oppRecords = await GetOperationRecordsOperationalTeamIDTable();
            List<int> oppRecordsOppTeamID = new List<int>();
            oppRecordsOppTeamID = oppRecords.Select(oppRecordsOPpTeamID => oppRecordsOPpTeamID.OperationalTeamID).ToList();
            return oppRecordsOppTeamID;
        }

        public async Task<List<OperationRecords>> GetOperationRecordsRequested_ByTable()
        {
            await SetUpDB();
            return await _dbConnection.Table<OperationRecords>().ToListAsync();
        }

        public async Task<List<string>> GetOperationReordsRequested_By()
        {
            List<OperationRecords> oppRecords = await GetOperationRecordsRequested_ByTable();
            List<string> oppRecordsRequestedBy = new List<string>();
            oppRecordsRequestedBy = oppRecords.Select(oppRecordsRB => oppRecordsRB.Requested_By).ToList();
            return oppRecordsRequestedBy;
        }

        public async Task<List<OperationRecords>> GetOperationRecordsRequest_DateTable()
        {
            await SetUpDB();
            return await _dbConnection.Table<OperationRecords>().ToListAsync();
        }

        public async Task<List<DateTime>> GetOperationRecordsRequest_Date()
        {
            List<OperationRecords> oppRecords = await GetOperationRecordsRequest_DateTable();
            List<DateTime> oppRequestedDate = new List<DateTime>();
            oppRequestedDate = oppRecords.Select(oppRecordsRequestDate => oppRecordsRequestDate.Requested_Date).ToList();
            return oppRequestedDate;
        }

        public async Task<List<OperationRecords>> GetOperationRecordsRequest_DetailsTable()
        {
            await SetUpDB();
            return await _dbConnection.Table<OperationRecords>().ToListAsync();
        }

        public async Task<List<string>> GetOperationRequest_Details()
        {
            List<OperationRecords> oppRecords = await GetOperationRecordsRequest_DetailsTable();
            List<string> oppDetails = new List<string>();
            oppDetails = oppRecords.Select(operationRequestDetails => operationRequestDetails.Requested_Detail).ToList();
            return oppDetails;
            
        }

        public async Task<List<OperationRecords>> GetOperationRecordsStatusTable()
        {
            await SetUpDB();
            return await _dbConnection.Table<OperationRecords>().ToListAsync();
        }

        public async Task<List<string>> GetOperationsRecordsStatus()
        {
            List<OperationRecords> oppRecords = await GetOperationRecordsStatusTable();
            List <string> oppRecordsStatus = new List<string>();
            oppRecordsStatus = oppRecords.Select(operationRecordsStatus => operationRecordsStatus.Status).ToList();
            return oppRecordsStatus;
        }
        #endregion

        #region add data
        public async Task<int> AddOperationRecords_Operational_TeamID(OperationRecords operationRecords, OperationalTeamStatus operational_team_status)
        {
            await SetUpDB();
            await SetUpDBOppTeamStatus();
            if(operationRecords.OperationalTeamID != operational_team_status.ID)
            {
                operationRecords.OperationalTeamID = operational_team_status.ID;
            }

            return await _dbConnection.InsertAsync(operationRecords.OperationalTeamID);
        }

        public async Task<int> AddOperationsRecords_Confirmed_By(OperationRecords operationRecords)
        {
            await SetUpDB();
            return await _dbConnection.InsertAsync(operationRecords.Confirmed_By);
        }

        public async Task<int> AddOperationsRecords_Status(OperationRecords operationRecords)
        {
            await SetUpDB();
            return await _dbConnection.InsertAsync(operationRecords.Status);
        }

        public async Task<int> AddOperationsRecprds_Confirmed_Date(OperationRecords operationRecords)
        {
            await SetUpDB();
            return await _dbConnection.InsertAsync(operationRecords.Confirmed_Date);
        }
        #endregion

        #region task to create a displayable item
        public async Task<int> addOperationRecordrquest(OperationRecords operationRecords)
        {
            await SetUpDB();
            return await _dbConnection.InsertAsync(operationRecords.FK_OpperationRecordsID_OperationalTeamID);
        }

        public async Task<List<string>> OperationRecordsRquest()
        {
            
            

        }
        #endregion
    }
}
