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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string connectionString = "Server=PC\\SQLEXPRESS;Database=assettracking;Integrated Security=True;";
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // call LoadData when button3 is clicked
            LoadData();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // call the class-level method
            LoadData();
            DataTable dt = new DataTable();

            // colums matching on db
            dt.Columns.Add("AssetId");
            dt.Columns.Add("AssetName");
            dt.Columns.Add("Assignedto");
            dt.Columns.Add("Condtion");
            dt.Columns.Add("Status");
            dt.Columns.Add("Location");
            dt.Columns.Add("Serialnumber");


            // Sample data (remove this if using database)
            dt.Rows.Add("E001"," Dell latitude" , "John Doe", "new", " Active"," Waterfalls","WR456 - 334");
            dt.Rows.Add("E002" , " Hp probook " ,"Jane Smith" , "good"  , " Active"  ,"Highfield" , " WR787 - 092");
            dt.Rows.Add("E003","lounge chairs","Mike Johnson","damaged","Inactive","Southlands","MY785-098");
            dt.Rows.Add("E004","Nissan Nivara","Emily Davis","new","Active","Belvedere","GT564-743");
            dt.Rows.Add("E005","Huawei p smart","David Lee","good\tActive\tRidgeveiw","tOP674-735");
            dt.Rows.Add("E006","Hp ProDesk","Sarah Brown","in repair","Inactive","Mbare","WR123-443");
            dt.Rows.Add("E007", "Nissan Tida","James Wilson","good","Active","Southerton","GT628-789");
            dt.Rows.Add("E008", "Executive desk","Laura Garcia","new","Active","Highlands","MY452-045");
            dt.Rows.Add("E009", "Toyota Passo","Tom Harris","damaged","Active","Avondale","OC789-012");
            dt.Rows.Add("E010", "Samsung S24","Alice Moore","damaged","inactive","Madokero","OP948-454");
            dt.Rows.Add("E011", "Toyota Vitz","Jack Dealer","new","Active","Belvedere","OP65-999");
            dt.Rows.Add("E012", "Dell insperation","Nawu","damaged","Active","Ivory park","WR289");
            dt.Rows.Add("E013", "Dell inperation","Noku","new","Active","Avondale","OYU-7888");

            dataGridView1.DataSource = dt;
        }
       

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Fix SQL: remove trailing dot and use fully qualified table name
                    string query = "SELECT * FROM [dbo].[assets];"; // From your table name

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"INSERT INTO [dbo].[assets]
                            (AssetID, AssetName, Assignedto, Condition, Status, Location, Serialnumber)
                            VALUES
                            (@Assetid, @Assetname, @Assignedto, @Condition, @Status, @Location, @Serialnumber)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Assetid", txtasset.Text);
                    cmd.Parameters.AddWithValue("@AssetName", txtassetname.Text);
                    cmd.Parameters.AddWithValue("@Assignedto", txtassigned.Text);
                    cmd.Parameters.AddWithValue("@Condition", txtcondition.Text);
                    cmd.Parameters.AddWithValue("@Status", txtstatus.Text);
                    cmd.Parameters.AddWithValue("@Location", txtlocation.Text);
                    cmd.Parameters.AddWithValue("@Serialnumber", txtserial.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("employee has been added");

                    LoadData(); // refresh DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtasset_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=PC\\SQLEXPRESS;Database=assettracking;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM assets WHERE AssetName LIKE @Search OR Assignedto LIKE @Search OR Location LIKE @Search";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@Search", "%" + txtsearch.Text + "%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void employeedelete_Click(object sender, EventArgs e)
        {
           
        
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this record?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo
                );

                if (result == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }
    }
    }

