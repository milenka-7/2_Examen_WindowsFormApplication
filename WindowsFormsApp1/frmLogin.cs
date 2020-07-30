using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private NpgsqlConnection conn;
        string connstring = String.Format("Server={0}; Port={1};" +
            "User Id={2}; Password={3}; Database={4}",
            "localhost", "5432", "postgress", "17052016", "Examen_328");
        private DataTable dt;
        private NpgsqlCommand cmd;
        private string sql = null;

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // this.Hide();
            // new frmMain(textUsername.Text).Show();
            try
            {
                conn.Open();
                sql = @"select * from u_login(:_username,:_password)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_username", textUsername.Text);
                cmd.Parameters.AddWithValue("_password", textPassword.Text);

                int result = (int)cmd.ExecuteScalar();
                
                conn.Close();
                if (result == 1)
                {
                    this.Hide();
                    new frmMain(textUsername.Text).Show();
                }
                else
                {
                    MessageBox.Show("Error en el usuario o contraseña");
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Ha ocurrido un error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
        }
    }
}
