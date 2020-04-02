Imports KMP_Modifier_v2.KMPMod.MainForm
Public Class ObjImporter
    Dim One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten As New ArrayList
    Dim f1, f2 As New ArrayList
    Dim Vertices As Object()
    Dim Vertices0 As New ArrayList
    Dim Faces As Object()
    Dim Faces0 As New ArrayList
    Dim lines As String()

    Dim line As New ArrayList

    Dim Datagridview1 As DataGridView
    Dim Combo As Comboboxje

    Public Overloads Sub Showdialog(ByRef Datagridview1a As DataGridView, ByRef Comboa As Comboboxje)
        Datagridview1 = Datagridview1a
        Combo = Comboa
        Me.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim dlg As New OpenFileDialog
        dlg.Filter = "Wavefront OBJ Files|*.obj"
        Try
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then

                Dim sr As New IO.StreamReader(dlg.FileName)
                Do While sr.Peek() >= 0
                    line.Add(sr.ReadLine)
                Loop
                lines = CType(line.ToArray(GetType(String)), String())

                For i = 0 To lines.Count - 1
                    If Mid(lines(i), 1, 2) = "g " Then
                        lines(i) = lines(i).Replace("  ", " ")
                        CheckedListBox1.Items.Add(Mid(lines(i), 3))
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message & Chr(13) & ex.StackTrace)
        End Try
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Try
            'Dialog1.ShowDialog()

            Dim dowork As Boolean = False
            For i = 0 To lines.Count - 1
                If Mid(lines(i), 1, 2) = "g " Or Mid(lines(i), 1, 8) = "# object" Then
                    For Each item In CheckedListBox1.CheckedItems
                        Dim compare1 As String = item.ToString
                        Dim compare2 As String = Mid(lines(i), 3)
                        Dim compare3 As String = Mid(lines(i), 10)
                        If (compare1 = compare2) Or (compare1 = compare3) Then
                            dowork = True
                            Exit For
                        End If
                        dowork = False
                    Next
                End If


                If Mid(lines(i), 1, 2) = "v " Then
                    lines(i) = lines(i).Replace(".", ",")
                    lines(i) = lines(i).Replace("  ", " ")
                    Dim a As String() = System.Text.RegularExpressions.Regex.Split(lines(i), " ")
                    Dim b As Single() = {a(1), a(2), a(3)}
                    Vertices0.Add(b)
                ElseIf Mid(lines(i), 1, 2) = "f " And dowork = True Then
                    Dim a As String() = System.Text.RegularExpressions.Regex.Split(lines(i), " ")
                    Dim b(0 To 2) As Integer
                    b(0) = System.Text.RegularExpressions.Regex.Split(a(1), "/")(0)
                    b(1) = System.Text.RegularExpressions.Regex.Split(a(2), "/")(0)
                    b(2) = System.Text.RegularExpressions.Regex.Split(a(3), "/")(0)
                    Faces0.Add(b)
                End If
            Next
            Vertices = DirectCast(Vertices0.ToArray(GetType(Single())), Object())
            Faces = DirectCast(Faces0.ToArray(GetType(Integer())), Object())

            Dim Progressbarvalues As Integer
            If NumericUpDown1.Value = 0 Then
                Progressbarvalues = ProgressBar1.Maximum / (Faces.Length * 6)
            Else
                Progressbarvalues = ProgressBar1.Maximum / (Faces.Length * 3)
            End If


            Dim Done As New ArrayList
            For i = 0 To 2
                For Each triangle As Integer() In Faces
                    If Not ProgressBar1.Value + Progressbarvalues > ProgressBar1.Maximum Then ProgressBar1.Value += Progressbarvalues
                    Dim am As Single = 0

                    If Done.Contains(triangle(i)) Then GoTo nexthing
                    For Each compare As Integer() In Faces
                        If ArrayCompare(Vertices(triangle(i) - 1), Vertices(compare(0) - 1)) Or ArrayCompare(Vertices(triangle(i) - 1), Vertices(compare(1) - 1)) Or ArrayCompare(Vertices(triangle(i) - 1), Vertices(compare(2) - 1)) Then am += 1
                    Next
                    Done.Add(Vertices(triangle(i) - 1))

                    If am >= 1 And Not ArraylistCheck(One, Vertices(triangle(i) - 1)) Then One.Add(Vertices(triangle(i) - 1))
                    If am >= 2 And Not ArraylistCheck(Two, Vertices(triangle(i) - 1)) Then Two.Add(Vertices(triangle(i) - 1))
                    If am >= 3 And Not ArraylistCheck(Three, Vertices(triangle(i) - 1)) Then Three.Add(Vertices(triangle(i) - 1))
                    If am >= 4 And Not ArraylistCheck(Four, Vertices(triangle(i) - 1)) Then Four.Add(Vertices(triangle(i) - 1))
                    If am >= 5 And Not ArraylistCheck(Five, Vertices(triangle(i) - 1)) Then Five.Add(Vertices(triangle(i) - 1))
                    If am >= 6 And Not ArraylistCheck(Six, Vertices(triangle(i) - 1)) Then Six.Add(Vertices(triangle(i) - 1))
                    If am >= 7 And Not ArraylistCheck(Seven, Vertices(triangle(i) - 1)) Then Seven.Add(Vertices(triangle(i) - 1))
                    If am >= 8 And Not ArraylistCheck(Eight, Vertices(triangle(i) - 1)) Then Eight.Add(Vertices(triangle(i) - 1))
                    If am >= 9 And Not ArraylistCheck(Nine, Vertices(triangle(i) - 1)) Then Nine.Add(Vertices(triangle(i) - 1))
                    If am >= 10 And Not ArraylistCheck(Ten, Vertices(triangle(i) - 1)) Then Ten.Add(Vertices(triangle(i) - 1))
