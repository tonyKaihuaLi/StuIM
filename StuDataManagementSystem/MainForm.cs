using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using SubLib;
using SubLib.Facade;
using SubLib.Bll;


namespace StuDataManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void mainFormDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadStuInfo();
        }

        private void LoadStuInfo()
        {

            this.mainFormDataGridView.DataSource = ExecuteBehavior.LoadStuInformation();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.mainFormDataGridView.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please make a selection");
                return;
            }

            if (MessageBox.Show("Delete Confrim", "Attention!", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            int deleteID = int.Parse(this.mainFormDataGridView.SelectedRows[0].Cells["stuID"].Value.ToString());

            int deleteResult = ExecuteBehavior.Delete(deleteID);
            if (deleteResult > 0)
            {
                MessageBox.Show("Succeed!");
            }
            LoadStuInfo();

        }

        private void mainFormDataGridView_Click(object sender, EventArgs e)
        {
            int selectRowID = int.Parse(this.mainFormDataGridView.SelectedRows[0].Cells[0].Value.ToString());
            List<string> stuList = ExecuteBehavior.ShowSelectedInformation(selectRowID);

            this.studentNameTextBox.Text = stuList[0];
            this.studentGenderTextBox.Text = stuList[1];
            this.studentPhoneNumberTextBox.Text = stuList[2];
            this.studenBirthDateTextBox.Text = stuList[3];

        }

        private void studentChangeButton_Click(object sender, EventArgs e)
        {
            List<string> stuInfoList = new List<string>();
            string selectRowID = this.mainFormDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            stuInfoList.Add(selectRowID);
            stuInfoList.Add(this.studentNameTextBox.Text);
            stuInfoList.Add(this.studentGenderTextBox.Text);
            stuInfoList.Add(this.studentPhoneNumberTextBox.Text);
            stuInfoList.Add(this.studenBirthDateTextBox.Text);

            ExecuteBehavior.ChangeStuInfo(stuInfoList);
            MessageBox.Show("Succeed!");
            LoadStuInfo();
        }

        private void mainFormDataGridView_DoubleClick(object sender, EventArgs e)
        {

            if (this.mainFormDataGridView.SelectedRows.Count <= 0)
            {
                return;
            }
            int selectedRowID = int.Parse(this.mainFormDataGridView.SelectedRows[0].Cells[0].Value.ToString());

            StuInfoEditorForm stuInfoEditor = new StuInfoEditorForm(new StuInfo(){stuID = selectedRowID});

            stuInfoEditor.FormClosing += stuInfoEditor_FormClosing;

            stuInfoEditor.Show();

        }

        private void stuInfoEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoadStuInfo();
        }

        private void mainFormSearchButton_Click(object sender, EventArgs e)
        {

            string connectionString = SqlHelper.GetSqlConnectionString();
            string sqlText = "select [stuID], [stuName], [stuGender], [stuBirthDate], [stuPhoneNumber], [DelFlag] from[dbo].[StuInfo]";

            List<StuInfo> StuInformationList = new List<StuInfo>();
            List<string> searchList =new List<string>();
            List<SqlParameter> parameters=new List<SqlParameter>();

            if (!string.IsNullOrEmpty(this.studentNameTextBox.Text.Trim()))
            {
                searchList.Add("stuName like @stuName");
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@stuName";
                parameter.Value = "%" + studentNameTextBox.Text + "%";
                parameters.Add(parameter);
            }



            if (!string.IsNullOrEmpty(this.studentPhoneNumberTextBox.Text.Trim()))
            {
                searchList.Add("stuPhoneNumber like @stuPhoneNumber");
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@stuPhoneNumber";
                parameter.Value = "%" + studentPhoneNumberTextBox.Text + "%";
                parameters.Add(parameter);
            }

            if (searchList.Count > 0)
            {
                sqlText += " where "+string.Join(" and ", searchList);
            }

            StuInformationList = SqlHelper.LoadData(sqlText, parameters.ToArray());

            this.mainFormDataGridView.DataSource = StuInformationList;

        }
    }
}
