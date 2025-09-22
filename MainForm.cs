using PR1_SEM3_LOGIC;
using SEM3_PR1_MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR1_SEM3_WinForm
{
    public partial class MainForm : Form
    {
        private readonly PRLibraryLogic logic = new PRLibraryLogic();
        public MainForm()
        {
            InitializeComponent();
            UpdateRecordList();
        }

        private void UpdateRecordList()
        {
            dgvRecords.DataSource = null;
            dgvRecords.DataSource = logic.GetAllRecords().ToList();
        }

        private void ClearInputs()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                    textBox.Clear();
            }
        }

        //Создание пластинки.
        public void CreateRecord()
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtMusician.Text) ||
                string.IsNullOrWhiteSpace(txtYear.Text) ||
                string.IsNullOrWhiteSpace(txtJanre.Text) ||
                string.IsNullOrWhiteSpace(txtLaybel.Text) ||
                string.IsNullOrWhiteSpace(txtNativeRegion.Text))
            {
                MessageBox.Show("Заполните все поля корректно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtId.Text.Trim());
            logic.AddRecord(id, txtName.Text.Trim(), txtMusician.Text.Trim(), txtYear.Text.Trim(),
                txtJanre.Text.Trim(), txtLaybel.Text.Trim(), txtNativeRegion.Text.Trim()); //Trim() удаляет пробельные символы.
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Некорректный Id!");
                txtId.Focus();
                return;
            }

            if (logic.ExistsId(id))
            {
                MessageBox.Show("Такой Id уже существует! Введите уникальный Id.");
                txtId.Focus();
                txtId.SelectAll();
                return;
            }

            CreateRecord();
            UpdateRecordList();
            ClearInputs();

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtNativeRegion_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            UpdateRecordList();
        }

        private void btnSearchUsing_Click(object sender, EventArgs e)
        {
            string year = txtSearchYear.Text.Trim();
            if (string.IsNullOrWhiteSpace(year))
            {
                MessageBox.Show("Введите год для поиска!");
                return;
            }

            var results = logic.FindByYear(year);

            if (results.Count == 0)
            {
                MessageBox.Show("Пластинки с таким годом выпуска не найдены.");
                return;
            }

            string message = "Найденные пластинки:\n\n";
            foreach (var record in results)
            {
                message += $"{record.Id}. {record.Name}\n" + "------------------------\n";
            }

            MessageBox.Show(message, "Результаты поиска");
        }

        private void btnSearchId_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearchId.Text, out int id))
            {
                var record = logic.GetRecordById(id);
                MessageBox.Show(record?.ToString() ?? "Пластинка не найдена", "Результат");
            }
            else
            {
                MessageBox.Show("Введите корректный Id!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            dgvRecords.DataSource = null;
            dgvRecords.Rows.Clear();
            dgvRecords.Columns.Clear();
            dgvRecords.Columns.Add("Info", "Информация");

            var groups = logic.GroupByJanre();
            foreach (var g in groups)
            {
                dgvRecords.Rows.Add($"Жанр: {g.Key}");
                foreach (var b in g.Value)
                    dgvRecords.Rows.Add($"   {b}");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRecords.CurrentRow?.DataBoundItem is Record record)
            {
                var res = MessageBox.Show($"Вы действительно хотите удалить пластинку '{record.Name}'?", "Подтвердите", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    logic.DeleteRecord(record.Id);
                    UpdateRecordList();
                }
            }
            else MessageBox.Show("Выберите пластинку для удаления!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvRecords.CurrentRow?.DataBoundItem is Record record)
            {
                using (var form = new EditRecordForm(record, logic))
                if (form.ShowDialog() == DialogResult.OK)
                {
                   UpdateRecordList();
                }
            }
            else MessageBox.Show("Выберите пластинку для редактирования!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
