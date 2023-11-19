using UndacApp.Data;
using UndacApp.Models.Temp_Models;
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
using System.Security.Cryptography;

namespace UndacApp.Services.Temp_Services
{
    class Temp_Resource_Operation_Team_Operation_Resource_Request
    {
        private SQLiteAsyncConnection _dbConnection;

        #region database initilaisation
        async Task SetUpDBTempOperation_Resource_Request()
        {
            if (_dbConnection != null)
                return;

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Temp_Operatartion_Resourse_Request>();
        }

        async Task SetUpDBTempOperation_Team()
        {
            if (_dbConnection != null)
                return;

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Temp_Operation_Team>();
        }

        async Task SetUpDBTempResource()
        {
            if (_dbConnection != null)
                return;

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Temp_Resource>();
        }

        #endregion
        #region Temporary Data
        #region temporary data for Operation_resource_request
        private List<string> DataORR1 = new List<string>();
        private List<string> DataORR2 = new List<string>();
        private List<DateTime> DataORR3 = new List<DateTime>();
        private List<string> DataORR4 = new List<string>();
        #endregion
        #region Temporary data for Operation_Team
        private List<string> DataOT1 = new List<string>();
        private List<string> DataOT2 = new List<string>();
        private List<int> DataOT3 = new List<int>();
        private List<string> DataOT4 = new List<string>();
        #endregion
        #region Temporary Data for resource

        #endregion
        #endregion

        
        /*public void LoadDataOperation_resource_Request()
        {
            DataORR1.Add("james");
            DataORR1.Add("jane");
            DataORR1.Add("goeroge");
            DataORR1.Add("ranger");
            DataORR1.Add("broader");

            DataORR2.Add("we need this...  and this... and this...");
            DataORR2.Add("we need this...  and this... and this...");
            DataORR2.Add("we need this...  and this... and this...");
            DataORR2.Add("we need this...  and this... and this...");
            DataORR2.Add("we need this...  and this... and this...");

            DataORR3.Add(new DateTime(2002, 07, 01, 12, 00, 00));
            DataORR3.Add(new DateTime(2002, 08, 01, 12, 00, 00));
            DataORR3.Add(new DateTime(2002, 09, 01, 12, 00, 00));
            DataORR3.Add(new DateTime(2002, 10, 01, 12, 00, 00));
            DataORR3.Add(new DateTime(2002, 11, 01, 12, 00, 00));

            DataORR4.Add("progressing");
            DataORR4.Add("progressing");
            DataORR4.Add("working");
            DataORR4.Add("begining");
            DataORR4.Add("reviewing");
        }*/
        /*public void LoadDataOperation_Team()
        {
            DataOT1.Add("Green");
            DataOT1.Add("Blue");
            DataOT1.Add("Red");
            DataOT1.Add("Indigo");
            DataOT1.Add("Black");

            DataOT2.Add("jay");
            DataOT2.Add("brian");
            DataOT2.Add("bob");
            DataOT2.Add("brian");
            DataOT2.Add("david");

            DataOT3.Add(1);
            DataOT3.Add(2);
            DataOT3.Add(3);
            DataOT3.Add(4);
            DataOT3.Add(5);

            DataOT4.Add("progressing");
            DataOT4.Add("progressing");
            DataOT4.Add("working");
            DataOT4.Add("begining");
            DataOT4.Add("reviewing");
        }*/
        public (List<string>, List<string>,List<int>, List<int>, List<int>) loadDataResource()
        {
            List<string> DataR1 = new List<string>();
            List<string> DataR2 = new List<string>();
            List<int> DataR3 = new List<int>();
            List<int> DataR4 = new List<int>();
            List<int> DataR5 = new List<int>();

            DataR1.Add("Platinum");
            DataR1.Add("Gold");
            DataR1.Add("Bronze");
            DataR1.Add("David");
            DataR1.Add("klied");

            DataR2.Add("Matierial");
            DataR2.Add("Matierial");
            DataR2.Add("Matierial");
            DataR2.Add("Human");
            DataR2.Add("Human");

            DataR3.Add(20);
            DataR3.Add(100);
            DataR3.Add(1000);
            DataR3.Add(1);
            DataR3.Add(1);

            DataR4.Add(1000);
            DataR4.Add(1000);
            DataR4.Add(1000);
            DataR4.Add(0);
            DataR4.Add(0);

            for (int i = 0; i < 5; i++)
            {
                DataR5.Add(i);
            }
            


            return (DataR1, DataR2, DataR3, DataR4, DataR5);
        }  


        public async Task AddAllTempData()
        {
            foreach(Temp_Resource T in DataConvertionForResource())
            await AddDataForResource(T);
        }

        public List<Temp_Resource> DataConvertionForResource()
        {
            List<Temp_Resource> tempR = new List<Temp_Resource>();
            List<string> DataR1;
            List<string> DataR2;
            List<int> DataR3;
            List<int> DataR4;
            List<int> DataR5;


            var result = loadDataResource();
            DataR1 = result.Item1;
            DataR2 = result.Item2;
            DataR3 = result.Item3;
            DataR4 = result.Item4;
            DataR5 = result.Item5;
            int count1 = DataR1.Count;
            int count2 = DataR2.Count;
            int count3 = DataR3.Count;
            int count4 = DataR4.Count;
            int count5 = DataR5.Count;
            
            for (int s1 = 0; s1 < count1; s1++)
            { 
                for(int s2 =0; s2 < count2; s2++)
                {
                    for(int s3=0; s3 < count3; s3++)
                    {
                        for (int s4 = 0; s4 < count4; s4++)
                        {
                            for (int s5 = 0; s5 < count5; s5++)
                            {
                                Temp_Resource tempRs = new Temp_Resource
                                {
                                    Resourcename = (s1 < count1) ? DataR1[s1] : null,
                                    ResourceType = (s2 < count2) ? DataR2[s2] : null,
                                    CurrentStock = (s3 < count3) ? DataR3[s3] : 0,
                                    ReOrder_Level = (s4  < count4) ? DataR4[s4] : 0,
                                    Supplier_Id = (s5 < count5) ? DataR5[s5] : 0
                                };
                                tempR.Add(tempRs);
                            }
                            
                        }
                        
                    }
                    
                }

            }
            return tempR;
        }

        public async Task<int> AddDataForResource(Temp_Resource temp_Resource)
        {
            await SetUpDBTempResource();
            return await _dbConnection.InsertAsync(temp_Resource);
            
        }

        public async Task<List<string>> GetResourceName()
        {
            List<Temp_Resource> temps;
            List<string> resourceName = new List<string>();
            await SetUpDBTempResource();
            temps = await _dbConnection.Table<Temp_Resource>().ToListAsync();
            
            foreach(Temp_Resource T in temps)
            {
                resourceName.Add(T.Resourcename);
            }
            
            return resourceName;//issue
        }

    }
}
