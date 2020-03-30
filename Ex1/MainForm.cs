using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

using Tekla.Structures;
using Tekla.Structures.Catalogs;
using Tekla.Structures.Dialog;
using Tekla.Structures.Dialog.UIControls;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using TSD = Tekla.Structures.Datatype;
using Point = Tekla.Structures.Geometry3d.Point;
using System.Text;
using System.ComponentModel;
using System.Linq;
using Tekla.Structures.Drawing;

namespace Ex1
{
    public partial class MainForm : /*Form */ApplicationFormBase
    {
        private Model _model;
        private ModelInfo _modelInfo;
        //private StringBuilder _modelInfoRawString;
        private string _padFootingsSize = "1500"; //default value
        private static CreatedModelEntity _createdModels;
        private MaterialItem _material;
        private DrawingHandler _drawingHandler;
        private Drawing _drawing;
        private static readonly int X_MAX = 12000;
        private static readonly int X_MIN = 0;
        private static readonly int Y_MAX = 30000;
        private static readonly int Y_MIN = 0;
        private static readonly int X_STEP = 3000;
        private static readonly int Y_STEP = 6000;
        private BackgroundWorker _bwCreatePadFootings;
        private BackgroundWorker _bwCreateRebars;

        public PadFootingPointController[] _padFootingPointController = new PadFootingPointController[]
        {
            new PadFootingPointController(p => p.Y == Y_MIN, p => AddPoint(p)),
            new PadFootingPointController(p => p.X == X_MAX, p => AddPoint(p)),
            new PadFootingPointController(p => p.Y == Y_MAX, p => AddPoint(p)),
            new PadFootingPointController(p => p.X == X_MIN, p => AddPoint(p)),
            new PadFootingPointController (p => p.X > X_MIN && p.X < X_MAX && p.Y > Y_MIN && p.Y < Y_MAX, p => DoNothing()),
            new PadFootingPointController(p => true, p => DoNothing())
        };

        private static void DoNothing()
        {
            //default case that just do nothing!
        }

        public static void AddPoint(Point p) => _createdModels.points.Add(p);

        public MainForm()
        {
            base.InitializeForm();
            InitializeComponent();
        }

        private void Initialize()
        {
            _model = new Model();
            if (!_model.GetConnectionStatus())
                throw new Exception("Tekla isn't connected!");

            //_modelInfoRawString = new StringBuilder();
            _modelInfo = _model.GetInfo();
            _material = new MaterialItem();
            _drawingHandler = new DrawingHandler();
            _bwCreatePadFootings = new BackgroundWorker();
            _bwCreatePadFootings.DoWork += _bwCreatePadFootings_DoWork;
            _bwCreatePadFootings.ProgressChanged += _bwCreatePadFootings_ProgressChanged;
            _bwCreatePadFootings.RunWorkerCompleted += _bwCreatePadFootings_RunWorkerCompleted;
            _bwCreatePadFootings.WorkerReportsProgress = true;
            _createdModels = new CreatedModelEntity();
            _bwCreateRebars = new BackgroundWorker();
            _bwCreateRebars.DoWork += _bwCreateRebars_DoWork;
            _bwCreateRebars.ProgressChanged += _bwCreateRebars_ProgressChanged;
            _bwCreateRebars.RunWorkerCompleted += _bwCreateRebars_RunWorkerCompleted;
            _bwCreateRebars.WorkerReportsProgress = true;
        }

        private void _bwCreateRebars_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnCreateFootings.Enabled = true;
            btnCreateRebars.Enabled = true;
        }

        private void _bwCreateRebars_ProgressChanged(object sender, ProgressChangedEventArgs e) => lbResult.Text = "Progress : " + e.ProgressPercentage.ToString() + "% completed";

        private void _bwCreateRebars_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            double progressStep = 0;
            int counter = 0;

            ModelObjectEnumerator padFotings = _model.GetModelObjectSelector().GetAllObjectsWithType(Tekla.Structures.Model.ModelObject.ModelObjectEnum.BEAM);

            //calculate progress step for the progress reporting on the form
            if (padFotings != null)
                progressStep = (double)1 / padFotings.GetSize();

