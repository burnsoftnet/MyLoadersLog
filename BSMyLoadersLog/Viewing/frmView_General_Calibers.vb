Imports System.ComponentModel

Namespace Viewing

    Public Class FrmViewGeneralCalibers
        ''' <summary>
        ''' The update pending
        ''' </summary>
        Public UpdatePending As Boolean
        ''' <summary>
        ''' Handles the Load event of the frmView_General_Calibers control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub frmView_General_Calibers_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                General_CalibersTableAdapter.Fill(MLLDataSet.General_Calibers)
            Catch ex As Exception
                Call LogError(Name, "Load", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the ListChanged event of the GeneralCalibersBindingSource control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="ListChangedEventArgs"/> instance containing the event data.</param>
        Private Sub GeneralCalibersBindingSource_ListChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs) Handles GeneralCalibersBindingSource.ListChanged
            Try
                If MLLDataSet.HasChanges Then
                    UpdatePending = True
                End If
            Catch ex As Exception
                Call LogError(Name, "GeneralCalibersBindingSource_ListChanged", Err.Number, ex.Message.ToString)
            End Try
        End Sub
        ''' <summary>
        ''' Handles the RowValidated event of the DataGridView1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        Private Sub DataGridView1_RowValidated(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView1.RowValidated
            Try
                If UpdatePending Then
                    General_CalibersTableAdapter.Update(MLLDataSet.General_Calibers)
                    UpdatePending = False
                End If
            Catch ex As Exception
                Call LogError(Name, "DataGridView1_RowValidated", Err.Number, ex.Message.ToString)
            End Try
        End Sub
    End Class
End NameSpace