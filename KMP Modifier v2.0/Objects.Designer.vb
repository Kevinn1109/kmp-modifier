<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Objects
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Objects))
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.ComboBox5 = New System.Windows.Forms.ComboBox()
        Me.ComboBox6 = New System.Windows.Forms.ComboBox()
        Me.ComboBox7 = New System.Windows.Forms.ComboBox()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox8 = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox9 = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox10 = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox11 = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox12 = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox13 = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox14 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox15 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelS1 = New System.Windows.Forms.Label()
        Me.LabelS2 = New System.Windows.Forms.Label()
        Me.LabelS3 = New System.Windows.Forms.Label()
        Me.LabelS4 = New System.Windows.Forms.Label()
        Me.LabelS5 = New System.Windows.Forms.Label()
        Me.LabelS6 = New System.Windows.Forms.Label()
        Me.LabelS7 = New System.Windows.Forms.Label()
        Me.LabelS8 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboBox12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboBox14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ListView1.LargeImageList = Me.ImageList1
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(587, 122)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "kmp.ico")
        Me.ImageList1.Images.SetKeyName(1, "Plugin.ico")
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.CheckBox2)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 128)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(269, 225)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Information"
        '
        'CheckBox2
        '
        Me.CheckBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(6, 202)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(58, 17)
        Me.CheckBox2.TabIndex = 2
        Me.CheckBox2.Text = "Is solid"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(6, 179)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(84, 17)
        Me.CheckBox1.TabIndex = 1
        Me.CheckBox1.Text = "Needs route"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(6, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(257, 71)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Description"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(290, 180)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(132, 21)
        Me.ComboBox1.TabIndex = 3
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(440, 181)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(132, 21)
        Me.ComboBox2.TabIndex = 4
        '
        'ComboBox3
        '
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(290, 233)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(132, 21)
        Me.ComboBox3.TabIndex = 5
        '
        'ComboBox4
        '
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(440, 233)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(132, 21)
        Me.ComboBox4.TabIndex = 6
        '
        'ComboBox5
        '
        Me.ComboBox5.FormattingEnabled = True
        Me.ComboBox5.Location = New System.Drawing.Point(290, 285)
        Me.ComboBox5.Name = "ComboBox5"
        Me.ComboBox5.Size = New System.Drawing.Size(132, 21)
        Me.ComboBox5.TabIndex = 7
        '
        'ComboBox6
        '
        Me.ComboBox6.FormattingEnabled = True
        Me.ComboBox6.Location = New System.Drawing.Point(440, 285)
        Me.ComboBox6.Name = "ComboBox6"
        Me.ComboBox6.Size = New System.Drawing.Size(132, 21)
        Me.ComboBox6.TabIndex = 8
        '
        'ComboBox7
        '
        Me.ComboBox7.FormattingEnabled = True
        Me.ComboBox7.Location = New System.Drawing.Point(290, 337)
        Me.ComboBox7.Name = "ComboBox7"
        Me.ComboBox7.Size = New System.Drawing.Size(132, 21)
        Me.ComboBox7.TabIndex = 9
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(343, 128)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(132, 20)
        Me.NumericUpDown1.TabIndex = 3
        '
        'ComboBox8
        '
        Me.ComboBox8.Location = New System.Drawing.Point(290, 337)
        Me.ComboBox8.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ComboBox8.Name = "ComboBox8"
        Me.ComboBox8.Size = New System.Drawing.Size(132, 20)
        Me.ComboBox8.TabIndex = 16
        '
        'ComboBox9
        '
        Me.ComboBox9.Location = New System.Drawing.Point(440, 285)
        Me.ComboBox9.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ComboBox9.Name = "ComboBox9"
        Me.ComboBox9.Size = New System.Drawing.Size(132, 20)
        Me.ComboBox9.TabIndex = 15
        '
        'ComboBox10
        '
        Me.ComboBox10.Location = New System.Drawing.Point(290, 285)
        Me.ComboBox10.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ComboBox10.Name = "ComboBox10"
        Me.ComboBox10.Size = New System.Drawing.Size(132, 20)
        Me.ComboBox10.TabIndex = 14
        '
        'ComboBox11
        '
        Me.ComboBox11.Location = New System.Drawing.Point(440, 233)
        Me.ComboBox11.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ComboBox11.Name = "ComboBox11"
        Me.ComboBox11.Size = New System.Drawing.Size(132, 20)
        Me.ComboBox11.TabIndex = 13
        '
        'ComboBox12
        '
        Me.ComboBox12.Location = New System.Drawing.Point(290, 233)
        Me.ComboBox12.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ComboBox12.Name = "ComboBox12"
        Me.ComboBox12.Size = New System.Drawing.Size(132, 20)
        Me.ComboBox12.TabIndex = 12
        '
        'ComboBox13
        '
        Me.ComboBox13.Location = New System.Drawing.Point(440, 181)
        Me.ComboBox13.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ComboBox13.Name = "ComboBox13"
        Me.ComboBox13.Size = New System.Drawing.Size(132, 20)
        Me.ComboBox13.TabIndex = 11
        '
        'ComboBox14
        '
        Me.ComboBox14.Location = New System.Drawing.Point(290, 180)
        Me.ComboBox14.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ComboBox14.Name = "ComboBox14"
        Me.ComboBox14.Size = New System.Drawing.Size(132, 20)
        Me.ComboBox14.TabIndex = 10
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.Location = New System.Drawing.Point(440, 337)
        Me.NumericUpDown2.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(132, 20)
        Me.NumericUpDown2.TabIndex = 18
        '
        'ComboBox15
        '
        Me.ComboBox15.FormattingEnabled = True
        Me.ComboBox15.Location = New System.Drawing.Point(440, 337)
        Me.ComboBox15.Name = "ComboBox15"
        Me.ComboBox15.Size = New System.Drawing.Size(132, 21)
        Me.ComboBox15.TabIndex = 17
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(287, 130)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Route ID"
        '
        'LabelS1
        '
        Me.LabelS1.AutoEllipsis = True
        Me.LabelS1.Location = New System.Drawing.Point(287, 151)
        Me.LabelS1.Name = "LabelS1"
        Me.LabelS1.Size = New System.Drawing.Size(135, 29)
        Me.LabelS1.TabIndex = 20
        Me.LabelS1.Text = "S1"
        Me.LabelS1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelS2
        '
        Me.LabelS2.AutoEllipsis = True
        Me.LabelS2.Location = New System.Drawing.Point(437, 151)
        Me.LabelS2.Name = "LabelS2"
        Me.LabelS2.Size = New System.Drawing.Size(135, 29)
        Me.LabelS2.TabIndex = 21
        Me.LabelS2.Text = "S2"
        Me.LabelS2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelS3
        '
        Me.LabelS3.AutoEllipsis = True
        Me.LabelS3.Location = New System.Drawing.Point(287, 204)
        Me.LabelS3.Name = "LabelS3"
        Me.LabelS3.Size = New System.Drawing.Size(135, 29)
        Me.LabelS3.TabIndex = 22
        Me.LabelS3.Text = "S3"
        Me.LabelS3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelS4
        '
        Me.LabelS4.AutoEllipsis = True
        Me.LabelS4.Location = New System.Drawing.Point(437, 204)
        Me.LabelS4.Name = "LabelS4"
        Me.LabelS4.Size = New System.Drawing.Size(135, 29)
        Me.LabelS4.TabIndex = 23
        Me.LabelS4.Text = "S4"
        Me.LabelS4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelS5
        '
        Me.LabelS5.AutoEllipsis = True
        Me.LabelS5.Location = New System.Drawing.Point(287, 256)
        Me.LabelS5.Name = "LabelS5"
        Me.LabelS5.Size = New System.Drawing.Size(135, 29)
        Me.LabelS5.TabIndex = 24
        Me.LabelS5.Text = "S5"
        Me.LabelS5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelS6
        '
        Me.LabelS6.AutoEllipsis = True
        Me.LabelS6.Location = New System.Drawing.Point(437, 256)
        Me.LabelS6.Name = "LabelS6"
        Me.LabelS6.Size = New System.Drawing.Size(135, 29)
        Me.LabelS6.TabIndex = 25
        Me.LabelS6.Text = "S6"
        Me.LabelS6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelS7
        '
        Me.LabelS7.AutoEllipsis = True
        Me.LabelS7.Location = New System.Drawing.Point(287, 308)
        Me.LabelS7.Name = "LabelS7"
        Me.LabelS7.Size = New System.Drawing.Size(135, 29)
        Me.LabelS7.TabIndex = 26
        Me.LabelS7.Text = "S7"
        Me.LabelS7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelS8
        '
        Me.LabelS8.AutoEllipsis = True
        Me.LabelS8.Location = New System.Drawing.Point(437, 308)
        Me.LabelS8.Name = "LabelS8"
        Me.LabelS8.Size = New System.Drawing.Size(135, 29)
        Me.LabelS8.TabIndex = 27
        Me.LabelS8.Text = "S8"
        Me.LabelS8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Objects
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 363)
        Me.Controls.Add(Me.LabelS8)
        Me.Controls.Add(Me.LabelS7)
        Me.Controls.Add(Me.LabelS6)
        Me.Controls.Add(Me.LabelS5)
        Me.Controls.Add(Me.LabelS4)
        Me.Controls.Add(Me.LabelS3)
        Me.Controls.Add(Me.LabelS2)
        Me.Controls.Add(Me.LabelS1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.NumericUpDown2)
        Me.Controls.Add(Me.ComboBox15)
        Me.Controls.Add(Me.ComboBox8)
        Me.Controls.Add(Me.ComboBox9)
        Me.Controls.Add(Me.ComboBox10)
        Me.Controls.Add(Me.ComboBox11)
        Me.Controls.Add(Me.ComboBox12)
        Me.Controls.Add(Me.ComboBox13)
        Me.Controls.Add(Me.ComboBox14)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.ComboBox7)
        Me.Controls.Add(Me.ComboBox6)
        Me.Controls.Add(Me.ComboBox5)
        Me.Controls.Add(Me.ComboBox4)
        Me.Controls.Add(Me.ComboBox3)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ListView1)
        Me.MaximumSize = New System.Drawing.Size(603, 401)
        Me.MinimumSize = New System.Drawing.Size(603, 327)
        Me.Name = "Objects"
        Me.Text = "Objects"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboBox10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboBox11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboBox12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboBox13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboBox14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox5 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox6 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox7 As System.Windows.Forms.ComboBox
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBox8 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBox9 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBox10 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBox11 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBox12 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBox13 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBox14 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBox15 As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LabelS1 As System.Windows.Forms.Label
    Friend WithEvents LabelS2 As System.Windows.Forms.Label
    Friend WithEvents LabelS3 As System.Windows.Forms.Label
    Friend WithEvents LabelS4 As System.Windows.Forms.Label
    Friend WithEvents LabelS5 As System.Windows.Forms.Label
    Friend WithEvents LabelS6 As System.Windows.Forms.Label
    Friend WithEvents LabelS7 As System.Windows.Forms.Label
    Friend WithEvents LabelS8 As System.Windows.Forms.Label
End Class
