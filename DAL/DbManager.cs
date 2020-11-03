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
        string connectionstring = "data source = (local); initial catalog =RedCrossItCheckingSystemdb; integrated security = sspi";
        public List<DataContainer> GetDataFromserial(DataContainer container)
        {
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetData", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@SerialNumber", System.Data.SqlDbType.VarChar).Value = container.SerialNumber;

            List<DataContainer> containers = new List<DataContainer>();
            
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                int counter = 0;
                
                //read the data and count for every row
                while (reader.Read())
                {
                    DataContainer output = new DataContainer();

                    output.SerialNumber = (string)reader["serialNumber"];
                    output.CaseID = (int)reader["caseID"];
                    output.Accessories = (string)reader["accessories"];
                    output.Department = (string)reader["department"];
                    output.Description = (string)reader["description"];
                    output.DeviceName = (string)reader["deviceName"];
                    output.DeviceType = (string)reader["deviceType"];
                    output.EmplyeeName = (string)reader["employeeName"];
                    output.Status = (string)reader["status"];


                    DateTime startdate = reader.GetDateTime(reader.GetOrdinal("dateStart"));
                    DateTime enddate = reader.GetDateTime(reader.GetOrdinal("dateEnd"));
                    output.DateStart = startdate;
                    output.DateEnd = enddate;

                    counter++;
                    
                   
                    // check if reader has rows
                    if (counter > 0)
                    {
                        output.IsValid = true;
                        containers.Add(output);
                        
                    }
                }

                Debug.Write(containers.Count);
              
            }
            catch (Exception)
            {
                DataContainer output = new DataContainer();
                //disable container
                output.IsValid = false;
            }


            con.Close();
            return containers;
        }

        public DataContainer GetDataFromCaseID(int caseID)
        {
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetData_caseID", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@caseID", System.Data.SqlDbType.Int).Value = caseID;

            DataContainer container = new DataContainer();

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                

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
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand("SetData", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@caseID", System.Data.SqlDbType.VarChar).Value = container.CaseID;
            cmd.Parameters.Add("@SerialNumber", System.Data.SqlDbType.VarChar).Value = container.SerialNumber;
            cmd.Parameters.Add("@department", System.Data.SqlDbType.VarChar).Value = container.Department;
            cmd.Parameters.Add("@dateStart", System.Data.SqlDbType.Date).Value = container.DateStart;
            cmd.Parameters.Add("@dateEnd", System.Data.SqlDbType.Date).Value = container.DateEnd;
            cmd.Parameters.Add("@status", System.Data.SqlDbType.VarChar).Value = container.Status;
            cmd.Parameters.Add("@deviceName", System.Data.SqlDbType.VarChar).Value = container.DeviceName;
            cmd.Parameters.Add("@deviceType", System.Data.SqlDbType.VarChar).Value = container.DeviceType;
            cmd.Parameters.Add("@accessories", System.Data.SqlDbType.VarChar).Value = container.Accessories;
            cmd.Parameters.Add("@employeeName", System.Data.SqlDbType.VarChar).Value = container.EmplyeeName;
            cmd.Parameters.Add("@description", System.Data.SqlDbType.VarChar).Value = container.Description;

            cmd.ExecuteNonQuery();
        }

        public void CreateData(DataContainer container)
        {
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand("CreateData", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@SerialNumber", System.Data.SqlDbType.VarChar).Value = container.SerialNumber;
            cmd.Parameters.Add("@department", System.Data.SqlDbType.VarChar).Value = container.Department;
            cmd.Parameters.Add("@dateStart", System.Data.SqlDbType.Date).Value = container.DateStart;
            cmd.Parameters.Add("@dateEnd", System.Data.SqlDbType.Date).Value = container.DateEnd;
            cmd.Parameters.Add("@status", System.Data.SqlDbType.VarChar).Value = container.Status;
            cmd.Parameters.Add("@deviceName", System.Data.SqlDbType.VarChar).Value = container.DeviceName;
            cmd.Parameters.Add("@deviceType", System.Data.SqlDbType.VarChar).Value = container.DeviceType;
            cmd.Parameters.Add("@accessories", System.Data.SqlDbType.VarChar).Value = container.Accessories;
            cmd.Parameters.Add("@employeeName", System.Data.SqlDbType.VarChar).Value = container.EmplyeeName;
            cmd.Parameters.Add("@description", System.Data.SqlDbType.VarChar).Value = container.Description;

            cmd.ExecuteNonQuery();
        }

        public List<DataContainer> GetAllData()
        {
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetAllData", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
           
            List<DataContainer> containers = new List<DataContainer>();

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                int counter = 0;

                //read the data and count for every row
                while (reader.Read())
                {
                    DataContainer output = new DataContainer();

                    output.SerialNumber = (string)reader["serialNumber"];
                    output.CaseID = (int)reader["caseID"];
                    output.Accessories = (string)reader["accessories"];
                    output.Department = (string)reader["department"];
                    //output.Description = (string)reader["description"];
                    output.DeviceName = (string)reader["deviceName"];
                    output.DeviceType = (string)reader["deviceType"];
                    output.EmplyeeName = (string)reader["employeeName"];
                    output.Status = (string)reader["status"];


                    DateTime startdate = reader.GetDateTime(reader.GetOrdinal("dateStart"));
                    DateTime enddate = reader.GetDateTime(reader.GetOrdinal("dateEnd"));
                    output.DateStart = startdate;
                    output.DateEnd = enddate;
                    
                    counter++;


                    // check if reader has rows
                    if (counter > 0)
                    {
                        output.IsValid = true;
                        containers.Add(output);
                    }
                }

               // Debug.Write(containers.Count);

            }
            catch (Exception)
            {
                DataContainer output = new DataContainer();
                //disable container
                output.IsValid = false;
            }


            con.Close();
            return containers;
        }
    }
}
