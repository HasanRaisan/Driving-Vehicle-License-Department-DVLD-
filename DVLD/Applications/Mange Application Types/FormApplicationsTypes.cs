using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BusinessLayer;
namespace DVLD.Applications
{
    public partial class FormApplicationsTypes : Form
    {
        public FormApplicationsTypes()
        {
            InitializeComponent();

        }

        void LoadDataToDataGridView()
        {
            dgvApplicationType.DataSource = clsApplicationTypes.GetAllApplictionTypes();
            lblRecordsNum.Text = "# Records: " + (dgvApplicationType.RowCount).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormApplicationsTypes_Load(object sender, EventArgs e)
        {
            LoadDataToDataGridView();
        }

        private void perToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditApplicationsType formEditApplication = new FormEditApplicationsType((int)dgvApplicationType.CurrentRow.Cells[0].Value);
            formEditApplication.ShowDialog();
            LoadDataToDataGridView();
        }
    }
}
