using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Catalogs;
using Tekla.Structures.Drawing;

namespace Ex1
{
    /// <summary>
    /// Interface that defines between main app & TeklaModelController
    /// </summary>
    public interface ITeklaModelController
    {
        string MaterialDisplayMember { get; }
        /// <summary>
        /// Size of padFooting
        /// </summary>
        string PadFootingSize { get; set; }
        object Material { get; set; }
        string ColumnProfile { get; set; }
        string RebarGroupGrade { get; set; }
        string RebarGroupSize { get; set; }
        string RebarGroupRadius { get; set; }
        /// <summary>
        /// MaterialItem list used for bounding comboBox on the main form (MainForm)
        /// </summary>
        IList<MaterialItem> MaterialItems { get; /*set; */}
        /// <summary>
        /// List of material used for bounding to dataGrid on the dialog form (MaterialSelectionForm)
        /// </summary>
        object MaterialList { get; }
        /// <summary>
        /// Drawing object that will be set active by button click event at the main form
        /// </summary>
        object SelectedDrawingToActivate { set; }
        void CreateColumns();
        /// <summary>
        /// Creates rebar model
        /// </summary>
        void CreateRebars();
        void SetDrawingActive();
        /// <summary>
        /// Gets list of material items available in Tekla structure
        /// <para/>Populates list of materials for DataGrid UI control on Dialog form
        /// <para/>Populates Binding list for comboBox UI control on the main form
        /// </summary>
        void GetMaterialItems();
        ListViewItem[] GetDrowings();
        /// <summary>
        /// Methods creates a new rectangle with given text in all drawings available
        /// </summary>
        /// <param name="message">Text taken from form and inserted</param>
        void CreateDrawing(string message);
    }
}
