using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Catalogs;
using Tekla.Structures.Drawing;

namespace Ex1
{
    internal class Fasade : ITeklaModelController
    {
        private ITeklaModelController _controller;
        public Fasade(ITeklaModelController controller)
        {
            _controller = controller;
        }

        public int progress => throw new NotImplementedException();

        public string PadFootingSize { get => _controller.PadFootingSize; set => _controller.PadFootingSize = value; }
        public string ColumnProfile { get => _controller.ColumnProfile; set => _controller.ColumnProfile = value; }
        public /*MaterialItem*/ object Material { get => _controller.Material; set => _controller.Material = value; }
        public string RebarGroupGrade { get => _controller.RebarGroupGrade; set => _controller.RebarGroupGrade = value; }
        public string RebarGroupSize { get => _controller.RebarGroupSize; set => _controller.RebarGroupSize = value; }
        public string RebarGroupRadius { get => _controller.RebarGroupRadius; set => _controller.RebarGroupRadius = value; }
        public IList<MaterialItem> MaterialItems { get => _controller.MaterialItems;}
        public object SelectedDrawingToActivate { set => _controller.SelectedDrawingToActivate = value; }

        public object MaterialList => _controller.MaterialList;

        public string MaterialDisplayMember => _controller.MaterialDisplayMember;

        public void CreateColumns()
        {
            _controller.CreateColumns();
        }

        public void CreateDrawing(string message)
        {
            _controller.CreateDrawing(message);
        }

        public void CreateRebars()
        {
            _controller.CreateRebars();
        }

        public ListViewItem[] GetDrowings()
        {
            return _controller.GetDrowings();
        }

        public void GetMaterialItems()
        {
            _controller.GetMaterialItems();
        }

        public void SetDrawingActive()
        {
            _controller.SetDrawingActive();
        }
    }
}
