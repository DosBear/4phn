using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4phn
{
    class Const
    {
        public struct App
        {
            public readonly static string ABOUT_STR = String.Format("4phn v.{0}", Properties.Settings.Default.Version);
            public readonly static string PATH = System.AppDomain.CurrentDomain.BaseDirectory;
        }

        public struct Phone
        {
            public readonly static string DND = "Custom:DND{0}";
            public readonly static string DND_STATE_BUSY = "BUSY";
            public readonly static string UNKNOWN = "<unknown>";
        }

        public struct SQL
        {
            public readonly static string DATABASE = "asteriskcdr";
            public readonly static string CONNECTION_STRING = "Database={0};Data Source={1};User Id={2};Password={3}; Charset=utf8;";
            public readonly static string SELECT_HISTORY = "SELECT calldate, clid, src, dst, duration, disposition, src as srcimage FROM cdr WHERE (src= '{0}' or dst= '{0}' or realdst= '{0}') and dst <> '*76' ORDER BY id DESC LIMIT 100;";
            public readonly static string SELECT_PHONEBOOK = "SELECT CAST(number as CHAR(50)) as number, name FROM companies";
            public readonly static string INSERT_PHONEBOOK = "INSERT INTO companies(name, number) values ('{0}', {1})";
            public readonly static string UPDATE_PHONEBOOK = "UPDATE companies SET name = '{0}' WHERE number = {1}";
        }
    }
}
