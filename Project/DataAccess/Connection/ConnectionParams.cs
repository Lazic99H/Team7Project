using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Connection
{
    [ExcludeFromCodeCoverage]
    public class ConnectionParams
    {
        //TODO: change connection string
        public static readonly string DATA_SOURCE = "//localhost:1521/xe";

        //TODO: change username and password
        public static readonly string USER_ID = "res";
        public static readonly string PASSWORD = "ftn";
    }
}
