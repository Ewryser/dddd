using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PR1_SEM3_LOGIC;
using SEM3_PR1_MODEL;

namespace PR1_SEM3_WinForm
{
    public partial class EditRecordForm : Form
    {
        private readonly PRLibraryLogic logic;
        private readonly int originalId;
        public EditRecordForm(Record record, PRLibraryLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            this.originalId = record.Id;

            txtId.Text = record.Id.ToString();
            txtName.Text = record.Name;
            txtMusician.Text = record.Musician;
            txtYear.Text = record.Year;
            txtJanre.Text = record.Janre;
            txtLaybel.Text = record.Laybel;
            txtNativeRegion.Text = record.NativeRegion;
        }

        private void txtLatinName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Некорректный Id!"); txtId.Focus(); return;
            }

            if (logic.ExistsId(id, originalId))
            {
                MessageBox.Show("Такой Id уже существует! Введите другой Id."); txtId.Focus(); txtId.SelectAll(); return;
            }

            // Если id меняется, мы удалим старую запись с первоначальным id и добавляем новую с новым id.
            bool success = logic.UpdateRecord(originalId,
                                             txtName.Text.Trim(),
                                             txtMusician.Text.Trim(),
                                             txtYear.Text.Trim(),
                                             txtJanre.Text.Trim(),
                                             txtLaybel.Text.Trim(),
                                             txtNativeRegion.Text.Trim());

            if (!success)
            {
                MessageBox.Show("Ошибка при обновлении (оригинальная пластинка не найдена).");
                return;
            }

            if (originalId != id)
            {
                // копируем данные, удаляем старые, добавляем новые с новым id.
                var name = txtName.Text.Trim();
                var musician = txtMusician.Text.Trim();
                var year = txtYear.Text.Trim();
                var janre = txtJanre.Text.Trim();
                var symptoms = txtLaybel.Text.Trim();
                var region = txtNativeRegion.Text.Trim();

                logic.DeleteRecord(originalId);
                logic.AddRecord(id, name, musician, year, janre, symptoms, region);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
