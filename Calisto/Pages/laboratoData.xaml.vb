Imports System.Data
Imports System.ServiceModel
Imports wsCalistoProxy.IwsCalistoClient
Imports System.Windows.FrameworkElement

Class laboratoData

    Private inicio As Integer
    Private sCnn As String
    Private compresion As Compresion.Compresion
    Private appProxy As wsCalistoProxy.IwsCalistoClient
    Private sCadena As String
    Private nId_archivo As Integer
    Private bOk As Boolean
    'Private dtcLaboratio As datacontext

    Private Sub Cargar_lista(sDesde As String, sHasta As String)

        Dim ds As DataSet
        Dim sds As String
        Dim sFecha As String

        ds = New DataSet
        sds = String.Empty
        sFecha = String.Empty
        appProxy = New wsCalistoProxy.IwsCalistoClient
        compresion = New Compresion.Compresion

        Try
            sds = appProxy.get_data_Laboratorio(sDesde, sHasta)
        Catch ex As Exception
            sds = String.Empty
        Finally
            'Mensaje de error
        End Try

        If sds <> String.Empty Then
            Try
                ds = compresion.DescomprimirDataset(sds)
            Catch ex As Exception
                sds = String.Empty
            Finally
            End Try
        End If

        If sds <> String.Empty Then
            dpnlLaboratorio.DataContext = ds
            dgvLaboratorio.DataContext = ds

            'dg_Laboratorio.DataContext
            'dgvLaboratorio.DataContext =
            '.DataSource = Nothing
            'ds.Tables(0).DefaultView.Sort = " ingreso DESC"
            'gv_data.DataSource = ds.Tables(0)
            'gv_data.DataBind()
        End If

    End Sub



End Class
