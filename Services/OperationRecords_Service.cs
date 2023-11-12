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
using System.Net.NetworkInformation;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Immutable;

namespace UndacApp.Services
{
	internal class OperationRecords_Service : IOperation_Records//note unfinished build
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
        #region constructor
        public OperationRecords_Service() { }
        #endregion

        #region getting data

		public async Task<List<OperationRecords>> GetOperationRecordsTable()
		{
			await SetUpDB();
			return await _dbConnection.Table<OperationRecords>().ToListAsync();
		}

        public async Task<List<OperationalTeamStatus>> GetOperationalTeamStatusesID(int id)/*returns the Operational Teams ID from the foreign table specfic instance*/
		{
			await SetUpDBOppTeamStatus();
			List<OperationalTeamStatus> OppT = new List<OperationalTeamStatus>();
            OppT = await _dbConnection.Table<OperationalTeamStatus>().Where(ids => ids.ID == id).ToListAsync();
            return OppT;
            
		}
        public async Task<List<int>> GetOperationalTeamStatusesIDL()//return the Operational Teams ID List in full
        {
            await SetUpDBOppTeamStatus();
			List<OperationalTeamStatus> OppT;
			OppT = await _dbConnection.Table<OperationalTeamStatus>().ToListAsync();
			List<int> newOppT = new List<int>();
			newOppT = OppT.Select(i => i.ID).ToList();
			return newOppT;

        }


        public async Task<List<OperationRecords>> GetOperationRecordsConfirmed_By(string Confirmed_By)//returns the list of Confirmed_By
		{
			
			await SetUpDB();
			List<OperationRecords> OppT = new List<OperationRecords>();
			OppT = await _dbConnection.Table<OperationRecords>().Where(CB => CB.Confirmed_By == Confirmed_By).ToListAsync();
			return OppT;

		}
        public async Task<List<string>> GetOperationRecordsConfirmed_ByL()//returns the list of Confirmed_By data
        {

            await SetUpDB();
            List<OperationRecords> Oppr = new List<OperationRecords>();
            Oppr = await _dbConnection.Table<OperationRecords>().ToListAsync();
            List<string> oppr = Oppr.Select(OppR => OppR.Confirmed_By).ToList();
            return oppr;

        }

        public async Task<List<OperationRecords>> GetOperationRecordsConfirmed_Date(DateTime dateAndTimeCondirmed)//returns the date time of the database as a list of specific data
		{
			await SetUpDB();
			List<OperationRecords> OppR = new List<OperationRecords>();
			OppR = await _dbConnection.Table<OperationRecords>().Where(CD => CD.Confirmed_Date == dateAndTimeCondirmed).ToListAsync();
			return OppR;
		}
        public async Task<List<DateTime>> GetOperationRecordsConfirmed_DateL()//returns the date time of the database as a full list
        {
            await SetUpDB();
			List<OperationRecords> OppR = new List<OperationRecords>();
            OppR = await _dbConnection.Table<OperationRecords>().ToListAsync();
			List<DateTime> OppRT = OppR.Select(Oppr =>  Oppr.Confirmed_Date).ToList();
			return OppRT;
        }

        public async Task<List<OperationRecords>> GetOperationRecordsID(int id)//returns the list of IDs for the operations records specific ids
		{
			await SetUpDB();
			List<OperationRecords> OppR = new List<OperationRecords>();
			OppR = await _dbConnection.Table<OperationRecords>().Where(ids => ids.Id == id).ToListAsync();
			return OppR;

		}
        public async Task<List<int>> GetOperationRecordsIDL()//returns the list of IDs for the operations records id in full
        {
            await SetUpDB();
			List<OperationRecords> OppR = new List<OperationRecords>();
            OppR = await _dbConnection.Table<OperationRecords>().ToListAsync();
			List<int> OppRI = OppR.Select(Oppr => Oppr.Id).ToList();
			return OppRI;
        }


        public async Task<List<OperationRecords>> GetOperationRecordsOperationalTeamID(int id)//returns the operational team id from the table OperationRecords in a list specific items
		{
			await SetUpDB();
            List<OperationRecords> OppR = new List<OperationRecords>();
            OppR = await _dbConnection.Table<OperationRecords>().Where(OPOPT => OPOPT.OperationalTeamID == id).ToListAsync();
			return OppR;
		}
        public async Task<List<int>> GetOperationRecordsOperationalTeamIDL()//returns the operational team id from the table OperationRecords in a list as full
        {
            await SetUpDB();
            List<OperationRecords> OppRTI = new List<OperationRecords>();
            OppRTI = await _dbConnection.Table<OperationRecords>().ToListAsync();
            List<int> oppr = OppRTI.Select(OppR => OppR.OperationalTeamID).ToList();
			return oppr;
        }


