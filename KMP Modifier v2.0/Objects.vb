Public Class Objects
    Public Objects As Item()
    Public Datagridview1 As DataGridView
    Dim parsing As Boolean = False

    Public Sub New(ByVal ObjectList As Item(), ByRef Datagridview As DataGridView)
        Objects = ObjectList
        Datagridview1 = Datagridview
        InitializeComponent()
    End Sub

    Function GetNumericByID(ByVal id As Integer) As NumericUpDown
        If id = 1 Then Return ComboBox14
        If id = 2 Then Return ComboBox13
        If id = 3 Then Return ComboBox12
        If id = 4 Then Return ComboBox11
        If id = 5 Then Return ComboBox10
        If id = 6 Then Return ComboBox9
        If id = 7 Then Return ComboBox8
        If id = 8 Then Return NumericUpDown2
        Return Nothing
    End Function
    Function GetComboByID(ByVal id As Integer) As ComboBox
        If id = 1 Then Return ComboBox1
        If id = 2 Then Return ComboBox2
        If id = 3 Then Return ComboBox3
        If id = 4 Then Return ComboBox4
        If id = 5 Then Return ComboBox5
        If id = 6 Then Return ComboBox6
        If id = 7 Then Return ComboBox7
        If id = 8 Then Return ComboBox15
        Return Nothing
    End Function
    Function GetLabelByID(ByVal id As Integer) As Label
        If id = 1 Then Return LabelS1
        If id = 2 Then Return LabelS2
        If id = 3 Then Return LabelS3
        If id = 4 Then Return LabelS4
        If id = 5 Then Return LabelS5
        If id = 6 Then Return LabelS6
        If id = 7 Then Return LabelS7
        If id = 8 Then Return LabelS8
        Return Nothing
    End Function

    Sub FillBoxes()
        parsing = True
        Dim id As Integer = Datagridview1.CurrentCell.RowIndex
        Dim Item As Item = Objects(id)
        Label1.Text = Item.Description

        Dim s1, s2, s3, s4, s5, s6, s7, s8 As New Integer
        Dim sCount As Integer = 0
        If Not Item.s1 Is Nothing Then
            sCount += 1
            s1 = sCount
        End If
        If Not Item.s2 Is Nothing Then
            sCount += 1
            s2 = sCount
        End If
        If Not Item.s3 Is Nothing Then
            sCount += 1
            s3 = sCount
        End If
        If Not Item.s4 Is Nothing Then
            sCount += 1
            s4 = sCount
        End If
        If Not Item.s5 Is Nothing Then
            sCount += 1
            s5 = sCount
        End If
        If Not Item.s6 Is Nothing Then
            sCount += 1
            s6 = sCount
        End If
        If Not Item.s7 Is Nothing Then
            sCount += 1
            s7 = sCount
        End If
        If Not Item.s8 Is Nothing Then
            sCount += 1
            s8 = sCount
        End If

        If sCount < 8 Then
            ComboBox15.Visible = False
            NumericUpDown2.Visible = False
            LabelS8.Visible = False
        End If
        If sCount < 7 Then
            ComboBox7.Visible = False
            ComboBox8.Visible = False
            LabelS7.Visible = False
        End If
        If sCount < 6 Then
            ComboBox6.Visible = False
            ComboBox9.Visible = False
            LabelS6.Visible = False
        End If
        If sCount < 5 Then
            ComboBox5.Visible = False
            ComboBox10.Visible = False
            LabelS5.Visible = False
        End If
        If sCount < 4 Then
            ComboBox4.Visible = False
            ComboBox11.Visible = False
            LabelS4.Visible = False
        End If
        If sCount < 3 Then
            ComboBox3.Visible = False
            ComboBox12.Visible = False
            LabelS3.Visible = False
        End If
        If sCount < 2 Then
            ComboBox2.Visible = False
            ComboBox13.Visible = False
            LabelS2.Visible = False
        End If
        If sCount < 1 Then
            ComboBox14.Visible = False
            ComboBox1.Visible = False
            LabelS1.Visible = False
        End If

        If sCount = 1 Or sCount = 3 Or sCount = 5 Or sCount = 7 Then
            Me.Height = Me.MaximumSize.Height - Math.Round((4 - sCount / 2) - 0.5, 0) * 46
        Else
            Me.Height = Me.MaximumSize.Height - Math.Round((4 - sCount / 2), 0) * 46
        End If
        GroupBox1.Height = 225 - (Me.MaximumSize.Height - Me.Size.Height)
        Dim newa As Integer = 179 - (Me.MaximumSize.Height - Me.Size.Height)
        CheckBox1.Location = New System.Drawing.Point(6, newa)
        newa = 202 - (Me.MaximumSize.Height - Me.Size.Height)
        CheckBox2.Location = New System.Drawing.Point(6, newa)

        CheckBox1.Checked = Item.Needsroute
        CheckBox2.Checked = Item.IsSolid

        If CheckBox1.Checked = True Then
            NumericUpDown1.Value = "&H" & Datagridview1.Rows(id).Cells(11).Value
            NumericUpDown1.Hexadecimal = False
            NumericUpDown1.Enabled = True
        Else
            NumericUpDown1.Hexadecimal = True
            NumericUpDown1.Value = &HFFFF
            NumericUpDown1.Enabled = False
        End If
        If Not s1 = 0 Then
            GetNumericByID(s1).Hexadecimal = (Item.s1(1) = "hex")
            GetNumericByID(s1).Visible = (Not Item.s1(1) = "ids")
            GetNumericByID(s1).Value = "&H" & Datagridview1.Rows(id).Cells(12).Value
            GetLabelByID(s1).Text = Item.s1(0)
            If Item.s1(1) = "ids" Then
                Dim combo As ComboBox = GetComboByID(s1)
                For Each value In Item.ids(0)
                    combo.Items.Add(value)
                Next
                If Not combo.Items.Count - 1 < Datagridview1.Rows(id).Cells(12).Value Then
                    combo.SelectedIndex = Datagridview1.Rows(id).Cells(12).Value + 1
                Else
                    combo.SelectedIndex = 1
                End If
            End If
        End If
        If Not s2 = 0 Then
            GetNumericByID(s2).Hexadecimal = (Item.s2(1) = "hex")
            GetNumericByID(s2).Visible = (Not Item.s2(1) = "ids")
            GetNumericByID(s2).Value = "&H" & Datagridview1.Rows(id).Cells(13).Value
            GetLabelByID(s2).Text = Item.s2(0)
            If Item.s2(1) = "ids" Then
                Dim combo As ComboBox = GetComboByID(s2)
                combo.Items.Clear()
                For Each value In Item.ids(1)
                    combo.Items.Add(value)
                Next
                If Not combo.Items.Count - 1 < Datagridview1.Rows(id).Cells(13).Value Then
                    combo.SelectedIndex = Datagridview1.Rows(id).Cells(13).Value
                Else
                    combo.SelectedIndex = 1
                End If
            End If
        End If
        If Not s3 = 0 Then
            GetNumericByID(s3).Hexadecimal = (Item.s3(1) = "hex")
            GetNumericByID(s3).Visible = (Not Item.s3(1) = "ids")
            GetNumericByID(s3).Value = "&H" & Datagridview1.Rows(id).Cells(14).Value
            GetLabelByID(s3).Text = Item.s3(0)
            If Item.s3(1) = "ids" Then
                Dim combo As ComboBox = GetComboByID(s3)
                For Each value In Item.ids(2)
                    combo.Items.Add(value)
                Next
                If Not combo.Items.Count - 1 < Datagridview1.Rows(id).Cells(14).Value Then
                    combo.SelectedIndex = Datagridview1.Rows(id).Cells(14).Value + 1
                Else
                    combo.SelectedIndex = 1
                End If
            End If
        End If
        If Not s4 = 0 Then
            GetNumericByID(s4).Hexadecimal = (Item.s4(1) = "hex")
            GetNumericByID(s4).Visible = (Not Item.s4(1) = "ids")
            GetNumericByID(s4).Value = "&H" & Datagridview1.Rows(id).Cells(15).Value
            GetLabelByID(s4).Text = Item.s4(0)
            If Item.s4(1) = "ids" Then
                Dim combo As ComboBox = GetComboByID(s4)
                For Each value In Item.ids(3)
                    combo.Items.Add(value)
                Next
                If Not combo.Items.Count - 1 < Datagridview1.Rows(id).Cells(15).Value Then
                    combo.SelectedIndex = Datagridview1.Rows(id).Cells(15).Value + 1
                Else
                    combo.SelectedIndex = 1
                End If
            End If
        End If
        If Not s5 = 0 Then
            GetNumericByID(s5).Hexadecimal = (Item.s5(1) = "hex")
            GetNumericByID(s5).Visible = (Not Item.s5(1) = "ids")
            GetNumericByID(s5).Value = "&H" & Datagridview1.Rows(id).Cells(16).Value
            GetLabelByID(s5).Text = Item.s5(0)
            If Item.s5(1) = "ids" Then
                Dim combo As ComboBox = GetComboByID(s5)
                For Each value In Item.ids(4)
                    combo.Items.Add(value)
                Next
                If Not combo.Items.Count - 1 < Datagridview1.Rows(id).Cells(16).Value Then
                    combo.SelectedIndex = Datagridview1.Rows(id).Cells(16).Value + 1
                Else
                    combo.SelectedIndex = 1
                End If
            End If
        End If
        If Not s6 = 0 Then
            GetNumericByID(s6).Hexadecimal = (Item.s6(1) = "hex")
            GetNumericByID(s6).Visible = (Not Item.s6(1) = "ids")
            GetNumericByID(s6).Value = "&H" & Datagridview1.Rows(id).Cells(17).Value
            GetLabelByID(s6).Text = Item.s6(0)
            If Item.s6(1) = "ids" Then
                Dim combo As ComboBox = GetComboByID(s6)
                For Each value In Item.ids(5)
                    combo.Items.Add(value)
                Next
                If Not combo.Items.Count - 1 < Datagridview1.Rows(id).Cells(17).Value Then
                    combo.SelectedIndex = Datagridview1.Rows(id).Cells(17).Value + 1
                Else
                    combo.SelectedIndex = 1
                End If
            End If
        End If
        If Not s7 = 0 Then
            GetNumericByID(s7).Hexadecimal = (Item.s7(1) = "hex")
            GetNumericByID(s7).Visible = (Not Item.s7(1) = "ids")
            GetNumericByID(s7).Value = "&H" & Datagridview1.Rows(id).Cells(18).Value
            GetLabelByID(s7).Text = Item.s7(0)
            If Item.s7(1) = "ids" Then
                Dim combo As ComboBox = GetComboByID(s7)
                For Each value In Item.ids(6)
                    combo.Items.Add(value)
                Next
                If Not combo.Items.Count - 1 < Datagridview1.Rows(id).Cells(18).Value Then
                    combo.SelectedIndex = Datagridview1.Rows(id).Cells(18).Value + 1
                Else
                    combo.SelectedIndex = 1
                End If
            End If
        End If
        If Not s8 = 0 Then
            GetNumericByID(s8).Hexadecimal = (Item.s8(1) = "hex")
            GetNumericByID(s8).Visible = (Not Item.s8(1) = "ids")
            GetNumericByID(s8).Value = "&H" & Datagridview1.Rows(id).Cells(19).Value
            GetLabelByID(s8).Text = Item.s8(0)
            If Item.s8(1) = "ids" Then
                Dim combo As ComboBox = GetComboByID(s8)
                For Each value In Item.ids(7)
                    combo.Items.Add(value)
                Next
                If Not combo.Items.Count - 1 < Datagridview1.Rows(id).Cells(19).Value Then
                    combo.SelectedIndex = Datagridview1.Rows(id).Cells(19).Value + 1
                Else
                    combo.SelectedIndex = 1
                End If
            End If
        End If
        parsing = False
    End Sub

    Sub WriteBoxes()
        Dim id As Integer = Datagridview1.CurrentCell.RowIndex
        Dim Item As Item = Objects(id)

        Dim s1, s2, s3, s4, s5, s6, s7, s8 As New Integer
        Dim sCount As Integer = 0
        If Not Item.s1 Is Nothing Then
            sCount += 1
            s1 = sCount
        End If
        If Not Item.s2 Is Nothing Then
            sCount += 1
            s2 = sCount
        End If
        If Not Item.s3 Is Nothing Then
            sCount += 1
            s3 = sCount
        End If
        If Not Item.s4 Is Nothing Then
            sCount += 1
            s4 = sCount
        End If
        If Not Item.s5 Is Nothing Then
            sCount += 1
            s5 = sCount
        End If
        If Not Item.s6 Is Nothing Then
            sCount += 1
            s6 = sCount
        End If
        If Not Item.s7 Is Nothing Then
            sCount += 1
            s7 = sCount
        End If
        If Not Item.s8 Is Nothing Then
            sCount += 1
            s8 = sCount
        End If

        Datagridview1.Rows(id).Cells(11).Value = NumericUpDown1.Value

        If Not s1 = 0 Then
            If Not Item.s1(1) = "ids" Then
                Dim value As Integer = GetNumericByID(s1).Value
                Datagridview1.Rows(id).Cells(12).Value = value.ToString("X4")
            Else
                Datagridview1.Rows(id).Cells(12).Value = GetComboByID(s1).SelectedIndex.ToString("X4")
            End If
        End If
        If Not s2 = 0 Then
            If Not Item.s2(1) = "ids" Then
                Dim value As Integer = GetNumericByID(s2).Value
                Datagridview1.Rows(id).Cells(13).Value = value.ToString("X4")
            Else
                Datagridview1.Rows(id).Cells(13).Value = GetComboByID(s2).SelectedIndex.ToString("X4")
            End If
        End If
        If Not s3 = 0 Then
            If Not Item.s3(1) = "ids" Then
                Dim value As Integer = GetNumericByID(s3).Value
                Datagridview1.Rows(id).Cells(14).Value = value.ToString("X4")
            Else
                Datagridview1.Rows(id).Cells(14).Value = GetComboByID(s3).SelectedIndex.ToString("X4")
            End If
        End If
        If Not s4 = 0 Then
            If Not Item.s4(1) = "ids" Then
                Dim value As Integer = GetNumericByID(s4).Value
                Datagridview1.Rows(id).Cells(15).Value = value.ToString("X4")
            Else
                Datagridview1.Rows(id).Cells(15).Value = GetComboByID(s4).SelectedIndex.ToString("X4")
            End If
        End If
        If Not s5 = 0 Then
            If Not Item.s5(1) = "ids" Then
                Dim value As Integer = GetNumericByID(s5).Value
                Datagridview1.Rows(id).Cells(16).Value = value.ToString("X4")
            Else
                Datagridview1.Rows(id).Cells(16).Value = GetComboByID(s5).SelectedIndex.ToString("X4")
            End If
        End If
        If Not s6 = 0 Then
            If Not Item.s6(1) = "ids" Then
                Dim value As Integer = GetNumericByID(s6).Value
                Datagridview1.Rows(id).Cells(17).Value = value.ToString("X4")
            Else
                Datagridview1.Rows(id).Cells(17).Value = GetComboByID(s6).SelectedIndex.ToString("X4")
            End If
        End If
        If Not s7 = 0 Then
            If Not Item.s7(1) = "ids" Then
                Dim value As Integer = GetNumericByID(s7).Value
                Datagridview1.Rows(id).Cells(18).Value = value.ToString("X4")
            Else
                Datagridview1.Rows(id).Cells(18).Value = GetComboByID(s7).SelectedIndex.ToString("X4")
            End If
        End If
        If Not s8 = 0 Then
            If Not Item.s8(1) = "ids" Then
                Dim value As Integer = GetNumericByID(s8).Value
                Datagridview1.Rows(id).Cells(19).Value = value.ToString("X4")
            Else
                Datagridview1.Rows(id).Cells(19).Value = GetComboByID(s8).SelectedIndex.ToString("X4")
            End If
        End If
    End Sub


    Private Sub Objects_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated, Me.GotFocus, Me.SizeChanged, Me.Validated
        FillBoxes()
    End Sub

    Private Sub Objects_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'ListView1.Items.Add("win", 0)
    End Sub

    Private Sub BoxEdited(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox7.SelectedIndexChanged, ComboBox6.SelectedIndexChanged, ComboBox5.SelectedIndexChanged, ComboBox4.SelectedIndexChanged, ComboBox3.SelectedIndexChanged, ComboBox2.SelectedIndexChanged, ComboBox15.SelectedIndexChanged, ComboBox1.SelectedIndexChanged, NumericUpDown2.ValueChanged, ComboBox9.ValueChanged, ComboBox8.ValueChanged, ComboBox14.ValueChanged, ComboBox13.ValueChanged, ComboBox12.ValueChanged, ComboBox11.ValueChanged, ComboBox10.ValueChanged, NumericUpDown1.ValueChanged
        If parsing = False Then WriteBoxes()
    End Sub
End Class