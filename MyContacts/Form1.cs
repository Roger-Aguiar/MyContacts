using MyContacts.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyContacts
{
    public partial class FormContacts : Form
    {
        private DatabaseService databaseService = new DatabaseService();
        private SqlConnectionStringBuilder stringConnection = new SqlConnectionStringBuilder();
        private StringBuilder stringBuilder = new StringBuilder();
        SqlConnection connection;
        private string sql;

        public FormContacts()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            textBoxName.Clear();
            textBoxNickName.Clear();
            textBoxEmail.Clear();
            maskedTextBoxPhoneNumber.Clear();

            textBoxName.Focus();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            int rowsAffected;
            using (connection)
            {
                connection.Open();
                connection.Close();
                
                stringBuilder.Clear();
                stringBuilder.Append("INSERT INTO Contacts(FullName, Nickname, PhoneNumber, Email)");
                stringBuilder.Append("VALUES (@FullName, @Nickname, @phoneNumber, @email);");
                sql = stringBuilder.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FullName", textBoxName.Text);
                    command.Parameters.AddWithValue("@Nickname", textBoxNickName.Text);
                    command.Parameters.AddWithValue("@phoneNumber", maskedTextBoxPhoneNumber.Text);
                    command.Parameters.AddWithValue("@email", textBoxEmail.Text);
                    rowsAffected = command.ExecuteNonQuery();                    
                }
            }
            MessageBox.Show(rowsAffected + " row(s) inserted", "Added", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Operation has been completed!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Operation has been completed!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormContacts_Load(object sender, EventArgs e)
        {
            stringConnection = databaseService.GetConnectionString();
            connection = new SqlConnection(stringConnection.ConnectionString);
        }
    }
}
