Imports MySql.Data.MySqlClient
Public Class Form2
    Dim serv As String = "Server=localhost" & ";"
    Dim dbase As String = "Database=resto" & ";"
    Dim uid As String = "user=root" & ";"
    Dim pwd As String = "pwd=sojoyenjoy" & ";"
    Dim Kueri As String
    Dim Source = serv & dbase & uid & pwd & "default command timeout=3600; Allow User Variables=true;"
    Public Koneksi As New MySqlConnection(Source)
    Public mycommand As New MySqlCommand
    Public Adapter As MySqlDataAdapter
    Public DS As DataSet
    Public DR As MySqlDataReader
    Private Sub Form2_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Form1.Show()
    End Sub
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Set1()
    End Sub
    'sub inisialisasi set1 berisi , sub labelfooter, set tampilan textbox , dis-enabled function dan buka koneksi database
    Sub Set1()
        LabelFooter()
        TextBoxSet()
        DisEnabled()
        BukaDB()
    End Sub
    'sub dis-enabled function , tampilan button input , set tampilan textbox , clearteks dan labelfooter
    Sub DisEnabled()
        If BtnInput.BackColor = Color.BurlyWood Then
            TextBox1.Enabled = False
            ComboBox1.Enabled = False
            TextBox2.Enabled = False
            TextBox4.Enabled = False
            BtnSave.Enabled = False
            BtnEdit.Enabled = False
            BtnDelete.Enabled = False
        Else
            ComboBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox4.Enabled = True
        End If
    End Sub
    Sub BtnInputSet()
        If BtnInput.BackColor = Color.Chocolate Then
            B1.BackColor = Color.Chocolate
            B2.BackColor = Color.Chocolate
            B3.BackColor = Color.Chocolate
            B5.BackColor = Color.Chocolate
            BtnSave.BackColor = Color.Chocolate
            BtnEdit.BackColor = Color.Chocolate
            BtnDelete.BackColor = Color.Chocolate
            BtnSave.ForeColor = Color.White
            BtnEdit.ForeColor = Color.White
            BtnDelete.ForeColor = Color.White
        Else
            B1.BackColor = Color.BurlyWood
            B2.BackColor = Color.BurlyWood
            B3.BackColor = Color.BurlyWood
            B5.BackColor = Color.BurlyWood
            BtnSave.BackColor = Color.BurlyWood
            BtnEdit.BackColor = Color.BurlyWood
            BtnDelete.BackColor = Color.BurlyWood
            BtnSave.ForeColor = Color.Brown
            BtnEdit.ForeColor = Color.Brown
            BtnDelete.ForeColor = Color.Brown
        End If
    End Sub
    Sub ClearTeks()
        TextBox1.Text = ""
        ComboBox1.Text = ""
        TextBox2.Text = ""
        TextBox4.Text = ""
    End Sub
    Sub TextBoxSet()
        TxtCari.AppendText("Cari Berdasarkan ..")
        TextBox1.AppendText(" - ID -")
        ComboBox1.Text = " - Kategori - "
        TextBox2.AppendText(" - Nama - ")
        TextBox4.AppendText(" - Harga - ")
    End Sub
    Sub LabelFooter()
        Label1.Text = " Copyright © 2014."
        Label2.Text = "    Designed by"
        Label3.Text = " Adham Hayukalbu"
    End Sub
    'sub buka koneksi , loaddatakueri , browsing teks untuk pencarian dan beberapa list data 
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
    Sub BrowsingTeks()
        Kueri = "Select a.Id,b.Nama,a.Nama,a.Harga from Tabelmenu a LEFT JOIN Kategori b ON a.Kategori_id = b.id Where a.ID Like '%" & TxtCari.Text & "%' OR a.Nama Like '%" & TxtCari.Text & "%' OR a.Harga Like '%" & TxtCari.Text & "%'"
        LoadDataKueri()
        TextBox1.Text = DS.Tables("Data").Rows(0).Item(0).ToString()
        ComboBox1.Text = DS.Tables("Data").Rows(0).Item(1).ToString()
        TextBox2.Text = DS.Tables("Data").Rows(0).Item(2).ToString()
        TextBox4.Text = DS.Tables("Data").Rows(0).Item(3).ToString()
    End Sub
    Sub Kategori()
        Kueri = "Select ID, Nama from Kategori"
        LoadDataKueri()
        ComboBox1.DataSource = DS.Tables("Data")
        ComboBox1.DisplayMember = "Nama"
        ComboBox1.ValueMember = "ID"
    End Sub
    Sub TeksRefresh()
        If TxtCari.Text = "" Then
            TxtCari.AppendText("Cari Berdasarkan ..")
        ElseIf TextBox1.Text = "" Then
            TextBox1.AppendText(" - ID -")
        ElseIf TextBox2.Text = "" Then
            TextBox2.AppendText(" - Nama - ")
        ElseIf TextBox4.Text = "" Then
            TextBox4.AppendText(" - Harga - ")
        End If
    End Sub
    'Sub Menu Id untuk menampilkan auto increment primary key
    Sub Menu_Id()
        mycommand = New MySqlCommand("Select ID From Tabelmenu", Koneksi)
        DR = mycommand.ExecuteReader
        Try
            Dim X, Y As Integer
            X = 0
            While DR.Read
                TextBox1.Text = DR.Item(0)
                If TextBox1.TextLength = 8 Then
                    Y = ((TextBox1.Text.Substring(5, 3)) + 1)
                ElseIf TextBox1.TextLength = 7 Then
                    Y = ((TextBox1.Text.Substring(5, 2)) + 1)
                Else
                    Y = ((TextBox1.Text.Substring(5, 1)) + 1)
                End If
                X = X + 1
            End While

            Select Case X
                Case Is = 0
                    TextBox1.Text = "IDMN-1"
                Case Is > 0
                    TextBox1.Text = "IDMN-" & Y & ""
            End Select
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    'Sub Button Function list
    Private Sub BtnInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnInput.Click
        BtnInput.BackColor = Color.Chocolate
        BtnInputSet()
        BtnSave.Enabled = True
        DisEnabled()
        Kategori()
        Menu_Id()
    End Sub
    Private Sub BtnInput_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnInput.MouseEnter
        ToolTip1.SetToolTip(BtnInput, "Aktifkan Button untuk melakukan Input Data")
    End Sub
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try
            BtnSave.BackColor = Color.BurlyWood
            If BtnSave.BackColor = Color.BurlyWood Then
                BtnInput.BackColor = Color.BurlyWood
                BtnInputSet()
                DisEnabled()
            End If
            With mycommand
                .CommandText = "Insert into Tabelmenu(ID,Kategori_id,Nama,Harga) Values(@id,@K,@N,@H)"
                .Parameters.AddWithValue("@id", Me.TextBox1.Text)
                .Parameters.AddWithValue("@K", Me.ComboBox1.SelectedValue)
                .Parameters.AddWithValue("@N", Me.TextBox2.Text)
                .Parameters.AddWithValue("@H", Me.TextBox4.Text)
                .Connection = Koneksi
                .ExecuteNonQuery()
            End With
            MsgBox("Data Tersimpan")
            mycommand.Parameters.Clear()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        mycommand.Dispose()
        ClearTeks()
    End Sub
    Private Sub BtnSave_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.MouseEnter
        BtnSave.ForeColor = Color.Brown
    End Sub
    Private Sub BtnSave_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.MouseLeave
        BtnSave.ForeColor = Color.White
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            BtnEdit.BackColor = Color.BurlyWood
            If BtnEdit.BackColor = Color.BurlyWood Then
                BtnInput.BackColor = Color.BurlyWood
                BtnInputSet()
                DisEnabled()
            End If
            With mycommand
                .CommandText = "Update Tabelmenu set ID='" & TextBox1.Text & "',Kategori_id='" & ComboBox1.SelectedValue & "',Nama='" & TextBox2.Text & "',Harga='" & TextBox4.Text & "' Where ID='" & TextBox1.Text & "'"
                .Connection = Koneksi
                .ExecuteNonQuery()
            End With
            MsgBox("Data Telah DiUbah")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        mycommand.Dispose()
        ClearTeks()
    End Sub
    Private Sub BtnEdit_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEdit.MouseEnter
        BtnEdit.ForeColor = Color.Brown
    End Sub
    Private Sub BtnEdit_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEdit.MouseLeave
        BtnEdit.ForeColor = Color.White
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            BtnDelete.BackColor = Color.BurlyWood
            If BtnDelete.BackColor = Color.BurlyWood Then
                BtnInput.BackColor = Color.BurlyWood
                BtnInputSet()
                DisEnabled()
            End If
            With mycommand
                .CommandText = "Delete From Tabelmenu Where ID=@id"
                .Parameters.AddWithValue("@id", Me.TextBox1.Text)
                .Connection = Koneksi
                .ExecuteNonQuery()
            End With
            MsgBox("Data Telah Dihapus")
            mycommand.Parameters.Clear()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        mycommand.Dispose()
        ClearTeks()
    End Sub
    Private Sub BtnDelete_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.MouseEnter
        BtnDelete.ForeColor = Color.Brown
    End Sub
    Private Sub BtnDelete_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.MouseLeave
        BtnDelete.ForeColor = Color.White
    End Sub
    Private Sub Go_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Go.Click
        BtnInput.BackColor = Color.Chocolate
        BrowsingTeks()
        BtnInputSet()
        DisEnabled()
        BtnSave.Enabled = False
        If TextBox1.Text = DS.Tables("Data").Rows(0).Item(0).ToString() Then
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
        End If
    End Sub
    Private Sub BHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BHome.Click
        Form1.Show()
        Me.Visible = False
    End Sub
    Private Sub BtnTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTrans.Click
        Form3.Show()
        Me.Visible = False
    End Sub
    Private Sub BtnTrans_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnTrans.MouseEnter
        ToolTip1.SetToolTip(BtnTrans, "Aktifkan Button untuk melakukan Transaksi Data")
    End Sub
    'Sub Teks ResetText list
    Private Sub TxtCari_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TxtCari.MouseClick
        TxtCari.ResetText()
        ToolTip1.SetToolTip(TxtCari, "Anda dapat mencari berdasarkan ID atau Nama Makanan/Minuman")
    End Sub
    Private Sub TextBox1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.ResetText()
    End Sub
    Private Sub TextBox2_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox2.MouseClick
        TextBox2.ResetText()
    End Sub
    Private Sub TextBox4_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox4.MouseClick
        TextBox4.ResetText()
    End Sub
    'Sub PictureBox , AppendText to TextBox list
    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        TeksRefresh()
        BView.BackColor = Color.BurlyWood
        BtnInput.BackColor = Color.BurlyWood
        BtnInputSet()
        DisEnabled()
    End Sub
    Private Sub TextBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Leave
        TeksRefresh()
    End Sub
    Private Sub TxtCari_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCari.Leave
        TeksRefresh()
    End Sub
    Private Sub TextBox2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
        TeksRefresh()
    End Sub
    Private Sub TextBox4_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox4.Leave
        TeksRefresh()
    End Sub
    Private Sub BView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BView.Click
        BView.BackColor = Color.Chocolate
        Form4.Show()
    End Sub
    Private Sub BView_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BView.Leave
        BView.BackColor = Color.BurlyWood
    End Sub
    Private Sub BView_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BView.MouseEnter
        ToolTip1.SetToolTip(BView, "Untuk melihat Tabelmenu")
    End Sub
End Class