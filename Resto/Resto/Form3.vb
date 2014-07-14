Imports MySql.Data.MySqlClient
Public Class Form3
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
    Private Sub Form3_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Form1.Show()
    End Sub
    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Set1()
    End Sub
    Private Sub BtnInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnInput.Click
        Form2.Show()
        Me.Visible = False
    End Sub
    'sub inisialisasi set1 berisi , sub labelfooter, set tampilan textbox , dis-enabled function dan buka koneksi database
    Sub Set1()
        TextBoxSet()
        LabelFooter()
        DisEnabled()
        BukaDB()
    End Sub
    Sub LabelFooter()
        Label1.Text = " Copyright © 2014."
        Label2.Text = "    Designed by"
        Label3.Text = " Adham Hayukalbu"
        Label4.Text = "Jml Item : "
        Label5.Text = "  Tot Bayar : "
        Label6.Text = " Cash       : "
        Label7.Text = " Kembali : "
        Label8.Text = "Rp.0"
    End Sub
    Sub DisEnabled()
        If BtnTrans.BackColor = Color.BurlyWood Then
            TextBox1.Enabled = False
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            BtnSave.Enabled = False
        End If
    End Sub
    Sub BtnTransSet()
        If BtnTrans.BackColor = Color.Chocolate Then
            B1.BackColor = Color.Chocolate
            B2.BackColor = Color.Chocolate
            BtnSave.BackColor = Color.BurlyWood
            BtnSave.ForeColor = Color.Brown
            BtnDelete.BackColor = Color.BurlyWood
            BtnDelete.ForeColor = Color.Brown
        Else
            B1.BackColor = Color.BurlyWood
            B2.BackColor = Color.BurlyWood
        End If
    End Sub
    Sub ClearTeks()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        Label8.Text = "Rp.0"
        ComboBox1.Text = ""
        DataGridView1.DataMember = Nothing
    End Sub
    Sub TextBoxSet()
        TxtCari.AppendText("Cari Berdasarkan ..")
        TextBox4.TextAlign = HorizontalAlignment.Center
        TextBox4.AppendText("Rp.")
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
    Sub Order()
        Kueri = "Select ID,Nama From Tabelmenu "
        LoadDataKueri()
        ComboBox1.DataSource = DS.Tables("Data")
        ComboBox1.DisplayMember = "Nama"
        ComboBox1.ValueMember = "ID"
    End Sub
    Sub TeksRefresh()
        If TxtCari.Text = "" Then
            TxtCari.AppendText("Cari Berdasarkan ..")
        End If
    End Sub
    Sub No_Trans()
        mycommand = New MySqlCommand("Select No_Trans From Transaksi", Koneksi)
        DR = mycommand.ExecuteReader
        Try
            Dim X, Y As Integer
            X = 0
            While DR.Read
                TextBox1.Text = DR.Item(0)
                If TextBox1.TextLength = 9 Then
                    Y = ((TextBox1.Text.Substring(6, 3)) + 1)
                ElseIf TextBox1.TextLength = 8 Then
                    Y = ((TextBox1.Text.Substring(6, 2)) + 1)
                Else
                    Y = ((TextBox1.Text.Substring(6, 1)) + 1)
                End If
                X = X + 1
            End While

            Select Case X
                Case Is = 0
                    TextBox1.Text = " TRKD-1"
                Case Is > 0
                    TextBox1.Text = " TRKD-" & Y & ""
            End Select
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Sub HitungTotalItem()
        Dim X As Integer
        For i = 0 To DataGridView1.RowCount - 2
            X = X + DataGridView1.Rows(i).Cells(2).Value
        Next
        TextBox2.TextAlign = HorizontalAlignment.Center
        TextBox2.Text = X.ToString()
    End Sub
    Sub HitungTotalBayar()
        Dim X As Integer
        For i = 0 To DataGridView1.RowCount - 2
            X = X + DataGridView1.Rows(i).Cells(3).Value
        Next
        TextBox3.TextAlign = HorizontalAlignment.Center
        TextBox3.Text = "Rp." & X.ToString & ""
    End Sub
    Private Sub BtnTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTrans.Click
        BtnTrans.BackColor = Color.Chocolate
        BtnTransSet()
        DisEnabled()
        Order()
        No_Trans()
        BtnSave.Enabled = True
        BtnDelete.Enabled = True
    End Sub
    Private Sub BtnTrans_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnTrans.MouseEnter
        ToolTip1.SetToolTip(BtnTrans, "Aktifkan Button untuk melakukan Transaksi Data")
    End Sub
    Private Sub BtnADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnADD.Click
        Dim i As Integer = DataGridView1.Rows.Add() 'menambah setiap baris DGV disimpan dalam var i tipe integer
        DataGridView1.Rows.Item(i).Cells(0).Value = ComboBox1.Text
    End Sub
    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim Baris As Integer
            Baris = e.RowIndex - 1
            If e.ColumnIndex = 1 Then
                mycommand = New MySqlCommand("SELECT Harga from Tabelmenu WHERE Nama = '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value & "'", Koneksi)
                DR = mycommand.ExecuteReader
                If DR.Read Then
                    DataGridView1.Rows(e.RowIndex).Cells(1).Value = DR.Item("Harga")
                    DataGridView1.Rows(e.RowIndex).Cells(2).Value = 0
                    DataGridView1.Rows(e.RowIndex).Cells(3).Value = 0
                Else
                    DataGridView1.Focus()
                End If
                DR.Close()
            End If
            If e.ColumnIndex = 3 Then
                DataGridView1.Rows(e.RowIndex).Cells(3).Value = (DataGridView1.Rows(e.RowIndex).Cells(1).Value * DataGridView1.Rows(e.RowIndex).Cells(2).Value)
                ComboBox1.Focus()
            End If
        End If
        HitungTotalItem()
        HitungTotalBayar()
    End Sub
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try
            BtnSave.BackColor = Color.BurlyWood
            If BtnSave.BackColor = Color.BurlyWood Then
                BtnTrans.BackColor = Color.BurlyWood
                BtnTransSet()
                DisEnabled()
            End If
            For i = 0 To DataGridView1.RowCount - 2
                With mycommand
                    .CommandText = "Insert into Transaksi(No_trans,Order_id,JumlahBeli,JmlItem,TotalBayar) Values(@NT,@O,@JB,@JI,@TB)"
                    .Parameters.AddWithValue("@NT", Me.TextBox1.Text)
                    .Parameters.AddWithValue("@O", Me.DataGridView1.Rows(i).Cells(0).Value)
                    .Parameters.AddWithValue("@JB", Me.DataGridView1.Rows(i).Cells(2).Value)
                    .Parameters.AddWithValue("@JI", Me.TextBox2.Text)
                    .Parameters.AddWithValue("@TB", Me.TextBox3.Text)
                    .Connection = Koneksi
                    .ExecuteNonQuery()
                End With
                mycommand.Parameters.Clear()
            Next
            MsgBox("Data Tersimpan")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        mycommand.Dispose()
        ClearTeks()
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        DataGridView1.Rows.RemoveAt(DataGridView1.CurrentRow.Index)
        HitungTotalBayar()
        HitungTotalItem()
    End Sub
    Private Sub BtnDelete_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.MouseEnter
        BtnDelete.ForeColor = SystemColors.Info
        ToolTip1.SetToolTip(BtnDelete, "Hapus Item")
    End Sub
    Private Sub BtnDelete_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.MouseLeave
        BtnDelete.ForeColor = Color.Brown
    End Sub
    Private Sub BtnSave_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.MouseEnter
        BtnSave.ForeColor = SystemColors.Info
    End Sub
    Private Sub BtnSave_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.MouseLeave
        BtnSave.ForeColor = Color.Brown
    End Sub
    Private Sub TxtCari_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TxtCari.MouseClick
        TxtCari.ResetText()
        ToolTip1.SetToolTip(TxtCari, "Anda dapat mencari berdasarkan ID atau Nama Makanan/Minuman")
    End Sub
    Private Sub TextBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Leave
        TeksRefresh()
    End Sub
    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        TeksRefresh()
        BtnTrans.BackColor = Color.BurlyWood
        BtnTransSet()
        DisEnabled()
        ClearTeks()
    End Sub
    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        BtnTrans.BackColor = Color.BurlyWood
        BtnTransSet()
        DisEnabled()
        ClearTeks()
    End Sub
    Private Sub BHitung_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BHitung.Click
        Dim X, Y, Hasil As Integer
        If TextBox3.TextLength = 8 And TextBox4.TextLength = 9 Then
            X = TextBox3.Text.Substring(3, 5)
            Y = TextBox4.Text.Substring(3, 6)
            Hasil = Y - X
            Label8.Text = "Rp." & Hasil.ToString & ".-"
        ElseIf TextBox3.TextLength = 9 And TextBox4.TextLength = 9 Then
            X = TextBox3.Text.Substring(3, 6)
            Y = TextBox4.Text.Substring(3, 6)
            Hasil = Y - X
            Label8.Text = "Rp." & Hasil.ToString & ".-"
        Else
            X = TextBox3.Text.Substring(3, 5)
            Y = TextBox4.Text.Substring(3, 5)
            Hasil = Y - X
            Label8.Text = "Rp." & Hasil.ToString & ".-"
        End If
    End Sub
End Class