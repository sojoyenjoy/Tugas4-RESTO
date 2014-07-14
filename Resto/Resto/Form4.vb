Imports MySql.Data.MySqlClient
Public Class Form4
    Dim serv As String = "Server=localhost" & ";"
    Dim dbase As String = "Database=resto" & ";"
    Dim uid As String = "user=root" & ";"
    Dim pwd As String = "pwd=sojoyenjoy" & ";"
    Dim Kueri As String
    Dim Source = serv & dbase & uid & pwd & "default command timeout=3600; Allow User Variables=true"
    Public Koneksi As New MySqlConnection(Source)
    Public mycommand As MySqlCommand
    Public Adapter As MySqlDataAdapter
    Public DS As DataSet
    Public DR As MySqlDataReader
    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Set1()
    End Sub
    Sub Set1()
        BukaDB()
        TampilData()
        LabelFooter()
    End Sub
    Sub BukaDB()
        If Koneksi.State = ConnectionState.Closed Then
            Koneksi.Open()
        End If
    End Sub
    Sub LoadDataKueri()
        Adapter = New MySqlDataAdapter(Kueri, Koneksi)
        DS = New DataSet
        Adapter.Fill(DS, "Data")
    End Sub
    Sub TampilData()
        Kueri = "Select a.Id,b.Nama AS Kategori,a.Nama,a.Harga From Tabelmenu a LEFT JOIN Kategori b ON a.Kategori_id=b.ID"
        LoadDataKueri()
        DataGridView1.DataSource = DS.Tables("Data")
        DataGridView1.Columns(0).Width = 70
        DataGridView1.Columns(1).Width = 80
    End Sub
    Sub LabelFooter()
        Label1.Text = " Copyright © 2014."
        Label2.Text = "    Designed by"
        Label3.Text = " Adham Hayukalbu"
    End Sub
End Class