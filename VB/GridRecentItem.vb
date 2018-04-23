Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.ComponentModel

Namespace WindowsApplication1
	Public Class GridRecentItem
		Public Sub New(ByVal caption As String, ByVal itemImage As Image, ByVal isPinned As Boolean)
			Me.Caption = caption
			Image = itemImage
			Me.IsPinned = isPinned
		End Sub

		Private _IsPinned As Boolean
		Public Property IsPinned() As Boolean
			Get
				Return _IsPinned
			End Get
			Set(ByVal value As Boolean)
				_IsPinned = value
			End Set
		End Property


		Private _Caption As String
		Public Property Caption() As String
			Get
				Return _Caption
			End Get
			Set(ByVal value As String)
				_Caption = value
			End Set
		End Property

		Private _Image As Image
		Public Property Image() As Image
			Get
				Return _Image
			End Get
			Set(ByVal value As Image)
				_Image = value
			End Set
		End Property

		Public ReadOnly Property PinnedImage() As Object
			Get
				Return IsPinned
			End Get
		End Property


	End Class
End Namespace
