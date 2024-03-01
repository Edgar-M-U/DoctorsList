using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsList.Helpers
{
    public static class Constants
    {
        private const string DBFIleName = "SQLite.db3";

        public const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        public static string DatabasePath { get { return Path.Combine(FileSystem.AppDataDirectory, DBFIleName); } }
    }
}
