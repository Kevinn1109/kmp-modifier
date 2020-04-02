Imports KMP_Modifier_v2.KMPMod.MainForm

Module CSVManager
    Sub Addrow(ByRef CSV As String, ByVal values As String(), i As Integer)
        Dim serperated As String = i
        For Each value As String In values
            serperated &= ";" & "=" & Chr(&H22) & value & Chr(&H22)
        Next
        CSV &= serperated & Chr(13)
    End Sub

    Public Sub ExportAll()
        Dim TheCSV As String
        TheCSV = My.Resources.CSVHeader
        TheCSV &= "<KTPT>;" & (KTPT.Length - 8) / &H1C & " Entries" & Chr(13)
        TheCSV &= "ID;XPos;Ypos;ZPos;XRot;YRot;ZRot;Player" & Chr(13)
        For i = 0 To (KTPT.Length - &H24) / &H1C
            Dim valueX As Single = Hextofloat(Readkmp(KTPT, 8 + i * 28, 4))
            Dim valueY As Single = Hextofloat(Readkmp(KTPT, 12 + i * 28, 4))
            Dim valueZ As Single = Hextofloat(Readkmp(KTPT, 16 + i * 28, 4))
            Dim rotX As Single = Hextofloat(Readkmp(KTPT, 20 + i * 28, 4))
            Dim rotY As Single = Hextofloat(Readkmp(KTPT, 24 + i * 28, 4))
            Dim rotZ As Single = Hextofloat(Readkmp(KTPT, 28 + i * 28, 4))
            Dim temp As Short = Readkmp(KTPT, 32 + i * 28, 2)
            Dim Player As String = temp.ToString("X4")
            Addrow(TheCSV, {valueX, valueY, valueZ, rotX, rotY, rotZ, Player}, i)
        Next
        TheCSV &= "</KTPT>" & Chr(13) & Chr(13)

        TheCSV &= "<ENPT>;" & (ENPT.Length - 8) / 20 & " Entries" & Chr(13)
        TheCSV &= "ID;XPos;YPos;ZPos;Scale;Setting1;Setting2" & Chr(13)
        For i = 0 To (ENPT.Length - &H1C) / &H14
            Dim valueX As Single = Hextofloat(Readkmp(ENPT, 8 + i * 20, 4))
            Dim valueY As Single = Hextofloat(Readkmp(ENPT, 12 + i * 20, 4))
            Dim valueZ As Single = Hextofloat(Readkmp(ENPT, 16 + i * 20, 4))
            Dim Scale As Single = Hextofloat(Readkmp(ENPT, 20 + i * 20, 4))
            Dim S1 As Short = Readkmp(ENPT, 24 + i * 20, 2)
            Dim S2 As Byte = Readkmp(ENPT, 26 + i * 20, 1)
            Addrow(TheCSV, {valueX, valueY, valueZ, Scale, S1, S2}, i)
        Next
        TheCSV &= "</ENPT>" & Chr(13) & Chr(13)

        TheCSV &= "<ITPT>;" & (ITPT.Length - 8) / 20 & " Entries" & Chr(13)
        TheCSV &= "ID;XPos;YPos;ZPos;Scale;Setting1;Setting2" & Chr(13)
        For i = 0 To (ITPT.Length - &H1C) / &H14
            Dim valueX As Single = Hextofloat(Readkmp(ITPT, 8 + i * 20, 4))
            Dim valueY As Single = Hextofloat(Readkmp(ITPT, 12 + i * 20, 4))
            Dim valueZ As Single = Hextofloat(Readkmp(ITPT, 16 + i * 20, 4))
            Dim Scale As Single = Hextofloat(Readkmp(ITPT, 20 + i * 20, 4))
            Dim S1 As Short = Readkmp(ITPT, 24 + i * 20, 2)
            Dim S2 As Short = Readkmp(ITPT, 26 + i * 20, 2)
            Addrow(TheCSV, {valueX, valueY, valueZ, Scale, S1, S2}, i)
        Next
        TheCSV &= "</ITPT>" & Chr(13)

        TheCSV &= "<CKPT;>" & (CKPT.Length - 8) / &H14 & " Entries" & Chr(13)
        TheCSV &= "ID;Xpos;Zpos;Xpos2;Zpos2;Respawn;Type;Last;Next" & Chr(13)
        For i = 0 To (CKPT.Length - &H1C) / &H14
            Dim valueX As Single = Hextofloat(Readkmp(CKPT, 8 + i * 20, 4))
            Dim valueY As Single = Hextofloat(Readkmp(CKPT, 12 + i * 20, 4))
            Dim valueX2 As Single = Hextofloat(Readkmp(CKPT, 16 + i * 20, 4))
            Dim valueY2 As Single = Hextofloat(Readkmp(CKPT, 20 + i * 20, 4))
            Dim rspn As Byte = Readkmp(CKPT, 24 + i * 20, 1)
            Dim type As Byte = Readkmp(CKPT, 25 + i * 20, 1)
            Dim type2 As String = type.ToString("X2")
            Dim last As Byte = Readkmp(CKPT, 26 + i * 20, 1)
            Dim nexti As Byte = Readkmp(CKPT, 27 + i * 20, 1)
            Addrow(TheCSV, {valueX, valueY, valueX2, valueY2, rspn, type2, last, nexti}, i)
        Next
        TheCSV &= "</CKPT>" & Chr(13) & Chr(13)

        TheCSV &= "<GOBJ>;" & (GOBJ.Length - 8) / &H3C & " Entries" & Chr(13)
        TheCSV &= "ID;XPos;YPos;ZPos;XRot;YRot;ZRot;XScale;YScale;ZScale;Route;Settings;;;;;;;;Presence" & Chr(13)
        For i = 0 To (GOBJ.Length - &H44) / &H3C
            Dim objid As Short = Readkmp(GOBJ, 8 + i * &H3C, 2)
            Dim ID As String = XMLRead(objid, "XML/items/item", "ObjCollection.xml", False)
            Dim valueX As Single = Hextofloat(Readkmp(GOBJ, 12 + i * 60, 4))
            Dim valueY As Single = Hextofloat(Readkmp(GOBJ, 16 + i * 60, 4))
            Dim valueZ As Single = Hextofloat(Readkmp(GOBJ, 20 + i * 60, 4))
            Dim rotX As Single = Hextofloat(Readkmp(GOBJ, 24 + i * 60, 4))
            Dim rotY As Single = Hextofloat(Readkmp(GOBJ, 28 + i * 60, 4))
            Dim rotZ As Single = Hextofloat(Readkmp(GOBJ, 32 + i * 60, 4))
            Dim sclX As Single = Hextofloat(Readkmp(GOBJ, 36 + i * 60, 4))
            Dim sclY As Single = Hextofloat(Readkmp(GOBJ, 40 + i * 60, 4))
            Dim sclZ As Single = Hextofloat(Readkmp(GOBJ, 44 + i * 60, 4))
            Dim route As Short = Readkmp(GOBJ, 48 + i * 60, 2)
            Dim routeid As String = route.ToString("X4")
            Dim s1 As Short = Readkmp(GOBJ, 50 + i * 60, 2)
            Dim s2 As Short = Readkmp(GOBJ, 52 + i * 60, 2)
            Dim s3 As Short = Readkmp(GOBJ, 54 + i * 60, 2)
            Dim s4 As Short = Readkmp(GOBJ, 56 + i * 60, 2)
            Dim s5 As Short = Readkmp(GOBJ, 58 + i * 60, 2)
            Dim s6 As Short = Readkmp(GOBJ, 60 + i * 60, 2)
            Dim s7 As Short = Readkmp(GOBJ, 62 + i * 60, 2)
            Dim s8 As Short = Readkmp(GOBJ, 64 + i * 60, 2)
            Dim set1 As String = s1.ToString("X4")
            Dim set2 As String = s2.ToString("X4")
            Dim set3 As String = s3.ToString("X4")
            Dim set4 As String = s4.ToString("X4")
            Dim set5 As String = s5.ToString("X4")
            Dim set6 As String = s6.ToString("X4")
            Dim set7 As String = s7.ToString("X4")
            Dim set8 As String = s8.ToString("X4")
            Dim Presence As Short = Readkmp(GOBJ, 66 + i * 60, 2)
            Dim presenceflag As String = Presence.ToString("X4")
            Addrow(TheCSV, {ID, valueX, valueY, valueZ, rotX, rotY, rotZ, sclX, sclY, sclZ, routeid, set1, set2, set3, set4, set5, set6, set7, set8, presenceflag}, i)
        Next
        TheCSV &= "</GOBJ>" & Chr(13) & Chr(13)

        TheCSV &= "The routes are the same as the other groups but the group of points are devided by empty lines and a recounting ID." & Chr(13)
        TheCSV &= "<POTI>;" & Parsedroutes.Length & " Entries" & Chr(13)
        TheCSV &= "ID;Xpos;Ypos;Zpos;Setting1;Setting2"
        For i = 0 To Parsedroutes.Length - 1
            TheCSV &= Chr(13)
            For id = 0 To Parsedroutes(i).points.Length - 1
                Dim Point As Route.Point = Parsedroutes(i).points(id)
                Addrow(TheCSV, {Point.Location(0), Point.Location(1), Point.Location(2), Point.Pointsettings.ToString("X4"), Point.Additional.ToString("X4")}, id)
            Next
        Next
        TheCSV &= "</POTI>" & Chr(13) & Chr(13)

        TheCSV &= "<AREA>;" & (AREA.Length - 56) / 48 & " Entries" & Chr(13)
        TheCSV &= "ID;Mode;Type;Camera;Xpos;Ypos;Zpos;Xrot;Yrot;Zrot;Xscale;Yscale;Zscale;Settings;Route;Enemy" & Chr(13)
        For i = 0 To (AREA.Length - 56) / 48
            Dim mode As Byte = Readkmp(AREA, 8 + i * 48, 1)
            Dim S1 As Short = Readkmp(AREA, 9 + i * 48, 1)
            Dim modes As String = mode.ToString("X2")
            Dim Camei As Byte = Readkmp(AREA, 10 + i * 48, 1)
            Dim Cames As String = Camei.ToString("X2")
            Dim valueX As Integer = Hextofloat(Readkmp(AREA, 12 + i * 48, 4))
            Dim valueY As Integer = Hextofloat(Readkmp(AREA, 16 + i * 48, 4))
            Dim valueZ As Integer = Hextofloat(Readkmp(AREA, 20 + i * 48, 4))
            Dim rotX As Single = Hextofloat(Readkmp(AREA, 24 + i * 48, 4))
            Dim rotY As Single = Hextofloat(Readkmp(AREA, 28 + i * 48, 4))
            Dim rotZ As Single = Hextofloat(Readkmp(AREA, 32 + i * 48, 4))
            Dim sclX As Single = Hextofloat(Readkmp(AREA, 36 + i * 48, 4))
            Dim sclY As Single = Hextofloat(Readkmp(AREA, 40 + i * 48, 4))
            Dim sclZ As Single = Hextofloat(Readkmp(AREA, 44 + i * 48, 4))
            Dim Seti As Integer = Readkmp(AREA, 48 + i * 48, 4)
            Dim Sets As String = Seti.ToString("X8")
            Dim routi As Byte = Readkmp(AREA, 53 + i * 48, 1)
            Dim routes As String = routi.ToString("X2")
            Dim enemi As Byte = Readkmp(AREA, 55 + i * 48, 1)
            Dim enemies As String = enemi.ToString("X2")
            Addrow(TheCSV, {modes, S1, Cames, valueX, valueY, valueZ, rotX, rotY, rotZ, sclX, sclY, sclZ, Sets, routes, enemies}, i)
        Next
        TheCSV &= "</AREA>" & Chr(13) & Chr(13)

        TheCSV &= "<CAME>;" & (CAME.Length - 8) / 72 & " Entries" & Chr(13)
        TheCSV &= "ID;Type;Next;Shake;Route;Speed;Zoomspeed;Viewspeed;Flag;Xpos;Ypos;Zpos;Xrot;Yrot;Zrot" & Chr(13)
        TheCSV &= ";Zoomstart;Zoomend;View X;View Y;View Z;View2 X;View2 Y;View2 Z;Seconds" & Chr(13)
        For i = 0 To (CAME.Length - 80) / 72
            Dim S1 As Byte = Readkmp(CAME, 8 + i * 72, 1)
            Dim Nexti As Byte = Readkmp(CAME, 9 + i * 72, 1)
            Dim Cames As String = Nexti.ToString("X2")
            Dim Shake As Byte = Readkmp(CAME, 10 + i * 72, 1)
            Dim Shakei As String = Shake.ToString("X2")
            Dim Route As Byte = Readkmp(CAME, 11 + i * 72, 1)
            Dim Routi As String = Route.ToString("X2")
            Dim Vs As Short = Readkmp(CAME, 12 + i * 72, 2)
            Dim V As String = Vs.ToString("X4")
            Dim VZs As Short = Readkmp(CAME, 14 + i * 72, 2)
            Dim VZ As String = VZs.ToString("X4")
            Dim VP As Short = Readkmp(CAME, 16 + i * 72, 2)
            Dim VPs As String = VP.ToString("X4")
            Dim flag As Short = Readkmp(CAME, 18 + i * 72, 2)
            Dim flagi As String = flag.ToString("X4")

            Dim valueX As Integer = Hextofloat(Readkmp(CAME, 20 + i * 72, 4))
            Dim valueY As Integer = Hextofloat(Readkmp(CAME, 24 + i * 72, 4))
            Dim valueZ As Integer = Hextofloat(Readkmp(CAME, 28 + i * 72, 4))
            Dim rotX As Single = Hextofloat(Readkmp(CAME, 32 + i * 72, 4))
            Dim rotY As Single = Hextofloat(Readkmp(CAME, 36 + i * 72, 4))
            Dim rotZ As Single = Hextofloat(Readkmp(CAME, 40 + i * 72, 4))
            Dim Zstart As Integer = Hextofloat(Readkmp(CAME, 44 + i * 72, 4))
            Dim Zend As Integer = Hextofloat(Readkmp(CAME, 48 + i * 72, 4))
            Dim viewX As Integer = Hextofloat(Readkmp(CAME, 52 + i * 72, 4))
            Dim viewY As Integer = Hextofloat(Readkmp(CAME, 56 + i * 72, 4))
            Dim viewZ As Integer = Hextofloat(Readkmp(CAME, 60 + i * 72, 4))
            Dim view2X As Integer = Hextofloat(Readkmp(CAME, 64 + i * 72, 4))
            Dim view2Y As Integer = Hextofloat(Readkmp(CAME, 68 + i * 72, 4))
            Dim view2Z As Integer = Hextofloat(Readkmp(CAME, 72 + i * 72, 4))
            Dim Time As Decimal = (Hextofloat(Readkmp(CAME, 76 + i * 72, 4))) / 60
            Addrow(TheCSV, {S1, Cames, Shakei, Routi, V, VZ, VPs, flagi, valueX, valueY, valueZ, rotX, rotY, rotZ}, i)
            TheCSV &= ";" & Zstart & ";" & Zend & ";" & viewX & ";" & viewY & ";" & viewZ & ";" & view2X & ";" & view2Y & ";" & view2Z & ";" & Time & Chr(13)
        Next
        TheCSV &= "</CAME>" & Chr(13) & Chr(13)

        TheCSV &= "<JGPT>;" & (JGPT.Length - 8) / &H1C & " Entries" & Chr(13)
        TheCSV &= "ID;Xpos;Ypos;Zpos;Xrot;Yrot;Zrot;ID2;Scale" & Chr(13)
        For i = 0 To (JGPT.Length - &H24) / &H1C
            Dim valueX As Integer = Hextofloat(Readkmp(JGPT, 8 + i * 28, 4))
            Dim valueY As Integer = Hextofloat(Readkmp(JGPT, 12 + i * 28, 4))
            Dim valueZ As Integer = Hextofloat(Readkmp(JGPT, 16 + i * 28, 4))
            Dim rotX As Single = Hextofloat(Readkmp(JGPT, 20 + i * 28, 4))
            Dim rotY As Single = Hextofloat(Readkmp(JGPT, 24 + i * 28, 4))
            Dim rotZ As Single = Hextofloat(Readkmp(JGPT, 28 + i * 28, 4))
            Dim ID2 As Short = Readkmp(JGPT, 32 + i * 28, 2)
            Dim scale As Short = Readkmp(JGPT, 34 + i * 28, 2)
            Dim scales As String = scale.ToString("X4")
            Addrow(TheCSV, {valueX, valueY, valueZ, rotX, rotY, rotZ, ID2, scales}, i)
        Next
        TheCSV &= "</JGPT>" & Chr(13) & Chr(13)

        TheCSV &= "<CNPT>;" & (CNPT.Length - 8) / &H1C & " Entries" & Chr(13)
        For i = 0 To (CNPT.Length - &H24) / &H1C
            Dim valueX As Integer = Hextofloat(Readkmp(CNPT, 8 + i * 28, 4))
            Dim valueY As Integer = Hextofloat(Readkmp(CNPT, 12 + i * 28, 4))
            Dim valueZ As Integer = Hextofloat(Readkmp(CNPT, 16 + i * 28, 4))
            Dim rotX As Single = Hextofloat(Readkmp(CNPT, 20 + i * 28, 4))
            Dim rotY As Single = Hextofloat(Readkmp(CNPT, 24 + i * 28, 4))
            Dim rotZ As Single = Hextofloat(Readkmp(CNPT, 28 + i * 28, 4))
            Dim ID2 As Short = Readkmp(CNPT, 32 + i * 28, 2)
            Dim S1 As Short = Readkmp(CNPT, 34 + i * 28, 2)
            Addrow(TheCSV, {valueX, valueY, valueZ, rotX, rotY, rotZ, ID2, S1}, i)
        Next

        Dim dlg As New SaveFileDialog
        dlg.Filter = "Comma-serperated values (*.csv)|*.csv"
        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            My.Computer.FileSystem.WriteAllText(dlg.FileName, TheCSV, False)
        End If
    End Sub



    Public Sub ImportAll()
        Dim TheCSV As String
        Dim dlg As New OpenFileDialog
        dlg.Filter = "Comma-serperated values (*.csv)|*.csv"
        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                TheCSV = My.Computer.FileSystem.ReadAllText(dlg.FileName)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
        Else : Exit Sub
        End If

        TheCSV = TheCSV.Replace(Chr(10), "").Replace("=", "").Replace(Chr(&H22), "")
        Dim rows As String() = TheCSV.Split(Chr(13))

        Dim Read As Boolean = False
        Dim count As Integer = 0
        Dim Leftat As Integer = 0
        Dim temp(0) As Byte
        For i = 0 To rows.Length - 1
            If Read = False Then
                If rows(i).Split(";")(0) = "<KTPT>" Then
                    Read = True

                    count = 0
                    For curro = i + 2 To rows.Length - 1
                        If Not rows(curro).Split(";")(0) = "</KTPT>" Then : count += 1
                        Else : Exit For
                        End If
                    Next

                    Dim temp2(0 To count * &H1C + 7) As Byte
                    temp = temp2
                    Dim name As Byte() = {MainForm.Strtoint("K"), MainForm.Strtoint("T"), MainForm.Strtoint("P"), MainForm.Strtoint("T")}
                    Writekmp(temp, 0, name)
                    Dim amount As Byte() = BitConverter.GetBytes(count) : Array.Reverse(amount)
                    Writekmp(temp, 4, amount, 2)
                End If
            Else
                For current = i + 1 To i + count
                    Dim LocX, LocY, LocZ, RotX, RotY, RotZ, Player As Byte()
                    LocX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(1))) : Array.Reverse(LocX) : Writekmp(temp, 8 + (current - (i + 1)) * &H1C, LocX)
                    LocY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(2))) : Array.Reverse(LocY) : Writekmp(temp, 12 + (current - (i + 1)) * &H1C, LocY)
                    LocZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(3))) : Array.Reverse(LocZ) : Writekmp(temp, 16 + (current - (i + 1)) * &H1C, LocZ)
                    RotX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(4))) : Array.Reverse(RotX) : Writekmp(temp, 20 + (current - (i + 1)) * &H1C, RotX)
                    RotY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(5))) : Array.Reverse(RotY) : Writekmp(temp, 24 + (current - (i + 1)) * &H1C, RotY)
                    RotZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(6))) : Array.Reverse(RotZ) : Writekmp(temp, 28 + (current - (i + 1)) * &H1C, RotZ)
                    Dim player2 As Integer = "&h" & "0" & rows(current).Split(";")(7)
                    Player = BitConverter.GetBytes(player2) : Array.Reverse(Player, 0, 2) : Writekmp(temp, 32 + (current - (i + 1)) * &H1C, Player)
                Next
                KTPT = temp
                Leftat = i : Read = False
                Exit For
            End If
        Next

        For i = Leftat To rows.Length - 1
            If Read = False Then
                If rows(i).Split(";")(0) = "<ENPT>" Then
                    Read = True

                    count = 0
                    For curro = i + 2 To rows.Length - 1
                        If Not rows(curro).Split(";")(0) = "</ENPT>" Then : count += 1
                        Else : Exit For
                        End If
                    Next

                    Dim temp2(0 To count * &H14 + 7) As Byte
                    temp = temp2
                    Dim name As Byte() = {MainForm.Strtoint("E"), MainForm.Strtoint("N"), MainForm.Strtoint("P"), MainForm.Strtoint("T")}
                    Writekmp(temp, 0, name)
                    Dim amount As Byte() = BitConverter.GetBytes(count) : Array.Reverse(amount)
                    Writekmp(temp, 4, amount, 2)
                End If
            Else
                Dim cells As String() = rows(i).Split(";")
                For current = i + 1 To i + count
                    Dim LocX, LocY, LocZ, Scale, S1, S2 As Byte()
                    LocX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(1))) : Array.Reverse(LocX) : Writekmp(temp, 8 + (current - (i + 1)) * &H14, LocX)
                    LocY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(2))) : Array.Reverse(LocY) : Writekmp(temp, 12 + (current - (i + 1)) * &H14, LocY)
                    LocZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(3))) : Array.Reverse(LocZ) : Writekmp(temp, 16 + (current - (i + 1)) * &H14, LocZ)
                    Scale = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(4))) : Array.Reverse(Scale) : Writekmp(temp, 20 + (current - (i + 1)) * &H14, Scale)
                    S1 = MainForm.XMLWrite(rows(current).Split(";")(5), "XML/ENPT/Setting1", "Tablefill.xml") : Array.Reverse(S1) : Writekmp(temp, 24 + (current - (i + 1)) * &H14, S1, 2)
                    S2 = MainForm.XMLWrite(rows(current).Split(";")(6), "XML/ENPT/Setting2", "Tablefill.xml") : Array.Reverse(S2)
                    Writekmp(temp, 26 + (current - (i + 1)) * &H14, S2, 2)
                Next
                ENPT = temp
                Leftat = i : Read = False
                Exit For
            End If
        Next

        For i = Leftat To rows.Length - 1
            If Read = False Then
                If rows(i).Split(";")(0) = "<ITPT>" Then
                    Read = True

                    count = 0
                    For curro = i + 2 To rows.Length - 1
                        If Not rows(curro).Split(";")(0) = "</ITPT>" Then : count += 1
                        Else : Exit For
                        End If
                    Next

                    Dim temp2(0 To count * &H14 + 7) As Byte
                    temp = temp2
                    Dim name As Byte() = {MainForm.Strtoint("I"), MainForm.Strtoint("T"), MainForm.Strtoint("P"), MainForm.Strtoint("T")}
                    Writekmp(temp, 0, name)
                    Dim amount As Byte() = BitConverter.GetBytes(count) : Array.Reverse(amount)
                    Writekmp(temp, 4, amount, 2)
                End If
            Else
                Dim cells As String() = rows(i).Split(";")
                For current = i + 1 To i + count
                    Dim LocX, LocY, LocZ, Scale, S1, S2 As Byte()
                    LocX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(1))) : Array.Reverse(LocX) : Writekmp(temp, 8 + (current - (i + 1)) * &H14, LocX)
                    LocY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(2))) : Array.Reverse(LocY) : Writekmp(temp, 12 + (current - (i + 1)) * &H14, LocY)
                    LocZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(3))) : Array.Reverse(LocZ) : Writekmp(temp, 16 + (current - (i + 1)) * &H14, LocZ)
                    Scale = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(4))) : Array.Reverse(Scale) : Writekmp(temp, 20 + (current - (i + 1)) * &H14, Scale)
                    S1 = MainForm.XMLWrite(rows(current).Split(";")(5), "XML/ITPT/Setting1", "Tablefill.xml") : Array.Reverse(S1) : Writekmp(temp, 24 + (current - (i + 1)) * &H14, S1, 2)
                    S2 = MainForm.XMLWrite(rows(current).Split(";")(6), "XML/ITPT/Setting2", "Tablefill.xml") : Array.Reverse(S2) : Writekmp(temp, 26 + (current - (i + 1)) * &H14, S2, 2)
                Next
                ITPT = temp
                Leftat = i : Read = False
                Exit For
            End If
        Next

        For i = Leftat To rows.Length - 1
            If Read = False Then
                If rows(i).Split(";")(0) = "<CKPT>" Then
                    Read = True

                    count = 0
                    For curro = i + 2 To rows.Length - 1
                        If Not rows(curro).Split(";")(0) = "</CKPT>" Then : count += 1
                        Else : Exit For
                        End If
                    Next

                    Dim temp2(0 To count * &H14 + 7) As Byte
                    temp = temp2
                    Dim name As Byte() = {MainForm.Strtoint("C"), MainForm.Strtoint("K"), MainForm.Strtoint("P"), MainForm.Strtoint("T")}
                    Writekmp(temp, 0, name)
                    Dim amount As Byte() = BitConverter.GetBytes(count) : Array.Reverse(amount)
                    Writekmp(temp, 4, amount, 2)
                End If
            Else
                Dim cells As String() = rows(i).Split(";")
                For current = i + 1 To i + count
                    Dim LocX, LocZ, LocX2, LocZ2, Rspn, Key, Lst, Nxt As Byte()
                    Dim temp3 As Integer
                    LocX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(1))) : Array.Reverse(LocX) : Writekmp(temp, 8 + (current - (i + 1)) * &H14, LocX)
                    LocZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(2))) : Array.Reverse(LocZ) : Writekmp(temp, 12 + (current - (i + 1)) * &H14, LocZ)
                    LocX2 = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(3))) : Array.Reverse(LocX2) : Writekmp(temp, 16 + (current - (i + 1)) * &H14, LocX2)
                    LocZ2 = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(4))) : Array.Reverse(LocZ2) : Writekmp(temp, 20 + (current - (i + 1)) * &H14, LocZ2)
                    temp3 = "0" & rows(current).Split(";")(5)
                    Rspn = BitConverter.GetBytes(temp3) : Array.Reverse(Rspn) : Writekmp(temp, 24 + (current - (i + 1)) * &H14, Rspn, 1)
                    temp3 = "&h" & "0" & rows(current).Split(";")(6)
                    Key = BitConverter.GetBytes(temp3) : Array.Reverse(Key) : Writekmp(temp, 25 + (current - (i + 1)) * &H14, Key, 1)
                    temp3 = "0" & rows(current).Split(";")(7)
                    Lst = BitConverter.GetBytes(temp3) : Array.Reverse(Lst) : Writekmp(temp, 26 + (current - (i + 1)) * &H14, Lst, 1)
                    temp3 = "0" & rows(current).Split(";")(8)
                    Nxt = BitConverter.GetBytes(temp3) : Array.Reverse(Nxt) : Writekmp(temp, 27 + (current - (i + 1)) * &H14, Nxt, 1)
                Next
                CKPT = temp : Leftat = i
                Exit For
            End If
        Next

        For i = Leftat To rows.Length - 1
            If Read = False Then
                If rows(i).Split(";")(0) = "<GOBJ>" Then
                    Read = True

                    count = 0
                    For curro = i + 2 To rows.Length - 1
                        If Not rows(curro).Split(";")(0) = "</GOBJ>" Then : count += 1
                        Else : Exit For
                        End If
                    Next

                    Dim temp2(0 To count * &H3C + 7) As Byte
                    temp = temp2
                    Dim name As Byte() = {MainForm.Strtoint("G"), MainForm.Strtoint("O"), MainForm.Strtoint("B"), MainForm.Strtoint("J")}
                    Writekmp(temp2, 0, name)
                    Dim amount As Byte() = BitConverter.GetBytes(count) : Array.Reverse(amount)
                    Writekmp(temp2, 4, amount, 2)
                End If
            Else
                Dim cells As String() = rows(i).Split(";")
                For current = i + 1 To i + count
                    Dim ID, LocX, LocY, LocZ, RotX, RotY, RotZ, SclX, SclY, SclZ, route, s1, s2, s3, s4, s5, s6, s7, s8, Pres As Byte()
                    Dim temp2 As Integer = 0
                    ID = MainForm.XMLWrite(rows(current).Split(";")(1), "XML/items/item", "ObjCollection.xml") : Array.Reverse(ID) : Writekmp(temp, 8 + (current - (i + 1)) * &H3C, ID, 2)
                    LocX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(2))) : Array.Reverse(LocX) : Writekmp(temp, 12 + (current - (i + 1)) * &H3C, LocX)
                    LocY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(3))) : Array.Reverse(LocY) : Writekmp(temp, 16 + (current - (i + 1)) * &H3C, LocY)
                    LocZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(4))) : Array.Reverse(LocZ) : Writekmp(temp, 20 + (current - (i + 1)) * &H3C, LocZ)
                    RotX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(5))) : Array.Reverse(RotX) : Writekmp(temp, 24 + (current - (i + 1)) * &H3C, RotX)
                    RotY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(6))) : Array.Reverse(RotY) : Writekmp(temp, 28 + (current - (i + 1)) * &H3C, RotY)
                    RotZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(7))) : Array.Reverse(RotZ) : Writekmp(temp, 32 + (current - (i + 1)) * &H3C, RotZ)
                    SclX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(8))) : Array.Reverse(SclX) : Writekmp(temp, 36 + (current - (i + 1)) * &H3C, SclX)
                    SclY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(9))) : Array.Reverse(SclY) : Writekmp(temp, 40 + (current - (i + 1)) * &H3C, SclY)
                    SclZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(10))) : Array.Reverse(SclZ) : Writekmp(temp, 44 + (current - (i + 1)) * &H3C, SclZ)
                    temp2 = "&h" & "0" & rows(current).Split(";")(11) : route = BitConverter.GetBytes(temp2) : Array.Reverse(route) : Writekmp(temp, 48 + (current - (i + 1)) * &H3C, route, 2)
                    temp2 = "&h" & "0" & rows(current).Split(";")(12) : s1 = BitConverter.GetBytes(temp2) : Array.Reverse(s1) : Writekmp(temp, 50 + (current - (i + 1)) * &H3C, s1, 2)
                    temp2 = "&h" & "0" & rows(current).Split(";")(13) : s2 = BitConverter.GetBytes(temp2) : Array.Reverse(s2) : Writekmp(temp, 52 + (current - (i + 1)) * &H3C, s2, 2)
                    temp2 = "&h" & "0" & rows(current).Split(";")(14) : s3 = BitConverter.GetBytes(temp2) : Array.Reverse(s3) : Writekmp(temp, 54 + (current - (i + 1)) * &H3C, s3, 2)
                    temp2 = "&h" & "0" & rows(current).Split(";")(15) : s4 = BitConverter.GetBytes(temp2) : Array.Reverse(s4) : Writekmp(temp, 56 + (current - (i + 1)) * &H3C, s4, 2)
                    temp2 = "&h" & "0" & rows(current).Split(";")(16) : s5 = BitConverter.GetBytes(temp2) : Array.Reverse(s5) : Writekmp(temp, 58 + (current - (i + 1)) * &H3C, s5, 2)
                    temp2 = "&h" & "0" & rows(current).Split(";")(17) : s6 = BitConverter.GetBytes(temp2) : Array.Reverse(s6) : Writekmp(temp, 60 + (current - (i + 1)) * &H3C, s6, 2)
                    temp2 = "&h" & "0" & rows(current).Split(";")(18) : s7 = BitConverter.GetBytes(temp2) : Array.Reverse(s7) : Writekmp(temp, 62 + (current - (i + 1)) * &H3C, s7, 2)
                    temp2 = "&h" & "0" & rows(current).Split(";")(19) : s8 = BitConverter.GetBytes(temp2) : Array.Reverse(s8) : Writekmp(temp, 64 + (current - (i + 1)) * &H3C, s8, 2)
                    temp2 = "&h" & "0" & rows(current).Split(";")(20) : Pres = BitConverter.GetBytes(temp2) : Array.Reverse(Pres) : Writekmp(temp, 66 + (current - (i + 1)) * &H3C, Pres, 2)
                Next
                GOBJ = temp
                Leftat = i : Read = False
                Exit For
            End If
        Next

        For i = Leftat To rows.Length - 1
            If Read = False Then
                If rows(i).Split(";")(0) = "<POTI>" Then
                    Read = True

                    count = 0
                    For curro = i + 2 To rows.Length - 1
                        If rows(curro) = "" And count > 0 Then
                            count += 1
                        ElseIf Not rows(curro) = "" And count = 0 Then
                            count = 1
                        End If
                        If rows(curro).Split(";")(0) = "</POTI>" Then Exit For
                    Next
                End If
            Else
                Dim currow As Integer = i + 1
                Dim newroutes(0 To count - 1) As Route
                MainForm.Parsedroutes = newroutes
                For curent = 0 To count - 1
                    Dim Thepoints As New ArrayList
                    Dim newcur As Integer
                    For row = currow To rows.Length - 1
                        If rows(row) = "" Or rows(row) = "</POTI>" Then
                            newcur = row + 1
                            Exit For
                        End If
                        Dim Thepoint As New Route.Point
                        Thepoint.Location(0) = rows(row).Split(";")(1)
                        Thepoint.Location(1) = rows(row).Split(";")(2)
                        Thepoint.Location(2) = rows(row).Split(";")(3)
                        Thepoint.Pointsettings = "&H" & "0" & rows(row).Split(";")(4)
                        Thepoint.Additional = "&H" & "0" & rows(row).Split(";")(5)
                        Thepoints.Add(Thepoint)
                    Next

                    MainForm.Parsedroutes(curent) = New Route
                    MainForm.Parsedroutes(curent).points = CType(Thepoints.ToArray(GetType(Route.Point)), Route.Point())
                    MainForm.Parsedroutes(curent).Count = Thepoints.Count
                    currow = newcur
                Next
                Leftat = i : Read = False
                Exit For
            End If
        Next

        For i = Leftat To rows.Length - 1
            If Read = False Then
                If rows(i).Split(";")(0) = "<AREA>" Then
                    Read = True

                    count = 0
                    For curro = i + 2 To rows.Length - 1
                        If Not rows(curro).Split(";")(0) = "</AREA>" Then : count += 1
                        Else : Exit For
                        End If
                    Next

                    Dim temp2(0 To count * &H30 + 7) As Byte
                    AREA = temp2
                    Dim name As Byte() = {MainForm.Strtoint("A"), MainForm.Strtoint("R"), MainForm.Strtoint("E"), MainForm.Strtoint("A")}
                    Writekmp(AREA, 0, name)
                    Dim amount As Byte() = BitConverter.GetBytes(count) : Array.Reverse(amount)
                    Writekmp(AREA, 4, amount, 2)
                End If
            Else
                Dim cells As String() = rows(i).Split(";")
                For current = i + 1 To i + count
                    Dim Mode, Type, Cam, LocX, LocY, LocZ, RotX, RotY, RotZ, SclX, SclY, SclZ, Setting, Route, Enemy As Byte()
                    Dim temp2 As Integer
                    temp2 = "&h" & "0" & rows(current).Split(";")(1)
                    Mode = BitConverter.GetBytes(temp2) : Array.Reverse(Mode) : Writekmp(AREA, 8 + (current - (i + 1)) * &H30, Mode, 1)
                    Type = MainForm.XMLWrite(rows(current).Split(";")(2), "XML/AREA/Variants", "Tablefill.xml") : Array.Reverse(Type) : Writekmp(AREA, 9 + (current - (i + 1)) * &H30, Type, 1)
                    temp2 = "&h" & "0" & rows(current).Split(";")(3)
                    Cam = BitConverter.GetBytes(temp2) : Array.Reverse(Cam) : Writekmp(AREA, 10 + (current - (i + 1)) * &H30, Cam, 1)
                    LocX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(4))) : Array.Reverse(LocX) : Writekmp(AREA, 12 + (current - (i + 1)) * &H30, LocX)
                    LocY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(5))) : Array.Reverse(LocY) : Writekmp(AREA, 16 + (current - (i + 1)) * &H30, LocY)
                    LocZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(6))) : Array.Reverse(LocZ) : Writekmp(AREA, 20 + (current - (i + 1)) * &H30, LocZ)
                    RotX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(7))) : Array.Reverse(RotX) : Writekmp(AREA, 24 + (current - (i + 1)) * &H30, RotX)
                    RotY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(8))) : Array.Reverse(RotY) : Writekmp(AREA, 28 + (current - (i + 1)) * &H30, RotY)
                    RotZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(9))) : Array.Reverse(RotZ) : Writekmp(AREA, 32 + (current - (i + 1)) * &H30, RotZ)
                    SclX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(10))) : Array.Reverse(SclX) : Writekmp(AREA, 36 + (current - (i + 1)) * &H30, SclX)
                    SclY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(11))) : Array.Reverse(SclY) : Writekmp(AREA, 40 + (current - (i + 1)) * &H30, SclY)
                    SclZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(12))) : Array.Reverse(SclZ) : Writekmp(AREA, 44 + (current - (i + 1)) * &H30, SclZ)
                    Dim temp3 As Long = "&H0" & rows(current).Split(";")(13)
                    Setting = BitConverter.GetBytes(temp3) : Array.Reverse(Setting) : Writekmp(AREA, 48 + (current - (i + 1)) * &H30, Setting, 4)
                    temp2 = "&h" & "0" & rows(current).Split(";")(14)
                    Route = BitConverter.GetBytes(temp2) : Array.Reverse(Route) : Writekmp(AREA, 52 + (current - (i + 1)) * &H30, Route, 2)
                    temp2 = "&h" & "0" & rows(current).Split(";")(15)
                    Enemy = BitConverter.GetBytes(temp2) : Array.Reverse(Enemy) : Writekmp(AREA, 54 + (current - (i + 1)) * &H30, Enemy, 2)
                Next
                Leftat = i : Read = False
                Exit For
            End If
        Next

        For i = Leftat To rows.Length - 1
            If Read = False Then
                If rows(i).Split(";")(0) = "<CAME>" Then
                    Read = True

                    count = 0
                    For curro = i + 3 To rows.Length - 1 Step 2
                        If Not rows(curro).Split(";")(0) = "</CAME>" Then : count += 1
                        Else : Exit For
                        End If
                    Next

                    Dim temp2(0 To count * 72 + 7) As Byte
                    CAME = temp2
                    Dim name As Byte() = {MainForm.Strtoint("C"), MainForm.Strtoint("A"), MainForm.Strtoint("M"), MainForm.Strtoint("E")}
                    Writekmp(CAME, 0, name)
                    Dim amount As Byte() = BitConverter.GetBytes(count) : Array.Reverse(amount)
                    Writekmp(CAME, 4, amount, 2)
                End If
            Else
                Dim cells As String() = rows(i).Split(";")
                For current = i + 2 To i + count * 2 Step 2
                    Dim Type, NCam, Shake, Route, V, VZ, VP, flag, LocX, LocY, LocZ, RotX, RotY, RotZ, Zstart, Zend, ViewX, ViewY, ViewZ, View2X, View2Y, View2Z, Time As Byte()
                    Dim temp3 As Integer
                    Type = MainForm.XMLWrite(rows(current).Split(";")(1), "XML/CAME/Variants", "Tablefill.xml") : Array.Reverse(Type) : Writekmp(CAME, 8 + (current - (i + 2)) * 36, Type, 1)
                    temp3 = "&h" & "0" & rows(current).Split(";")(2) : NCam = BitConverter.GetBytes(temp3) : Array.Reverse(NCam) : Writekmp(CAME, 9 + (current - (i + 2)) * 36, NCam, 1)
                    temp3 = "&h" & "0" & rows(current).Split(";")(3) : Shake = BitConverter.GetBytes(temp3) : Array.Reverse(Shake) : Writekmp(CAME, 10 + (current - (i + 2)) * 36, Shake, 1)
                    temp3 = "&h" & "0" & rows(current).Split(";")(4) : Route = BitConverter.GetBytes(temp3) : Array.Reverse(Route) : Writekmp(CAME, 11 + (current - (i + 2)) * 36, Route, 1)
                    temp3 = "&h" & "0" & rows(current).Split(";")(5) : V = BitConverter.GetBytes(temp3) : Array.Reverse(V) : Writekmp(CAME, 12 + (current - (i + 2)) * 36, V, 2)
                    temp3 = "&h" & "0" & rows(current).Split(";")(6) : VZ = BitConverter.GetBytes(temp3) : Array.Reverse(VZ) : Writekmp(CAME, 14 + (current - (i + 2)) * 36, VZ, 2)
                    temp3 = "&h" & "0" & rows(current).Split(";")(7) : VP = BitConverter.GetBytes(temp3) : Array.Reverse(VP) : Writekmp(CAME, 16 + (current - (i + 2)) * 36, VP, 2)
                    temp3 = "&h" & "0" & rows(current).Split(";")(8) : flag = BitConverter.GetBytes(temp3) : Array.Reverse(flag) : Writekmp(CAME, 18 + (current - (i + 2)) * 36, flag, 2)

                    LocX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(9))) : Array.Reverse(LocX) : Writekmp(CAME, 20 + (current - (i + 2)) * 36, LocX)
                    LocY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(10))) : Array.Reverse(LocY) : Writekmp(CAME, 24 + (current - (i + 2)) * 36, LocY)
                    LocZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(11))) : Array.Reverse(LocZ) : Writekmp(CAME, 28 + (current - (i + 2)) * 36, LocZ)
                    RotX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(12))) : Array.Reverse(RotX) : Writekmp(CAME, 32 + (current - (i + 2)) * 36, RotX)
                    RotY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(13))) : Array.Reverse(RotY) : Writekmp(CAME, 36 + (current - (i + 2)) * 36, RotY)
                    RotZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(14))) : Array.Reverse(RotZ) : Writekmp(CAME, 40 + (current - (i + 2)) * 36, RotZ)
                    Zstart = BitConverter.GetBytes(Floattohex(rows(current + 1).Split(";")(1))) : Array.Reverse(Zstart) : Writekmp(CAME, 44 + (current - (i + 2)) * 36, Zstart)
                    Zend = BitConverter.GetBytes(Floattohex(rows(current + 1).Split(";")(2))) : Array.Reverse(Zend) : Writekmp(CAME, 48 + (current - (i + 2)) * 36, Zend)
                    ViewX = BitConverter.GetBytes(Floattohex(rows(current + 1).Split(";")(3))) : Array.Reverse(ViewX) : Writekmp(CAME, 52 + (current - (i + 2)) * 36, ViewX)
                    ViewY = BitConverter.GetBytes(Floattohex(rows(current + 1).Split(";")(4))) : Array.Reverse(ViewY) : Writekmp(CAME, 56 + (current - (i + 2)) * 36, ViewY)
                    ViewZ = BitConverter.GetBytes(Floattohex(rows(current + 1).Split(";")(5))) : Array.Reverse(ViewZ) : Writekmp(CAME, 60 + (current - (i + 2)) * 36, ViewZ)
                    View2X = BitConverter.GetBytes(Floattohex(rows(current + 1).Split(";")(6))) : Array.Reverse(View2X) : Writekmp(CAME, 64 + (current - (i + 2)) * 36, View2X)
                    View2Y = BitConverter.GetBytes(Floattohex(rows(current + 1).Split(";")(7))) : Array.Reverse(View2Y) : Writekmp(CAME, 68 + (current - (i + 2)) * 36, View2Y)
                    View2Z = BitConverter.GetBytes(Floattohex(rows(current + 1).Split(";")(8))) : Array.Reverse(View2Z) : Writekmp(CAME, 72 + (current - (i + 2)) * 36, View2Z)
                    Time = BitConverter.GetBytes(Floattohex(rows(current + 1).Split(";")(9))) : Array.Reverse(Time) : Writekmp(CAME, 76 + (current - (i + 2)) * 36, Time)
                Next
                Leftat = i : Read = False
                Exit For
            End If
        Next

        For i = Leftat To rows.Length - 1
            If Read = False Then
                If rows(i).Split(";")(0) = "<JGPT>" Then
                    Read = True

                    count = 0
                    For curro = i + 2 To rows.Length - 1
                        If Not rows(curro).Split(";")(0) = "</JGPT>" Then : count += 1
                        Else : Exit For
                        End If
                    Next

                    Dim temp2(0 To count * &H1C + 7) As Byte
                    JGPT = temp2
                    Dim name As Byte() = {MainForm.Strtoint("J"), MainForm.Strtoint("G"), MainForm.Strtoint("P"), MainForm.Strtoint("T")}
                    Writekmp(JGPT, 0, name)
                    Dim amount As Byte() = BitConverter.GetBytes(count) : Array.Reverse(amount)
                    Writekmp(JGPT, 4, amount, 2)
                End If
            Else
                Dim cells As String() = rows(i).Split(";")
                For current = i + 1 To i + count
                    Dim LocX, LocY, LocZ, RotX, RotY, RotZ, ID, Scale As Byte()
                    Dim temp2 As Integer
                    LocX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(1))) : Array.Reverse(LocX) : Writekmp(JGPT, 8 + (current - (i + 1)) * 28, LocX)
                    LocY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(2))) : Array.Reverse(LocY) : Writekmp(JGPT, 12 + (current - (i + 1)) * 28, LocY)
                    LocZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(3))) : Array.Reverse(LocZ) : Writekmp(JGPT, 16 + (current - (i + 1)) * 28, LocZ)
                    RotX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(4))) : Array.Reverse(RotX) : Writekmp(JGPT, 20 + (current - (i + 1)) * 28, RotX)
                    RotY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(5))) : Array.Reverse(RotY) : Writekmp(JGPT, 24 + (current - (i + 1)) * 28, RotY)
                    RotZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(6))) : Array.Reverse(RotZ) : Writekmp(JGPT, 28 + (current - (i + 1)) * 28, RotZ)
                    temp2 = "0" & rows(current).Split(";")(7)
                    ID = BitConverter.GetBytes(temp2) : Array.Reverse(ID) : Writekmp(JGPT, 32 + (current - (i + 1)) * 28, ID, 2)
                    temp2 = "&h" & "0" & rows(current).Split(";")(8)
                    Scale = BitConverter.GetBytes(temp2) : Array.Reverse(Scale) : Writekmp(JGPT, 34 + (current - (i + 1)) * 28, Scale, 2)
                Next
                Leftat = i : Read = False
                Exit For
            End If
        Next

        For i = Leftat To rows.Length - 1
            If Read = False Then
                If rows(i).Split(";")(0) = "<CNPT>" Then
                    Read = True

                    count = 0
                    For curro = i + 2 To rows.Length - 1
                        If Not rows(curro).Split(";")(0) = "</CNPT>" Then : count += 1
                        Else : Exit For
                        End If
                    Next

                    Dim temp2(0 To count * &H1C + 7) As Byte
                    CNPT = temp2
                    Dim name As Byte() = {MainForm.Strtoint("C"), MainForm.Strtoint("N"), MainForm.Strtoint("P"), MainForm.Strtoint("T")}
                    Writekmp(CNPT, 0, name)
                    Dim amount As Byte() = BitConverter.GetBytes(count) : Array.Reverse(amount)
                    Writekmp(CNPT, 4, amount, 2)
                End If
            Else
                Dim cells As String() = rows(i).Split(";")
                For current = i + 1 To i + count
                    Dim LocX, LocY, LocZ, RotX, RotY, RotZ, ID, S1 As Byte()
                    Dim temp2 As Integer
                    LocX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(1))) : Array.Reverse(LocX) : Writekmp(CNPT, 8 + (current - (i + 1)) * 28, LocX)
                    LocY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(2))) : Array.Reverse(LocY) : Writekmp(CNPT, 12 + (current - (i + 1)) * 28, LocY)
                    LocZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(3))) : Array.Reverse(LocZ) : Writekmp(CNPT, 16 + (current - (i + 1)) * 28, LocZ)
                    RotX = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(4))) : Array.Reverse(RotX) : Writekmp(CNPT, 20 + (current - (i + 1)) * 28, RotX)
                    RotY = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(5))) : Array.Reverse(RotY) : Writekmp(CNPT, 24 + (current - (i + 1)) * 28, RotY)
                    RotZ = BitConverter.GetBytes(Floattohex(rows(current).Split(";")(6))) : Array.Reverse(RotZ) : Writekmp(CNPT, 28 + (current - (i + 1)) * 28, RotZ)
                    temp2 = "0" & rows(current).Split(";")(7)
                    ID = BitConverter.GetBytes(temp2) : Array.Reverse(ID) : Writekmp(CNPT, 32 + (current - (i + 1)) * 28, ID, 2)
                    S1 = MainForm.XMLWrite(rows(current).Split(";")(8), "XML/CNPT/Effects", "Tablefill.xml") : Array.Reverse(S1) : Writekmp(CNPT, 34 + (current - (i + 1)) * 28, S1, 2)
                Next
                Leftat = i : Read = False
                Exit For
            End If
        Next
    End Sub
End Module
