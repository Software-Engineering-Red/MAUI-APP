using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiApp1.Data
{
    public static class DatabaseSettings
    {
        public const string Filename = "TodoSQLite.db3";

        // These flags will open the database in read/write mode, create database if it doesn't exist, and enables multi-threaded access
        public const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        public static string DBPath => Path.Combine(FileSystem.AppDataDirectory, Filename);
    }
}
