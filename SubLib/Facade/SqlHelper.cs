using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace SubLib.Facade
{
    public class SqlHelper
    {
        #region Connect to Database
        public static string GetSqlConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        }
        #endregion



        #region Execute Non Query


        public static int ExecuteNonQuery(string sqlText, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(GetSqlConnectionString()))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = sqlText;
                    command.Parameters.AddRange(parameters);
                    return command.ExecuteNonQuery();
                }
            }
        }

        #endregion


        #region return the first cell

        public static object ExecuteScalar(string sqlText, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(GetSqlConnectionString()))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = sqlText;
                    command.Parameters.AddRange(parameters);
                    return command.ExecuteScalar();
                }
            }
        }

        public static T ExcuteScalar<T>(string sqlText, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(GetSqlConnectionString()))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = sqlText;
                    command.Parameters.AddRange(parameters);
                    return (T)command.ExecuteScalar();
                }
            }
        }

        #endregion

        #region Return a data table

        public static DataTable ExecuteDataTable(string sqlText, params SqlParameter[] parameter)
        {
            using (SqlConnection connection= new SqlConnection(GetSqlConnectionString()))
            {
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlText, GetSqlConnectionString()))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.SelectCommand.Parameters.AddRange(parameter);
                    dataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }



        #endregion

        #region return a data reader

        public static SqlDataReader ExecuteDataReader(string sqlText, params SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(GetSqlConnectionString());
            SqlCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = sqlText;
            command.Parameters.AddRange(parameters);
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        #endregion

        #region Change

        public static int ChangeStuInfo(string sqlText, List<SqlParameter> sqlParameters)
        {
            using (SqlConnection connection = new SqlConnection(GetSqlConnectionString()))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = sqlText;
                    SqlParameter[] stuInfo = new SqlParameter[]{sqlParameters[0],sqlParameters[1],sqlParameters[2],sqlParameters[3],sqlParameters[4]};

                    foreach (SqlParameter itemsParameter in stuInfo)
                    {
                        //itemsParameter = Convert;
                        command.Parameters.Add(itemsParameter);
                        
                    }
                    //command.Parameters.AddRange(sqlParameters[0]);

                    return command.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Load Data

        public static List<StuInfo> LoadData(string sqlText, params SqlParameter[] parameters)
        {
            string cnnStr = GetSqlConnectionString();
            //DataTable data = new DataTable();
            //adapter.Fill(data);
            List<StuInfo> StuInformationList = new List<StuInfo>();
            DataTable dataTable = ExecuteDataTable(sqlText, parameters);
            foreach (DataRow itmeRow in dataTable.Rows)
            {
                StuInfo stuInfo = new StuInfo();
                stuInfo.stuID = int.Parse(itmeRow["stuID"].ToString().Trim());
                stuInfo.stuName = itmeRow["stuName"].ToString().Trim();
                stuInfo.stuGender = itmeRow["stuGender"].ToString().Trim();
                stuInfo.stuBirthDate = DateTime.Parse(itmeRow["stuBirthDate"].ToString().Trim());
                stuInfo.stuPhoneNumber = itmeRow["stuPhoneNumber"].ToString().Trim();
                stuInfo.DelFlag = int.Parse(itmeRow["DelFlag"].ToString().Trim());

                StuInformationList.Add(stuInfo);


            }

            return StuInformationList;
        }

        #endregion

    }
}
