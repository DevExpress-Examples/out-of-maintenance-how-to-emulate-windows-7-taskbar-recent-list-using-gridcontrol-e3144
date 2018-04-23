using System;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Grid;
using System.ComponentModel;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace WindowsApplication1
{
    public class GridRecentItemsHelper
    {

        private RepositoryItemButtonEdit _EditPinned;
              private RepositoryItemButtonEdit _EditUnPinned;
        public GridRecentItemsHelper(GridView view)
        {
            _View = view;
            view.BeginUpdate();
            InitView();
            InitColumns();
            view.EndUpdate();
        }

        private void InitView()
        {
            _View.GridControl.ForceInitialize();
            _View.GridControl.DataSource = RecentItems;
            _View.Columns["IsPinned"].GroupIndex = 0;
            _View.Columns["IsPinned"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            _View.OptionsView.ShowGroupPanel = false;
            _View.OptionsView.ShowHorzLines = false;
            _View.OptionsView.GroupDrawMode = GroupDrawMode.Office2003;
            _View.GroupRowHeight = 20;
            _View.OptionsView.ShowVertLines = false;
            _View.OptionsBehavior.AutoExpandAllGroups = true;
            _View.OptionsBehavior.ImmediateUpdateRowPosition = true;
            _View.OptionsView.ShowColumnHeaders = false;
            _View.OptionsSelection.EnableAppearanceFocusedCell = false;
            _View.FocusRectStyle = DrawFocusRectStyle.None;
            _View.CustomDrawGroupRow += view_CustomDrawGroupRow;
            _View.CustomDrawCell += view_CustomDrawCell;
            _View.GroupRowCollapsing += view_GroupRowCollapsing;
            _View.MouseMove += view_MouseMove;
            _View.CustomRowCellEdit += _View_CustomRowCellEdit;
            _View.OptionsView.ShowIndicator = false;
        }

        void _View_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "PinnedImage" && e.RowHandle == _View.FocusedRowHandle)
                e.RepositoryItem = true.Equals(e.CellValue) ? _EditPinned : _EditUnPinned ;
        }
        private void InitColumns()
        {
            DevExpress.XtraGrid.Columns.GridColumn col = _View.Columns["Image"];
            col.OptionsColumn.AllowEdit = false;
            col.VisibleIndex = 0;
            col.MinWidth = 16;
            col.Width = 10;
            col = _View.Columns["Caption"];
            col.OptionsColumn.AllowEdit = false;
            col = _View.Columns["PinnedImage"];
            col.Width = 16;
            _EditPinned = new RepositoryItemButtonEdit();
            _EditPinned.TextEditStyle = TextEditStyles.HideTextEditor;
            _EditPinned.Buttons[0].Kind = ButtonPredefines.Glyph;
            _EditPinned.ButtonClick += _EditPinned_ButtonClick;
            _EditUnPinned = _EditPinned.Clone() as RepositoryItemButtonEdit;
            _EditPinned.Buttons[0].Image = WindowsApplication1.Properties.Resources.pin16;
            _EditUnPinned.Buttons[0].Image = WindowsApplication1.Properties.Resources.unpin16;
        }

        void _EditPinned_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            _View.CloseEditor();
            GridRecentItem item = _View.GetFocusedRow() as GridRecentItem;
            item.IsPinned = !item.IsPinned;
            _View.RefreshData();
            _View.ExpandAllGroups();
        }

        void view_MouseMove(object sender, MouseEventArgs e)
        {
            GridHitInfo hi = _View.CalcHitInfo(e.Location);
            if (hi.InRow)
                _View.FocusedRowHandle = hi.RowHandle;
        }

        

        void view_GroupRowCollapsing(object sender, RowAllowEventArgs e)
        {
            e.Allow = false;
        }

        private void DrawPinnedImage(RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle != _View.FocusedRowHandle)
                e.Handled = true;

        }

        void view_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            DrawHotTrack(e);
            switch (e.Column.FieldName)
            {
                case "Image":
                    DrawImage(e);
                    break;
                case "Caption":
                    DrawCaption(e);
                    break;
                case "PinnedImage":
                    DrawPinnedImage(e);
                    break;
                default:
                    break;
            }
        }

        private void DrawHotTrack(RowCellCustomDrawEventArgs e)
        {
            
        }

        private void DrawImage(RowCellCustomDrawEventArgs e)
        {
            Image image = e.CellValue as Image;
            if (image == null)
                return;
            Rectangle bounds = e.Bounds;
            bounds.Inflate(-2, -2);
            e.Graphics.DrawImage(image, bounds);
            e.Handled = true;
        }
        private void DrawCaption(RowCellCustomDrawEventArgs e)
        {
            Rectangle bounds = e.Bounds;
            bounds.Inflate(-2, -2);
            e.Appearance.DrawString(e.Cache, e.DisplayText, bounds);
            e.Handled = true;
        }
        void view_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo info = (GridGroupRowInfo)(((CustomDrawObjectEventArgs)e).Info);
            Rectangle bounds = info.DataBounds;
            bounds.Offset(2, 0);
            string text = (bool)info.EditValue ? "Pinned" : "Recent";
            e.Appearance.DrawString(e.Cache, text, bounds);
            SizeF size = e.Appearance.CalcTextSize(e.Cache, text, bounds.Width);
            bounds.Offset(size.ToSize().Width, 0);
            bounds.Width -= size.ToSize().Width;
            bounds.Inflate(-2, 0);
            Point point1 = new Point(bounds.X, bounds.Y + bounds.Height / 2);
            Point point2 = new Point(bounds.Right, bounds.Y + bounds.Height / 2);
            e.Graphics.DrawLine(new Pen(e.Appearance.GetForeBrush(e.Cache)), point1, point2);
            e.Handled = true;
        }

        private BindingList<GridRecentItem> _RecentItems = new BindingList<GridRecentItem>();
        private readonly GridView _View;
        public BindingList<GridRecentItem> RecentItems
        {
            get { return _RecentItems; }
            set { _RecentItems = value; }
        }
    }
}
