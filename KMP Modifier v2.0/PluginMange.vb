Imports System.Windows.Forms

Public Class PluginMange

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub PluginMange_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        KTPTBox.Items.Clear()
        Dim split As String() = Mid(My.Settings.KTPTPlugins, 1).Split(New [Char]() {"+"c, CChar(vbTab)})
        For Each element In split
            KTPTBox.Items.Add(element)
        Next
        KTPTBox.Text = My.Settings.KTPTPlugin


        ENPTBox.Items.Clear()
        split = Mid(My.Settings.ENPTPlugins, 1).Split(New [Char]() {"+"c, CChar(vbTab)})
        For Each element In split
            ENPTBox.Items.Add(element)
        Next
        ENPTBox.Text = My.Settings.ENPTPlugin


        ENPHBox.Items.Clear()
        split = Mid(My.Settings.ENPHPlugins, 1).Split(New [Char]() {"+"c, CChar(vbTab)})
        For Each element In split
            ENPHBox.Items.Add(element)
        Next
        ENPHBox.Text = My.Settings.ENPHPlugin


        ITPTBox.Items.Clear()
        split = Mid(My.Settings.ITPTPlugins, 1).Split(New [Char]() {"+"c, CChar(vbTab)})
        For Each element In split
            ITPTBox.Items.Add(element)
        Next
        ITPTBox.Text = My.Settings.ITPTPlugin

        ITPHBox.Items.Clear()
        split = Mid(My.Settings.ITPHPlugins, 1).Split(New [Char]() {"+"c, CChar(vbTab)})
        For Each element In split
            ITPHBox.Items.Add(element)
        Next
        ITPHBox.Text = My.Settings.ITPHPlugin


        CKPTBox.Items.Clear()
        split = Mid(My.Settings.CKPTPlugins, 1).Split(New [Char]() {"+"c, CChar(vbTab)})
        For Each element In split
            CKPTBox.Items.Add(element)
        Next
        CKPTBox.Text = My.Settings.CKPTPlugin


        CKPHBox.Items.Clear()
        split = Mid(My.Settings.CKPHPlugins, 1).Split(New [Char]() {"+"c, CChar(vbTab)})
        For Each element In split
            CKPHBox.Items.Add(element)
        Next
        CKPHBox.Text = My.Settings.CKPHPlugin


        GOBJBox.Items.Clear()
        split = Mid(My.Settings.GOBJPlugins, 1).Split(New [Char]() {"+"c, CChar(vbTab)})
        For Each element In split
            GOBJBox.Items.Add(element)
        Next
        GOBJBox.Text = My.Settings.GOBJPlugin


        POTIBox.Items.Clear()
        split = Mid(My.Settings.POTIPlugins, 1).Split(New [Char]() {"+"c, CChar(vbTab)})
        For Each element In split
            POTIBox.Items.Add(element)
        Next
        POTIBox.Text = My.Settings.POTIPlugin


        GOBJBox.Items.Clear()
        split = Mid(My.Settings.GOBJPlugins, 1).Split(New [Char]() {"+"c, CChar(vbTab)})
        For Each element In split
            GOBJBox.Items.Add(element)
        Next
        GOBJBox.Text = My.Settings.GOBJPlugin


        AREABox.Items.Clear()
        split = Mid(My.Settings.AREAPlugins, 1).Split(New [Char]() {"+"c, CChar(vbTab)})
        For Each element In split
            AREABox.Items.Add(element)
        Next
        AREABox.Text = My.Settings.AREAPlugin


        JGPTBox.Items.Clear()
        split = Mid(My.Settings.JGPTPlugins, 1).Split(New [Char]() {"+"c, CChar(vbTab)})
        For Each element In split
            JGPTBox.Items.Add(element)
        Next
        JGPTBox.Text = My.Settings.JGPTPlugin


        CNPTBox.Items.Clear()
        split = Mid(My.Settings.CNPTPlugins, 1).Split(New [Char]() {"+"c, CChar(vbTab)})
        For Each element In split
            CNPTBox.Items.Add(element)
        Next
        CNPTBox.Text = My.Settings.CNPTPlugin


        MSPTBox.Items.Clear()
        split = Mid(My.Settings.MSPTPlugins, 1).Split(New [Char]() {"+"c, CChar(vbTab)})
        For Each element In split
            MSPTBox.Items.Add(element)
        Next
        MSPTBox.Text = My.Settings.MSPTPlugin
    End Sub

    Private Sub Button_Click(sender As System.Object, e As System.EventArgs) Handles KTPTButton.Click, ENPTButton.Click, ENPHButton.Click,
                                                                                     ITPTButton.Click, ITPHButton.Click, CKPTButton.Click,
                                                                                     CKPHButton.Click, POTIButton.Click, GOBJButton.Click,
                                                                                     AREAButton.Click, CAMEButton.Click, JGPTButton.Click,
                                                                                     CNPTButton.Click, MSPTButton.Click
        Dim a As Button = sender
        Dim dlg As New OpenFileDialog
        dlg.Filter = "All supported plugins|*.exe|*.dll"
        Try
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                If a.Tag = 1 Then : KTPTBox.Text = dlg.FileName : My.Settings.KTPTPlugins = My.Settings.KTPTPlugins & "+" & dlg.FileName : My.Settings.KTPTPlugin = dlg.FileName
                ElseIf a.Tag = 2 Then : ENPTBox.Text = dlg.FileName : My.Settings.ENPTPlugins = My.Settings.ENPTPlugins & "+" & dlg.FileName : My.Settings.ENPTPlugin = dlg.FileName
                ElseIf a.Tag = 3 Then : ENPHBox.Text = dlg.FileName : My.Settings.ENPHPlugins = My.Settings.ENPHPlugins & "+" & dlg.FileName : My.Settings.ENPHPlugin = dlg.FileName
                ElseIf a.Tag = 4 Then : ITPTBox.Text = dlg.FileName : My.Settings.ITPTPlugins = My.Settings.ITPTPlugins & "+" & dlg.FileName : My.Settings.ITPTPlugin = dlg.FileName
                ElseIf a.Tag = 5 Then : ITPHBox.Text = dlg.FileName : My.Settings.ITPHPlugins = My.Settings.ITPHPlugins & "+" & dlg.FileName : My.Settings.ITPHPlugin = dlg.FileName
                ElseIf a.Tag = 6 Then : CKPTBox.Text = dlg.FileName : My.Settings.CKPTPlugins = My.Settings.CKPTPlugins & "+" & dlg.FileName : My.Settings.CKPTPlugin = dlg.FileName
                ElseIf a.Tag = 7 Then : CKPHBox.Text = dlg.FileName : My.Settings.CKPHPlugins = My.Settings.CKPHPlugins & "+" & dlg.FileName : My.Settings.CKPHPlugin = dlg.FileName
                ElseIf a.Tag = 8 Then : GOBJBox.Text = dlg.FileName : My.Settings.GOBJPlugins = My.Settings.GOBJPlugins & "+" & dlg.FileName : My.Settings.GOBJPlugin = dlg.FileName
                ElseIf a.Tag = 9 Then : POTIBox.Text = dlg.FileName : My.Settings.POTIPlugins = My.Settings.POTIPlugins & "+" & dlg.FileName : My.Settings.POTIPlugin = dlg.FileName
                ElseIf a.Tag = 10 Then : AREABox.Text = dlg.FileName : My.Settings.AREAPlugins = My.Settings.AREAPlugins & "+" & dlg.FileName : My.Settings.AREAPlugin = dlg.FileName
                ElseIf a.Tag = 11 Then : CAMEBox.Text = dlg.FileName : My.Settings.CAMEPlugins = My.Settings.CAMEPlugins & "+" & dlg.FileName : My.Settings.CAMEPlugin = dlg.FileName
                ElseIf a.Tag = 12 Then : JGPTBox.Text = dlg.FileName : My.Settings.JGPTPlugins = My.Settings.JGPTPlugins & "+" & dlg.FileName : My.Settings.JGPTPlugin = dlg.FileName
                ElseIf a.Tag = 13 Then : CNPTBox.Text = dlg.FileName : My.Settings.CNPTPlugins = My.Settings.CNPTPlugins & "+" & dlg.FileName : My.Settings.CNPTPlugin = dlg.FileName
                ElseIf a.Tag = 14 Then : MSPTBox.Text = dlg.FileName : My.Settings.MSPTPlugins = My.Settings.MSPTPlugins & "+" & dlg.FileName : My.Settings.MSPTPlugin = dlg.FileName
                End If
            End If
        Catch ex As Exception : MsgBox(ex.Message)
        End Try
    End Sub
End Class
