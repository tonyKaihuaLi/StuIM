using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SubLib.Facade;

namespace SubLib.Bll
{
    public class ExecuteBehavior
    {
        #region Load Information
        public static List<StuInfo> LoadStuInformation()
        {
            List<StuInfo> StuInformationList = new List<StuInfo>();
            string cnnStr = SqlHelper.GetSqlConnectionString();
            string sql =
                "select [stuID], [stuName], [stuGender], [stuBirthDate], [stuPhoneNumber], [DelFlag] from[dbo].[StuInfo] where [Delflag]=0";

            StuInformationList = SqlHelper.LoadData(sql);
            return StuInformationList;
        }
        #endregion

        #region Delete

        public static int Delete(int deleteStuID)
        {
            string sqlCommend = "update stuInfo set DelFlag = 1 where stuID=@stuID";
            int deleteResult = int.Parse(SqlHelper.ExecuteNonQuery(sqlCommend, new SqlParameter("StuID", (object) deleteStuID)).ToString());
            return deleteResult;
        }



        #endregion

        #region Show Selected Information

        public static List<string> ShowSelectedInformation(int selectedRow)
        {
            List<string> stuList = new List<string>();
            string sqlCommend =
                "select [stuID], [stuName], [stuGender], [stuBirthDate], [stuPhoneNumber], [DelFlag] from[dbo].[StuInfo] where stuID=@stuID";
            SqlDataReader sqlDataReader =
                SqlHelper.ExecuteDataReader(sqlCommend, new SqlParameter("StuID", (object) selectedRow));
            if (sqlDataReader.Read())
            {
                string studentNameText = sqlDataReader["stuName"].ToString();
                string studentGenderText= sqlDataReader["stuGender"].ToString();
                string studentPhoneNumberText = sqlDataReader["stuPhoneNumber"].ToString();
                string studenBirthDateText = sqlDataReader["stuBirthDate"].ToString();

                stuList.Add(studentNameText);
                stuList.Add(studentGenderText);
                stuList.Add(studentPhoneNumberText);
                stuList.Add(studenBirthDateText);
            }

            return stuList;
        }


        #endregion

        #region Change Student Information

        public static void ChangeStuInfo(List<string> changStuInfo)
        {
            string sqlCommend =
                "update stuInfo set stuName=@stuName, stuGender=@stuGender, stuBirthDate=@stuBirthDate, stuPhoneNumber=@stuPhoneNumber where stuID=@stuID";
            SqlParameter stuIdParameter =new SqlParameter("@stuID",changStuInfo[0]);
            SqlParameter stuNameParameter = new SqlParameter("@stuName",changStuInfo[1]);
            SqlParameter stuGenderParameter = new SqlParameter("@stuGender",changStuInfo[2]);
            SqlParameter stuPhoneNumberParameter = new SqlParameter("@stuPhoneNumber",changStuInfo[3]);
            SqlParameter stuBirthDateParameter = new SqlParameter("@stuBirthDate",changStuInfo[4]);

            List<SqlParameter> StuInfo = new List<SqlParameter>();
            StuInfo.Add(stuIdParameter);
            StuInfo.Add(stuNameParameter);
            StuInfo.Add(stuGenderParameter);
            StuInfo.Add(stuPhoneNumberParameter);
            StuInfo.Add(stuBirthDateParameter);

            SqlHelper.ChangeStuInfo(sqlCommend, StuInfo);

        }

        #endregion


    }
}
