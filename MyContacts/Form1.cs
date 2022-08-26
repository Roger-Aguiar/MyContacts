using MyContacts.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace MyContacts
{
    public partial class FormContacts : Form
    {
        private readonly DatabaseService databaseService = new DatabaseService();        
        private readonly StringBuilder stringBuilder = new StringBuilder();
        private SqlConnectionStringBuilder stringConnection = new SqlConnectionStringBuilder();
        private string sql;

        public FormContacts()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            textBoxName.Clear();
            textBoxNickName.Clear();
            textBoxEmail.Clear();
            maskedTextBoxPhoneNumber.Clear();

            textBoxName.Focus();

            EnableControls();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            int rowsAffected;
            using (SqlConnection connection = new SqlConnection(stringConnection.ConnectionString))
            {
                connection.Open();
                
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
            Read();
            LinkDataGridViewToFields();
            DisableControls();            
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            int rowsAffected;
            int idContact = Convert.ToInt32(dataGridViewRows.Rows[dataGridViewRows.CurrentRow.Index].Cells[0].Value.ToString());
            using (SqlConnection connection = new SqlConnection(stringConnection.ConnectionString))
            {
                connection.Open();

                stringBuilder.Clear();
                stringBuilder.Append("UPDATE Contacts SET FullName = @name, Nickname = @nickname, PhoneNumber = @phoneNumber, Email = @email WHERE IdContact = @idContact");
                sql = stringBuilder.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", textBoxName.Text);
                    command.Parameters.AddWithValue("@idContact", idContact);
                    command.Parameters.AddWithValue("@nickname", textBoxNickName.Text);
                    command.Parameters.AddWithValue("@phoneNumber", maskedTextBoxPhoneNumber.Text);
                    command.Parameters.AddWithValue("@email", textBoxEmail.Text);
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            MessageBox.Show(rowsAffected + " row(s) updated!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            Read();
            LinkDataGridViewToFields();            
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int rowsAffected;
            int idContact = Convert.ToInt32(dataGridViewRows.Rows[dataGridViewRows.CurrentRow.Index].Cells[0].Value.ToString());
            using (SqlConnection connection = new SqlConnection(stringConnection.ConnectionString))
            {
                connection.Open();

                stringBuilder.Clear();
                stringBuilder.Append("DELETE Contacts WHERE IdContact = @idContact");
                sql = stringBuilder.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idContact", idContact);
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            MessageBox.Show(rowsAffected + " row(s) deleted!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Read();
            LinkDataGridViewToFields();
        }

        private void FormContacts_Load(object sender, EventArgs e)
        {
            stringConnection = databaseService.GetConnectionString();
            Read();
            LinkDataGridViewToFields();
        }

        private void dataGridViewRows_SelectionChanged(object sender, EventArgs e)
        {
            LinkDataGridViewToFields();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Read();
            LinkDataGridViewToFields();
            DisableControls();
        }

        #region Methods

        private void LinkDataGridViewToFields()
        {
            if (dataGridViewRows.Rows.Count > 0)
            {
                textBoxName.Text = dataGridViewRows.Rows[dataGridViewRows.CurrentRow.Index].Cells[1].Value.ToString();
                textBoxNickName.Text = dataGridViewRows.Rows[dataGridViewRows.CurrentRow.Index].Cells[2].Value.ToString();
                maskedTextBoxPhoneNumber.Text = dataGridViewRows.Rows[dataGridViewRows.CurrentRow.Index].Cells[3].Value.ToString();
                textBoxEmail.Text = dataGridViewRows.Rows[dataGridViewRows.CurrentRow.Index].Cells[4].Value.ToString();                
            }
        }

        private void Read()
        {
            using (SqlConnection connection = new SqlConnection(stringConnection.ConnectionString))
            {
                connection.Open();

                stringBuilder.Clear();
                stringBuilder.Append("SELECT IdContact AS 'Id', FullName AS 'Name' , Nickname, PhoneNumber, Email FROM Contacts ORDER BY FullName;");
                sql = stringBuilder.ToString();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Contacts");
                dataGridViewRows.DataSource = dataSet.Tables["Contacts"].DefaultView;
            }
        }

        private void DisableControls()
        {
            buttonSave.Enabled = false;
            buttonCancel.Enabled = false;
            buttonDelete.Enabled = true;
            buttonUpdate.Enabled = true;
            buttonAdd.Enabled = true;
        }

        private void EnableControls()
        {
            buttonSave.Enabled = true;
            buttonCancel.Enabled = true;
            buttonDelete.Enabled = false;
            buttonUpdate.Enabled = false;
            buttonAdd.Enabled = false;
        }
        #endregion


    }
}
