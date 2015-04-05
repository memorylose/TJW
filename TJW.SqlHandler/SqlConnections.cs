/* ======================================================================== 
* Author：Cass 
* Time：8/8/2014 9:19:05 AM 
* Description:  Sql db connections
* ======================================================================== 
*/

using System.Data.SqlClient;

namespace TJW.SqlHandler
{
    public class SqlConnections
    {
        public static string RtnConecs()
        {
            string local = ".";
            string production = @"211.149.221.157\CASS01";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder["Server"] = local;
            builder["user id"] = "sa";
            builder["password"] = "admin123_";
            builder["database"] = "TJW_DB";
            return builder.ConnectionString;
        }
    }
}
