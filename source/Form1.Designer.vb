<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.FileBox = New System.Windows.Forms.ListBox()
        Me.PathBox = New System.Windows.Forms.TextBox()
        Me.FolderBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.BrowseButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ConvertButton = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.DoColorize = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ResizeButton = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ScaleBox = New System.Windows.Forms.TextBox()
        Me.ScaleUpDown = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.HeightBox = New System.Windows.Forms.TextBox()
        Me.HeightUpDown = New System.Windows.Forms.NumericUpDown()
        Me.WidthBox = New System.Windows.Forms.TextBox()
        Me.WidthUpDown = New System.Windows.Forms.NumericUpDown()
        Me.DoRescale = New System.Windows.Forms.RadioButton()
        Me.DoResize = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.DoBilinear = New System.Windows.Forms.CheckBox()
        Me.OutBox = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.ScaleUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HeightUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WidthUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'FileBox
        '
        Me.FileBox.FormattingEnabled = True
        Me.FileBox.Location = New System.Drawing.Point(12, 82)
        Me.FileBox.Name = "FileBox"
        Me.FileBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.FileBox.Size = New System.Drawing.Size(204, 355)
        Me.FileBox.TabIndex = 2
        '
        'PathBox
        '
        Me.PathBox.Location = New System.Drawing.Point(82, 35)
        Me.PathBox.Name = "PathBox"
        Me.PathBox.ReadOnly = True
        Me.PathBox.Size = New System.Drawing.Size(662, 20)
        Me.PathBox.TabIndex = 4
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "folderBrowse.png")
        '
        'BrowseButton
        '
        Me.BrowseButton.ImageIndex = 0
        Me.BrowseButton.ImageList = Me.ImageList1
        Me.BrowseButton.Location = New System.Drawing.Point(12, 12)
        Me.BrowseButton.Name = "BrowseButton"
        Me.BrowseButton.Size = New System.Drawing.Size(64, 64)
        Me.BrowseButton.TabIndex = 5
        Me.BrowseButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ConvertButton)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Location = New System.Drawing.Point(222, 82)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(355, 169)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Convert to PNG"
        '
        'ConvertButton
        '
        Me.ConvertButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.ConvertButton.Location = New System.Drawing.Point(265, 117)
        Me.ConvertButton.Name = "ConvertButton"
        Me.ConvertButton.Size = New System.Drawing.Size(75, 40)
        Me.ConvertButton.TabIndex = 10
        Me.ConvertButton.Text = "Convert"
        Me.ConvertButton.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.DoColorize)
        Me.GroupBox4.Location = New System.Drawing.Point(213, 19)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(133, 76)
        Me.GroupBox4.TabIndex = 9
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Options"
        '
        'DoColorize
        '
        Me.DoColorize.AutoSize = True
        Me.DoColorize.Location = New System.Drawing.Point(6, 19)
        Me.DoColorize.Name = "DoColorize"
        Me.DoColorize.Size = New System.Drawing.Size(97, 17)
        Me.DoColorize.TabIndex = 0
        Me.DoColorize.Text = "Colorize Layers"
        Me.DoColorize.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ResizeButton)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.ScaleBox)
        Me.GroupBox2.Controls.Add(Me.ScaleUpDown)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.HeightBox)
        Me.GroupBox2.Controls.Add(Me.HeightUpDown)
        Me.GroupBox2.Controls.Add(Me.WidthBox)
        Me.GroupBox2.Controls.Add(Me.WidthUpDown)
        Me.GroupBox2.Controls.Add(Me.DoRescale)
        Me.GroupBox2.Controls.Add(Me.DoResize)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Location = New System.Drawing.Point(222, 268)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(355, 169)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Resize"
        '
        'ResizeButton
        '
        Me.ResizeButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.ResizeButton.Location = New System.Drawing.Point(265, 121)
        Me.ResizeButton.Name = "ResizeButton"
        Me.ResizeButton.Size = New System.Drawing.Size(75, 40)
        Me.ResizeButton.TabIndex = 19
        Me.ResizeButton.Text = "Resize"
        Me.ResizeButton.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Scale"
        '
        'ScaleBox
        '
        Me.ScaleBox.Location = New System.Drawing.Point(51, 100)
        Me.ScaleBox.Name = "ScaleBox"
        Me.ScaleBox.ReadOnly = True
        Me.ScaleBox.Size = New System.Drawing.Size(36, 20)
        Me.ScaleBox.TabIndex = 17
        Me.ScaleBox.Text = "1"
        '
        'ScaleUpDown
        '
        Me.ScaleUpDown.Location = New System.Drawing.Point(88, 101)
        Me.ScaleUpDown.Maximum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.ScaleUpDown.Minimum = New Decimal(New Integer() {4, 0, 0, -2147483648})
        Me.ScaleUpDown.Name = "ScaleUpDown"
        Me.ScaleUpDown.Size = New System.Drawing.Size(17, 20)
        Me.ScaleUpDown.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(111, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Height"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Width"
        '
        'HeightBox
        '
        Me.HeightBox.Location = New System.Drawing.Point(153, 43)
        Me.HeightBox.Name = "HeightBox"
        Me.HeightBox.ReadOnly = True
        Me.HeightBox.Size = New System.Drawing.Size(36, 20)
        Me.HeightBox.TabIndex = 13
        Me.HeightBox.Text = "512"
        '
        'HeightUpDown
        '
        Me.HeightUpDown.Location = New System.Drawing.Point(190, 44)
        Me.HeightUpDown.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.HeightUpDown.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.HeightUpDown.Name = "HeightUpDown"
        Me.HeightUpDown.Size = New System.Drawing.Size(17, 20)
        Me.HeightUpDown.TabIndex = 12
        Me.HeightUpDown.Value = New Decimal(New Integer() {9, 0, 0, 0})
        '
        'WidthBox
        '
        Me.WidthBox.Location = New System.Drawing.Point(51, 42)
        Me.WidthBox.Name = "WidthBox"
        Me.WidthBox.ReadOnly = True
        Me.WidthBox.Size = New System.Drawing.Size(36, 20)
        Me.WidthBox.TabIndex = 11
        Me.WidthBox.Text = "512"
        '
        'WidthUpDown
        '
        Me.WidthUpDown.Location = New System.Drawing.Point(88, 43)
        Me.WidthUpDown.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.WidthUpDown.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.WidthUpDown.Name = "WidthUpDown"
        Me.WidthUpDown.Size = New System.Drawing.Size(17, 20)
        Me.WidthUpDown.TabIndex = 10
        Me.WidthUpDown.Value = New Decimal(New Integer() {9, 0, 0, 0})
        '
        'DoRescale
        '
        Me.DoRescale.AutoSize = True
        Me.DoRescale.Checked = True
        Me.DoRescale.Location = New System.Drawing.Point(15, 77)
        Me.DoRescale.Name = "DoRescale"
        Me.DoRescale.Size = New System.Drawing.Size(64, 17)
        Me.DoRescale.TabIndex = 9
        Me.DoRescale.TabStop = True
        Me.DoRescale.Text = "Rescale"
        Me.DoRescale.UseVisualStyleBackColor = True
        '
        'DoResize
        '
        Me.DoResize.AutoSize = True
        Me.DoResize.Location = New System.Drawing.Point(15, 19)
        Me.DoResize.Name = "DoResize"
        Me.DoResize.Size = New System.Drawing.Size(57, 17)
        Me.DoResize.TabIndex = 2
        Me.DoResize.Text = "Resize"
        Me.DoResize.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.DoBilinear)
        Me.GroupBox3.Location = New System.Drawing.Point(213, 19)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(133, 75)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Options"
        '
        'DoBilinear
        '
        Me.DoBilinear.AutoSize = True
        Me.DoBilinear.Location = New System.Drawing.Point(6, 19)
        Me.DoBilinear.Name = "DoBilinear"
        Me.DoBilinear.Size = New System.Drawing.Size(121, 17)
        Me.DoBilinear.TabIndex = 1
        Me.DoBilinear.Text = "Bilinear Interpolation"
        Me.DoBilinear.UseVisualStyleBackColor = True
        '
        'OutBox
        '
        Me.OutBox.Location = New System.Drawing.Point(583, 82)
        Me.OutBox.Multiline = True
        Me.OutBox.Name = "OutBox"
        Me.OutBox.ReadOnly = True
        Me.OutBox.Size = New System.Drawing.Size(161, 355)
        Me.OutBox.TabIndex = 8
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(756, 450)
        Me.Controls.Add(Me.OutBox)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BrowseButton)
        Me.Controls.Add(Me.PathBox)
        Me.Controls.Add(Me.FileBox)
        Me.Name = "Form1"
        Me.Text = "PLT Tools GUI"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.ScaleUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HeightUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WidthUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FileBox As ListBox
    Friend WithEvents PathBox As TextBox
    Friend WithEvents FolderBrowser As FolderBrowserDialog
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents BrowseButton As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents DoColorize As CheckBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents WidthBox As TextBox
    Friend WithEvents WidthUpDown As NumericUpDown
    Friend WithEvents DoRescale As RadioButton
    Friend WithEvents DoResize As RadioButton
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents DoBilinear As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents HeightBox As TextBox
    Friend WithEvents HeightUpDown As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents ScaleBox As TextBox
    Friend WithEvents ScaleUpDown As NumericUpDown
    Friend WithEvents OutBox As TextBox
    Friend WithEvents ConvertButton As Button
    Friend WithEvents ResizeButton As Button
End Class
