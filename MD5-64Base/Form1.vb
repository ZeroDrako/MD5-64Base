Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class Form1

    Private Sub md5base64()
        If OpenFileDialog1.FileName = "" Then Exit Sub

        Dim RD As FileStream = New FileStream(TextBox1.Text, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
        RD = New FileStream(TextBox1.Text, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
        Dim md5 As MD5CryptoServiceProvider = New MD5CryptoServiceProvider
        md5.ComputeHash(RD)
        RD.Close()

        Dim hash As Byte() = md5.Hash
        Dim SB As StringBuilder = New StringBuilder
        Dim HB As Byte
        For Each HB In hash
            SB.Append(String.Format("{0:X1}", HB))
        Next
        Dim filemd5 As String = SB.ToString()
        TextBox2.Text = filemd5
        TextBox3.Text = Convert.ToBase64String(hash)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.Filter = "All Files (*.*)|*.*"
        If (OpenFileDialog1.ShowDialog() = DialogResult.OK) Then TextBox1.Text = OpenFileDialog1.FileName
        md5base64()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        My.Computer.Clipboard.SetText(TextBox2.Text)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        My.Computer.Clipboard.SetText(TextBox3.Text)
    End Sub

    Private Sub TextBox1_DragDrop(sender As Object, e As DragEventArgs) Handles TextBox1.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each path In files
            TextBox1.Text = path
        Next
    End Sub

    Private Sub TextBox1_DragEnter(sender As Object, e As DragEventArgs) Handles TextBox1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        md5base64()
    End Sub
End Class
