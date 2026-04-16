Partial Class MLLDataSet
    Partial Public Class Loaders_Log_FirearmsDataTable
        Private Sub Loaders_Log_FirearmsDataTable_Loaders_Log_FirearmsRowChanging(sender As Object, e As BSMyLoadersLog.MLLDataSet.Loaders_Log_FirearmsRowChangeEvent) Handles Me.Loaders_Log_FirearmsRowChanging

        End Sub

    End Class

    Partial Class ConfigList_Simple_SGDataTable

        Private Sub ConfigList_Simple_SGDataTable_ConfigList_Simple_SGRowChanging(ByVal sender As System.Object, ByVal e As ConfigList_Simple_SGRowChangeEvent) Handles Me.ConfigList_Simple_SGRowChanging

        End Sub

    End Class

    Partial Class Config_List_Powder_Data_SG_ViewDataTable

        Private Sub Config_List_Powder_Data_SG_ViewDataTable_Config_List_Powder_Data_SG_ViewRowChanging(ByVal sender As System.Object, ByVal e As Config_List_Powder_Data_SG_ViewRowChangeEvent) Handles Me.Config_List_Powder_Data_SG_ViewRowChanging

        End Sub

    End Class

    Partial Class Gun_Collection_AmmoDataTable

        Private Sub Gun_Collection_AmmoDataTable_Gun_Collection_AmmoRowChanging(ByVal sender As System.Object, ByVal e As Gun_Collection_AmmoRowChangeEvent) Handles Me.Gun_Collection_AmmoRowChanging

        End Sub

    End Class

    Partial Class List_Bullets_DetailsDataTable

        Private Sub List_Bullets_DetailsDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.PriceColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class


