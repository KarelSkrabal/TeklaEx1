using System;
using System.Windows.Forms;

namespace Ex1
{
    public partial class MaterialSelectionForm : Form
    {
        public ITeklaModelController facade;
        public MaterialSelectionForm(ITeklaModelController controller)
        {
            InitializeComponent();
            facade = controller;
        }

        /// <summary>
        /// Gets the list of available materials
        /// </summary>
        private void GetMaterialItems() => facade.GetMaterialItems();

        private void MaterialSelectionForm_Load(object sender, EventArgs e)
        {
            GetMaterialItems();
            ShowMaterialList();
        }

        private void ShowMaterialList() => dataGrid1.DataSource = new BindingSource(facade.MaterialList, null);

        private void btnClose_Click(object sender, EventArgs e) => Close();
    }
}
