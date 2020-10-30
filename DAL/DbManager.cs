using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
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

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                int counter = 0;
                
                //read the data and count for every row
                while (reader.Read())
                {
                    container.SerialNumber = (string)reader["serialNumber"];
                    container.CaseID = (int)reader["caseID"];
                    container.Accessories = (string)reader["accessories"];
                    container.Department = (string)reader["department"];
                    container.Description = (string)reader["description"];
                    container.DeviceName = (string)reader["deviceName"];
                    container.DeviceType = (string)reader["deviceType"];
                    container.EmplyeeName = (string)reader["employeeName"];
                    container.Status = (string)reader["status"];


                    DateTime startdate = reader.GetDateTime(reader.GetOrdinal("dateStart"));
                    DateTime enddate = reader.GetDateTime(reader.GetOrdinal("dateEnd"));
                    container.DateStart = startdate;
                    container.DateEnd = enddate;

                    counter++;

                }
               
                // check if reader has rows
                if (counter > 0)
                {
                    container.IsValid = true;
                }
              
            }
            catch (Exception)
            {
                //disable container
                container.IsValid = false;
            }


            con.Close();
            return container;
        }

        public void SetData(DataContainer container)
        {
            //SqlConnection con = new SqlConnection("data source = (local); initial catalog =RedCrossItCheckingSystemdb; integrated security = sspi");
            //con.Open();
            //SqlCommand cmd = new SqlCommand("GetData", con);
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.Add("@SerialNumber", System.Data.SqlDbType.VarChar).Value = container.SerialNumber;




            //cmd.ExecuteNonQuery();
        }
    }
}
