using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Catalogs;
using Tekla.Structures.Dialog;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using Point = Tekla.Structures.Geometry3d.Point;
using System.Text;
using System.ComponentModel;
using System.Linq;
using Tekla.Structures.Drawing;
using TSM = Tekla.Structures.Model;

namespace Ex1
{
    public partial class MainForm : ApplicationFormBase
    {
        private Fasade _fasade;
        private BackgroundWorker _bwCreatePadFootings;
        private BackgroundWorker _bwCreateRebars;
        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            base.InitializeForm();
            InitializeComponent();
            _fasade = new Fasade(new TeklaModelController());
        }

        /// <summary>
        /// Initialize fields
        /// </summary>
        private void Initialize()
        {
            _bwCreatePadFootings = new BackgroundWorker();
            _bwCreatePadFootings.DoWork += _bwCreatePadFootings_DoWork;
            _bwCreatePadFootings.ProgressChanged += _bwCreatePadFootings_ProgressChanged;
            _bwCreatePadFootings.RunWorkerCompleted += _bwCreatePadFootings_RunWorkerCompleted;
            _bwCreatePadFootings.WorkerReportsProgress = true;
            _bwCreateRebars = new BackgroundWorker();
            _bwCreateRebars.DoWork += _bwCreateRebars_DoWork;
            _bwCreateRebars.ProgressChanged += _bwCreateRebars_ProgressChanged;
            _bwCreateRebars.RunWorkerCompleted += _bwCreateRebars_RunWorkerCompleted;
            _bwCreateRebars.WorkerReportsProgress = true;
        }

        #region FormEvents
        private void MainForm_Load(object sender, EventArgs e)
        {
            Initialize();
            btnCreateRebars.Enabled = false;
            ShowDrawings();
        }

        private void btnCreateFootings_Click(object sender, EventArgs e)
        {
            SetDefaultTextToResultLabel();
            try
            {
                _fasade.CreateColumns();
                btnCreateRebars.Enabled = true;
            }
            catch (Exception) { }

        }

        private void txtPadFootingSize_Leave(object sender, EventArgs e)
        {
            _fasade.PadFootingSize = (sender as TextBox)?.Text;
        }

        private void btnCreateRebars_Click(object sender, EventArgs e)
        {
            try
            {
                _fasade.CreateRebars();
            }
            catch (Exception) { }
        }

        private void profileCatalog1_SelectClicked(object sender, EventArgs e)
        {
            profileCatalog1.SelectedProfile = ColumnsProfileTextBox.Text;
        }

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
            if (selectedItem.ToString().Equals("Select ..."))
                _fasade.Material = selectedItem;
        }

        private void btnInsertDrawing_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDrawingText.Text))
                    _fasade.CreateDrawing(txtDrawingText.Text);
            }
            catch (Exception ex) { }
        }

        private void lswDrawings_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedList = (sender as ListView).SelectedItems;
            if (selectedList.Count != 0)
                _fasade.SelectedDrawingToActivate = /*(Drawing)*/selectedList[0].Tag;
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

        private void _bwCreateRebars_ProgressChanged(object sender, ProgressChangedEventArgs e) => lbResult.Text = "Progress : " + e.ProgressPercentage.ToString() + "% completed";

        /// <summary>
        /// DoWork event for btnCreateRebars Click event
        /// </summary>
        private void _bwCreateRebars_DoWork(object sender, DoWorkEventArgs e)
        {
            //BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            //double progressStep = 0;
            //int counter = 0;

            ////must be called
            //ModelObjectEnumerator padFotings = _model.GetModelObjectSelector().GetAllObjectsWithType(Tekla.Structures.Model.ModelObject.ModelObjectEnum.BEAM);

            ////calculate progress step for the progress reporting on the form
            //if (padFotings != null)
            //    progressStep = (double)1 / padFotings.GetSize();

            //foreach (Beam padFooting in padFotings)
            //{
            //    //must be called!!!!
            //    //CreateRebar(padFooting);
            //    counter++;
            //    backgroundWorker.ReportProgress((int)Math.Round(counter * progressStep * 100, MidpointRounding.AwayFromZero));
            //}
            ////e.Result = ret;
        }

        //private void GetPadFootingsSize() => _padFootingsSize = this.txtPadFootingSize.Text;

        private void _bwCreatePadFootings_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //lbResult.Text = "Created : " + e.Result.ToString() + " padfootings.";
            btnCreateFootings.Enabled = true;
            btnCreateRebars.Enabled = true;
        }

        private void _bwCreatePadFootings_ProgressChanged(object sender, ProgressChangedEventArgs e) => lbResult.Text = "Progress : " + e.ProgressPercentage.ToString() + "% completed";

        /// <summary>
        /// DoWork event for btnCreateFootings Click event
        /// </summary>
        private void _bwCreatePadFootings_DoWork(object sender, DoWorkEventArgs e)
        {
            //BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            //double progressStep = 0;
            //int counter = 0;

            ////calculate progress step for the progress reporting on the form
            //if (_createdModels.points != null && _createdModels.points.Count > 0)
            //    progressStep = (double)1 / _createdModels.points.Count;

            //foreach (var point in _createdModels.points)
            //{
            //    //TODO-change this, consider bool return value if it's usefull
            //    if (InsertModel(point))
            //    {
            //        CreateConnection(counter);
            //        counter++;
            //    }
            //    backgroundWorker.ReportProgress((int)Math.Round(counter * progressStep * 100, MidpointRounding.AwayFromZero));
            //}
            ////e.Result = ret;
        }

        /// <summary>
        /// Sets some default text to let user know that process's just started
        /// </summary>
        private void SetDefaultTextToResultLabel() => lbResult.Text = "Progress : start .. ";
        #endregion

        #region TeklaMethods

        /// <summary>
        /// Get & bound drawings to the ListView
        /// </summary>
        private void ShowDrawings()
        {
            lswDrawings.Items.AddRange(_fasade.GetDrowings());
        }

        #endregion
    }
}
