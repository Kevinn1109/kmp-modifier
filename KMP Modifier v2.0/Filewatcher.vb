Imports KMP_Modifier_v2.KMPMod.MainForm
Module Filewatcher
    Public Routelist As String()
    Public Class Errors
        Public SectionLoc As Integer
        Public IDLoc As Integer
        Public CellLoc As Integer
        Public Errortxt As String

        Public Sub New(Group As Integer, Point As Integer, Cellno As Integer, TXT As String)
            SectionLoc = Group
            IDLoc = Point
            CellLoc = Cellno
        End Sub
    End Class

    Public Class Filewatcher
        Public Shared Function CheckRoutes() As String()
            Dim list(Parsedroutes.Length - 1) As String
            For listie = 0 To list.Length - 1
                list(listie) = "-"
            Next
            For i = 0 To (GOBJ.Length - &H44) / &H3C
                Dim route As Short = Readkmp(GOBJ, 8 + i * &H3C + &H28, 2)
                If Not route = -1 Then
                    If route > Parsedroutes.Length - 1 Then
                        GoTo nah
                    ElseIf Not list(route) = "-" Then
                        list(route) &= "o" & i & " " & XMLRead(Readkmp(GOBJ, 8 + i * &H3C, 2), "XML/items/item", "ObjCollection.xml", False)
                        GoTo nah
                    End If

                    list(route) = "o" & i & " " & XMLRead(Readkmp(GOBJ, 8 + i * &H3C, 2), "XML/items/item", "ObjCollection.xml", False)
                End If
nah:
            Next

            For i = 0 To (CAME.Length - &H50) / &H48
                Dim route As Byte = Readkmp(CAME, 8 + i * &H48 + 3, 1)
                If Not route = 255 Then
                    If route > Parsedroutes.Length - 1 Then
                        GoTo nah2
                    ElseIf Not list(route) = "-" Then
                        list(route) &= " + Camera " & i
                        GoTo nah2
                    End If

                    list(route) = "Camera " & i
                End If
nah2:
            Next

            Return list
        End Function


        Public Shared Function ErrorCheck() As ArrayList
            Dim Errorlist As New ArrayList
            'Sections should cover all points, valid to/last entries
            'CKPT Mandatory checkpoints in order

            Dim list(Parsedroutes.Length - 1) As String
            For listie = 0 To list.Length - 1
                list(listie) = "-"
            Next
            For i = 0 To (GOBJ.Length - &H44) / &H3C
                Dim route As Short = Readkmp(GOBJ, 8 + i * &H3C + &H28, 2)
                If Not route = -1 Then
                    If route > Parsedroutes.Length - 1 Then
                        MsgBox("o" & i & " " & XMLRead(Readkmp(GOBJ, 8 + i * &H3C, 2), "XML/items/item", "ObjCollection.xml", False) & " Points to a nonexisting route (" & route & ").", MsgBoxStyle.Exclamation, "Error")
                        Errorlist.Add(New Errors(4, i, 11, "Invalid Route ID"))
                        GoTo nah
                    ElseIf Not list(route) = "-" Then
                        list(route) &= "o" & i & " " & XMLRead(Readkmp(GOBJ, 8 + i * &H3C, 2), "XML/items/item", "ObjCollection.xml", False)
                        GoTo nah
                    End If

                    list(route) = "o" & i & " " & XMLRead(Readkmp(GOBJ, 8 + i * &H3C, 2), "XML/items/item", "ObjCollection.xml", False)
                End If
nah:
            Next
            Dim zerofound As Boolean = False
            For i = 0 To (CAME.Length - &H50) / &H48
                If Readkmp(CAME, 8 + i * &H48, 1) = 0 Then
                    If zerofound = False Then
                        zerofound = True
                    Else
                        MsgBox("More than one camera with type 0.", MsgBoxStyle.Exclamation, "Error")
                        Errorlist.Add(New Errors(7, i, 1, "Type 0 already in use."))
                    End If
                Else

                End If

                Dim route As Byte = Readkmp(CAME, 8 + i * &H48 + 3, 1)
                If Not route = 255 Then
                    If route > Parsedroutes.Length - 1 Then
                        MsgBox("Camera " & i & " Points to a nonexisting route (" & route & ").", MsgBoxStyle.Exclamation, "Error")
                        Errorlist.Add(New Errors(7, i, 4, "Invalid Route ID"))
                        GoTo nah2
                    ElseIf Not list(route) = "-" Then
                        MsgBox(list(route) & " and Camera " & i & " both use the same route (" & route & ").", MsgBoxStyle.Exclamation, "Error")
                        Errorlist.Add(New Errors(7, i, 4, "This route is already in use by an object"))
                        list(route) &= " + Camera " & i
                        GoTo nah2
                    End If

                    list(route) = "Camera " & i
                End If
nah2:       Next

            For i = 0 To (AREA.Length - 80) / 72
                Dim mode As Byte = Readkmp(AREA, 8 + i * 72 + 1, 1)
                Dim cam As Byte = Readkmp(AREA, 8 + i * 72 + 2, 1)
                If mode = 0 And cam = 255 Then
                    MsgBox("Area " & i & " doesn't have a camera reference.", MsgBoxStyle.Exclamation, "Error")
                    Errorlist.Add(New Errors(6, i, 3, "Missing camera reference."))
                ElseIf mode = 0 And cam > (CAME.Length - 8) / &H48 Then
                    MsgBox("Area " & i & " does not have a valid camera reference.", MsgBoxStyle.Exclamation, "Error")
                    Errorlist.Add(New Errors(6, i, 3, "Invalid camera reference."))
                ElseIf mode = Not 0 And cam = Not 255 Then
                    MsgBox("Area " & i & " should not be referencing to a camera or should be a camera type.", MsgBoxStyle.Exclamation, "Error")
                    Errorlist.Add(New Errors(6, i, 3, "Disallowed camera reference."))
                End If
            Next

            If CAME.Length = 8 Then GoTo Nocame
            Dim firstOP As Byte = Readkmp(CAME, 6, 1)
            If Not Readkmp(CAME, 8 + firstOP * &H48, 1) = 5 Then
                MsgBox("The first opening pan index in the header does not point to a camera with type 5.", MsgBoxStyle.Exclamation, "Error")
                Errorlist.Add(New Errors(7, -1, 0, "Does not point to a camera with type 5."))
            Else
                Dim nextcam As Byte = Readkmp(CAME, 8 + firstOP * &H48 + 1, 1)
                Dim i As Integer = 0
                Do
                    If nextcam = 255 Then Exit Do
                    If Not Readkmp(CAME, 8 + nextcam * &H48, 1) = 5 Then
                        MsgBox("Camera " & nextcam & " is used as opening pan but is not a type 5 camera.", MsgBoxStyle.Exclamation, "Error")
                        Errorlist.Add(New Errors(7, nextcam, 1, "Used as opening pan thought it isn't."))
                        If nextcam = 255 Then Exit Do
                    End If
                    nextcam = Readkmp(CAME, 8 + nextcam * &H48 + 1, 1)
                    i += 1
                    If i = 1000 Then
                        Exit Do
                    End If
                Loop
            End If
nocame:
            Return Errorlist
        End Function
    End Class
End Module
