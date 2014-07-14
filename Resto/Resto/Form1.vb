Imports MySql.Data.MySqlClient
Public Class Form1
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
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetView()
        BukaDB()
    End Sub
    'koneksi
    Sub BukaDB()
        If Koneksi.State = ConnectionState.Closed Then
            Koneksi.Open()
        End If
    End Sub
    'pencocokan user login
    Sub User()
        mycommand = New MySqlCommand("Select Username,Password,Level_id From User", Koneksi)
        DR = mycommand.ExecuteReader
        Dim x As Integer
        While DR.Read
            x = DR.Item(2).ToString()
            If TextBox1.Text = DR.Item(0).ToString() And TextBox2.Text = DR.Item(1).ToString() And x = 1 Then
                Form2.Show()
                Me.Visible = False
            ElseIf TextBox1.Text = DR.Item(0).ToString() And TextBox2.Text = DR.Item(1).ToString() And x > 1 Then
            ElseIf TextBox1.Text = "" Or TextBox2.Text = "" Then
                MsgBox("Masukkan ID Login Anda")
            Else
                MsgBox("Username dan Password yang kamu masukan mismatch")
            End If
        End While
        DR.Close()
    End Sub
    'keterangan teks tampilan
    Sub SetView()
        Label1.Text = "Copyright © 2014. Resto.com | Designed by Adham Hayukalbu"
        Label2.Text = "Tanggal : " & Format(Now, "dd-MMMM-yyyy") & ""
        TextBox2.PasswordChar = "*"
        TxtCari.AppendText("Search ...")
    End Sub
    'sub button,textbox,picture click list
    Private Sub BHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BHome.Click
        Me.Focus()
    End Sub
    Private Sub BLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BLogin.Click
        User()
    End Sub
    Private Sub TxtCari_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCari.Leave
        If TxtCari.Text = "" Then
            TxtCari.AppendText("Search ...")
        End If
    End Sub
    Private Sub TxtCari_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TxtCari.MouseClick
        TxtCari.ResetText()
    End Sub
    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        If TxtCari.Text = "" Then
            TxtCari.AppendText("Search ...")
        End If
    End Sub
    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        If TxtCari.Text = "" Then
            TxtCari.AppendText("Search ...")
        End If
    End Sub
    Private Sub BTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTrans.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Masukkan ID Login Anda")
        Else
            User()
        End If
    End Sub
    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            BLogin.PerformClick()
        End If
    End Sub
End Class
