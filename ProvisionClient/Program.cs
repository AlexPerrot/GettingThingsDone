using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace ProvisionClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creates a connection to the localgtd database
            SqlConnection clientConn = new SqlConnection(@"Data Source='.\SQLEXPRESS';Initial Catalog=localgtd;Integrated Security=True");

            // Creates a connection to the gtd server database
            SqlConnection serverConn = new SqlConnection(@"Data Source='.\SQLEXPRESS'; Initial Catalog=gtd; Integrated Security=True");

            // Gets the description of UserScope from the gtd server database
            DbSyncScopeDescription scopeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope("UserScope", serverConn);

            // Creates CE provisioning object based on the UserScope
            SqlSyncScopeProvisioning clientProvision = new SqlSyncScopeProvisioning(clientConn, scopeDesc);

            // Starts the provisioning process
            clientProvision.Apply();
        }
    }
}
