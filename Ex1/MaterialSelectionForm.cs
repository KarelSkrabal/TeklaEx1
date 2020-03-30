using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Catalogs;

namespace Ex1
{
    public partial class MaterialSelectionForm : Form
    {
        public IList<MaterialItem> _materialItems;
        private CatalogHandler _catalogHandler;
        public MaterialSelectionForm()
        {
            InitializeComponent();            
            Inialize();
        }

        private void Inialize()
        {
            _catalogHandler = new CatalogHandler();
            _materialItems = new List<MaterialItem>();
        }

        private void GetMaterialItems()
        {
            MaterialItemEnumerator materialEnumerator = _catalogHandler.GetMaterialItems();
            while (materialEnumerator.MoveNext())
            {
                if (materialEnumerator.Current.Type == MaterialItem.MaterialItemTypeEnum.MATERIAL_STEEL)
                    _materialItems.Add(materialEnumerator.Current);
            }
        }

        private void MaterialSelectionForm_Load(object sender, EventArgs e)
        {
            GetMaterialItems();
            ShowMaterialList();
        }

        private void ShowMaterialList()
        {
            var bindingList = new BindingList<Material>();
            
            //bindingList.Add(nullValue);
            foreach (var material in _materialItems)
            {
                bindingList.Add(new Material()
                {
                    Name = material.MaterialName,
                    Type = material.Type.ToString(),
                    Alias1 = material.AliasName1,
                    Alias2 = material.AliasName2,
                    Alias3 = material.AliasName3
                });
            }
            dataGrid1.DataSource = new BindingSource(bindingList, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class Material
    {
        public string Name { get; set; }
        public string Alias1 { get; set; }
        public string Alias2 { get; set; }
        public string  Alias3 { get; set; }
        public string Type { get; set; }
        public Material()
        {

        }
    }
}
