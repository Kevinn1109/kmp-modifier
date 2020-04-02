Public Class SectionEdit
    Public SectionID As Integer
    Dim Point As Byte()
    Dim starting As Boolean = True
    Dim buffer2 As Byte()

    Public Sub New(Section As Byte(), ByVal ID As Integer)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Point = Readkmp(Section, 8 + ID * &H10, &H10)
    End Sub

    Public Sub Numericupdown_Enabled()
        NumericUpDown15.Value = Readkmp(Point, 0, 1)
        NumericUpDown16.Value = Readkmp(Point, 1, 1)

        NumericUpDown3.Value = Readkmp(Point, 2, 1)
        If NumericUpDown1.Value < 2 Then
            NumericUpDown4.Value = 255
            NumericUpDown4.Enabled = False
        Else
            NumericUpDown4.Value = Readkmp(Point, 3, 1)
            If NumericUpDown4.Value = 255 Then : NumericUpDown4.Value = 0 : End If
            NumericUpDown4.Enabled = True
        End If
        If NumericUpDown1.Value < 3 Then
            NumericUpDown5.Value = 255
            NumericUpDown5.Enabled = False
        Else
            NumericUpDown5.Value = Readkmp(Point, 4, 1)
            If NumericUpDown5.Value = 255 Then : NumericUpDown5.Value = 0 : End If
            NumericUpDown5.Enabled = True
        End If
        If NumericUpDown1.Value < 4 Then
            NumericUpDown6.Value = 255
            NumericUpDown6.Enabled = False
        Else
            NumericUpDown6.Value = Readkmp(Point, 5, 1)
            If NumericUpDown6.Value = 255 Then : NumericUpDown6.Value = 0 : End If
            NumericUpDown6.Enabled = True
        End If
        If NumericUpDown1.Value < 5 Then
            NumericUpDown7.Value = 255
            NumericUpDown7.Enabled = False
        Else
            NumericUpDown7.Value = Readkmp(Point, 6, 1)
            If NumericUpDown7.Value = 255 Then : NumericUpDown7.Value = 0 : End If
            NumericUpDown7.Enabled = True
        End If
        If NumericUpDown1.Value < 6 Then
            NumericUpDown8.Value = 255
            NumericUpDown8.Enabled = False
        Else
            NumericUpDown8.Value = Readkmp(Point, 7, 1)
            If NumericUpDown8.Value = 255 Then : NumericUpDown8.Value = 0 : End If
            NumericUpDown8.Enabled = True
        End If

        NumericUpDown9.Value = Readkmp(Point, 8, 1)
        If NumericUpDown2.Value < 2 Then
            NumericUpDown10.Value = 255
            NumericUpDown10.Enabled = False
        Else
            NumericUpDown10.Value = Readkmp(Point, 9, 1)
            If NumericUpDown10.Value = 255 Then : NumericUpDown10.Value = 0 : End If
            NumericUpDown10.Enabled = True
        End If
        If NumericUpDown2.Value < 3 Then
            NumericUpDown11.Value = 255
            NumericUpDown11.Enabled = False
        Else
            NumericUpDown11.Value = Readkmp(Point, 10, 1)
            If NumericUpDown11.Value = 255 Then : NumericUpDown11.Value = 0 : End If
            NumericUpDown11.Enabled = True
        End If
        If NumericUpDown2.Value < 4 Then
            NumericUpDown12.Value = 255
            NumericUpDown12.Enabled = False
        Else
            NumericUpDown12.Value = Readkmp(Point, 11, 1)
            If NumericUpDown12.Value = 255 Then : NumericUpDown12.Value = 0 : End If
            NumericUpDown12.Enabled = True
        End If
        If NumericUpDown2.Value < 5 Then
            NumericUpDown13.Value = 255
            NumericUpDown13.Enabled = False
        Else
            NumericUpDown13.Value = Readkmp(Point, 12, 1)
            If NumericUpDown13.Value = 255 Then : NumericUpDown13.Value = 0 : End If
            NumericUpDown13.Enabled = True
        End If
        If NumericUpDown2.Value < 6 Then
            NumericUpDown14.Value = 255
            NumericUpDown14.Enabled = False
        Else
            NumericUpDown14.Value = Readkmp(Point, 13, 1)
            If NumericUpDown14.Value = 255 Then : NumericUpDown14.Value = 0 : End If
            NumericUpDown14.Enabled = True
        End If
    End Sub

    Public Shared Function Readkmp(ByVal Part As Byte(), ByVal IpOffset As Integer, ByVal amount As Integer)
        If amount = 1 Then
            Dim thestring As String
            thestring = "&H" + BitConverter.ToString(Part, IpOffset, 1)
            Dim theint As Integer = thestring
            Return theint
        ElseIf amount = 2 Then
            Dim read As Integer
            Dim bytes As Byte()
            Try
                read = BitConverter.ToInt16(Part, IpOffset)
            Catch ex As Exception
                MsgBox(ex.StackTrace)
            End Try
            bytes = BitConverter.GetBytes(read)
            Array.Reverse(bytes)
            Return BitConverter.ToInt16(bytes, 2)
        ElseIf amount = 4 Then
            Dim read As Integer
            Dim bytes As Byte()
            read = BitConverter.ToInt32(Part, IpOffset)
            bytes = BitConverter.GetBytes(read)
            Array.Reverse(bytes)
            Return BitConverter.ToInt32(bytes, 0)
        ElseIf amount > 7 Then
            Dim read As Integer
            Dim bytes As Byte()
            read = BitConverter.ToInt32(Part, IpOffset)
            bytes = BitConverter.GetBytes(read)
            Dim v1(0 To amount - 1) As Byte
            bytes.CopyTo(v1, 0)

            Dim i As Integer = 4
            Do While i < amount
                read = BitConverter.ToInt32(Part, IpOffset + i)
                bytes = BitConverter.GetBytes(read)
                Dim v2 As Byte() = bytes
                v2.CopyTo(v1, i)
                i = i + 4
            Loop
            v1.Reverse()
            Return v1
        End If
        Return Nothing
    End Function

    Public Shared Sub Writekmp(ByRef Part As Byte(), ByVal Ipoffset As Integer, ByVal Source As Byte(), Optional ByVal Length As Integer = 0)
        For i = 0 To (If(Length = 0, Source.Length - 1, Length - 1))
            If Length = 0 Then
                Part(i + Ipoffset) = Source(i)
            Else
                Part(Ipoffset + Length - 1 - i) = If(i > Source.Length - 1, 0, Source(Source.Length - 1 - i))
            End If
        Next
    End Sub

    Private Sub SectionEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Label1.Text = "Editing Section " & SectionID
        If Readkmp(Point, 2, 1) = 255 Then : MsgBox("No fromsections")
        ElseIf Readkmp(Point, 3, 1) = 255 Then : NumericUpDown1.Value = 1
        ElseIf Readkmp(Point, 4, 1) = 255 Then : NumericUpDown1.Value = 2
        ElseIf Readkmp(Point, 5, 1) = 255 Then : NumericUpDown1.Value = 3
        ElseIf Readkmp(Point, 6, 1) = 255 Then : NumericUpDown1.Value = 4
        ElseIf Readkmp(Point, 7, 1) = 255 Then : NumericUpDown1.Value = 5
        Else : NumericUpDown1.Value = 6
        End If
        If Readkmp(Point, 8, 1) = 255 Then : NumericUpDown2.Value = 0
        ElseIf Readkmp(Point, 9, 1) = 255 Then : NumericUpDown2.Value = 1
        ElseIf Readkmp(Point, 10, 1) = 255 Then : NumericUpDown2.Value = 2
        ElseIf Readkmp(Point, 11, 1) = 255 Then : NumericUpDown2.Value = 3
        ElseIf Readkmp(Point, 12, 1) = 255 Then : NumericUpDown2.Value = 4
        ElseIf Readkmp(Point, 13, 1) = 255 Then : NumericUpDown2.Value = 5
        Else : NumericUpDown2.Value = 6
        End If

        starting = False
        Numericupdown_Enabled()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim FS As IO.FileStream = New IO.FileStream(Mid(Command, 9), IO.FileMode.Open)
        Dim BR As IO.BinaryReader = New IO.BinaryReader(FS)
        BR.BaseStream.Position = Readkmp(buffer2, &H18, 4) + &H4C
        Dim buffer As Byte() = BR.ReadBytes(Readkmp(buffer2, &H1C, 4) - Readkmp(buffer2, &H1C, 4))
        buffer.Reverse()
        BR.Close()
        FS.Close()

        Dim FW As IO.FileStream
        FW = New IO.FileStream(Mid(Command, 1, 4) & ".kmf", IO.FileMode.Create)
        Dim BW As IO.BinaryWriter = New IO.BinaryWriter(FW)
        BW.BaseStream.Position = 0 : BW.Write(buffer)

        Dim a1 As Byte()
        a1 = {Decimal.ToByte(NumericUpDown15.Value)} : a1.Reverse()
        Dim a2 As Byte()
        a2 = {Decimal.ToByte(NumericUpDown16.Value)} : a2.Reverse()
        Dim a3 As Byte()
        a3 = {Decimal.ToByte(NumericUpDown3.Value)} : a3.Reverse()
        Dim a4 As Byte()
        a4 = {Decimal.ToByte(NumericUpDown4.Value)} : a4.Reverse()
        Dim a5 As Byte()
        a5 = {Decimal.ToByte(NumericUpDown5.Value)} : a5.Reverse()
        Dim a6 As Byte()
        a6 = {Decimal.ToByte(NumericUpDown6.Value)} : a6.Reverse()
        Dim a7 As Byte()
        a7 = {Decimal.ToByte(NumericUpDown7.Value)} : a7.Reverse()
        Dim a8 As Byte()
        a8 = {Decimal.ToByte(NumericUpDown8.Value)} : a8.Reverse()
        Dim a9 As Byte()
        a9 = {Decimal.ToByte(NumericUpDown9.Value)} : a9.Reverse()
        Dim a10 As Byte()
        a10 = {Decimal.ToByte(NumericUpDown10.Value)} : a10.Reverse()
        Dim a11 As Byte()
        a11 = {Decimal.ToByte(NumericUpDown11.Value)} : a11.Reverse()
        Dim a12 As Byte()
        a12 = {Decimal.ToByte(NumericUpDown12.Value)} : a12.Reverse()
        Dim a13 As Byte()
        a13 = {Decimal.ToByte(NumericUpDown13.Value)} : a13.Reverse()
        Dim a14 As Byte()
        a14 = {Decimal.ToByte(NumericUpDown14.Value)} : a14.Reverse()

        BW.BaseStream.Position = 0
        BW.BaseStream.Write({&H4B, &H4D, &H48, &H46, 0, 0, 0, &H2C, 2, 0, 0, 0, 0, 1, 0, 20}, 0, 16)
        If Mid(Command, 1, 4) = "ENPH" Then
            BW.BaseStream.Write({0, 0, 0, &H14, &H45, &H4E, &H50, &H48}, 0, 8)
        ElseIf Mid(Command, 1, 4) = "ITPH" Then
            BW.BaseStream.Write({0, 0, 0, &H14, &H49, &H54, &H50, &H48}, 0, 8)
        Else
            BW.BaseStream.Write({0, 0, 0, &H14, &H43, &H4B, &H50, &H48}, 0, 8)
        End If

        BW.BaseStream.Write({0, SectionID, 0, 1}, 0, 4)
        BW.BaseStream.Write({a1(0), a2(0), a3(0), a4(0), a5(0), a6(0), a7(0), a8(0), a9(0), a10(0), a11(0), a12(0), a13(0), a14(0), 0, 0}, 0, &H10)

        Me.Close()
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        If starting = False Then
            Numericupdown_Enabled()
        End If
    End Sub

    Private Sub NumericUpDown2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown2.ValueChanged
        If starting = False Then
            Numericupdown_Enabled()
        End If
    End Sub
End Class