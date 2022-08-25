using System.Data.SqlClient;

namespace MyContacts.Classes
{
    public class DatabaseService
    {
        public SqlConnectionStringBuilder GetConnectionString()
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = "SQNOT13825\\SQLEXPRESS",
                InitialCatalog = "MyContacts",
                IntegratedSecurity = true,
            };           
        }
    }
}