        public async Task<List<OperationRecords>> GetOperationRecordsRequested_By(string RequestBy)//returns the operationalrecords requested_by data in a list specific data
		{
			await SetUpDB();
            List<OperationRecords> Oppr = new List<OperationRecords>();
            Oppr = await _dbConnection.Table<OperationRecords>().Where(RB => RB.Requested_By == RequestBy).ToListAsync();
			return Oppr;
		}
        public async Task<List<string>> GetOperationRecordsRequested_ByL()//returns the operationalrecords requested_by data in a list as a full list
        {
            await SetUpDB();
            List<OperationRecords> Oppr = new List<OperationRecords>();
            Oppr = await _dbConnection.Table<OperationRecords>().ToListAsync();
			List<string> oppr = Oppr.Select(OppRRB => OppRRB.Requested_By).ToList();
			return oppr;
        }

        public async Task<List<OperationRecords>> GetOperationRecordsRequest_Date(DateTime DateAndTimeRequest)//returns the operational records request_Date in a list specfic items
		{
			await SetUpDB();
            List<OperationRecords> OppR = new List<OperationRecords>();
            OppR = await _dbConnection.Table<OperationRecords>().Where(RDT => RDT.Requested_Date == DateAndTimeRequest).ToListAsync();
            return OppR;

        }
        public async Task<List<DateTime>> GetOperationRecordsRequest_Date()//returns the operational records request_Date in a full list 
        {
            await SetUpDB();
            List<OperationRecords> Oppr = new List<OperationRecords>();
            Oppr = await _dbConnection.Table<OperationRecords>().ToListAsync();
            List<DateTime> oppr = Oppr.Select(OppR => OppR.Requested_Date).ToList();
            return oppr;

        }


        public async Task<List<OperationRecords>> GetOperationRecordsRequest_Details(int id)//returns the operational records request_Details specific items
		{
			await SetUpDB();
            List<OperationRecords> OppT = new List<OperationRecords>();
            OppT = await _dbConnection.Table<OperationRecords>().Where(RQD => RQD.Requested_Detail == RQD.Requested_Detail[id].ToString()).ToListAsync();
            return OppT;

        }
        public async Task<List<string>> GetOperationRecordsRequest_DetailsL()//returns the operational records request_Details as a full list
        {
            await SetUpDB();
            List<OperationRecords> Oppr = new List<OperationRecords>();
            Oppr = await _dbConnection.Table<OperationRecords>().ToListAsync();
            List<string> oppr = Oppr.Select(OppR => OppR.Requested_Detail).ToList();
            return oppr;

        }

        public async Task<List<OperationRecords>> GetOperationRecordsStatus(string status)//returns the status as a list from the OperationRecords table
		{
			await SetUpDB();
            List<OperationRecords> OppR = new List<OperationRecords>();
            OppR = await _dbConnection.Table<OperationRecords>().Where(CB => CB.Status == status).ToListAsync();
            return OppR;

        }
        public async Task<List<string>> GetOperationRecordsStatusL()//returns the status as a list from the OperationRecords table
        {
			await SetUpDB();
			List<OperationRecords> Oppr = new List<OperationRecords>();
            Oppr = await _dbConnection.Table<OperationRecords>().ToListAsync();
            List<string> oppr = Oppr.Select(OppR => OppR.Status).ToList();
            return oppr;

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
	    public async Task<List<string>> OperationRecordsRquest()
		{
			/*List<int> ID;
			List<int> OppTID;
			List<string> reqestByName;

			List<string> requestRecordsName = new List<string>();
			ID = await GetOperationRecordsIDL();
			OppTID = await  GetOperationalTeamStatusesIDL();
			reqestByName = await GetOperationRecordsRequested_ByL();

			foreach(int id in ID)
			{
                if (id >= ID.Count)
                {
                    break;
                }
                requestRecordsName[id] = ID[id].ToString() + ", " + OppTID[id].ToString() + ", " + reqestByName[id];
                if (id >= ID.Count)
                {
                    break;
                }
            }
			return requestRecordsName;*/
			List<string> placeholder = new List<string>();
			placeholder.Add("placeholder");
			return placeholder;
		}
		#endregion
	}
}
