Imports KMP_Modifier_v2.KMPMod.MainForm
Public Class KMFManager
    Dim kmf As String
    Dim pointarrays As ArrayList
    Dim Names As String
    Dim totalsize As Integer

    Public Function Hextostr(ByVal str As String)
        Dim sStr As String

        sStr = ""
        For i = 1 To Len(str) Step 2
            sStr = sStr + Chr(CLng("&H" & Mid(str, i, 2)))
        Next

        Return sStr
    End Function

    Sub Fill()
        Dim FS As New IO.FileStream(kmf, IO.FileMode.Open)
        Dim BR As New IO.BinaryReader(FS)
        BR.BaseStream.Position = 0
        Dim buffer As Byte() = BR.ReadBytes(FS.Length)
        Dim buffer2 As Byte() = buffer
        buffer2.Reverse()
        FS.Close()
        BR.Close()

        Dim magic As Integer = Readkmp(buffer2, 0, 4)
        Dim Version As Byte() = {Readkmp(buffer2, 8, 1), Readkmp(buffer2, 9, 1), Readkmp(buffer2, 10, 1), Readkmp(buffer2, 11, 1)}
        Dim Amount As Integer = Readkmp(buffer2, 12, 2)
        Dim Data As Byte() = Readkmp(buffer2, Readkmp(buffer2, 14, 2), buffer2.Length - Readkmp(buffer2, 14, 2))

        If Not magic = &H4B4D4846 Then : MsgBox("This is not a valid KMF file") : Exit Sub : End If
        Dim Compare As Byte() = {2, 0, 0, 0}
        If Not Version(0) = Compare(0) Or Not Version(1) = Compare(1) Then : MsgBox("This KMF file is not compatible with this version of the KMP Modifier") : Exit Sub : End If

        Dim Pointlength As Integer
        Dim Group As Byte()
        Dim Groups(0 To Amount - 1) As Object

        For i = 0 To Amount - 1
            Dim value As Integer = Readkmp(buffer2, Readkmp(buffer2, &H10 + i * 4, 4), 4)

            If value = &H4B545054 Or value = &H4A475054 Or value = &H434E5054 Or value = &H4D535054 Then
                Pointlength = &H1C
            ElseIf value = &H454E5054 Or value = &H49545054 Or value = &H434B5054 Then
                Pointlength = &H14
            ElseIf value = &H454E5048 Or value = &H49545048 Or value = &H434B5048 Or value = &H504F5449 Then
                Pointlength = &H10
            ElseIf value = &H474F424A Then
                Pointlength = &H3C
            ElseIf value = &H41524541 Then
                Pointlength = &H30
            ElseIf value = &H43414D45 Then
                Pointlength = &H48
            ElseIf value = &H53544749 Then
                Pointlength = &HC
            End If

            Group = Readkmp(buffer2, Readkmp(buffer2, &H10 + i * 4, 4), Pointlength + 8)
            Groups(i) = Group
        Next

        Dim Namearray As New ArrayList

        For Each Element In Groups

            Dim Section As Byte() = Element
            Dim Name As String
            Dim AtPoint As Byte
            Dim AmPoint As Byte

            Dim j As Integer = Readkmp(Section, 0, 4)
            Name = Hextostr(j.ToString("X8"))
            Namearray.Add(Name)
            AtPoint = Readkmp(Section, 4, 2)
            AmPoint = Readkmp(Section, 6, 2)
            Dim Points(0 To AmPoint - 1) As Byte

            Points = Readkmp(Section, 8, Section.Length - 8)
            pointarrays.Add(Points)
        Next
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        Dim dlg As New OpenFileDialog
        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                kmf = dlg.FileName
            Catch ex As Exception : End Try
        End If
    End Sub

End Class