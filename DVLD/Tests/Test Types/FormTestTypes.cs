using BusinessLayer;
using DVLD.Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class FormTestTypes : Form
    {
        public FormTestTypes()
        {
            InitializeComponent();
        }

        private void FormApplicationTestTypes_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgvTestType.DataSource = clsTestTypes.GetAllTestType();
            lblRecordsNum.Text = "# Records " + dgvTestType.RowCount;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditTestType formEditTest = new FormEditTestType((int)dgvTestType.CurrentRow.Cells[0].Value);
            formEditTest.ShowDialog();
            LoadData();
        }
    }
}
