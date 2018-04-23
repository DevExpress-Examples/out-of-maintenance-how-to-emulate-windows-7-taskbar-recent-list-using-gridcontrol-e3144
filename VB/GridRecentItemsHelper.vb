Imports System
Imports System.Collections.Generic
Imports DevExpress.XtraGrid.Views.Grid
Imports System.ComponentModel
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls

Namespace WindowsApplication1
	Public Class GridRecentItemsHelper

		Private _EditPinned As RepositoryItemButtonEdit
			  Private _EditUnPinned As RepositoryItemButtonEdit
		Public Sub New(ByVal view As GridView)
			_View = view
			view.BeginUpdate()
			InitView()
			InitColumns()
			view.EndUpdate()
		End Sub

		Private Sub InitView()
			_View.GridControl.ForceInitialize()
			_View.GridControl.DataSource = RecentItems
			_View.Columns("IsPinned").GroupIndex = 0
			_View.Columns("IsPinned").SortOrder = DevExpress.Data.ColumnSortOrder.Descending
			_View.OptionsView.ShowGroupPanel = False
			_View.OptionsView.ShowHorzLines = False
			_View.OptionsView.GroupDrawMode = GroupDrawMode.Office2003
			_View.GroupRowHeight = 20
			_View.OptionsView.ShowVertLines = False
			_View.OptionsBehavior.AutoExpandAllGroups = True
			_View.OptionsBehavior.ImmediateUpdateRowPosition = True
			_View.OptionsView.ShowColumnHeaders = False
			_View.OptionsSelection.EnableAppearanceFocusedCell = False
			_View.FocusRectStyle = DrawFocusRectStyle.None
			AddHandler _View.CustomDrawGroupRow, AddressOf view_CustomDrawGroupRow
			AddHandler _View.CustomDrawCell, AddressOf view_CustomDrawCell
			AddHandler _View.GroupRowCollapsing, AddressOf view_GroupRowCollapsing
			AddHandler _View.MouseMove, AddressOf view_MouseMove
			AddHandler _View.CustomRowCellEdit, AddressOf _View_CustomRowCellEdit
			_View.OptionsView.ShowIndicator = False
		End Sub

		Private Sub _View_CustomRowCellEdit(ByVal sender As Object, ByVal e As CustomRowCellEditEventArgs)
			If e.Column.FieldName = "PinnedImage" AndAlso e.RowHandle = _View.FocusedRowHandle Then
				e.RepositoryItem = If(True.Equals(e.CellValue), _EditPinned, _EditUnPinned)
			End If
		End Sub
		Private Sub InitColumns()
			Dim col As DevExpress.XtraGrid.Columns.GridColumn = _View.Columns("Image")
			col.OptionsColumn.AllowEdit = False
			col.VisibleIndex = 0
			col.MinWidth = 16
			col.Width = 10
			col = _View.Columns("Caption")
			col.OptionsColumn.AllowEdit = False
			col = _View.Columns("PinnedImage")
			col.Width = 16
			_EditPinned = New RepositoryItemButtonEdit()
			_EditPinned.TextEditStyle = TextEditStyles.HideTextEditor
			_EditPinned.Buttons(0).Kind = ButtonPredefines.Glyph
			AddHandler _EditPinned.ButtonClick, AddressOf _EditPinned_ButtonClick
			_EditUnPinned = TryCast(_EditPinned.Clone(), RepositoryItemButtonEdit)
			_EditPinned.Buttons(0).Image = My.Resources.pin16
			_EditUnPinned.Buttons(0).Image = My.Resources.unpin16
		End Sub

		Private Sub _EditPinned_ButtonClick(ByVal sender As Object, ByVal e As ButtonPressedEventArgs)
			_View.CloseEditor()
			Dim item As GridRecentItem = TryCast(_View.GetFocusedRow(), GridRecentItem)
			item.IsPinned = Not item.IsPinned
			_View.RefreshData()
			_View.ExpandAllGroups()
		End Sub

		Private Sub view_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
			Dim hi As GridHitInfo = _View.CalcHitInfo(e.Location)
			If hi.InRow Then
				_View.FocusedRowHandle = hi.RowHandle
			End If
		End Sub



		Private Sub view_GroupRowCollapsing(ByVal sender As Object, ByVal e As RowAllowEventArgs)
			e.Allow = False
		End Sub

		Private Sub DrawPinnedImage(ByVal e As RowCellCustomDrawEventArgs)
			If e.RowHandle <> _View.FocusedRowHandle Then
				e.Handled = True
			End If

		End Sub

		Private Sub view_CustomDrawCell(ByVal sender As Object, ByVal e As RowCellCustomDrawEventArgs)
			DrawHotTrack(e)
			Select Case e.Column.FieldName
				Case "Image"
					DrawImage(e)
				Case "Caption"
					DrawCaption(e)
				Case "PinnedImage"
					DrawPinnedImage(e)
				Case Else
			End Select
		End Sub

		Private Sub DrawHotTrack(ByVal e As RowCellCustomDrawEventArgs)

		End Sub

		Private Sub DrawImage(ByVal e As RowCellCustomDrawEventArgs)
			Dim image As Image = TryCast(e.CellValue, Image)
			If image Is Nothing Then
				Return
			End If
			Dim bounds As Rectangle = e.Bounds
			bounds.Inflate(-2, -2)
			e.Cache.Paint.DrawImage(e.Cache.Graphics, image, bounds)
			e.Handled = True
		End Sub
		Private Sub DrawCaption(ByVal e As RowCellCustomDrawEventArgs)
			Dim bounds As Rectangle = e.Bounds
			bounds.Inflate(-2, -2)
			e.Appearance.DrawString(e.Cache, e.DisplayText, bounds)
			e.Handled = True
		End Sub
		Private Sub view_CustomDrawGroupRow(ByVal sender As Object, ByVal e As RowObjectCustomDrawEventArgs)
			Dim info As GridGroupRowInfo = CType(CType(e, CustomDrawObjectEventArgs).Info, GridGroupRowInfo)
			Dim bounds As Rectangle = info.DataBounds
			bounds.Offset(2, 0)
			Dim text As String = If(DirectCast(info.EditValue, Boolean), "Pinned", "Recent")
			e.Appearance.DrawString(e.Cache, text, bounds)
			Dim size As SizeF = e.Appearance.CalcTextSize(e.Cache, text, bounds.Width)
			bounds.Offset(size.ToSize().Width, 0)
			bounds.Width -= size.ToSize().Width
			bounds.Inflate(-2, 0)
			Dim point1 As New Point(bounds.X, bounds.Y + bounds.Height \ 2)
			Dim point2 As New Point(bounds.Right, bounds.Y + bounds.Height \ 2)
			e.Cache.DrawLine(e.Cache.GetPen(e.Appearance.GetForeColor()), point1, point2)
			e.Handled = True
		End Sub

		Private _RecentItems As New BindingList(Of GridRecentItem)()
		Private ReadOnly _View As GridView
		Public Property RecentItems() As BindingList(Of GridRecentItem)
			Get
				Return _RecentItems
			End Get
			Set(ByVal value As BindingList(Of GridRecentItem))
				_RecentItems = value
			End Set
		End Property
	End Class
End Namespace
