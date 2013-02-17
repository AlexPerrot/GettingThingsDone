using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace ProvisionServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection serverConn = new SqlConnection(@"Data Source='.\SQLEXPRESS';Initial Catalog=gtd;Integrated Security=True");

            // Defines a new synchronization scope named UserScope
            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription("UserScope");

            // Gets the description of the Users table from the gtd database
            DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Users", serverConn);

            // Adds the table description to the sync scope definition
            scopeDesc.Tables.Add(tableDesc);

            // Creates a server scope provisioning object based on the ProductScope
            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);

            // Skipping the creation of table since table already exists on server
            serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);

            // Starts the provisioning process
            serverProvision.Apply();
        }
    }
}
