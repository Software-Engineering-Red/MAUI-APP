using UndacApp.Data;
using UndacApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
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
		public async Task<List<OperationalTeamStatus>> GetOperationalTeamStatusesID(int id)
		{
			await SetUpDBOppTeamStatus();
			return await _dbConnection.Table<OperationalTeamStatus>().ToListAsync();

		}


		public async Task<List<OperationRecords>> GetOperationRecordsConfirmed_By(string Confirmed_By)
		{
			
			await SetUpDB();
			List<OperationRecords> oppr = new List<OperationRecords>();
			oppr = await _dbConnection.Table<OperationRecords>().Where(confirmed_b1y => confirmed_b1y.Confirmed_By.Contains(Confirmed_By)).ToListAsync();
			Console.WriteLine(oppr);
			return oppr;

		}

		public async Task<List<OperationRecords>> GetOperationRecordsConfirmed_Date(DateTime dateAndTimeCondirmed)
		{
			await SetUpDB();
			return await _dbConnection.Table<OperationRecords>().ToListAsync();
		}

		

		public async Task<List<OperationRecords>> GetOperationRecordsID(int id)
		{
			await SetUpDB();
			return await _dbConnection.Table<OperationRecords>().ToListAsync();
		}

		

		public async Task<List<OperationRecords>> GetOperationRecordsOperationalTeamID(int id)
		{
			await SetUpDB();
			return await _dbConnection.Table<OperationRecords>().ToListAsync();
		}

		

		public async Task<List<OperationRecords>> GetOperationRecordsRequested_By(string RequestBy)
		{
			await SetUpDB();
			return await _dbConnection.Table<OperationRecords>().ToListAsync();
		}

		

		public async Task<List<OperationRecords>> GetOperationRecordsRequest_Date(DateTime DateAndTimeRequest)
		{
			await SetUpDB();
			return await _dbConnection.Table<OperationRecords>().ToListAsync();
		}

		

		public async Task<List<OperationRecords>> GetOperationRecordsRequest_Details(string Details)
		{
			await SetUpDB();
			return await _dbConnection.Table<OperationRecords>().ToListAsync();
		}

		

		public async Task<List<OperationRecords>> GetOperationRecordsStatus(string status)
		{
			await SetUpDB();
			return await _dbConnection.Table<OperationRecords>().ToListAsync();
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

	   /* public async Task<List<string>> OperationRecordsRquest()
		{
			
			

		}*/
		#endregion
	}
}