            foreach (Beam padFooting in padFotings)
            {
                CreateRebar(padFooting);
                counter++;
                backgroundWorker.ReportProgress((int)Math.Round(counter * progressStep * 100, MidpointRounding.AwayFromZero));
            }
            //e.Result = ret;
        }

        private void GetPadFootingsSize() => _padFootingsSize = this.txtPadFootingSize.Text;

        private void _bwCreatePadFootings_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //lbResult.Text = "Created : " + e.Result.ToString() + " padfootings.";
            btnCreateFootings.Enabled = true;
            btnCreateRebars.Enabled = true;
        }

        private void _bwCreatePadFootings_ProgressChanged(object sender, ProgressChangedEventArgs e) => lbResult.Text = "Progress : " + e.ProgressPercentage.ToString() + "% completed";

        private void _bwCreatePadFootings_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            double progressStep = 0;
            int counter = 0;

            //calculate progress step for the progress reporting on the form
            if (_createdModels.points/*_points*/ != null && _createdModels.points.Count > 0)
                progressStep = (double)1 / _createdModels.points.Count;

            foreach (var point in _createdModels.points)
            {
                //TODO-change this, consider bool return value if it's usefull
                if (InsertModel(point))
                {
                    CreateConnection(counter);
                    counter++;
                }
                backgroundWorker.ReportProgress((int)Math.Round(counter * progressStep * 100, MidpointRounding.AwayFromZero));
            }
            //e.Result = ret;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialize();
            //GetModelInfo();
            //lbModelInfo.Text = _modelInfoRawString.ToString();
            GetPadFootingPoints();
            btnCreateRebars.Enabled = false;
            GetDrawings();
        }

        private void GetDrawings()
        {
            DrawingEnumerator drawingEnumerator = _drawingHandler.GetDrawings();
            ListViewItem item;
            while (drawingEnumerator.MoveNext())
            {
                item = new ListViewItem()
                {
                    Tag = drawingEnumerator.Current,
                    Text = drawingEnumerator.Current.Name,
                    ToolTipText = drawingEnumerator.Current.Title2
                };
                lswDrawings.Items.Add(item);
                item = null;
            }
        }

        //private void GetModelInfo()
        //{
        //    Type t = _modelInfo.GetType();
        //    foreach (var item in t.GetProperties())
        //    {
        //        _modelInfoRawString.AppendLine(item.Name + " : " + item.GetValue(_modelInfo).ToString());
        //    }
        //}

        private bool InsertModel(Point point)
        {
            //_modelInfoRawString = null;
            bool ret = CreatePadFootings(point) && CreateColumnOnPadFootings(point);
            //testing
            CreateRebar(_createdModels.padFootings.FirstOrDefault());
            return ret;
        }

        private bool CreatePadFootings(Point point)
        {
            Beam padFooting = new Beam();
            padFooting.Name = "FOOTING";
            padFooting.Profile.ProfileString = _padFootingsSize.GetPadFootingSizeString();
            padFooting.Material.MaterialString = "K30-2";
            padFooting.Class = "8";
            padFooting.StartPoint = point;
            padFooting.EndPoint = new Point() { X = point.X, Y = point.Y, Z = point.Z + 500 };
            padFooting.Position.Rotation = Position.RotationEnum.FRONT;
            padFooting.Position.Plane = Position.PlaneEnum.MIDDLE;
            padFooting.Position.Depth = Position.DepthEnum.MIDDLE;
            if (padFooting.Insert() && _model.CommitChanges())
            {
                _createdModels.padFootings.Add(padFooting);
                return true;
            }
            return false;
        }

        private bool CreateColumnOnPadFootings(Point point)
        {
            Beam column = new Beam();
            column.Name = "COLUMN";
            //column.Profile.ProfileString = "400*400";
            column.Profile.ProfileString = ColumnsProfileTextBox.Text;
            column.Material.MaterialString = "Concrete_Undefined";
            column.Material.MaterialString = _material.MaterialName;
            column.Class = "13";
            column.StartPoint = point;
            column.EndPoint = new Point() { X = point.X, Y = point.Y, Z = point.Z + 5000 };
            column.Position.Rotation = Position.RotationEnum.FRONT;
            column.Position.Plane = Position.PlaneEnum.MIDDLE;
            column.Position.Depth = Position.DepthEnum.MIDDLE;
            if (column.Insert() && _model.CommitChanges())
            {
                _createdModels.columns.Add(column);
                return true;
            }
            return false;
        }

        private bool CreateConnection(int actualPosition)
        {
            Tekla.Structures.Model.Connection connection = new Tekla.Structures.Model.Connection();
            connection.Name = "Stiffened Base Plate";
            connection.Number = 1014;
            connection.PositionType = PositionTypeEnum.COLLISION_PLANE;
            connection.SetAttribute("cut1", 1);
            connection.SetAttribute("cut2", 1);
            connection.LoadAttributesFromFile("standard");
            connection.UpVector = new Vector(0, 0, 1000);
            connection.SetPrimaryObject(_createdModels.columns.ElementAt(actualPosition));
            connection.SetSecondaryObject(_createdModels.padFootings.ElementAt(actualPosition));
            if (connection.Insert() && _model.CommitChanges())
            {
                _createdModels.connections.Add(connection);
                return true;
            }
            return false;
        }

        private void CreateRebar(Beam beam)
        {
            double MinimumX = beam.GetSolid().MinimumPoint.X;
            double MinimumY = beam.GetSolid().MinimumPoint.Y;
            double MinimumZ = beam.GetSolid().MinimumPoint.Z;
            double MaximumX = beam.GetSolid().MaximumPoint.X;
            double MaximumY = beam.GetSolid().MaximumPoint.Y;
            double MaximumZ = beam.GetSolid().MaximumPoint.Z;

            Tekla.Structures.Model.Polygon polygon = new Tekla.Structures.Model.Polygon();
            polygon.Points.Add(new Point(MinimumX, MaximumY, MinimumZ));
            polygon.Points.Add(new Point(MinimumX, MinimumY, MinimumZ));
            polygon.Points.Add(new Point(MinimumX, MinimumY, MaximumZ));
            polygon.Points.Add(new Point(MinimumX, MaximumY, MaximumZ));

            Tekla.Structures.Model.Polygon polygon2 = new Tekla.Structures.Model.Polygon();
            polygon2.Points.Add(new Point(MaximumX, MaximumY, MinimumZ));
            polygon2.Points.Add(new Point(MaximumX, MinimumY, MinimumZ));
            polygon2.Points.Add(new Point(MaximumX, MinimumY, MaximumZ));
            polygon2.Points.Add(new Point(MaximumX, MaximumY, MaximumZ));

            RebarGroup rebarGroup = new RebarGroup();
            rebarGroup.Polygons.Add(polygon);
            rebarGroup.Polygons.Add(polygon2);
            rebarGroup.Class = 3;
            rebarGroup.Name = "FootingRebar";
            rebarGroup.Father = beam;
            //rebarGroup.Grade = "Undefined";
            rebarGroup.Grade = GradeTextBox.Text;
            //rebarGroup.Size = "12";
            rebarGroup.Size = SizeTextBox.Text;
            //rebarGroup.RadiusValues.Add(40.0);
            SetRadius(ref rebarGroup);
            rebarGroup.SpacingType = BaseRebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_TARGET_SPACE;
            rebarGroup.Spacings.Add(100.0);
            rebarGroup.ExcludeType = RebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_BOTH;
            rebarGroup.NumberingSeries.StartNumber = 0;
            rebarGroup.NumberingSeries.Prefix = "Group";
            rebarGroup.OnPlaneOffsets.Add(25.0);
            rebarGroup.FromPlaneOffset = 40;
            rebarGroup.Insert();

            rebarGroup.Name = "Modified Group 1";
            rebarGroup.Modify();
        }

        private void SetRadius(ref RebarGroup rebarGroup)
        {
            string[] radiuses = BendingRadiusTextBox.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var radius in radiuses)
            {
                rebarGroup.RadiusValues.Add(Convert.ToDouble(radius));
            }
        }

        private void GetPadFootingPoints()
        {
            for (int x = 0; x <= X_MAX; x += X_STEP)
            {
                for (int y = 0; y <= Y_MAX; y += Y_STEP)
                {
                    var point = new Point() { X = x, Y = y, Z = 0 };
                    var runShortCut = _padFootingPointController.First(p => p._canApply(point));
                    runShortCut._action(point);
                }
            }
        }

        private void btnCreateFootings_Click(object sender, EventArgs e)
        {
            SetDefaultTextToResultLabel();
            if (!_bwCreatePadFootings.IsBusy)
            {
                _bwCreatePadFootings.RunWorkerAsync();
                btnCreateFootings.Enabled = false;
            }
        }

        private void SetDefaultTextToResultLabel()
        {
            lbResult.Text = "Progress : start .. ";
        }

        public class PadFootingPointController
        {
            public Func<Point, bool> _canApply { get; set; }
            public Action<Point> _action { get; set; }

            public PadFootingPointController(Func<Point, bool> canApply, Action<Point> action)
            {
                _canApply = canApply;
                _action = action;
            }
        }
        //TODO-consider singleton pattern for this object
        public class CreatedModelEntity
        {
            public List<Point> points;
            public List<Beam> padFootings;
            public List<Beam> columns;
            public List<Tekla.Structures.Model.Connection> connections;

            public CreatedModelEntity()
            {
                points = new List<Point>();
                padFootings = new List<Beam>();
                columns = new List<Beam>();
                connections = new List<Tekla.Structures.Model.Connection>();
            }
        }

        private void txtPadFootingSize_Leave(object sender, EventArgs e)
        {
            string padFootingSize = (sender as TextBox)?.Text;
            if (!string.IsNullOrEmpty(padFootingSize))
                _padFootingsSize = padFootingSize;
        }

        private void btnCreateRebars_Click(object sender, EventArgs e)
        {
            SetDefaultTextToResultLabel();
            if (!_bwCreateRebars.IsBusy)
            {
                _bwCreateRebars.RunWorkerAsync();
                btnCreateFootings.Enabled = false;
                btnCreateRebars.Enabled = false;
            }
        }

        private void profileCatalog1_SelectClicked(object sender, EventArgs e)
        {
            //profileCatalog1 = new ProfileCatalog(ColumnsProfileTextBox.Text);
            profileCatalog1.SelectedProfile = ColumnsProfileTextBox.Text;
        }

        private void profileCatalog1_SelectionDone(object sender, EventArgs e)
        {
            ColumnsProfileTextBox.Text = profileCatalog1.SelectedProfile;
            SetAttributeValue(this.ColumnsProfileTextBox, profileCatalog1.SelectedProfile);
        }

        private void reinforcementCatalog1_SelectionDone(object sender, EventArgs e)
        {
            SizeTextBox.Text = reinforcementCatalog1.SelectedRebarSize;
            GradeTextBox.Text = reinforcementCatalog1.SelectedRebarGrade;
            BendingRadiusTextBox.Text = reinforcementCatalog1.SelectedRebarBendingRadius.ToString();
        }

        private void btnSelectMaterial_Click(object sender, EventArgs e)
        {
            var selectMaterial = new MaterialSelectionForm();
            selectMaterial.ShowDialog();
            if (selectMaterial.DialogResult == DialogResult.Cancel)
            {
                MaterialItem nullValue = new MaterialItem() { MaterialName = "Select ..." };
                selectMaterial._materialItems.Insert(0, nullValue);
                var bindingList = new BindingList<MaterialItem>(selectMaterial._materialItems);
                cbMaterialList.DataSource = new BindingSource(bindingList, null);
                //cbMaterialList.DisplayMember = "MaterialName";
            }
        }

        private void cbMaterialList_SelectedValueChanged(object sender, EventArgs e)
        {
            var selectedItem = (MaterialItem)(sender as ComboBox).SelectedItem;
            if (selectedItem.MaterialName != "Select ...")
                _material = selectedItem;
        }

        private void btnInsertDrawing_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDrawingText.Text))
                CreateDrawing(txtDrawingText.Text);
        }

        private void CreateDrawing(string text)
        {
            if (_drawingHandler.GetConnectionStatus())
            {
                var drawings = _drawingHandler.GetDrawings();
                while (drawings.MoveNext())
                {
                    var drawing = drawings.Current;
                    var str = drawing.Name;
                    ContainerView containerView = drawing.GetSheet();
                    DrawingObjectEnumerator sheets1 = null;
                    if (containerView.IsSheet)
                    {
                        sheets1 = containerView.GetViews();
                        while (sheets1.MoveNext())
                        {
                            Tekla.Structures.Drawing.View view = sheets1.Current as Tekla.Structures.Drawing.View;
                            if (view != null)
                            {
                                RectangleBoundingBox ViewAABB = view.GetAxisAlignedBoundingBox();
                                Point CenterPoint = new Point();
                                CenterPoint.X = ViewAABB.LowerLeft.X + (ViewAABB.LowerRight.X - ViewAABB.LowerLeft.X) / 2.0;
                                CenterPoint.Y = ViewAABB.LowerLeft.Y - 5.0;  //5.0 mm below the view's bounding box

                                Text MyViewTitle = new Text(containerView, CenterPoint, txtDrawingText.Text, new Text.TextAttributes());
                                if (!MyViewTitle.Insert())
                                {
                                    Console.WriteLine("Insert failed.");
                                }
                                else
                                {
                                    RectangleBoundingBox TitleAABB = MyViewTitle.GetAxisAlignedBoundingBox();
                                    Rectangle myBox = new Rectangle(containerView, TitleAABB.LowerLeft, TitleAABB.UpperRight);
                                    myBox.Insert();
                                }
                            }
                        }
                    }
                }
                _drawingHandler.SaveActiveDrawing();
            }
        }

        private void DrawText(ViewBase drawView, RectangleBoundingBox boundingBox, DrawingColors color)
        {
            PointList points = new PointList();
            points.Add(boundingBox.LowerLeft);
            points.Add(boundingBox.UpperLeft);
            points.Add(boundingBox.UpperRight);
            points.Add(boundingBox.LowerRight);
            Tekla.Structures.Drawing.Polygon MyPolygon = new Tekla.Structures.Drawing.Polygon(drawView, points);
            MyPolygon.Attributes.Line.Color = color;

            MyPolygon.Insert();
        }

        private void lswDrawings_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedList = (sender as ListView).SelectedItems;
            if (selectedList.Count != 0)
                _drawing = (Drawing)selectedList[0].Tag;
        }

        private void btnSetActiveDrawing_Click(object sender, EventArgs e)
        {
            _drawingHandler.SetActiveDrawing(_drawing, true);
        }
    }

    public static class Extention
    {
        public static string GetPadFootingSizeString(this string value)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(value);
            sb.Append("*");
            sb.Append(value);
            return sb.ToString();
        }
    }
}
