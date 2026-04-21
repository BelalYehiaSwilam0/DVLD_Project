using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DataAccessLayer
{
    public class clsDatabaseAccessSettings
    {
        public static string  ConnectionString = ConfigurationManager.ConnectionStrings["DVLDConnectionString"].ConnectionString;
       
    }
}
