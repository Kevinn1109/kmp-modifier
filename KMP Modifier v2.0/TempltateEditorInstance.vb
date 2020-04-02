Imports Chadsoft.CTools

Public Class KMPModInstance
    Inherits EditorInstance

    Public Shared Instance As KMPModInstance
    Private _editor As Editor
    Private _window As MainForm

    ' This method is used by my tools to determine what type of eitors
    ' it has open.
    Public Overrides ReadOnly Property Editor As Chadsoft.CTools.Editor
        Get
            Return _editor
        End Get
    End Property

    ' This constructor is used by ToolInfo to set up the editor. You should
    ' load the file provided in data.
    Public Sub New(data As Byte(), editor As Editor, saveEvent As EventHandler(Of SaveEventArgs), _
                   closeEvent As EventHandler)
        MyBase.New(data, saveEvent, closeEvent)

        _editor = editor
        _window = New MainForm

        ' TODO: Load the file stored in data.

        _window.fill_arrays(data)
        _window.File = "CTools"
        _window.Instance = Me
        _window.Show()
    End Sub

    ' This method is called by my tools when they wish the window to close
    ' (for example the user closed the szs explorer. If you return false,
    ' the szs explorer will not close either.
    Public Overrides Function CloseEditor() As Boolean
        _window.Close()

        ' If you wish to prompt the user to save before closing for example,
        ' and they say no, return False instead, and it will be assumed that
        ' the editor was unable to close. Otherwise it will be assumed that
        ' it did successfully close.
        Return True
    End Function

    ' This method is for you to use to save, it is not called from elsewhere.
    ' This will return true if the save actually succeeded, useful for things
    ' like prompting the user to save unsaved changes.
    Function Save(data As Byte()) As Boolean
        Dim success As Boolean

        ' TODO: Copy the file into the byte array data.

        Try
            success = OnSave(data)
        Catch ex As Exception
            MessageBox.Show("The file could not be saved." + vbNewLine + ex.Message)
            success = False
        End Try

        Return success
    End Function

    Public Sub NotifyClosed()
        OnClose()
    End Sub
End Class
