Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Namespace WindowsApplication1
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Dim helper As New GridRecentItemsHelper(gridView1)
			helper.RecentItems.Add(New GridRecentItem("WindowsFormsApplication1", My.Resources.vs2010, False))
			helper.RecentItems.Add(New GridRecentItem("WindowsFormsApplication2", My.Resources.vs2010, True))
			helper.RecentItems.Add(New GridRecentItem("WindowsFormsApplication3", My.Resources.vs2010, False))
		End Sub
	End Class
End Namespace