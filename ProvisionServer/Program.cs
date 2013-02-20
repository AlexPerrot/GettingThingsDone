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
            // Defines the sync scope name
            const string scopeName = "UserScope";

            // Creates a connexion to the server database.
            string SqlConnString = @"Data Source='.\SQLEXPRESS';Initial Catalog=gtd;Integrated Security=True";
            SqlConnection serverConn = new SqlConnection(SqlConnString);

            // Remove the previous scope from the Sql Server database (no support for updating a scope).
            SqlSyncScopeDeprovisioning sqlDepro = new SqlSyncScopeDeprovisioning(serverConn);
            sqlDepro.DeprovisionScope(scopeName);

            // Defines a new synchronization scope named UserScope
            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription(scopeName);

            // Gets the description of the Users table from the gtd database
            DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Users", serverConn);

            // Adds the table description to the sync scope definition
            scopeDesc.Tables.Add(tableDesc);

            // Gets the description of the other tables from the gtd base and add them to the sync scope.
            DbSyncTableDescriptionCollection otherTables = new DbSyncTableDescriptionCollection();
            otherTables.Add(SqlSyncDescriptionBuilder.GetDescriptionForTable("Tasks", serverConn));
            otherTables.Add(SqlSyncDescriptionBuilder.GetDescriptionForTable("Lists", serverConn));
            otherTables.Add(SqlSyncDescriptionBuilder.GetDescriptionForTable("Projects", serverConn));
            otherTables.Add(SqlSyncDescriptionBuilder.GetDescriptionForTable("Tasks_Lists", serverConn));
            otherTables.Add(SqlSyncDescriptionBuilder.GetDescriptionForTable("Tasks_Tasks", serverConn));
            otherTables.Add(SqlSyncDescriptionBuilder.GetDescriptionForTable("Lists_Tasks", serverConn));
            otherTables.Add(SqlSyncDescriptionBuilder.GetDescriptionForTable("Projects_Tasks", serverConn));
            otherTables.Add(SqlSyncDescriptionBuilder.GetDescriptionForTable("Projects_Lists", serverConn));
            foreach (var table in otherTables)
            {
                scopeDesc.Tables.Add(table);
            }

            // Creates a server scope provisioning object based on the ProductScope
            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);

            // Skipping the creation of table since table already exists on server
            serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);

            // Starts the provisioning process
            serverProvision.Apply();
        }
    }
}
