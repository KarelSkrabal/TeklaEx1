using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Catalogs;
using Tekla.Structures.Dialog;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using Point = Tekla.Structures.Geometry3d.Point;
using System.ComponentModel;
using Tekla.Structures.Drawing;
using TSM = Tekla.Structures.Model;
using TSD = Tekla.Structures.Drawing;
using System.Collections;

namespace Ex1
{
    /// <summary>
    /// Class that manipulates Tekla structure models
    /// </summary>
    internal class TeklaModelController : ITeklaModelController
    {
        private Model _model = new Model();
        private static ModelEntity _modelEntity = ModelEntity.Instance;
        private DrawingHandler _drawingHandler = new DrawingHandler();
        private CatalogHandler _catalogHandler = new CatalogHandler();
        private string _padFootingsSize = "1500*1500"; //default value
        public string PadFootingSize
        {
            get => _padFootingsSize;
            set => _padFootingsSize = value.GetPadFootingSizeString() ?? "1500*1500";
        }
        private string _columnProfile;
        public string ColumnProfile
        {
            get => _columnProfile;
            set => _columnProfile = value ?? "some profile";
        }
        private MaterialItem _material;
        public MaterialItem Material { get => _material; set => _material = value; }

        private static readonly int X_MAX = 12000;
        private static readonly int X_MIN = 0;
        private static readonly int Y_MAX = 30000;
        private static readonly int Y_MIN = 0;
        private static readonly int X_STEP = 3000;
        private static readonly int Y_STEP = 6000;

        private ModelPointsController[] _modelPoints = new ModelPointsController[]
        {
            new ModelPointsController(p => p.Y == Y_MIN, p => AddPoint(p)),
            new ModelPointsController(p => p.X == X_MAX, p => AddPoint(p)),
            new ModelPointsController(p => p.Y == Y_MAX, p => AddPoint(p)),
            new ModelPointsController(p => p.X == X_MIN, p => AddPoint(p)),
            new ModelPointsController (p => p.X > X_MIN && p.X < X_MAX && p.Y > Y_MIN && p.Y < Y_MAX, p => DoNothing()),
            new ModelPointsController(p => true, p => DoNothing())
        };

        /// <summary>
        /// Adding point at the border of grid
        /// </summary>
        /// <param name="p"></param>
        private static void AddPoint(Point p) => _modelEntity.points.Add(p);

        /// <summary>
        /// Default method, neccassary!
        /// //default case that just does nothing!
        /// </summary>
        private static void DoNothing() { }

        public TeklaModelController()
        {
            if (!_model.GetConnectionStatus())
                throw new Exception("Tekla isn't connected!");

            GetPadFootingPoints();
        }

        public int progress => throw new NotImplementedException();
        private string _rebarGroupGrade;
        public string RebarGroupGrade { get => _rebarGroupGrade; set => _rebarGroupGrade = value; }
        private string _rebarGroupSize;
        public string RebarGroupSize { get => _rebarGroupSize; set => _rebarGroupSize = value; }
        private string _rebarGroupRadius;
        public string RebarGroupRadius { get => _rebarGroupRadius; set => _rebarGroupRadius = value; }
        private Drawing _selectedDrawingToActivate;
        public object SelectedDrawingToActivate {  set => _selectedDrawingToActivate = value as Drawing; }
        public object MaterialList { get => _modelEntity.materialBindingList; }
        public IList<MaterialItem> MaterialItems { get => _modelEntity.materialItems; /*set => throw new NotImplementedException(); */}

        public string MaterialDisplayMember => "MaterialName";


        #region Tekla Manipulation Methods