nexthing:
                Next
            Next


            If NumericUpDown1.Value = 0 Then
                Progressbarvalues = ProgressBar1.Maximum / (Faces.Length * 2)

                For Each intje As Integer() In Faces
                    ProgressBar1.Value += Progressbarvalues
                    Array.Sort(intje)
                    If Not ArraylistCheck(f1, {intje(0), intje(1)}) Then
                        f1.Add({intje(0), intje(1)})
                    Else
                        Dim x As Single = (Vertices(intje(0) - 1)(0) + Vertices(intje(1) - 1)(0)) / 2
                        Dim y As Single = (Vertices(intje(0) - 1)(1) + Vertices(intje(1) - 1)(1)) / 2
                        Dim z As Single = (Vertices(intje(0) - 1)(2) + Vertices(intje(1) - 1)(2)) / 2
                        AddPoint(x, y, z)
                    End If

                    If Not ArraylistCheck(f1, {intje(0), intje(2)}) Then
                        f1.Add({intje(0), intje(2)})
                    Else
                        Dim x As Single = (Vertices(intje(0) - 1)(0) + Vertices(intje(2) - 1)(0)) / 2
                        Dim y As Single = (Vertices(intje(0) - 1)(1) + Vertices(intje(2) - 1)(1)) / 2
                        Dim z As Single = (Vertices(intje(0) - 1)(2) + Vertices(intje(2) - 1)(2)) / 2
                        AddPoint(x, y, z)
                    End If

                    If Not ArraylistCheck(f1, {intje(1), intje(2)}) Then
                        f1.Add({intje(1), intje(2)})
                    Else
                        Dim x As Single = (Vertices(intje(1) - 1)(0) + Vertices(intje(2) - 1)(0)) / 2
                        Dim y As Single = (Vertices(intje(1) - 1)(1) + Vertices(intje(2) - 1)(1)) / 2
                        Dim z As Single = (Vertices(intje(1) - 1)(2) + Vertices(intje(2) - 1)(2)) / 2
                        AddPoint(x, y, z)
                    End If
                Next

            ElseIf NumericUpDown1.Value = 1 Then
                Progressbarvalues = ProgressBar1.Maximum / (One.Count * 2)
                For Each vertex As Single() In One
                    Dim x As Single = vertex(0)
                    Dim y As Single = vertex(1)
                    Dim z As Single = vertex(2)
                    AddPoint(x, y, z)
                Next
            ElseIf NumericUpDown1.Value = 2 Then
                Progressbarvalues = ProgressBar1.Maximum / (Two.Count * 2)
                For Each vertex As Single() In Two
                    Dim x As Single = vertex(0)
                    Dim y As Single = vertex(1)
                    Dim z As Single = vertex(2)
                    AddPoint(x, y, z)
                Next
            ElseIf NumericUpDown1.Value = 3 Then
                Progressbarvalues = ProgressBar1.Maximum / (Three.Count * 2)
                For Each vertex As Single() In Three
                    Dim x As Single = vertex(0)
                    Dim y As Single = vertex(1)
                    Dim z As Single = vertex(2)
                    AddPoint(x, y, z)
                Next
            ElseIf NumericUpDown1.Value = 4 Then
                Progressbarvalues = ProgressBar1.Maximum / (Four.Count * 2)
                For Each vertex As Single() In Four
                    Dim x As Single = vertex(0)
                    Dim y As Single = vertex(1)
                    Dim z As Single = vertex(2)
                    AddPoint(x, y, z)
                Next
            ElseIf NumericUpDown1.Value = 5 Then
                Progressbarvalues = ProgressBar1.Maximum / (Five.Count * 2)
                For Each vertex As Single() In Five
                    Dim x As Single = vertex(0)
                    Dim y As Single = vertex(1)
                    Dim z As Single = vertex(2)
                    AddPoint(x, y, z)
                Next
            ElseIf NumericUpDown1.Value = 6 Then
                Progressbarvalues = ProgressBar1.Maximum / (Six.Count * 2)
                For Each vertex As Single() In Six
                    Dim x As Single = vertex(0)
                    Dim y As Single = vertex(1)
                    Dim z As Single = vertex(2)
                    AddPoint(x, y, z)
                Next
            ElseIf NumericUpDown1.Value = 7 Then
                Progressbarvalues = ProgressBar1.Maximum / (Seven.Count * 2)
                For Each vertex As Single() In Seven
                    Dim x As Single = vertex(0)
                    Dim y As Single = vertex(1)
                    Dim z As Single = vertex(2)
                    AddPoint(x, y, z)
                Next
            ElseIf NumericUpDown1.Value = 8 Then
                Progressbarvalues = ProgressBar1.Maximum / (Eight.Count * 2)
                For Each vertex As Single() In Eight
                    Dim x As Single = vertex(0)
                    Dim y As Single = vertex(1)
                    Dim z As Single = vertex(2)
                    AddPoint(x, y, z)
                Next
            ElseIf NumericUpDown1.Value = 9 Then
                Progressbarvalues = ProgressBar1.Maximum / (Nine.Count * 2)
                For Each vertex As Single() In Nine
                    Dim x As Single = vertex(0)
                    Dim y As Single = vertex(1)
                    Dim z As Single = vertex(2)
                    AddPoint(x, y, z)
                Next
            ElseIf NumericUpDown1.Value = 10 Then
                Progressbarvalues = ProgressBar1.Maximum / (Ten.Count * 2)
                For Each vertex As Single() In Ten
                    Dim x As Single = vertex(0)
                    Dim y As Single = vertex(1)
                    Dim z As Single = vertex(2)
                    AddPoint(x, y, z)
                Next
            End If
            'MainForm.ReplaceDGV(Datagridview1)
        Catch ex As Exception
            MsgBox(ex.Message & Chr(13) & ex.StackTrace)
        End Try
    End Sub

    Sub AddPoint(ByVal X As Single, ByVal Y As Single, ByVal Z As Single)
        Datagridview1.Rows.Add()
        Dim currentrow As Integer = Datagridview1.RowCount - 2

        If Combo.text = My.Settings.Names(1) Or Combo.text = My.Settings.Names(2) Or Combo.text = My.Settings.Names(4) Then
            Datagridview1.Rows(currentrow).Cells(2).Value = X
            Datagridview1.Rows(currentrow).Cells(3).Value = Y
            Datagridview1.Rows(currentrow).Cells(4).Value = Z
        ElseIf Combo.text = My.Settings.Names(6) Then
            Datagridview1.Rows(currentrow).Cells(4).Value = X
            Datagridview1.Rows(currentrow).Cells(5).Value = Y
            Datagridview1.Rows(currentrow).Cells(6).Value = Z
        ElseIf Combo.text = My.Settings.Names(7) Then
            Datagridview1.Rows(currentrow).Cells(9).Value = X
            Datagridview1.Rows(currentrow).Cells(10).Value = Y
            Datagridview1.Rows(currentrow).Cells(11).Value = Z
        ElseIf Combo.text = My.Settings.Names(3) Then
            MsgBox("Adding checkpoints is not supported, please use another group.")
        Else
            Datagridview1.Rows(currentrow).Cells(1).Value = X
            Datagridview1.Rows(currentrow).Cells(2).Value = Y
            Datagridview1.Rows(currentrow).Cells(3).Value = Z
        End If
    End Sub

    Function ArrayCompare(ByVal ArrayA As Array, ByVal ArrayB As Array) As Boolean
        For i = 0 To ArrayA.Length - 1
            If Not ArrayA(i) = ArrayB(i) Then Return False
        Next
        Return True
    End Function

    Function ArraylistCheck(ByVal Arraylist As ArrayList, ByVal Array As Array) As Boolean
        For i = 0 To Arraylist.Count - 1
            If ArrayCompare(Arraylist(i), Array) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub CheckedListBox1_MouseHover(sender As System.Object, e As System.EventArgs) Handles CheckedListBox1.MouseHover
        CheckedListBox1.Focus()
    End Sub
End Class