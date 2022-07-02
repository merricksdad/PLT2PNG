Imports System.Configuration

Public Class Form1
    Public Sub GetFiles(sPath As String)
        Dim inf As New System.IO.DirectoryInfo(sPath)
        For Each file In inf.GetFiles()
            If file.Extension.ToLower() = ".plt" Then
                FileBox.Items.Add(file.Name)
            End If
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BrowseButton.Click
        FolderBrowser.ShowDialog()
        Dim folder = FolderBrowser.SelectedPath
        ''My.Settings.Item("workPath") = folder
        PathBox.Text = folder
    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed

    End Sub

    Sub AddUpdateAppSettings(key As String, value As String)
        Try
            Dim configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            Dim settings = configFile.AppSettings.Settings
            If IsNothing(settings(key)) Then
                settings.Add(key, value)
            Else
                settings(key).Value = value
            End If
            configFile.Save(ConfigurationSaveMode.Modified)
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name)
        Catch e As ConfigurationErrorsException
            Console.WriteLine("Error writing app settings")
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sPath = ConfigurationManager.AppSettings.Get("workPath")
        If Not sPath Is Nothing Then
            Dim dir = New IO.DirectoryInfo(sPath)
            If dir.Exists Then PathBox.Text = sPath
        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles WidthUpDown.ValueChanged
        WidthBox.Text = 2 ^ WidthUpDown.Value
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles HeightUpDown.ValueChanged
        HeightBox.Text = 2 ^ HeightUpDown.Value
    End Sub

    Private Sub NumericUpDown3_ValueChanged(sender As Object, e As EventArgs) Handles ScaleUpDown.ValueChanged
        Dim v = ScaleUpDown.Value
        If v >= 0 Then
            ScaleBox.Text = (2 ^ v)
        Else
            ScaleBox.Text = 1 / (2 ^ Math.Abs(v))
        End If
    End Sub

    Private Sub PathBox_TextChanged(sender As Object, e As EventArgs) Handles PathBox.TextChanged
        AddUpdateAppSettings("workPath", PathBox.Text)
        GetFiles(PathBox.Text)

    End Sub

    Private Sub ConvertToPNG(sender As Object, e As EventArgs) Handles ConvertButton.Click
        Dim pltFile As New clsPLT
        OutBox.Text = "Starting conversion"
        For Each sFile In FileBox.SelectedItems
            If pltFile.Open(PathBox.Text & "\" & sFile) Then
                If pltFile.ConvertToPNG(DoColorize.Checked) Then
                    OutBox.AppendText(vbCrLf & "..converted " & sFile)
                Else
                    OutBox.AppendText(vbCrLf & "..failed to convert " & sFile)
                End If
            Else
                OutBox.AppendText(vbCrLf & "..failed to open " & sFile)
            End If
        Next
        OutBox.AppendText(vbCrLf & "done")
        pltFile = Nothing
    End Sub

    Private Sub ResizeButton_Click(sender As Object, e As EventArgs) Handles ResizeButton.Click
        Dim pltFile As New clsPLT
        OutBox.Text = "Starting rescale"
        For Each sFile In FileBox.SelectedItems
            If pltFile.Open(PathBox.Text & "\" & sFile) Then
                Dim x As Integer, y As Integer
                If DoResize.Checked Then
                    x = Val(WidthBox.Text)
                    y = Val(HeightBox.Text)
                ElseIf DoRescale.checked Then
                    x = Val(ScaleBox.Text) * pltFile.Width
                    y = Val(ScaleBox.Text) * pltFile.Height
                End If
                Dim scaleMode = 0
                If DoBilinear.Checked Then scaleMode = 1
                If pltFile.Rescale(x, y, scaleMode:=scaleMode) Then
                    OutBox.AppendText(vbCrLf & "..rescaled " & sFile & " to " & x & "x" & y)
                    pltFile.Save()
                Else
                    OutBox.AppendText(vbCrLf & "..failed to rescale " & sFile)
                    pltFile.Save()
                End If
            Else
                OutBox.AppendText(vbCrLf & "..failed to open " & sFile)
            End If
        Next
        OutBox.AppendText(vbCrLf & "done")
        pltFile = Nothing
    End Sub
End Class


