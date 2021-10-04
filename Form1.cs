using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OpeningDatabaseFilesWithOpenFileDialogControl
{
    public partial class frmTitles : Form
    {
        public frmTitles()
        {
            InitializeComponent();
        }

        SqlConnection booksConnection;
        SqlCommand titlesCommand;
        SqlDataAdapter titlesAdapter;
        DataTable titlesTable;

        private void frmTitles_Load (object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK) {
                try
                {
                    //connect to books database
                    booksConnection = new SqlConnection("Data Source=.\\SQLEXPRESS; AttachDbFilename=" + dlgOpen.FileName + "; Integrated Security=True; Connect Timeout=30; User Instance=True");
                    booksConnection.Open();

                    //establish command object
                    titlesCommand = new SqlCommand("SELECT * FROM Titles ORDER BY Title", booksConnection);

                    //establish data adapter/data table
                    titlesAdapter = new SqlDataAdapter();
                    titlesAdapter.SelectCommand = titlesCommand;
                    titlesTable = new DataTable();
                    titlesAdapter.Fill(titlesTable);

                    // bind grid to data table
                    grdTitles.DataSource = titlesTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error establishing Titles table.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No file selected", "Program stopping", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void frmTitles_FormClosing(object sender, FormClosingEventArgs e)
        {
            booksConnection.Dispose();
            titlesCommand.Dispose();
            titlesAdapter.Dispose();
            titlesTable.Dispose();
        }
    }
}
