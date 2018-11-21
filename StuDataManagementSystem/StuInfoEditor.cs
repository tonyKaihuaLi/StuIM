using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SubLib.Bll;
using SubLib.Facade;

namespace StuDataManagementSystem
{
    public partial class StuInfoEditorForm : Form
    {

        public StuInfo StuInfo { get; set; }

        public StuInfoEditorForm(StuInfo stuInfo)
        {
            InitializeComponent();

            StuInfo = stuInfo;
        }

        private void StuInfoEditorForm_Load(object sender, EventArgs e)
        {

            
            List<string> stuList = ExecuteBehavior.ShowSelectedInformation(StuInfo.stuID);

            this.studentNameTextBox.Text = stuList[0];
            this.studentGenderTextBox.Text = stuList[1];
            this.studentPhoneNumberTextBox.Text = stuList[2];
            this.studenBirthDateTextBox.Text = stuList[3];

        }

        private void studentChangeButton_Click(object sender, EventArgs e)
        {
            string selectRowID = StuInfo.stuID.ToString();
            List<string> stuInfoList = new List<string>();
            stuInfoList.Add(selectRowID);
            stuInfoList.Add(this.studentNameTextBox.Text);
            stuInfoList.Add(this.studentGenderTextBox.Text);
            stuInfoList.Add(this.studentPhoneNumberTextBox.Text);
            stuInfoList.Add(this.studenBirthDateTextBox.Text);

            ExecuteBehavior.ChangeStuInfo(stuInfoList);
            MessageBox.Show("Succeed!");

        }

        private void studentNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
