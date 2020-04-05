using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Catalogs;
using Tekla.Structures.Drawing;

namespace Ex1
{
    public interface ITeklaModelController
    {
        int progress { get; }
        string PadFootingSize { get; set; }
        MaterialItem Material { get; set; }
        string ColumnProfile { get; set; }
        string RebarGroupGrade { get; set; }
        string RebarGroupSize { get; set; }
        string RebarGroupRadius { get; set; }
        IList<MaterialItem> MaterialItems { get; /*set; */}
        object MaterialList { get; }
        Drawing SelectedDrawingToActivate { get; set; }
        void CreateColumns();
        void CreateRebars();
        void SetDrawingActive();
        void GetMaterialItems();
        ListViewItem[] GetDrowings();
    }
}
