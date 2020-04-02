Imports System
Imports Chadsoft.CTools
Imports Chadsoft.CTools.ToolInfo
Imports System.Collections.ObjectModel
Imports System.Drawing
Imports System.Windows.Forms.DataObject

Public Module ToolInfo
    Private _tool As Tool
    Private KmpMod As Editor

    Public ReadOnly Property Tool As Tool
        Get
            If IsNothing(_tool) Then
                SetupTool()
            End If
            Return _tool
        End Get
    End Property

    Private Sub SetupTool()
        Dim formats As ReadOnlyCollection(Of FileFormat)

        formats = GetFormats()
        _tool = New Tool(
            "KMP Modifier",
            My.Application.Info.Description,
            "kHacker",
            My.Application.Info.Version,
            My.Resources.kmp1,
            GetEditors(formats),
            formats,
            GetNewFiles(formats))
    End Sub

    Private Function GetFormats() As ReadOnlyCollection(Of FileFormat)
        Return New ReadOnlyCollection(Of FileFormat)({New FileFormat("Nintendo KMP File", "Course Information File", "Data Files", My.Resources.kmp1, AddressOf BmgFormatMatch)})
    End Function

    Private Function GetEditors(formats As ReadOnlyCollection(Of FileFormat)) As ReadOnlyCollection(Of Editor)
        Return New ReadOnlyCollection(Of Editor)({New Editor("KMP Modifier", My.Application.Info.Description, "kHacker", My.Application.Info.Version, My.Resources.kmp1, formats, AddressOf CreateInstance, AddressOf RenderPreview)})
    End Function

    Private Function GetNewFiles(formats As ReadOnlyCollection(Of FileFormat))
        Return New ReadOnlyCollection(Of NewFile)({New NewFile("Nintendo KMP File", "new.kmp", "Course Information File", My.Resources.kmp1, formats(0), My.Resources.Empty)})
    End Function

    Private Function BmgFormatMatch(name As String, data As Byte(), offset As Integer) As Integer
        If data.Length >= offset + 4 AndAlso data(offset + 0) = &H52 AndAlso data(offset + 1) = &H4B AndAlso data(offset + 2) = &H4D AndAlso data(offset + 3) = &H44 Then
            Return 9999
        Else
            Return 0
        End If
    End Function

    Private Function CreateInstance(data As Byte(), name As String, saveEvent As EventHandler(Of SaveEventArgs), closeEvent As EventHandler) As EditorInstance
        Dim TheInstance As New KMPModInstance(data, KmpMod, saveEvent, closeEvent)

        Return TheInstance
    End Function
    Private Sub RenderPreview(data As Byte(), graphics As Graphics)
        ' TBD
    End Sub

End Module