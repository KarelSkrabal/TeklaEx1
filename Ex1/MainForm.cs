using System;
using System.Windows.Forms;
using Tekla.Structures.Catalogs;
using Tekla.Structures.Dialog;
using System.ComponentModel;

namespace Ex1
{
    public partial class MainForm : ApplicationFormBase
    {
        private Fasade _fasade;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            base.InitializeForm();
            InitializeComponent();
            _fasade = new Fasade(new TeklaModelController());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btnCreateRebars.Enabled = false;
            ShowDrawings();
        }

        private void btnCreateFootings_Click(object sender, EventArgs e)
        {
            try
            {
                _fasade.CreateColumns();
                btnCreateRebars.Enabled = true;
            }
            catch (Exception) { }

        }

        private void txtPadFootingSize_Leave(object sender, EventArgs e) => _fasade.PadFootingSize = (sender as TextBox)?.Text;

        private void btnCreateRebars_Click(object sender, EventArgs e)
        {
            try
            {
                _fasade.CreateRebars();
            }
            catch (Exception) { }
        }

        private void profileCatalog1_SelectClicked(object sender, EventArgs e) => profileCatalog1.SelectedProfile = ColumnsProfileTextBox.Text;

        private void profileCatalog1_SelectionDone(object sender, EventArgs e)
        {
            ColumnsProfileTextBox.Text = profileCatalog1.SelectedProfile;
            _fasade.ColumnProfile = profileCatalog1.SelectedProfile;
        }

        private void reinforcementCatalog1_SelectionDone(object sender, EventArgs e)
        {
            _fasade.RebarGroupSize = SizeTextBox.Text = reinforcementCatalog1.SelectedRebarSize;
            _fasade.RebarGroupGrade = GradeTextBox.Text = reinforcementCatalog1.SelectedRebarGrade;
            _fasade.RebarGroupRadius = BendingRadiusTextBox.Text = reinforcementCatalog1.SelectedRebarBendingRadius.ToString();
        }

        private void btnSelectMaterial_Click(object sender, EventArgs e)
        {
            var selectMaterial = new MaterialSelectionForm(_fasade);
            selectMaterial.ShowDialog();
            if (selectMaterial.DialogResult == DialogResult.Cancel)
            {
                cbMaterialList.DisplayMember = _fasade.MaterialDisplayMember;
                cbMaterialList.DataSource = new BindingSource(new BindingList<MaterialItem>(selectMaterial.facade.MaterialItems), null);
            }
        }

        private void cbMaterialList_SelectedValueChanged(object sender, EventArgs e)
        {
            var selectedItem = (sender as ComboBox).SelectedItem;
            if (!selectedItem.ToString().Equals("Select ..."))
                _fasade.Material = selectedItem;
        }

        private void btnInsertDrawing_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDrawingText.Text))
                    _fasade.CreateDrawing(txtDrawingText.Text);
            }
            catch (Exception) { }
        }

        private void lswDrawings_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedList = (sender as ListView).SelectedItems;
            if (selectedList.Count != 0)
                _fasade.SelectedDrawingToActivate = selectedList[0].Tag;
        }

        private void btnSetActiveDrawing_Click(object sender, EventArgs e)
        {
            try
            {
                _fasade.SetDrawingActive();
            }
            catch (Exception) { }
        }

        private void _bwCreateRebars_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnCreateFootings.Enabled = true;
            btnCreateRebars.Enabled = true;
        }

        /// <summary>
        /// Get & bound drawings to the ListView
        /// </summary>
        private void ShowDrawings() => lswDrawings.Items.AddRange(_fasade.GetDrowings());
    }
}
