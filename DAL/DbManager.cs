using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using RedCrossItCheckingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RedCrossItCheckingSystem.DAL
{
    public class DbManager
    {

        public DataContainer GetData(DataContainer container)
        {
            SqlConnection con = new SqlConnection("data source = (local); initial catalog =RedCrossItCheckingSystemdb; integrated security = sspi");
            con.Open();
            SqlCommand cmd = new SqlCommand("GetData", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@SerialNumber", System.Data.SqlDbType.VarChar).Value = container.SerialNumber;
            SqlDataReader reader = cmd.ExecuteReader();

          //  DataContainer container = new DataContainer();

            while (reader.Read())
            {
                container.SerialNumber = (string)reader["serialNumber"];
                container.CaseID = (int)reader["caseID"];
            }
            Debug.Write(container.CaseID);
            Debug.Write(container.SerialNumber);
            return container;
        }
    }
}
