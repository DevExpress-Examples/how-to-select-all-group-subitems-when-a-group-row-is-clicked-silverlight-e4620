Imports Microsoft.VisualBasic
Imports System.Windows
Imports System.Windows.Controls

Namespace dxExample
	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub OnMainPageLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Me.grid.ItemsSource = SampleDataRow.CreateRows()
		End Sub
	End Class
End Namespace