        /// <summary>
        /// Creates a padfooting model at given Point
        /// </summary>
        /// <returns>Returns true if successful</returns>
        private bool CreatePadFootings(Point point)
        {
            Beam padFooting = new Beam();
            padFooting.Name = "FOOTING";
            padFooting.Profile.ProfileString = _padFootingsSize;
            //padFooting.Profile.ProfileString = "1500*1500";
            padFooting.Material.MaterialString = "K30-2";
            padFooting.Class = "8";
            padFooting.StartPoint = point;
            padFooting.EndPoint = new Point() { X = point.X, Y = point.Y, Z = point.Z + 500 };
            padFooting.Position.Rotation = Position.RotationEnum.FRONT;
            padFooting.Position.Plane = Position.PlaneEnum.MIDDLE;
            padFooting.Position.Depth = Position.DepthEnum.MIDDLE;
            if (padFooting.Insert() && _model.CommitChanges())
            {
                _modelEntity.padFootings.Add(padFooting);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a column at the given Point
        /// </summary>
        /// <returns>Returns true if successful</returns>
        private bool CreateColumnOnPadFootings(Point point)
        {
            Beam column = new Beam();
            column.Name = "COLUMN";
            column.Profile.ProfileString = _columnProfile;
            column.Material.MaterialString = _material.MaterialName;
            column.Class = "13";
            column.StartPoint = point;
            column.EndPoint = new Point() { X = point.X, Y = point.Y, Z = point.Z + 5000 };
            column.Position.Rotation = Position.RotationEnum.FRONT;
            column.Position.Plane = Position.PlaneEnum.MIDDLE;
            column.Position.Depth = Position.DepthEnum.MIDDLE;
            if (column.Insert() && _model.CommitChanges())
            {
                _modelEntity.columns.Add(column);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates connection between the given column & padfooting
        /// </summary>
        private void CreateConnection(TSM.ModelObject column, TSM.ModelObject padFooting)
        {
            TSM.Connection connection = new TSM.Connection();
            connection.Name = "Stiffened Base Plate";
            connection.Number = 1014;
            connection.PositionType = PositionTypeEnum.COLLISION_PLANE;
            connection.SetAttribute("cut1", 1);
            connection.SetAttribute("cut2", 1);
            connection.LoadAttributesFromFile("standard");
            connection.UpVector = new Vector(0, 0, 1000);
            connection.SetPrimaryObject(column);
            connection.SetSecondaryObject(padFooting);
            connection.Insert();
            _model.CommitChanges();
        }

        /// <summary>
        /// Creates rebars to a given beam
        /// </summary>
        private void CreateRebar(Beam beam)
        {
            double MinimumX = beam.GetSolid().MinimumPoint.X;
            double MinimumY = beam.GetSolid().MinimumPoint.Y;
            double MinimumZ = beam.GetSolid().MinimumPoint.Z;
            double MaximumX = beam.GetSolid().MaximumPoint.X;
            double MaximumY = beam.GetSolid().MaximumPoint.Y;
            double MaximumZ = beam.GetSolid().MaximumPoint.Z;
            TSM.Polygon polygon = new TSM.Polygon();
            polygon.Points.Add(new Point(MinimumX, MaximumY, MinimumZ));
            polygon.Points.Add(new Point(MinimumX, MinimumY, MinimumZ));
            polygon.Points.Add(new Point(MinimumX, MinimumY, MaximumZ));
            polygon.Points.Add(new Point(MinimumX, MaximumY, MaximumZ));
            TSM.Polygon polygon2 = new TSM.Polygon();
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
            rebarGroup.Grade = _rebarGroupGrade;
            //rebarGroup.Size = "12";
            rebarGroup.Size = _rebarGroupSize;
            //rebarGroup.RadiusValues.Add(40.0);
            rebarGroup.RadiusValues = _rebarGroupRadius.GetRebarGroupRadiuses();
            rebarGroup.SpacingType = BaseRebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_TARGET_SPACE;
            rebarGroup.Spacings.Add(100.0);
            rebarGroup.ExcludeType = RebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_BOTH;
            rebarGroup.NumberingSeries.StartNumber = 0;
            rebarGroup.NumberingSeries.Prefix = "Group";
            rebarGroup.OnPlaneOffsets.Add(25.0);
            rebarGroup.FromPlaneOffset = 40;
            if (!rebarGroup.Insert())
                MessageBox.Show("Error inserting rebargroup");
            rebarGroup.Name = "Modified Group 1xx";
            rebarGroup.Modify();
        }
        #endregion

        /// <summary>
        /// Gets points at which models will be inserted
        /// </summary>
        private void GetPadFootingPoints()
        {
            for (int x = 0; x <= X_MAX; x += X_STEP)
            {
                for (int y = 0; y <= Y_MAX; y += Y_STEP)
                {
                    var point = new Point() { X = x, Y = y, Z = 0 };
                    var runShortCut = _modelPoints.First(p => p._canApply(point));
                    runShortCut._action(point);
                }
            }
        }

        public void CreateColumns()
        {
            try
            {
                foreach (var point in _modelEntity.points)
                {
                    if (CreatePadFootings(point) && CreateColumnOnPadFootings(point))
                        CreateConnection(_modelEntity.columns.Last(), _modelEntity.padFootings.Last());
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Method populates ViewList control by drawings available in the actual model
        /// </summary>
        /// <returns>Returns ListViewItem array used for a ListView bounding</returns>
        public ListViewItem[] GetDrowings()
        {
            DrawingEnumerator drawingEnumerator = _drawingHandler.GetDrawings();
            //TODO-check for null exception
            ListViewItem[] items = new ListViewItem[drawingEnumerator.GetSize()];
            int i = 0;
            while (drawingEnumerator.MoveNext())
            {
                items[i] = new ListViewItem()
                {
                    Tag = drawingEnumerator.Current,
                    Text = drawingEnumerator.Current.Name,
                    ToolTipText = drawingEnumerator.Current.Title2
                };
                i++;
            }
            return items;
        }

        public void CreateRebars()
        {
            ModelObjectEnumerator padFootings = _model.GetModelObjectSelector().GetAllObjectsWithType(TSM.ModelObject.ModelObjectEnum.BEAM);
            int test = padFootings.GetSize();
            foreach (Beam padFooting in padFootings)
            {
                CreateRebar(padFooting);
            }
        }

        public void SetDrawingActive()
        {
            _drawingHandler.SetActiveDrawing(_selectedDrawingToActivate, true);
        }

        /// <summary>
        /// Generates MateriaItem IList used for binding to the comboBox on the main form and
        /// ModelEntity.Material BindingList used for the datagrid on the form selection material form
        /// </summary>
        public void GetMaterialItems()
        {
            MaterialItemEnumerator materialEnumerator = _catalogHandler.GetMaterialItems();
            while (materialEnumerator.MoveNext())
            {
                if (materialEnumerator.Current.Type == MaterialItem.MaterialItemTypeEnum.MATERIAL_STEEL)
                {
                    _modelEntity.materialItems.Add(materialEnumerator.Current);
                    _modelEntity.materialBindingList.Add(new ModelEntity.Material()
                    {
                        Name = materialEnumerator.Current.MaterialName,
                        Type = materialEnumerator.Current.Type.ToString(),
                        Alias1 = materialEnumerator.Current.AliasName1,
                        Alias2 = materialEnumerator.Current.AliasName2,
                        Alias3 = materialEnumerator.Current.AliasName3
                    });
                }
            }
        }

        public void CreateDrawing(string messageText)
        {
            if (_drawingHandler.GetConnectionStatus())
            {
                Drawing drawing = _drawingHandler.GetActiveDrawing();
                ContainerView sheet = drawing.GetSheet();
                DrawingObjectEnumerator views = sheet.GetViews();
                while (views.MoveNext())
                {
                    TSD.View currentView = views.Current as TSD.View;
                    if (currentView != null)
                    {
                        RectangleBoundingBox viewRectangle = currentView.GetAxisAlignedBoundingBox();
                        Text message = new Text
                        (
                            sheet,
                            new Point()
                            {
                                X = viewRectangle.UpperLeft.X + (viewRectangle.UpperRight.X - viewRectangle.UpperLeft.X) / 2.0,
                                Y = 0
                            },
                            messageText,
                            new Text.TextAttributes()
                        );
                        if (message.Insert())
                        {
                            RectangleBoundingBox rectangle = message.GetAxisAlignedBoundingBox();
                            Rectangle box = new Rectangle(sheet, rectangle.LowerLeft, rectangle.UpperRight);
                            box.Insert();
                        }
                    }
                }
                drawing.CommitChanges();
            }
        }

        /// <summary>
        /// DTO for inserted models
        /// </summary>
        private class ModelEntity
        {
            internal List<Point> points = new List<Point>();
            internal List<Beam> padFootings = new List<Beam>();
            internal List<Beam> columns = new List<Beam>();
            internal IList<MaterialItem> materialItems = new List<MaterialItem>();
            internal BindingList<Material> materialBindingList = new BindingList<Material>();
            //internal List<Tekla.Structures.Model.Connection> connections;

            private static readonly ModelEntity _instance = new ModelEntity();
            /// <summary>
            /// Returns an instance of CreatedModelEntity DTO
            /// </summary>
            internal static ModelEntity Instance => _instance;
            private ModelEntity()
            {
                MaterialItem nullValue = new MaterialItem() { MaterialName = "Select ..." };
                materialItems.Insert(0, nullValue);
            }

            /// <summary>
            /// Material class defines the structure how material information are shown on the form
            /// </summary>
            public class Material
            {
                public string Name { get; set; }
                public string Alias1 { get; set; }
                public string Alias2 { get; set; }
                public string Alias3 { get; set; }
                public string Type { get; set; }
            }
        }

        /// <summary>
        /// Object for creating List<Point> at which model will be inserted
        /// </summary>
        public class ModelPointsController
        {
            public Func<Point, bool> _canApply { get; set; }
            public Action<Point> _action { get; set; }

            public ModelPointsController(Func<Point, bool> canApply, Action<Point> action)
            {
                _canApply = canApply;
                _action = action;
            }
        }
    }
}
