Imports System.Xml
Imports System.Xml.XPath
Imports System.ComponentModel
Imports System.Text
Imports System.Reflection
Imports KMP_Modifier_v2.KMPMod.Filewatcher.Filewatcher

'Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
'    Dim a As Assembly = Assembly.LoadFrom("D:\Users\KEVIN\Documents\Visual Studio 2010\Projects\Skype\Skype\bin\Debug\Skype.exe")
'    For Each t As Type In a.GetExportedTypes
'        MsgBox(t.Name)
'    Next
'End Sub

Public Class MainForm
    Public Shared Header, KTPT, ENPT, ENPH, ITPT, ITPH, CKPT, CKPH, GOBJ, POTI, AREA, CAME, JGPT, CNPT, MSPT, STGI, Backup As Byte()
    Public File As String

    Dim Filesavedcheck As Boolean = False
    Dim RouteUsage() As String
    Dim Errorlist As ArrayList

    Public Combobox1 As New Comboboxje

    Public Gridscale As Single = 501
    Public GridX As Single = 0
    Public GridY As Single = 0

    Public c As PowerPacks.ShapeContainer = New PowerPacks.ShapeContainer
    Public d As PowerPacks.ShapeContainer = New PowerPacks.ShapeContainer
    Public oval As PowerPacks.OvalShape()
    Public oval2 As PowerPacks.OvalShape()
    Public line As PowerPacks.LineShape()
    Dim PWDecrypted As String

    Dim Arrows As Boolean = False

    Public Shared Parsedroutes As Route()
    Dim routeshow As Boolean = False

    Dim Dontsave As Boolean = False

    Public Vertices(0) As Point
    Public Faces(0) As Object
    Public yValues(0) As Single

    Public ObjBG As Boolean = True
    Public PathBG As String

    Dim ToDegrees As Double = 180 / System.Math.PI
    Dim Dodraw As Boolean = False

    Dim Filling As Boolean
    Dim parsing As Boolean = False
    Dim group As String
    Dim selection As Integer
    Dim scrolling As Boolean = False

    Public Instance

    Dim currentDomain As AppDomain = AppDomain.CurrentDomain

    Public Sub Updatehandler()
    End Sub

    Private Sub MainForm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Filesavedcheck = True Then
            Dim a As MsgBoxResult = MsgBox("You have made unsaved changes, do you wish to save?", vbYesNo)
            If a = MsgBoxResult.Yes Then
                SaveToolStripMenuItem_Click(SaveToolStripMenuItem, New System.EventArgs)
            End If
        End If

        If File = "CTools" Then Instance.NotifyClosed()
    End Sub

    Dim thrd1 As New Threading.Thread(AddressOf Updatehandler)
    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandler currentDomain.UnhandledException, AddressOf MyHandler
        
		thrd1.Start()

        'Combobox1.Items.Clear()
        'Combobox1.Items.AddRange(My.Settings.Names.ToArray)

        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(New TabPage(My.Settings.Names(0)))
        TabControl1.TabPages.Add(New TabPage(My.Settings.Names(1)))
        TabControl1.TabPages.Add(New TabPage(My.Settings.Names(2)))
        TabControl1.TabPages.Add(New TabPage(My.Settings.Names(3)))
        TabControl1.TabPages.Add(New TabPage(My.Settings.Names(4)))
        TabControl1.TabPages.Add(New TabPage(My.Settings.Names(5)))
        TabControl1.TabPages.Add(New TabPage(My.Settings.Names(6)))
        TabControl1.TabPages.Add(New TabPage(My.Settings.Names(7)))
        TabControl1.TabPages.Add(New TabPage(My.Settings.Names(8)))
        TabControl1.TabPages.Add(New TabPage(My.Settings.Names(9)))
        TabControl1.TabPages.Add(New TabPage(My.Settings.Names(10)))

        If File = "CTools" Then
            NewToolStripMenuItem.Visible = False
            OpenToolStripMenuItem.Visible = False
            SaveAsToolStripMenuItem.Visible = False
        End If

        Me.DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        UpdateStyles()

        'Try
        '    Dim FS As New IO.FileStream("/temp.kmp", IO.FileMode.Open)
        '    Dim BR As New IO.BinaryReader(FS)
        '    Dim buffer As Byte() = BR.ReadBytes(FS.Length)
        '    fill_arrays(buffer)
        'Catch ex As IO.FileNotFoundException
        'End Try

        c.Dock = DockStyle.Fill
        d.Dock = DockStyle.Fill
        sc.Dock = DockStyle.Fill
        GridPanel.Controls.Add(c)
        GridPanel.Controls.Add(d)
        GridPanel.Controls.Add(sc)

        Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath)


        If Not Command() = "" And File = "" Then
            File = Command()
            Command.Remove(0)
            File = File.Replace(Chr(34), String.Empty)

            Try
retry:
                Dim FS As New IO.FileStream(File, IO.FileMode.Open)
                Dim BR As New IO.BinaryReader(FS)
                BR.BaseStream.Position = 0
                Dim buffer As Byte() = BR.ReadBytes(FS.Length)
                Dim buffer2 As Byte() = buffer
                buffer2.Reverse()
                Dim list As ArrayList = New ArrayList(buffer2)
                'My.Settings.Filebackup = list
                FS.Close()
                BR.Close()
                If Not Readkmp(buffer, 0, 4) = &H524B4D44 Then
                    MsgBox("This is not a valid KMP or SZS file", MsgBoxStyle.Critical, "KMP Modifier")
                Else
                    fill_arrays(buffer)
                    ''Maintabs.Enabled = True
                    ''PointTab.Show()
                    ''PointTab.Select()
                End If
            Catch iox As IO.IOException
                Dim box As MsgBoxResult = MsgBox(iox.Message & Chr(13) & Chr(13) & iox.StackTrace, MsgBoxStyle.RetryCancel)
                If box = MsgBoxResult.Retry Then
                    GoTo retry
                ElseIf box = MsgBoxResult.Cancel Then
                    Exit Try
                End If
            Catch aux As ArgumentException
                MsgBox("There's a problem with this KMP file, please use the errorfinder for details" & Chr(13) & Chr(13) & aux.Message & Chr(13) & aux.StackTrace & Chr(13) & Chr(13) & File)
            End Try
        End If
    End Sub

    Sub MyHandler(sender As Object, args As UnhandledExceptionEventArgs)
        Dim ex As Exception = DirectCast(args.ExceptionObject, Exception)
        Dim box As MsgBoxResult = MsgBox("An unhandled exception occured, would you like to inform the creator of this software about this error to help improving this software?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo)
        If box = MsgBoxResult.Yes Then
            Try
                'Dim PW As Long = "&H" & (Mid(PWDecrypted, 3, 2) & Mid(PWDecrypted, 5, 2) & Mid(PWDecrypted, 7, 2) & Mid(PWDecrypted, 9, 2) & Mid(PWDecrypted, 11, 2) & Mid(PWDecrypted, 13, 2) & Mid(PWDecrypted, 15, 2) & Mid(PWDecrypted, 17, 2))
                'Dim xi As String = PWDecrypted
                'PWDecrypted = PW

                'Dim PsWrd As String = 100 - Mid(PWDecrypted, 1, 2) & 0 + Mid(PWDecrypted, 3, 2) & 50 - Mid(PWDecrypted, 5, 2) & Mid(PWDecrypted, 7, 2) - 50 & 100 - Mid(PWDecrypted, 9, 2) & 0 + Mid(PWDecrypted, 11, 2) & 50 - Mid(PWDecrypted, 13, 2) & Mid(PWDecrypted, 15, 2) - 50
                'PWDecrypted = xi

                Dim PsWrd As String = 32619487

                Dim SmtpServer As New System.Net.Mail.SmtpClient()
                Dim mail As New System.Net.Mail.MailMessage()
                SmtpServer.Credentials = New Net.NetworkCredential("khacker01@hotmail.com", PsWrd)
                SmtpServer.Port = 587
                SmtpServer.Host = "smtp.live.com"
                SmtpServer.EnableSsl = True
                mail = New System.Net.Mail.MailMessage()
                mail.From = New System.Net.Mail.MailAddress("khacker01@hotmail.com")
                mail.To.Add("mail@kevin-noordover.nl")
                mail.Subject = "KMP Modifier v2.0 problem"
                mail.Body = ex.Message & Chr(13) & ex.StackTrace
                SmtpServer.Send(mail)
                MsgBox("mail sent")
            Catch exie As Exception
                MsgBox(ex.ToString)
            End Try
        End If
        Me.Close()
    End Sub

    '------------------------------------------------------------------------------------------------------------------
    '------------------------------------------Layout------------------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------------

    Dim Cellstyle As DataGridViewCellStyle = New DataGridViewCellStyle

    'HELP SUBS
    Public Shared Function XMLRead(ByVal attributeint As Integer, ByVal path As String, ByVal file As String, Optional ReturnInvalid As Boolean = True, Optional length As String = "X4") As String
        Dim doc As XPathDocument
        Dim nav As XPathNavigator
        Dim iter As XPathNodeIterator
        Dim lstNav As XPathNavigator


        doc = New XPathDocument(file)
        nav = doc.CreateNavigator
        iter = nav.Select(path) 'Your node name goes here
        'Loop through the records in that node
        While (iter.MoveNext)
            'Get the data we need from the node
            Dim iterNews As XPathNodeIterator
            lstNav = iter.Current
            iterNews = lstNav.SelectDescendants(XPathNodeType.Element, False)
            'Loop through the child nodes
            Dim attribute As XElement = XElement.Parse(iterNews.Current.OuterXml)

            If "&h" & attribute.@key = attributeint Then
                Dim a As String = attribute.@key
                Return iterNews.Current.Value
                Exit Function
            End If
        End While
        If ReturnInvalid = True Then
            Return "Invalid"
        Else
            Return attributeint.ToString(length)
        End If

    End Function

    Public Function XMLRead(ByVal attributeint As String, ByVal path As String, ByVal file As String, Optional ReturnInvalid As Boolean = True) As String
        Dim doc As XPathDocument
        Dim nav As XPathNavigator
        Dim iter As XPathNodeIterator
        Dim lstNav As XPathNavigator


        doc = New XPathDocument(file)
        nav = doc.CreateNavigator
        iter = nav.Select(path) 'Your node name goes here
        'Loop through the records in that node
        While (iter.MoveNext)
            'Get the data we need from the node
            Dim iterNews As XPathNodeIterator
            lstNav = iter.Current
            iterNews = lstNav.SelectDescendants(XPathNodeType.Element, False)
            'Loop through the child nodes
            Dim attribute As XElement = XElement.Parse(iterNews.Current.OuterXml)

            If attribute.@key = attributeint Then
                Return iterNews.Current.Value
                Exit Function
            End If
        End While
        If ReturnInvalid = True Then
            Return "Invalid"
        Else
            Return attributeint
        End If

    End Function


    Public Function ObjectRead(ByVal ObjID As Integer) As Item
        Dim doc As XPathDocument
        Dim nav As XPathNavigator
        Dim iter As XPathNodeIterator
        Dim lstNav As XPathNavigator

        Try
            doc = New XPathDocument("ObjCollection.xml")
            nav = doc.CreateNavigator
            iter = nav.Select("items/item") 'Your node name goes here
            'Loop through the records in that node
            While (iter.MoveNext)
                'Get the data we need from the node
                Dim iterNews As XPathNodeIterator
                lstNav = iter.Current
                iterNews = lstNav.SelectDescendants(XPathNodeType.Element, False)
                'Loop through the child nodes
                Dim attribute As XElement = XElement.Parse(iterNews.Current.OuterXml)

                If "&h" & attribute.@key = ObjID Then
                    Dim TheObject As New Item
                    TheObject.ID = ObjID
                    Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(iterNews.Current.OuterXml))
                    doc = New XPathDocument(ms)
                    nav = doc.CreateNavigator
                    iter = nav.Select("item/name")
                    While (iter.MoveNext)
                        Dim iterNews1 As XPathNodeIterator
                        lstNav = iter.Current
                        iterNews1 = lstNav.SelectDescendants(XPathNodeType.Element, False)
                        TheObject.Name = iterNews1.Current.InnerXml
                    End While

                    iter = nav.Select("item/description")
                    While (iter.MoveNext)
                        Dim iterNews1 As XPathNodeIterator
                        lstNav = iter.Current
                        iterNews1 = lstNav.SelectDescendants(XPathNodeType.Element, False)
                        TheObject.Description = iterNews1.Current.InnerXml.Replace(Chr(10), "").Replace("      ", "").Replace(Chr(13), "")
                    End While

                    iter = nav.Select("item/characteristics")
                    While (iter.MoveNext)
                        Dim iterNews1 As XPathNodeIterator
                        lstNav = iter.Current
                        iterNews1 = lstNav.SelectDescendants(XPathNodeType.Element, False)
                        Dim Inner As String = iterNews1.Current.InnerXml
                        Dim returned As Boolean() = TheObject.HandleCharacteristics(Inner)
                        TheObject.IsSolid = returned(0)
                        TheObject.Needsroute = returned(1)
                    End While

                    iter = nav.Select("item/setting") 'Your node name goes here
                    'Loop through the records in that node
                    While (iter.MoveNext)
                        'Get the data we need from the node
                        Dim iterNews1 As XPathNodeIterator
                        lstNav = iter.Current
                        iterNews1 = lstNav.SelectDescendants(XPathNodeType.Element, False)
                        'Loop through the child nodes
                        Dim attribute1 As XElement = XElement.Parse(iterNews1.Current.OuterXml)

                        If attribute1.@id = 1 Then
                            TheObject.s1 = {iterNews1.Current.InnerXml, attribute1.@type}
                        ElseIf attribute1.@id = 2 Then
                            TheObject.s2 = {iterNews1.Current.InnerXml, attribute1.@type}
                        ElseIf attribute1.@id = 3 Then
                            TheObject.s3 = {iterNews1.Current.InnerXml, attribute1.@type}
                        ElseIf attribute1.@id = 4 Then
                            TheObject.s4 = {iterNews1.Current.InnerXml, attribute1.@type}
                        ElseIf attribute1.@id = 5 Then
                            TheObject.s5 = {iterNews1.Current.InnerXml, attribute1.@type}
                        ElseIf attribute1.@id = 6 Then
                            TheObject.s6 = {iterNews1.Current.InnerXml, attribute1.@type}
                        ElseIf attribute1.@id = 7 Then
                            TheObject.s7 = {iterNews1.Current.InnerXml, attribute1.@type}
                        ElseIf attribute1.@id = 8 Then
                            TheObject.s8 = {iterNews1.Current.InnerXml, attribute1.@type}
                        End If
                    End While

                    Dim fulllist(0 To 7) As Object
                    For i = 1 To 8
                        Dim List As New ArrayList
                        iter = nav.Select("item/Entry" & i) 'Your node name goes here
                        'Loop through the records in that node
                        While (iter.MoveNext)
                            'Get the data we need from the node
                            Dim iterNews1 As XPathNodeIterator
                            lstNav = iter.Current
                            iterNews1 = lstNav.SelectDescendants(XPathNodeType.Element, False)
                            'Loop through the child nodes
                            List.Add(iterNews1.Current.InnerXml)
                        End While
                        fulllist(i - 1) = CType(List.ToArray(GetType(String)), String())
                    Next
                    TheObject.ids = fulllist
                    Return TheObject
                End If
            End While
            MsgBox("There is no object with id " & ObjID & " in ObjCollection.xml. This may mean that you have entered an invalid id.")
        Catch ex As Exception : MsgBox(ex.Message) : End Try
        Dim NewObject As New Item
        NewObject.IsSolid = False
        NewObject.Needsroute = False
        NewObject.Name = ObjID
        NewObject.ID = ObjID
        Return NewObject
    End Function

    Public Function ObjectWrite(ByVal Value As String) As Integer
        Dim doc As XPathDocument
        Dim nav As XPathNavigator
        Dim iter As XPathNodeIterator
        Dim iter2 As XPathNodeIterator
        Dim lstNav As XPathNavigator


        doc = New XPathDocument("ObjCollection.xml")
        nav = doc.CreateNavigator
        iter = nav.Select("items/item") 'Your node name goes here
        'Loop through the records in that node
        While (iter.MoveNext)
            'Get the data we need from the node
            Dim iterNews As XPathNodeIterator
            lstNav = iter.Current
            iterNews = lstNav.SelectDescendants(XPathNodeType.Element, False)
            'Loop through the child nodes
            Dim attribute As XElement = XElement.Parse(iterNews.Current.OuterXml)

            Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(iterNews.Current.OuterXml))
            doc = New XPathDocument(ms)
            nav = doc.CreateNavigator
            iter2 = nav.Select("item/name")
            While (iter2.MoveNext)
                Dim iterNews1 As XPathNodeIterator
                lstNav = iter2.Current
                iterNews1 = lstNav.SelectDescendants(XPathNodeType.Element, False)
                If Value = iterNews1.Current.InnerXml Then
                    Return "&h" & attribute.@key
                End If
            End While
        End While
        MsgBox("An error occured while reading from ObjCollection.xml. This may mean that the xml is not in its orginal state.")
        Return 0
    End Function

    Public Function XMLGet(ByVal index As Integer, ByVal path As String, ByVal file As String)
        Dim doc As XPathDocument
        Dim nav As XPathNavigator
        Dim iter As XPathNodeIterator
        Dim lstNav As XPathNavigator


        doc = New XPathDocument(file)
        nav = doc.CreateNavigator
        iter = nav.Select(path) 'Your node name goes here
        'Loop through the records in that node
        Dim i As Integer = 0
        Do While i < index + 1
            iter.MoveNext()
            i = i + 1
        Loop
        'Get the data we need from the node
        Dim iterNews As XPathNodeIterator
        lstNav = iter.Current
        iterNews = lstNav.SelectDescendants(XPathNodeType.Element, False)

        Return iterNews.Current.Value
    End Function

    Public Function XMLWrite(ByVal value As String, ByVal path As String, ByVal file As String) As Byte()
        Dim doc As XPathDocument
        Dim nav As XPathNavigator
        Dim iter As XPathNodeIterator
        Dim lstNav As XPathNavigator

        doc = New XPathDocument(file)
        nav = doc.CreateNavigator
        iter = nav.Select(path) 'Your node name goes here
        'Loop through the records in that node
        While (iter.MoveNext)
            'Get the data we need from the node
            Dim iterNews As XPathNodeIterator
            lstNav = iter.Current
            iterNews = lstNav.SelectDescendants(XPathNodeType.Element, False)
            'Loop through the child nodes
            Dim attribute As XElement = XElement.Parse(iterNews.Current.OuterXml)

            If iterNews.Current.Value = value Then
                Dim Attributeint As Integer = "&H" & attribute.@key
                Return BitConverter.GetBytes(Attributeint)
                Exit Function
            End If
        End While
        Dim Fail As Integer
        If IsNumeric(value) Then Fail = "&H" & "0" & value Else Fail = 0
        Return BitConverter.GetBytes(Fail)

    End Function

    Public Sub FillENPT()
        Dim doc As XPathDocument = New XPathDocument("Tablefill.xml")
        Dim nav As XPathNavigator = doc.CreateNavigator
        Dim iter As XPathNodeIterator

        '---------------------------------------------------------------------------------------------------------------------------------------
        '------------------------------------------------------------ENPT-----------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------------------------

        Dim t1 As DataGridViewComboBoxColumn = Datagridviewfill.Fill.ENPT("Setting1")
        Dim t2 As DataGridViewComboBoxColumn = Datagridviewfill.Fill.ENPT("Setting2")
        If My.Settings.ShowCombo = True Then
            iter = nav.Select("/XML/ENPT/Setting1") 'Your node name goes here
            Dim i As Integer = 0
            Dim j As Integer = iter.Count

            Dim values1(0 To j - 1) As String
            i = 0
            Do While i < j
                If Not XMLRead(i, "/XML/ENPT/Setting1", "Tablefill.xml") = "Invalid" Then
                    values1(i) = XMLRead(i, "XML/ENPT/Setting1", "Tablefill.xml")
                ElseIf i < 16 Then
                    values1(i) = "0x0" & Conversion.Hex(i)
                ElseIf i > 15 Then
                    values1(i) = "0x" & Conversion.Hex(i)
                End If
                i = i + 1
            Loop

            iter = nav.Select("/XML/ENPT/Setting2") 'Your node name goes here
            i = 0
            Dim j2 As Integer = iter.Count

            Dim values2(0 To j2 - 1) As String
            i = 0
            Do While i < j2
                If Not XMLRead(i, "/XML/ENPT/Setting2", "Tablefill.xml") = "Invalid" Then
                    values2(i) = XMLRead(i, "/XML/ENPT/Setting2", "Tablefill.xml")
                ElseIf i < 16 Then
                    values2(i) = "0x0" & Conversion.Hex(i)
                ElseIf i > 15 Then
                    values2(i) = "0x" & Conversion.Hex(i)
                End If
                i = i + 1
            Loop
            Dim c As Integer = 0
            Do While c < j
                t1.Items.AddRange(values1(c))
                c = c + 1
            Loop
            c = 0
            Do While c < j2
                t2.Items.AddRange(values2(c))
                c = c + 1
            Loop
        End If

        DataGridView1.Columns.Add(Datagridviewfill.Fill.ENPT("ID"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.ENPT("Section"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.ENPT("XPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.ENPT("YPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.ENPT("ZPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.ENPT("Scale"))
        If My.Settings.ShowCombo = True Then
            DataGridView1.Columns.Add(t1)
            DataGridView1.Columns.Add(t2)
        Else
            DataGridView1.Columns.Add(Datagridviewfill.Fill.ENPT("Setting"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.ENPT("Setting"))
        End If
    End Sub

    Public Sub FillITPT()
        Dim doc As XPathDocument = New XPathDocument("Tablefill.xml")
        Dim nav As XPathNavigator = doc.CreateNavigator
        Dim iter As XPathNodeIterator

        iter = nav.Select("/XML/ITPT/Setting1") 'Your node name goes here
        Dim i As Integer = 0
        Dim t As Integer = 0
        Dim j As Integer = iter.Count

        Dim values1(0 To j - 1) As String
        i = 0
        t = 0
        Do While t < j
            If Not XMLRead(i, "/XML/ITPT/Setting1", "Tablefill.xml") = "Invalid" Then
                values1(t) = XMLRead(i, "XML/ITPT/Setting1", "Tablefill.xml")
                t = t + 1
            End If
            i = i + 1
        Loop

        iter = nav.Select("/XML/ITPT/Setting2") 'Your node name goes here
        i = 0
        t = 0
        Dim j2 As Integer = iter.Count

        Dim values2(0 To j2 - 1) As String
        i = 0
        Do While t < j2
            If Not XMLRead(i, "/XML/ITPT/Setting2", "Tablefill.xml") = "Invalid" Then
                values2(t) = XMLRead(i, "/XML/ITPT/Setting2", "Tablefill.xml")
                t = t + 1
            End If
            i = i + 1
        Loop

        DataGridView1.Columns.Add(Datagridviewfill.Fill.ENPT("ID"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.ENPT("Section"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.ENPT("XPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.ENPT("YPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.ENPT("ZPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.ENPT("Scale"))
        Dim t1 As DataGridViewComboBoxColumn = Datagridviewfill.Fill.ENPT("Setting1")
        Dim c As Integer = 0
        Do While c < j
            t1.Items.AddRange(values1(c))
            c = c + 1
        Loop
        DataGridView1.Columns.Add(t1)
        Dim t2 As DataGridViewComboBoxColumn = Datagridviewfill.Fill.ENPT("Setting2")
        c = 0
        Do While c < j2
            t2.Items.AddRange(values2(c))
            c = c + 1
        Loop
        DataGridView1.Columns.Add(t2)

    End Sub

    Public Sub FillPOTI()
        Dim doc As XPathDocument = New XPathDocument("Tablefill.xml")
        Dim nav As XPathNavigator = doc.CreateNavigator
        Dim iter As XPathNodeIterator

        iter = nav.Select("/XML/POTI/Setting1") 'Your node name goes here
        Dim i As Integer = 0
        Dim t As Integer = 0
        Dim j As Integer = iter.Count

        Dim values1(0 To j - 1) As String
        i = 0
        t = 0
        Do While t < j
            If Not XMLRead(i, "/XML/POTI/Setting1", "Tablefill.xml") = "Invalid" Then
                values1(t) = XMLRead(i, "XML/POTI/Setting1", "Tablefill.xml")
                t = t + 1
            End If
            i = i + 1
        Loop

        iter = nav.Select("/XML/POTI/Setting2") 'Your node name goes here
        i = 0
        t = 0
        Dim j2 As Integer = iter.Count

        Dim values2(0 To j2 - 1) As String
        i = 0
        Do While t < j2
            If Not XMLRead(i, "/XML/POTI/Setting2", "Tablefill.xml") = "Invalid" Then
                values2(t) = XMLRead(i, "/XML/POTI/Setting2", "Tablefill.xml")
                t = t + 1
            End If
            i = i + 1
        Loop

        DataGridView1.Columns.Add(Datagridviewfill.Fill.POTI("ID"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.POTI("Amount"))
        Dim t1 As DataGridViewComboBoxColumn = Datagridviewfill.Fill.POTI("Setting1")
        Dim c As Integer = 0
        Do While c < j
            t1.Items.AddRange(values1(c))
            c = c + 1
        Loop
        DataGridView1.Columns.Add(t1)
        Dim t2 As DataGridViewComboBoxColumn = Datagridviewfill.Fill.POTI("Setting2")
        c = 0
        Do While c < j2
            t2.Items.AddRange(values2(c))
            c = c + 1
        Loop
        DataGridView1.Columns.Add(t2)

        DataGridView1.Columns.Add(Datagridviewfill.Fill.POTI("Showbtn"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.POTI("Usage"))
    End Sub

    Public Sub FillAREA()
        Dim doc As XPathDocument = New XPathDocument("Tablefill.xml")
        Dim nav As XPathNavigator = doc.CreateNavigator
        Dim iter As XPathNodeIterator

        iter = nav.Select("/XML/AREA/Variants") 'Your node name goes here
        Dim i As Integer = 0
        Dim t As Integer = 0
        Dim j As Integer = iter.Count

        Dim values1(0 To j - 1) As String
        i = 0
        t = 0
        Do While t < j
            If Not XMLRead(i, "/XML/AREA/Variants", "Tablefill.xml") = "Invalid" Then
                values1(t) = XMLRead(i, "XML/AREA/Variants", "Tablefill.xml")
                t = t + 1
            End If
            i = i + 1
        Loop

        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("ID"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("Mode"))
        Dim t1 As DataGridViewComboBoxColumn = Datagridviewfill.Fill.AREA("Type")
        Dim c As Integer = 0
        Do While c < j
            t1.Items.AddRange(values1(c))
            c = c + 1
        Loop
        DataGridView1.Columns.Add(t1)
        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("Camera"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("XPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("YPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("ZPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("XRoll"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("YRoll"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("ZRoll"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("XSize"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("YSize"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("ZSize"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("Settings"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("Route"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.AREA("Enemy"))
    End Sub

    Public Sub FillCAME()
        Dim doc As XPathDocument = New XPathDocument("Tablefill.xml")
        Dim nav As XPathNavigator = doc.CreateNavigator
        Dim iter As XPathNodeIterator

        iter = nav.Select("/XML/CAME/Variants") 'Your node name goes here
        Dim i As Integer = 0
        Dim t As Integer = 0
        Dim j As Integer = iter.Count

        Dim values1(0 To j - 1) As String
        i = 0
        t = 0
        Do While t < j
            If Not XMLRead(i, "/XML/CAME/Variants", "Tablefill.xml") = "Invalid" Then
                values1(t) = XMLRead(i, "XML/CAME/Variants", "Tablefill.xml")
                t = t + 1
            End If
            i = i + 1
        Loop

        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("ID"))
        Dim t1 As DataGridViewComboBoxColumn = Datagridviewfill.Fill.CAME("Type")
        Dim c As Integer = 0
        Do While c < j
            t1.Items.AddRange(values1(c))
            c = c + 1
        Loop
        DataGridView1.Columns.Add(t1)
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("Next"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("Shake"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("Route"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("VCam"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("VZoom"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("VView"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("Flag"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("XPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("YPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("ZPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("XRoll"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("YRoll"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("ZRoll"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("Zoom"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("Zoom2"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("ViewX"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("ViewY"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("ViewZ"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("View2X"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("View2Y"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("View2Z"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CAME("Secs"))
    End Sub

    Public Sub FillCNPT()
        Dim doc As XPathDocument = New XPathDocument("Tablefill.xml")
        Dim nav As XPathNavigator = doc.CreateNavigator
        Dim iter As XPathNodeIterator

        iter = nav.Select("/XML/CNPT/Effects") 'Your node name goes here
        Dim t As Integer = 0
        Dim j As Integer = iter.Count

        Dim values1(0 To j - 1) As String
        t = 0
        Do While t < j
            values1(t) = XMLGet(t, "XML/CNPT/Effects", "Tablefill.xml")
            t = t + 1
        Loop

        DataGridView1.Columns.Add(Datagridviewfill.Fill.CNPT("ID"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CNPT("XPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CNPT("YPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CNPT("ZPosition"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CNPT("XRoll"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CNPT("YRoll"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CNPT("ZRoll"))
        DataGridView1.Columns.Add(Datagridviewfill.Fill.CNPT("ID2"))
        Dim t1 As DataGridViewComboBoxColumn = Datagridviewfill.Fill.CNPT("Effect")
        Dim c As Integer = 0
        Do While c < j
            t1.Items.AddRange(values1(c))
            c = c + 1
        Loop
        DataGridView1.Columns.Add(t1)

    End Sub

    Public Function NewOval(ByVal id As Integer, ByVal locationX As Double, ByVal LocationY As Double) As PowerPacks.OvalShape
        Dim fill As Boolean = False
        If locationX = Nothing Then
            locationX = 0
            fill = True
        End If
        If LocationY = Nothing Then
            LocationY = 0
            fill = True
        End If
        If fill = True And scrolling = False And Filling = False Then
            FillGrid()
        End If

        If id = 0 Then
            Try
                c.Shapes.Clear()
            Catch ex As Exception : End Try
        End If

        locationX -= 2.5
        LocationY -= 2.5

        oval(id) = New PowerPacks.OvalShape
        oval(id).Tag = id
        oval(id).FillColor = My.Settings.Pointcolor
        oval(id).FillStyle = PowerPacks.FillStyle.Solid
        oval(id).Size = New System.Drawing.Size(5, 5)
        oval(id).Location = New System.Drawing.Point(locationX, LocationY)
        oval(id).Cursor = Cursors.SizeAll
        oval(id).SelectionColor = Color.Red
        c.Shapes.Add(oval(id))

        AddHandler oval(id).MouseDown, AddressOf OvalShape_MouseDown
        Return (oval(id))
    End Function

    Public Function NewOval2(ByVal id As Integer, ByVal locationX As Integer, ByVal LocationY As Integer) As PowerPacks.OvalShape
        Dim fill As Boolean = False
        If locationX = Nothing Then
            locationX = 0
            fill = True
        End If
        If LocationY = Nothing Then
            LocationY = 0
            fill = True
        End If
        If fill = True And scrolling = False And Filling = False Then
            FillGrid()
        End If

        oval2(id) = New PowerPacks.OvalShape
        oval2(id).Tag = id
        oval2(id).FillColor = Color.Green
        oval2(id).FillStyle = PowerPacks.FillStyle.Solid
        oval2(id).Size = New System.Drawing.Size(5, 5)
        oval2(id).Location = New System.Drawing.Point(locationX, LocationY)
        oval2(id).Cursor = Cursors.SizeAll
        oval2(id).SelectionColor = Color.Red
        c.Shapes.Add(oval2(id))

        AddHandler oval2(id).MouseDown, AddressOf OvalShape2_MouseDown
        Return (oval2(id))
    End Function

    Public Function NewLine(ByVal id As Integer) As PowerPacks.LineShape
        Dim fill As Boolean = False
        If fill = True And scrolling = False Then
            FillGrid()
        End If

        line(id) = New PowerPacks.LineShape
        line(id).BorderWidth = 3
        line(id).X1 = oval(id).Location.X + 2.5
        line(id).X2 = oval2(id).Location.X + 2.5
        line(id).Y1 = oval(id).Location.Y + 2.5
        line(id).Y2 = oval2(id).Location.Y + 2.5
        line(id).BorderColor = Color.Transparent


        'GridPanel_Paint(GridPanel, New System.Windows.Forms.PaintEventArgs(

        Return line(id)
    End Function

    Private Sub OvalShape_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim a As PowerPacks.OvalShape = sender
        oval(a.Tag).Cursor = Cursors.SizeAll
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Cursor = Cursors.SizeAll
            Timer1.Tag = a.Tag
            Timer1.Start()
            If Not DataGridView1.Rows(a.Tag).Cells(0).Visible = False Then DataGridView1.Rows(a.Tag).Cells(0).Selected = True
        ElseIf e.Button = Windows.Forms.MouseButtons.Right And My.Settings.Delright = True Then
            DataGridView1.Rows.Remove(DataGridView1.Rows(a.Tag))

            If CheckBox1.Checked = True And Sectionlist.SelectedItems.Count = 1 Then
                Dim GroupID As Integer = Mid(Sectionlist.SelectedItem, 9)
                Try
                    If Combobox1.text = My.Settings.Names(1) Then
                        SectionarrayENPT(GroupID).Amount -= 1
                        ENPH(GroupID * &H10 + 1) -= 1
                        For id = GroupID + 1 To SectionarrayENPT.Length - 1
                            SectionarrayENPT(id).Frompoint -= 1
                            ENPH(GroupID * &H10) -= 1
                        Next
                    End If
                    If Combobox1.text = My.Settings.Names(2) Then
                        SectionarrayITPT(GroupID).Amount -= 1
                        ITPH(GroupID * &H10 + 1) -= 1
                        For id = GroupID + 1 To SectionarrayITPT.Length - 1
                            SectionarrayITPT(id).Frompoint -= 1
                            ITPH(GroupID * &H10) -= 1
                        Next
                    End If
                    If Combobox1.text = My.Settings.Names(3) Then
                        SectionarrayCKPT(GroupID).Amount -= 1
                        CKPH(GroupID * &H10 + 1) -= 1
                        For id = GroupID + 1 To SectionarrayCKPT.Length - 1
                            SectionarrayCKPT(id).Frompoint -= 1
                            CKPH(GroupID * &H10) -= 1
                        Next
                    End If
                Catch ex As Exception : MsgBox("Unable to update sections, this must now be done manually.") : End Try

            End If
                FillGrid()
            ElseIf e.Button = Windows.Forms.MouseButtons.Middle Then
retry:          Dim ipb As String = InputBox("What ID do you want to give this point?", "New ID")
                If IsNumeric(ipb) = False Then : Exit Sub
                ElseIf DataGridView1.RowCount <= ipb Then : MsgBox("Invalid ID") : GoTo retry
                Else
                    Dim therow As DataGridViewRow = New DataGridViewRow
                    Dim Index As Integer = ipb
                    therow = DataGridView1.Rows(a.Tag)
                    DataGridView1.Rows.Remove(DataGridView1.Rows(a.Tag))
                    If Index = DataGridView1.RowCount Then
                        DataGridView1.Rows.Add(therow)
                    Else
                        DataGridView1.Rows.Insert(Index, therow)
                    End If
                    FillGrid()
                End If
            End If
    End Sub

    Private Sub OvalShape2_MouseDown(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim a As PowerPacks.OvalShape = sender
        Cursor = Cursors.SizeAll
        Timer2.Tag = a.Tag
        Timer2.Start()
    End Sub

    'WORK SUBS
    Public Shared Function Hextofloat(ByVal hex As Byte()) As Single
        Try
            Dim h As Byte() = hex
            Array.Reverse(h)
            Dim f As Single
            f = BitConverter.ToSingle(hex, 0)
            Return f
        Catch ex As Exception : End Try
        Return MsgBox("An error has occured, unable to read from the kmp")
    End Function

    Public Shared Function Hextofloat(ByVal hex As Long) As Single
        Try
            Dim bytes As Byte() = BitConverter.GetBytes(hex)
            bytes.Reverse()
            Return BitConverter.ToSingle(bytes, 0)
        Catch ex As Exception : End Try
        Return MsgBox("An error has occured, unable to read from the kmp")
    End Function

    Public Shared Function Floattohex(ByVal float As Single) As Integer
        If float = Nothing Then
            float = 0.0
        End If
        Dim b As Byte() = BitConverter.GetBytes(float)
        b.Reverse()
        Dim i As Integer = BitConverter.ToInt32(b, 0)
        Return i
    End Function

    Public Function Hextostr(ByVal str As String)
        Dim sStr As String

        sStr = ""
        For i = 1 To Len(str) Step 2
            sStr = sStr + Chr(CLng("&H" & Mid(str, i, 2)))
        Next

        Return sStr
    End Function

    Public Function Strtohex(ByVal Str As String) As String
        Dim Hex As String = ""
        For i = 1 To Str.Length
            Hex = Asc(Mid(Str, i, 1)).ToString("X")
            Str = CStr(Hex)
        Next
        Return Str
    End Function
    Public Function Strtoint(ByVal Str As String) As Integer
        Dim Hex As String = ""
        For i = 1 To Str.Length
            Hex = Asc(Mid(Str, i, 1)).ToString("X")
            Str = "&H" & CStr(Hex)
        Next
        Return Str
    End Function

    Public Sub ReadTable()
        If group = My.Settings.Names(0) Then
            Dim temp(0 To (DataGridView1.RowCount - 1) * &H1C + 7) As Byte
            Dim name As Byte() = {Strtoint("K"), Strtoint("T"), Strtoint("P"), Strtoint("T")}
            Writekmp(temp, 0, name)
            Dim amount As Byte() = BitConverter.GetBytes(DataGridView1.RowCount - 1) : Array.Reverse(amount)
            Writekmp(temp, 4, amount, 2)

            For i = 0 To DataGridView1.RowCount - 2
                Dim LocX, LocY, LocZ, RotX, RotY, RotZ, Player As Byte()
                LocX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(1).Value)) : Array.Reverse(LocX) : Writekmp(temp, 8 + (i) * &H1C, LocX)
                LocY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(2).Value)) : Array.Reverse(LocY) : Writekmp(temp, 12 + (i) * &H1C, LocY)
                LocZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(3).Value)) : Array.Reverse(LocZ) : Writekmp(temp, 16 + (i) * &H1C, LocZ)
                RotX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(4).Value)) : Array.Reverse(RotX) : Writekmp(temp, 20 + (i) * &H1C, RotX)
                RotY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(5).Value)) : Array.Reverse(RotY) : Writekmp(temp, 24 + (i) * &H1C, RotY)
                RotZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(6).Value)) : Array.Reverse(RotZ) : Writekmp(temp, 28 + (i) * &H1C, RotZ)
                Dim player2 As Integer = "&h" & "0" & DataGridView1.Rows(i).Cells(7).Value
                Player = BitConverter.GetBytes(player2) : Array.Reverse(Player, 0, 2) : Writekmp(temp, 32 + (i) * &H1C, Player)
            Next
            KTPT = temp
        ElseIf group = My.Settings.Names(1) Then
            Dim temp(0 To (DataGridView1.RowCount - 1) * &H14 + 7) As Byte
            Dim name As Byte() = {Strtoint("E"), Strtoint("N"), Strtoint("P"), Strtoint("T")}
            Writekmp(temp, 0, name)
            Dim amount As Byte() = BitConverter.GetBytes(DataGridView1.RowCount - 1) : Array.Reverse(amount)
            Writekmp(temp, 4, amount, 2)
            For i = 0 To DataGridView1.RowCount - 2
                Dim LocX, LocY, LocZ, Scale, S1, S2 As Byte()
                LocX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(2).Value)) : Array.Reverse(LocX) : Writekmp(temp, 8 + (i) * &H14, LocX)
                LocY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(3).Value)) : Array.Reverse(LocY) : Writekmp(temp, 12 + (i) * &H14, LocY)
                LocZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(4).Value)) : Array.Reverse(LocZ) : Writekmp(temp, 16 + (i) * &H14, LocZ)
                Scale = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(5).Value)) : Array.Reverse(Scale) : Writekmp(temp, 20 + (i) * &H14, Scale)
                S1 = XMLWrite(DataGridView1.Rows(i).Cells(6).Value, "XML/ENPT/Setting1", "Tablefill.xml") : Array.Reverse(S1) : Writekmp(temp, 24 + (i) * &H14, S1, 2)
                S2 = XMLWrite(DataGridView1.Rows(i).Cells(7).Value, "XML/ENPT/Setting2", "Tablefill.xml") : Array.Reverse(S2)
                Writekmp(temp, 26 + (i) * &H14, S2, 2)
            Next
            ENPT = temp
        ElseIf group = "ENPH" Then
            Dim temp(0 To (DataGridView1.RowCount - 1) * &H10 + 7) As Byte
            Dim name As Byte() = {Strtoint("E"), Strtoint("N"), Strtoint("P"), Strtoint("H")}
            Writekmp(temp, 0, name)
            Dim amount As Byte() = BitConverter.GetBytes(DataGridView1.RowCount - 1) : Array.Reverse(amount)
            If DataGridView1.RowCount - 1 = 0 Then
                GoTo mehish
            End If
            Writekmp(temp, 4, amount, 2)
            Dim tempsection(0 To DataGridView1.RowCount - 2) As Section.ENPH.Section : SectionarrayENPT = tempsection
            For i2 = 0 To DataGridView1.RowCount - 2
                SectionarrayENPT(i2) = New Section.ENPH.Section
                SectionarrayENPT(i2).ResetLast()
            Next
            For i = 0 To DataGridView1.RowCount - 2
                Dim frm, am, nex1, nex2, nex3, nex4, nex5, nex6 As Byte()
                frm = {DataGridView1.Rows(i).Cells(1).Value} : Writekmp(temp, 8 + i * &H10, frm)
                am = {DataGridView1.Rows(i).Cells(2).Value} : Writekmp(temp, 9 + i * &H10, am)
                Writekmp(temp, 10 + i * &H10, {255, 255, 255, 255, 255, 255}, 6)
                Dim check As Boolean = False
                Dim nex As Integer = DataGridView1.Rows(i).Cells(3).Value : If nex >= SectionarrayENPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex1 = BitConverter.GetBytes(nex) : Array.Reverse(nex1) : Writekmp(temp, 16 + i * &H10, nex1, 1) : If Not nex1(3) = 255 And check = False Then SectionarrayENPT(nex1(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(4).Value : If nex >= SectionarrayENPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex2 = BitConverter.GetBytes(nex) : Array.Reverse(nex2) : Writekmp(temp, 17 + i * &H10, nex2, 1) : If Not nex2(3) = 255 And check = False Then SectionarrayENPT(nex2(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(5).Value : If nex >= SectionarrayENPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex3 = BitConverter.GetBytes(nex) : Array.Reverse(nex3) : Writekmp(temp, 18 + i * &H10, nex3, 1) : If Not nex3(3) = 255 And check = False Then SectionarrayENPT(nex3(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(6).Value : If nex >= SectionarrayENPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex4 = BitConverter.GetBytes(nex) : Array.Reverse(nex4) : Writekmp(temp, 19 + i * &H10, nex4, 1) : If Not nex4(3) = 255 And check = False Then SectionarrayENPT(nex4(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(7).Value : If nex >= SectionarrayENPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex5 = BitConverter.GetBytes(nex) : Array.Reverse(nex5) : Writekmp(temp, 20 + i * &H10, nex5, 1) : If Not nex5(3) = 255 And check = False Then SectionarrayENPT(nex5(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(8).Value : If nex >= SectionarrayENPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex6 = BitConverter.GetBytes(nex) : Array.Reverse(nex6) : Writekmp(temp, 21 + i * &H10, nex6, 1) : If Not nex6(3) = 255 And check = False Then SectionarrayENPT(nex6(3)).AddLast(i, nex, temp)
            Next
            ENPH = temp
mehish:     FillSection()
            For i = 0 To DataGridView1.RowCount - 2
                If Not SectionarrayENPT(i).Nextsection(0) = 255 Then SectionarrayENPT(SectionarrayENPT(i).Nextsection(0)).AddLast(i, SectionarrayENPT(i).Nextsection(0), ENPH)
                If Not SectionarrayENPT(i).Nextsection(1) = 255 Then SectionarrayENPT(SectionarrayENPT(i).Nextsection(1)).AddLast(i, SectionarrayENPT(i).Nextsection(1), ENPH)
                If Not SectionarrayENPT(i).Nextsection(2) = 255 Then SectionarrayENPT(SectionarrayENPT(i).Nextsection(2)).AddLast(i, SectionarrayENPT(i).Nextsection(2), ENPH)
                If Not SectionarrayENPT(i).Nextsection(3) = 255 Then SectionarrayENPT(SectionarrayENPT(i).Nextsection(3)).AddLast(i, SectionarrayENPT(i).Nextsection(3), ENPH)
                If Not SectionarrayENPT(i).Nextsection(4) = 255 Then SectionarrayENPT(SectionarrayENPT(i).Nextsection(4)).AddLast(i, SectionarrayENPT(i).Nextsection(4), ENPH)
                If Not SectionarrayENPT(i).Nextsection(5) = 255 Then SectionarrayENPT(SectionarrayENPT(i).Nextsection(5)).AddLast(i, SectionarrayENPT(i).Nextsection(5), ENPH)
            Next
        ElseIf group = My.Settings.Names(2) Then
            Dim temp(0 To (DataGridView1.RowCount - 1) * &H14 + 7) As Byte
            Dim name As Byte() = {Strtoint("I"), Strtoint("T"), Strtoint("P"), Strtoint("T")}
            Writekmp(temp, 0, name)
            Dim amount As Byte() = BitConverter.GetBytes(DataGridView1.RowCount - 1) : Array.Reverse(amount)
            Writekmp(temp, 4, amount, 2)
            For i = 0 To DataGridView1.RowCount - 2
                Dim LocX, LocY, LocZ, Scale, S1, S2 As Byte()
                LocX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(2).Value)) : Array.Reverse(LocX) : Writekmp(temp, 8 + (i) * &H14, LocX)
                LocY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(3).Value)) : Array.Reverse(LocY) : Writekmp(temp, 12 + (i) * &H14, LocY)
                LocZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(4).Value)) : Array.Reverse(LocZ) : Writekmp(temp, 16 + (i) * &H14, LocZ)
                Scale = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(5).Value)) : Array.Reverse(Scale) : Writekmp(temp, 20 + (i) * &H14, Scale)
                S1 = XMLWrite(DataGridView1.Rows(i).Cells(6).Value, "XML/ITPT/Setting1", "Tablefill.xml") : Array.Reverse(S1) : Writekmp(temp, 24 + (i) * &H14, S1, 2)
                S2 = XMLWrite(DataGridView1.Rows(i).Cells(7).Value, "XML/ITPT/Setting2", "Tablefill.xml") : Array.Reverse(S2) : Writekmp(temp, 26 + (i) * &H14, S2, 2)
            Next
            ITPT = temp
        ElseIf group = "ITPH" Then
            Dim temp(0 To (DataGridView1.RowCount - 1) * &H10 + 7) As Byte
            Dim name As Byte() = {Strtoint("I"), Strtoint("T"), Strtoint("P"), Strtoint("H")}
            Writekmp(temp, 0, name)
            Dim amount As Byte() = BitConverter.GetBytes(DataGridView1.RowCount - 1) : Array.Reverse(amount)
            Writekmp(temp, 4, amount, 2)
            If DataGridView1.RowCount - 1 = 0 Then
                GoTo mehish2
            End If
            Dim tempsection(0 To DataGridView1.RowCount - 2) As Section.ITPH.Section : SectionarrayITPT = tempsection
            For i2 = 0 To DataGridView1.RowCount - 2
                SectionarrayITPT(i2) = New Section.ITPH.Section
                SectionarrayITPT(i2).ResetLast()
            Next
            For i = 0 To DataGridView1.RowCount - 2
                Dim frm, am, nex1, nex2, nex3, nex4, nex5, nex6 As Byte()
                frm = {DataGridView1.Rows(i).Cells(1).Value} : Writekmp(temp, 8 + i * &H10, frm)
                am = {DataGridView1.Rows(i).Cells(2).Value} : Writekmp(temp, 9 + i * &H10, am)
                Writekmp(temp, 10 + i * &H10, {255, 255, 255, 255, 255, 255}, 6)
                Dim check As Boolean = False
                Dim nex As Integer = DataGridView1.Rows(i).Cells(3).Value : If nex >= SectionarrayITPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex1 = BitConverter.GetBytes(nex) : Array.Reverse(nex1) : Writekmp(temp, 16 + i * &H10, nex1, 1) : If Not nex1(3) = 255 And check = False Then SectionarrayITPT(nex1(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(4).Value : If nex >= SectionarrayITPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex2 = BitConverter.GetBytes(nex) : Array.Reverse(nex2) : Writekmp(temp, 17 + i * &H10, nex2, 1) : If Not nex2(3) = 255 And check = False Then SectionarrayITPT(nex2(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(5).Value : If nex >= SectionarrayITPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex3 = BitConverter.GetBytes(nex) : Array.Reverse(nex3) : Writekmp(temp, 18 + i * &H10, nex3, 1) : If Not nex3(3) = 255 And check = False Then SectionarrayITPT(nex3(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(6).Value : If nex >= SectionarrayITPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex4 = BitConverter.GetBytes(nex) : Array.Reverse(nex4) : Writekmp(temp, 19 + i * &H10, nex4, 1) : If Not nex4(3) = 255 And check = False Then SectionarrayITPT(nex4(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(7).Value : If nex >= SectionarrayITPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex5 = BitConverter.GetBytes(nex) : Array.Reverse(nex5) : Writekmp(temp, 20 + i * &H10, nex5, 1) : If Not nex5(3) = 255 And check = False Then SectionarrayITPT(nex5(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(8).Value : If nex >= SectionarrayITPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex6 = BitConverter.GetBytes(nex) : Array.Reverse(nex6) : Writekmp(temp, 21 + i * &H10, nex6, 1) : If Not nex6(3) = 255 And check = False Then SectionarrayITPT(nex6(3)).AddLast(i, nex, temp)
            Next
            ITPH = temp
mehish2:    FillSection()
            For i = 0 To DataGridView1.RowCount - 2
                If Not SectionarrayITPT(i).Nextsection(0) = 255 Then SectionarrayITPT(SectionarrayITPT(i).Nextsection(0)).AddLast(i, SectionarrayITPT(i).Nextsection(0), ITPH)
                If Not SectionarrayITPT(i).Nextsection(1) = 255 Then SectionarrayITPT(SectionarrayITPT(i).Nextsection(1)).AddLast(i, SectionarrayITPT(i).Nextsection(1), ITPH)
                If Not SectionarrayITPT(i).Nextsection(2) = 255 Then SectionarrayITPT(SectionarrayITPT(i).Nextsection(2)).AddLast(i, SectionarrayITPT(i).Nextsection(2), ITPH)
                If Not SectionarrayITPT(i).Nextsection(3) = 255 Then SectionarrayITPT(SectionarrayITPT(i).Nextsection(3)).AddLast(i, SectionarrayITPT(i).Nextsection(3), ITPH)
                If Not SectionarrayITPT(i).Nextsection(4) = 255 Then SectionarrayITPT(SectionarrayITPT(i).Nextsection(4)).AddLast(i, SectionarrayITPT(i).Nextsection(4), ITPH)
                If Not SectionarrayITPT(i).Nextsection(5) = 255 Then SectionarrayITPT(SectionarrayITPT(i).Nextsection(5)).AddLast(i, SectionarrayITPT(i).Nextsection(5), ITPH)
            Next
        ElseIf group = My.Settings.Names(3) Then
            Dim temp2(0 To (DataGridView1.RowCount - 1) * &H14 + 7) As Byte
            Dim name As Byte() = {Strtoint("C"), Strtoint("K"), Strtoint("P"), Strtoint("T")}
            Writekmp(temp2, 0, name)
            Dim amount As Byte() = BitConverter.GetBytes(DataGridView1.RowCount - 1) : Array.Reverse(amount)
            Writekmp(temp2, 4, amount, 2)
            For i = 0 To DataGridView1.RowCount - 2
                Dim LocX, LocZ, LocX2, LocZ2, Rspn, Key, Lst, Nxt As Byte()
                Dim temp As Integer
                LocX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(2).Value)) : Array.Reverse(LocX) : Writekmp(temp2, 8 + (i) * &H14, LocX)
                LocZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(3).Value)) : Array.Reverse(LocZ) : Writekmp(temp2, 12 + (i) * &H14, LocZ)
                LocX2 = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(4).Value)) : Array.Reverse(LocX2) : Writekmp(temp2, 16 + (i) * &H14, LocX2)
                LocZ2 = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(5).Value)) : Array.Reverse(LocZ2) : Writekmp(temp2, 20 + (i) * &H14, LocZ2)
                temp = "0" & DataGridView1.Rows(i).Cells(6).Value
                Rspn = BitConverter.GetBytes(temp) : Array.Reverse(Rspn) : Writekmp(temp2, 24 + (i) * &H14, Rspn, 1)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(7).Value
                Key = BitConverter.GetBytes(temp) : Array.Reverse(Key) : Writekmp(temp2, 25 + (i) * &H14, Key, 1)
                temp = "0" & DataGridView1.Rows(i).Cells(8).Value
                Lst = BitConverter.GetBytes(temp) : Array.Reverse(Lst) : Writekmp(temp2, 26 + (i) * &H14, Lst, 1)
                temp = "0" & DataGridView1.Rows(i).Cells(9).Value
                Nxt = BitConverter.GetBytes(temp) : Array.Reverse(Nxt) : Writekmp(temp2, 27 + (i) * &H14, Nxt, 1)
            Next
            CKPT = temp2
        ElseIf group = "CKPH" Then
            Dim temp(0 To (DataGridView1.RowCount - 1) * &H10 + 7) As Byte
            Dim name As Byte() = {Strtoint("C"), Strtoint("K"), Strtoint("P"), Strtoint("H")}
            Writekmp(temp, 0, name)
            Dim amount As Byte() = BitConverter.GetBytes(DataGridView1.RowCount - 1) : Array.Reverse(amount)
            Writekmp(temp, 4, amount, 2)
            If DataGridView1.RowCount - 1 = 0 Then
                GoTo mehish3
            End If
            Dim tempsection(0 To DataGridView1.RowCount - 2) As Section.CKPH.Section : SectionarrayCKPT = tempsection
            For i2 = 0 To DataGridView1.RowCount - 2
                SectionarrayCKPT(i2) = New Section.CKPH.Section
                SectionarrayCKPT(i2).ResetLast()
            Next
            For i = 0 To DataGridView1.RowCount - 2
                Dim frm, am, nex1, nex2, nex3, nex4, nex5, nex6 As Byte()
                frm = {DataGridView1.Rows(i).Cells(1).Value} : Writekmp(temp, 8 + i * &H10, frm)
                am = {DataGridView1.Rows(i).Cells(2).Value} : Writekmp(temp, 9 + i * &H10, am)
                Writekmp(temp, 10 + i * &H10, {255, 255, 255, 255, 255, 255}, 6)
                Dim check As Boolean = False
                Dim nex As Integer = DataGridView1.Rows(i).Cells(3).Value : If nex >= SectionarrayCKPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex1 = BitConverter.GetBytes(nex) : Array.Reverse(nex1) : Writekmp(temp, 16 + i * &H10, nex1, 1) : If Not nex1(3) = 255 And check = False Then SectionarrayCKPT(nex1(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(4).Value : If nex >= SectionarrayCKPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex2 = BitConverter.GetBytes(nex) : Array.Reverse(nex2) : Writekmp(temp, 17 + i * &H10, nex2, 1) : If Not nex2(3) = 255 And check = False Then SectionarrayCKPT(nex2(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(5).Value : If nex >= SectionarrayCKPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex3 = BitConverter.GetBytes(nex) : Array.Reverse(nex3) : Writekmp(temp, 18 + i * &H10, nex3, 1) : If Not nex3(3) = 255 And check = False Then SectionarrayCKPT(nex3(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(6).Value : If nex >= SectionarrayCKPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex4 = BitConverter.GetBytes(nex) : Array.Reverse(nex4) : Writekmp(temp, 19 + i * &H10, nex4, 1) : If Not nex4(3) = 255 And check = False Then SectionarrayCKPT(nex4(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(7).Value : If nex >= SectionarrayCKPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex5 = BitConverter.GetBytes(nex) : Array.Reverse(nex5) : Writekmp(temp, 20 + i * &H10, nex5, 1) : If Not nex5(3) = 255 And check = False Then SectionarrayCKPT(nex5(3)).AddLast(i, nex, temp)
                nex = DataGridView1.Rows(i).Cells(8).Value : If nex >= SectionarrayCKPT.Length And Not nex = 255 Then : MsgBox("Section " & i & " is pointing to a nonexisting section, in this state the kmp will freeze.") : check = True : End If
                nex6 = BitConverter.GetBytes(nex) : Array.Reverse(nex6) : Writekmp(temp, 21 + i * &H10, nex6, 1) : If Not nex6(3) = 255 And check = False Then SectionarrayCKPT(nex6(3)).AddLast(i, nex, temp)
            Next
            CKPH = temp
mehish3:    FillSection()
            For i = 0 To DataGridView1.RowCount - 2
                If Not SectionarrayCKPT(i).Nextsection(0) = 255 Then SectionarrayCKPT(SectionarrayCKPT(i).Nextsection(0)).AddLast(i, SectionarrayCKPT(i).Nextsection(0), ITPT)
                If Not SectionarrayCKPT(i).Nextsection(1) = 255 Then SectionarrayCKPT(SectionarrayCKPT(i).Nextsection(1)).AddLast(i, SectionarrayCKPT(i).Nextsection(1), ITPT)
                If Not SectionarrayCKPT(i).Nextsection(2) = 255 Then SectionarrayCKPT(SectionarrayCKPT(i).Nextsection(2)).AddLast(i, SectionarrayCKPT(i).Nextsection(2), ITPT)
                If Not SectionarrayCKPT(i).Nextsection(3) = 255 Then SectionarrayCKPT(SectionarrayCKPT(i).Nextsection(3)).AddLast(i, SectionarrayCKPT(i).Nextsection(3), ITPT)
                If Not SectionarrayCKPT(i).Nextsection(4) = 255 Then SectionarrayCKPT(SectionarrayCKPT(i).Nextsection(4)).AddLast(i, SectionarrayCKPT(i).Nextsection(4), ITPT)
                If Not SectionarrayCKPT(i).Nextsection(5) = 255 Then SectionarrayCKPT(SectionarrayCKPT(i).Nextsection(5)).AddLast(i, SectionarrayCKPT(i).Nextsection(5), ITPT)
            Next
        ElseIf group = My.Settings.Names(4) Then
            Dim temp2(0 To (DataGridView1.RowCount - 1) * &H3C + 7) As Byte
            Dim name As Byte() = {Strtoint("G"), Strtoint("O"), Strtoint("B"), Strtoint("J")}
            Writekmp(temp2, 0, name)
            Dim amount As Byte() = BitConverter.GetBytes(DataGridView1.RowCount - 1) : Array.Reverse(amount)
            Writekmp(temp2, 4, amount, 2)
            For i = 0 To DataGridView1.RowCount - 2
                Dim ID, LocX, LocY, LocZ, RotX, RotY, RotZ, SclX, SclY, SclZ, route, s1, s2, s3, s4, s5, s6, s7, s8, Pres As Byte()
                Dim temp As Integer = 0
                ID = BitConverter.GetBytes(ObjectWrite(DataGridView1.Rows(i).Cells(1).Value)) : Array.Reverse(ID) : Writekmp(temp2, 8 + (i) * &H3C, ID, 2)
                LocX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(2).Value)) : Array.Reverse(LocX) : Writekmp(temp2, 12 + (i) * &H3C, LocX)
                LocY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(3).Value)) : Array.Reverse(LocY) : Writekmp(temp2, 16 + (i) * &H3C, LocY)
                LocZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(4).Value)) : Array.Reverse(LocZ) : Writekmp(temp2, 20 + (i) * &H3C, LocZ)
                RotX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(5).Value)) : Array.Reverse(RotX) : Writekmp(temp2, 24 + (i) * &H3C, RotX)
                RotY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(6).Value)) : Array.Reverse(RotY) : Writekmp(temp2, 28 + (i) * &H3C, RotY)
                RotZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(7).Value)) : Array.Reverse(RotZ) : Writekmp(temp2, 32 + (i) * &H3C, RotZ)
                SclX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(8).Value)) : Array.Reverse(SclX) : Writekmp(temp2, 36 + (i) * &H3C, SclX)
                SclY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(9).Value)) : Array.Reverse(SclY) : Writekmp(temp2, 40 + (i) * &H3C, SclY)
                SclZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(10).Value)) : Array.Reverse(SclZ) : Writekmp(temp2, 44 + (i) * &H3C, SclZ)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(11).Value : route = BitConverter.GetBytes(temp) : Array.Reverse(route) : Writekmp(temp2, 48 + (i) * &H3C, route, 2)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(12).Value : s1 = BitConverter.GetBytes(temp) : Array.Reverse(s1) : Writekmp(temp2, 50 + (i) * &H3C, s1, 2)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(13).Value : s2 = BitConverter.GetBytes(temp) : Array.Reverse(s2) : Writekmp(temp2, 52 + (i) * &H3C, s2, 2)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(14).Value : s3 = BitConverter.GetBytes(temp) : Array.Reverse(s3) : Writekmp(temp2, 54 + (i) * &H3C, s3, 2)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(15).Value : s4 = BitConverter.GetBytes(temp) : Array.Reverse(s4) : Writekmp(temp2, 56 + (i) * &H3C, s4, 2)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(16).Value : s5 = BitConverter.GetBytes(temp) : Array.Reverse(s5) : Writekmp(temp2, 58 + (i) * &H3C, s5, 2)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(17).Value : s6 = BitConverter.GetBytes(temp) : Array.Reverse(s6) : Writekmp(temp2, 60 + (i) * &H3C, s6, 2)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(18).Value : s7 = BitConverter.GetBytes(temp) : Array.Reverse(s7) : Writekmp(temp2, 62 + (i) * &H3C, s7, 2)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(19).Value : s8 = BitConverter.GetBytes(temp) : Array.Reverse(s8) : Writekmp(temp2, 64 + (i) * &H3C, s8, 2)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(20).Value : Pres = BitConverter.GetBytes(temp) : Array.Reverse(Pres) : Writekmp(temp2, 66 + (i) * &H3C, Pres, 2)
            Next
            GOBJ = temp2
        ElseIf group = My.Settings.Names(5) Then
            Dim allpoints As New ArrayList
            For i = 0 To Parsedroutes.Length - 1
                Dim points As Route.Point() = Parsedroutes(i).points
                allpoints.Add(points)
            Next
            For i = 0 To DataGridView1.RowCount - 2

                Parsedroutes(i).Setting1 = "&H" & "0" & XMLWrite(DataGridView1.Rows(i).Cells(2).Value, "/XML/POTI/Setting1", "Tablefill.xml")(0)
                Parsedroutes(i).Setting2 = "&H" & "0" & XMLWrite(DataGridView1.Rows(i).Cells(3).Value, "/XML/POTI/Setting2", "Tablefill.xml")(0)
            Next

        ElseIf group = My.Settings.Names(6) Then
            Dim temp2(0 To (DataGridView1.RowCount - 1) * &H30 + 7) As Byte
            AREA = temp2
            Dim name As Byte() = {Strtoint("A"), Strtoint("R"), Strtoint("E"), Strtoint("A")}
            Writekmp(AREA, 0, name)
            Dim amount As Byte() = BitConverter.GetBytes(DataGridView1.RowCount - 1) : Array.Reverse(amount)
            Writekmp(AREA, 4, amount, 2)
            For i = 0 To DataGridView1.RowCount - 2
                Dim Mode, Type, Cam, LocX, LocY, LocZ, RotX, RotY, RotZ, SclX, SclY, SclZ, Setting, Route, Enemy As Byte()
                Dim temp As Integer
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(1).Value
                Mode = BitConverter.GetBytes(temp) : Array.Reverse(Mode) : Writekmp(AREA, 8 + (i) * &H30, Mode, 1)
                Type = XMLWrite(DataGridView1.Rows(i).Cells(2).Value, "XML/AREA/Variants", "Tablefill.xml") : Array.Reverse(Type) : Writekmp(AREA, 9 + (i) * &H30, Type, 1)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(3).Value
                Cam = BitConverter.GetBytes(temp) : Array.Reverse(Cam) : Writekmp(AREA, 10 + (i) * &H30, Cam, 1)
                LocX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(4).Value)) : Array.Reverse(LocX) : Writekmp(AREA, 12 + (i) * &H30, LocX)
                LocY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(5).Value)) : Array.Reverse(LocY) : Writekmp(AREA, 16 + (i) * &H30, LocY)
                LocZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(6).Value)) : Array.Reverse(LocZ) : Writekmp(AREA, 20 + (i) * &H30, LocZ)
                RotX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(7).Value)) : Array.Reverse(RotX) : Writekmp(AREA, 24 + (i) * &H30, RotX)
                RotY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(8).Value)) : Array.Reverse(RotY) : Writekmp(AREA, 28 + (i) * &H30, RotY)
                RotZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(9).Value)) : Array.Reverse(RotZ) : Writekmp(AREA, 32 + (i) * &H30, RotZ)
                SclX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(10).Value)) : Array.Reverse(SclX) : Writekmp(AREA, 36 + (i) * &H30, SclX)
                SclY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(11).Value)) : Array.Reverse(SclY) : Writekmp(AREA, 40 + (i) * &H30, SclY)
                SclZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(12).Value)) : Array.Reverse(SclZ) : Writekmp(AREA, 44 + (i) * &H30, SclZ)
                Dim temp3 As Long = "&H0" & DataGridView1.Rows(i).Cells(13).Value
                Setting = BitConverter.GetBytes(temp3) : Array.Reverse(Setting) : Writekmp(AREA, 48 + (i) * &H30, Setting, 4)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(14).Value
                Route = BitConverter.GetBytes(temp) : Array.Reverse(Route) : Writekmp(AREA, 52 + (i) * &H30, Route, 2)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(15).Value
                Enemy = BitConverter.GetBytes(temp) : Array.Reverse(Enemy) : Writekmp(AREA, 54 + (i) * &H30, Enemy, 2)
            Next
        ElseIf group = My.Settings.Names(7) Then
            Dim temp2(0 To (DataGridView1.RowCount - 1) * 72 + 7) As Byte
            CAME = temp2
            Dim name As Byte() = {Strtoint("C"), Strtoint("A"), Strtoint("M"), Strtoint("E")}
            Writekmp(CAME, 0, name)
            Dim amount As Byte() = BitConverter.GetBytes(DataGridView1.RowCount - 1) : Array.Reverse(amount)
            Writekmp(CAME, 4, amount, 2)
            For i = 0 To DataGridView1.RowCount - 2
                Dim Type, NCam, Shake, Route, V, VZ, VP, flag, LocX, LocY, LocZ, RotX, RotY, RotZ, Zstart, Zend, ViewX, ViewY, ViewZ, View2X, View2Y, View2Z, Time As Byte()
                Dim temp As Integer
                Type = XMLWrite(DataGridView1.Rows(i).Cells(1).Value, "XML/CAME/Variants", "Tablefill.xml") : Array.Reverse(Type) : Writekmp(CAME, 8 + (i) * 72, Type, 1)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(2).Value : NCam = BitConverter.GetBytes(temp) : Array.Reverse(NCam) : Writekmp(CAME, 9 + (i) * 72, NCam, 1)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(3).Value : Shake = BitConverter.GetBytes(temp) : Array.Reverse(Shake) : Writekmp(CAME, 10 + (i) * 72, Shake, 1)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(4).Value : Route = BitConverter.GetBytes(temp) : Array.Reverse(Route) : Writekmp(CAME, 11 + (i) * 72, Route, 1)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(5).Value : V = BitConverter.GetBytes(temp) : Array.Reverse(V) : Writekmp(CAME, 12 + (i) * 72, V, 2)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(6).Value : VZ = BitConverter.GetBytes(temp) : Array.Reverse(VZ) : Writekmp(CAME, 14 + (i) * 72, VZ, 2)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(7).Value : VP = BitConverter.GetBytes(temp) : Array.Reverse(VP) : Writekmp(CAME, 16 + (i) * 72, VP, 2)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(8).Value : flag = BitConverter.GetBytes(temp) : Array.Reverse(flag) : Writekmp(CAME, 18 + (i) * 72, flag, 2)

                LocX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(9).Value)) : Array.Reverse(LocX) : Writekmp(CAME, 20 + (i) * 72, LocX)
                LocY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(10).Value)) : Array.Reverse(LocY) : Writekmp(CAME, 24 + (i) * 72, LocY)
                LocZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(11).Value)) : Array.Reverse(LocZ) : Writekmp(CAME, 28 + (i) * 72, LocZ)
                RotX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(12).Value)) : Array.Reverse(RotX) : Writekmp(CAME, 32 + (i) * 72, RotX)
                RotY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(13).Value)) : Array.Reverse(RotY) : Writekmp(CAME, 36 + (i) * 72, RotY)
                RotZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(14).Value)) : Array.Reverse(RotZ) : Writekmp(CAME, 40 + (i) * 72, RotZ)
                Zstart = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(15).Value)) : Array.Reverse(Zstart) : Writekmp(CAME, 44 + (i) * 72, Zstart)
                Zend = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(16).Value)) : Array.Reverse(Zend) : Writekmp(CAME, 48 + (i) * 72, Zend)
                ViewX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(17).Value)) : Array.Reverse(ViewX) : Writekmp(CAME, 52 + (i) * 72, ViewX)
                ViewY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(18).Value)) : Array.Reverse(ViewY) : Writekmp(CAME, 56 + (i) * 72, ViewY)
                ViewZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(19).Value)) : Array.Reverse(ViewZ) : Writekmp(CAME, 60 + (i) * 72, ViewZ)
                View2X = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(20).Value)) : Array.Reverse(View2X) : Writekmp(CAME, 64 + (i) * 72, View2X)
                View2Y = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(21).Value)) : Array.Reverse(View2Y) : Writekmp(CAME, 68 + (i) * 72, View2Y)
                View2Z = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(22).Value)) : Array.Reverse(View2Z) : Writekmp(CAME, 72 + (i) * 72, View2Z)
                Time = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(23).Value)) : Array.Reverse(Time) : Writekmp(CAME, 76 + (i) * 72, Time)
            Next

        ElseIf group = My.Settings.Names(8) Then
            Dim temp2(0 To (DataGridView1.RowCount - 1) * &H1C + 7) As Byte
            JGPT = temp2
            Dim name As Byte() = {Strtoint("J"), Strtoint("G"), Strtoint("P"), Strtoint("T")}
            Writekmp(JGPT, 0, name)
            Dim amount As Byte() = BitConverter.GetBytes(DataGridView1.RowCount - 1) : Array.Reverse(amount)
            Writekmp(JGPT, 4, amount, 2)

            For i = 0 To DataGridView1.RowCount - 2
                Dim LocX, LocY, LocZ, RotX, RotY, RotZ, ID, Scale As Byte()
                Dim temp As Integer
                LocX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(1).Value)) : Array.Reverse(LocX) : Writekmp(JGPT, 8 + (i) * 28, LocX)
                LocY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(2).Value)) : Array.Reverse(LocY) : Writekmp(JGPT, 12 + (i) * 28, LocY)
                LocZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(3).Value)) : Array.Reverse(LocZ) : Writekmp(JGPT, 16 + (i) * 28, LocZ)
                RotX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(4).Value)) : Array.Reverse(RotX) : Writekmp(JGPT, 20 + (i) * 28, RotX)
                RotY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(5).Value)) : Array.Reverse(RotY) : Writekmp(JGPT, 24 + (i) * 28, RotY)
                RotZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(6).Value)) : Array.Reverse(RotZ) : Writekmp(JGPT, 28 + (i) * 28, RotZ)
                temp = "0" & DataGridView1.Rows(i).Cells(7).Value
                ID = BitConverter.GetBytes(temp) : Array.Reverse(ID) : Writekmp(JGPT, 32 + (i) * 28, ID, 2)
                temp = "&h" & "0" & DataGridView1.Rows(i).Cells(8).Value
                Scale = BitConverter.GetBytes(temp) : Array.Reverse(Scale) : Writekmp(JGPT, 34 + (i) * 28, Scale, 2)
            Next

        ElseIf group = My.Settings.Names(9) Then
            Dim temp2(0 To (DataGridView1.RowCount - 1) * &H1C + 7) As Byte
            CNPT = temp2
            Dim name As Byte() = {Strtoint("C"), Strtoint("N"), Strtoint("P"), Strtoint("T")}
            Writekmp(CNPT, 0, name)
            Dim amount As Byte() = BitConverter.GetBytes(DataGridView1.RowCount - 1) : Array.Reverse(amount)
            Writekmp(CNPT, 4, amount, 2)

            For i = 0 To DataGridView1.RowCount - 2
                Dim LocX, LocY, LocZ, RotX, RotY, RotZ, ID, S1 As Byte()
                Dim temp As Integer
                LocX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(1).Value)) : Array.Reverse(LocX) : Writekmp(CNPT, 8 + (i) * 28, LocX)
                LocY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(2).Value)) : Array.Reverse(LocY) : Writekmp(CNPT, 12 + (i) * 28, LocY)
                LocZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(3).Value)) : Array.Reverse(LocZ) : Writekmp(CNPT, 16 + (i) * 28, LocZ)
                RotX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(4).Value)) : Array.Reverse(RotX) : Writekmp(CNPT, 20 + (i) * 28, RotX)
                RotY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(5).Value)) : Array.Reverse(RotY) : Writekmp(CNPT, 24 + (i) * 28, RotY)
                RotZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(6).Value)) : Array.Reverse(RotZ) : Writekmp(CNPT, 28 + (i) * 28, RotZ)
                temp = "0" & DataGridView1.Rows(i).Cells(7).Value
                ID = BitConverter.GetBytes(temp) : Array.Reverse(ID) : Writekmp(CNPT, 32 + (i) * 28, ID, 2)
                S1 = XMLWrite(DataGridView1.Rows(i).Cells(8).Value, "XML/CNPT/Effects", "Tablefill.xml") : Array.Reverse(S1) : Writekmp(CNPT, 34 + (i) * 28, S1, 2)
            Next
        ElseIf group = My.Settings.Names(10) Then
            Dim temp2(0 To (DataGridView1.RowCount - 1) * &H1C + 7) As Byte
            MSPT = temp2
            Dim name As Byte() = {Strtoint("M"), Strtoint("S"), Strtoint("P"), Strtoint("T")}
            Writekmp(MSPT, 0, name)
            Dim amount As Byte() = BitConverter.GetBytes(DataGridView1.RowCount - 1) : Array.Reverse(amount)
            Writekmp(MSPT, 4, amount, 2)

            For i = 0 To DataGridView1.RowCount - 2
                Dim LocX, LocY, LocZ, RotX, RotY, RotZ, ID, S1 As Byte()
                Dim temp As Integer
                LocX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(1).Value)) : Array.Reverse(LocX) : Writekmp(CNPT, 8 + (i) * 28, LocX)
                LocY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(2).Value)) : Array.Reverse(LocY) : Writekmp(CNPT, 12 + (i) * 28, LocY)
                LocZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(3).Value)) : Array.Reverse(LocZ) : Writekmp(CNPT, 16 + (i) * 28, LocZ)
                RotX = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(4).Value)) : Array.Reverse(RotX) : Writekmp(CNPT, 20 + (i) * 28, RotX)
                RotY = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(5).Value)) : Array.Reverse(RotY) : Writekmp(CNPT, 24 + (i) * 28, RotY)
                RotZ = BitConverter.GetBytes(Floattohex(DataGridView1.Rows(i).Cells(6).Value)) : Array.Reverse(RotZ) : Writekmp(CNPT, 28 + (i) * 28, RotZ)
                temp = "0" & DataGridView1.Rows(i).Cells(7).Value
                ID = BitConverter.GetBytes(temp)
                Array.Reverse(ID)
                Writekmp(CNPT, 32 + (i) * 28, ID, 2)
                temp = "&h" & DataGridView1.Rows(i).Cells(8).Value
                S1 = BitConverter.GetBytes(temp) : Array.Reverse(S1) : Writekmp(CNPT, 34 + (i) * 28, S1, 2)
            Next
        ElseIf Not group = "STGI" And Not group = String.Empty Then
            Route.Readtable(Parsedroutes(Mid(group, 11)))

            'Dim valueX As Integer = Hextofloat(Readkmp(MSPT, 8 + (i) * 28, 4))
            'Dim valueY As Integer = Hextofloat(Readkmp(MSPT, 12 + (i) * 28, 4))
            'Dim valueZ As Integer = Hextofloat(Readkmp(MSPT, 16 + (i) * 28, 4))
            'Dim rotX As Single = Hextofloat(Readkmp(MSPT, 20 + (i) * 28, 4))
            'Dim rotY As Single = Hextofloat(Readkmp(MSPT, 24 + (i) * 28, 4))
            'Dim rotZ As Single = Hextofloat(Readkmp(MSPT, 28 + (i) * 28, 4))
            'Dim id2 As Short = Readkmp(MSPT, 32 + (i) * 28, 2)
            'Dim Sets As Short = Readkmp(MSPT, 34 + (i) * 28, 2)
            'Dim setting As String = Sets.ToString("X4")
        End If
    End Sub

    Dim IgnoreSelect As Boolean = False
    Public Sub FillTable(Optional fillsection As Integer = -1, Optional clearitems As Boolean = True)
        parsing = True
        Try
            c.Shapes.Clear()
        Catch ex As Exception : End Try
        If clearitems = True Then
            IgnoreSelect = True
            DataGridView1.Columns.Clear()
            IgnoreSelect = False
        End If
        'Setting-up default cellstyle

        Dim i As Integer = 0

        'STPT
        If Combobox1.text = My.Settings.Names(0) Then
            Try
                If Readkmp(STGI, 4, 1) > STGISetting3.Maximum Then STGISetting3.Maximum = STGI(8)
                STGISetting3.Value = STGI(8)
                STGISetting1.SelectedIndex = STGI(9)
                STGISetting2.SelectedIndex = STGI(10)
                CAMESetting1.Value = CAME(6)
            Catch ex As Exception : End Try
            selection = 0
            DataGridView1.Columns.Add(Datagridviewfill.Fill.KTPT("ID"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.KTPT("XPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.KTPT("YPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.KTPT("ZPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.KTPT("XRoll"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.KTPT("YRoll"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.KTPT("ZRoll"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.KTPT("Player"))

            Dim array(0 To (KTPT.Length - 8) / &H1C) As PowerPacks.OvalShape
            oval = array
            Do While i < (KTPT.Length - 8) / &H1C
                Dim valueX As Single = Hextofloat(Readkmp(KTPT, 8 + i * 28, 4))
                Dim valueY As Single = Hextofloat(Readkmp(KTPT, 12 + i * 28, 4))
                Dim valueZ As Single = Hextofloat(Readkmp(KTPT, 16 + i * 28, 4))
                Dim rotX As Single = Hextofloat(Readkmp(KTPT, 20 + i * 28, 4))
                Dim rotY As Single = Hextofloat(Readkmp(KTPT, 24 + i * 28, 4))
                Dim rotZ As Single = Hextofloat(Readkmp(KTPT, 28 + i * 28, 4))
                Dim temp As Short = Readkmp(KTPT, 32 + i * 28, 2)
                Dim Player As String = temp.ToString("X4")

                DataGridView1.Rows.Add(i, valueX, valueY, valueZ, rotX, rotY, rotZ, Player)
                oval(i) = NewOval(i, GridX + (GridPanel.Size.Width / 2) + (Math.Round(valueX) / Gridscale), GridY + (GridPanel.Size.Height / 2) + (Math.Round(valueZ) / Gridscale))
                i = i + 1
            Loop
        ElseIf Combobox1.text = My.Settings.Names(1) Then
            selection = 0
            If clearitems = True Then
                FillENPT()
            End If

            Dim doc As XPathDocument = New XPathDocument("Tablefill.xml")
            Dim nav As XPathNavigator = doc.CreateNavigator

            '---------------------------------------------------------------------------------------------------------------------------------------
            '------------------------------------------------------------ENPT-----------------------------------------------------------------------
            '---------------------------------------------------------------------------------------------------------------------------------------
            Dim array(0 To (ENPT.Length - 8) / &H14) As PowerPacks.OvalShape
            oval = array
            Do While i < (ENPT.Length - 8) / &H14
                Dim S1 As String = 0
                Dim S2 As String = 0
                Dim temp1 As Short = Readkmp(ENPT, 24 + i * 20, 2)
                Dim temp2 As Byte = Readkmp(ENPT, 26 + i * 20, 1)
                Try
                    If Not XMLRead(temp1, "/XML/ENPT/Setting1", "Tablefill.xml") = "Invalid" Then
                        S1 = XMLRead(temp1, "XML/ENPT/Setting1", "Tablefill.xml")
                    Else
                        S1 = "Default"
                    End If

                    If Not XMLRead(temp2, "/XML/ENPT/Setting2", "Tablefill.xml") = "Invalid" Then
                        S2 = XMLRead(temp2, "/XML/ENPT/Setting2", "Tablefill.xml")
                    Else
                        S2 = "Default"
                        Exit Try
                    End If
                Catch ex As Exception
                End Try
                Try
                    Dim valueX As Single = Hextofloat(Readkmp(ENPT, 8 + i * 20, 4))
                    Dim valueY As Single = Hextofloat(Readkmp(ENPT, 12 + i * 20, 4))
                    Dim valueZ As Single = Hextofloat(Readkmp(ENPT, 16 + i * 20, 4))
                    Dim Scale As Single = Hextofloat(Readkmp(ENPT, 20 + i * 20, 4))

                    Dim sect As Integer = -1
                    For s = 0 To Section.ENPH.Count - 1

                        If SectionarrayENPT(s).Frompoint <= i And SectionarrayENPT(s).Frompoint + SectionarrayENPT(s).Amount - 1 >= i Then
                            sect = s
                            Exit For
                        End If
                    Next
                    If sect = -1 Then
                        sect = 255
                    End If
                    If Sectionlist.Items.Count < sect + 1 And Not sect = 255 Then
                        Sectionlist.Items.Add("Section " & sect)
                    End If
                    oval(i) = NewOval(i, GridX + (GridPanel.Size.Width / 2) + (Math.Round(valueX, 0) / Gridscale), GridY + (GridPanel.Size.Height / 2) + (Math.Round(valueZ, 0) / Gridscale))
                    If My.Settings.ShowCombo = True Then
                        DataGridView1.Rows.Add(i, sect, valueX, valueY, valueZ, Scale, S1, S2)
                    Else
                        DataGridView1.Rows.Add(i, sect, valueX, valueY, valueZ, Scale, temp1.ToString("X2"), temp2.ToString("X"))
                    End If

                    If Not fillsection = -1 And Not sect = fillsection Then
                        DataGridView1.Rows(i).Visible = False
                        For Each row As DataGridViewRow In DataGridView1.Rows
                            If row.Visible = True Then row.Cells(0).Selected = True
                        Next
                    End If
                    i = i + 1

                Catch ex As Exception
                End Try
            Loop
            DataGridView1.ShowCellErrors = False
            DataGridView1.ShowRowErrors = False
        ElseIf Combobox1.text = My.Settings.Names(2) Then
            selection = 0
            If clearitems = True Then
                FillITPT()
            End If

            Dim doc As XPathDocument = New XPathDocument("Tablefill.xml")
            Dim nav As XPathNavigator = doc.CreateNavigator
            Dim iter As XPathNodeIterator

            '---------------------------------------------------------------------------------------------------------------------------------------
            '------------------------------------------------------------ITPT-----------------------------------------------------------------------
            '---------------------------------------------------------------------------------------------------------------------------------------
            Dim array(0 To (ITPT.Length - 8) / &H14) As PowerPacks.OvalShape
            oval = array
            Do While i < (ITPT.Length - 8) / &H14
                Dim S1 As String = 0
                Dim S2 As String = 0
                Try
                    iter = nav.Select("/XML/ITPT/Setting1") 'Your node name goes here
                    Dim temp1 As Short = Readkmp(ITPT, 24 + i * 20, 2)
                    If Not XMLRead(temp1, "/XML/ITPT/Setting1", "Tablefill.xml") = "Invalid" Then
                        S1 = XMLRead(temp1, "XML/ITPT/Setting1", "Tablefill.xml")
                    Else
                        S1 = "Default"
                    End If

                    iter = nav.Select("/XML/ITPT/Setting2") 'Your node name goes here
                    Dim temp2 As Short = Readkmp(ITPT, 26 + i * 20, 2)
                    If Not XMLRead(temp2, "/XML/ITPT/Setting2", "Tablefill.xml") = "Invalid" Then
                        S2 = XMLRead(temp2, "/XML/ITPT/Setting2", "Tablefill.xml")
                    Else
                        S2 = "Default"
                        Exit Try
                    End If
                Catch ex As Exception
                End Try
                Try
                    Dim valueX As Single = Hextofloat(Readkmp(ITPT, 8 + i * 20, 4))
                    Dim valueY As Single = Hextofloat(Readkmp(ITPT, 12 + i * 20, 4))
                    Dim valueZ As Single = Hextofloat(Readkmp(ITPT, 16 + i * 20, 4))
                    Dim Scale As Single = Hextofloat(Readkmp(ITPT, 20 + i * 20, 4))

                    Dim sect As Integer = -1
                    For s = 0 To Section.ITPH.Count - 1
                        If SectionarrayITPT(s).Frompoint <= i And SectionarrayITPT(s).Frompoint + SectionarrayITPT(s).Amount - 1 >= i Then
                            sect = s
                            Exit For
                        End If
                    Next
                    If sect = -1 Then
                        sect = 255
                    End If
                    If Sectionlist.Items.Count < sect + 1 And Not sect = 255 Then
                        Sectionlist.Items.Add("Section " & sect)
                    End If
                    DataGridView1.Rows.Add(i, sect, valueX, valueY, valueZ, Scale, S1, S2)
                    oval(i) = NewOval(i, GridX + (GridPanel.Size.Width / 2) + (Math.Round(valueX, 0) / Gridscale), GridY + (GridPanel.Size.Height / 2) + (Math.Round(valueZ, 0) / Gridscale))

                    If Not fillsection = -1 And Not sect = fillsection Then
                        DataGridView1.Rows(i).Visible = False
                        For Each row As DataGridViewRow In DataGridView1.Rows
                            If row.Visible = True Then row.Cells(0).Selected = True
                        Next
                    End If
                    i = i + 1

                Catch ex As Exception
                End Try
            Loop
            DataGridView1.ShowCellErrors = False
            DataGridView1.ShowRowErrors = False
        ElseIf Combobox1.text = My.Settings.Names(3) Then
            selection = 0
            If clearitems = True Then
                DataGridView1.Columns.Add(Datagridviewfill.Fill.CKPT("ID"))
                DataGridView1.Columns.Add(Datagridviewfill.Fill.CKPT("Section"))
                DataGridView1.Columns.Add(Datagridviewfill.Fill.CKPT("XPosition1"))
                DataGridView1.Columns.Add(Datagridviewfill.Fill.CKPT("ZPosition1"))
                DataGridView1.Columns.Add(Datagridviewfill.Fill.CKPT("XPosition2"))
                DataGridView1.Columns.Add(Datagridviewfill.Fill.CKPT("ZPosition2"))
                DataGridView1.Columns.Add(Datagridviewfill.Fill.CKPT("Respawn"))
                DataGridView1.Columns.Add(Datagridviewfill.Fill.CKPT("Type"))
                DataGridView1.Columns.Add(Datagridviewfill.Fill.CKPT("Last"))
                DataGridView1.Columns.Add(Datagridviewfill.Fill.CKPT("Next"))
            End If
            Dim array(0 To (CKPT.Length - 8) / &H14) As PowerPacks.OvalShape
            oval = array
            Do While i < (CKPT.Length - 8) / &H14
                Dim valueX As Single = Hextofloat(Readkmp(CKPT, 8 + i * 20, 4))
                Dim valueY As Single = Hextofloat(Readkmp(CKPT, 12 + i * 20, 4))
                Dim valueX2 As Single = Hextofloat(Readkmp(CKPT, 16 + i * 20, 4))
                Dim valueY2 As Single = Hextofloat(Readkmp(CKPT, 20 + i * 20, 4))
                Dim rspn As Byte = Readkmp(CKPT, 24 + i * 20, 1)
                Dim type As Byte = Readkmp(CKPT, 25 + i * 20, 1)
                Dim type2 As String = type.ToString("X2")
                Dim last As Byte = Readkmp(CKPT, 26 + i * 20, 1)
                Dim nexti As Byte = Readkmp(CKPT, 27 + i * 20, 1)

                Dim sect As Integer = -1
                For s = 0 To Section.CKPH.Count - 1
                    If SectionarrayCKPT(s).Frompoint <= i And SectionarrayCKPT(s).Frompoint + SectionarrayCKPT(s).Amount - 1 >= i Then
                        sect = s
                        Exit For
                    End If
                Next
                If sect = -1 Then
                    sect = 255
                End If
                If Sectionlist.Items.Count < sect + 1 And Not sect = 255 Then
                    Sectionlist.Items.Add("Section " & sect)
                End If
                DataGridView1.Rows.Add(i, sect, valueX, valueY, valueX2, valueY2, rspn, type2, last, nexti)

                If Not fillsection = -1 And Not sect = fillsection Then
                    DataGridView1.Rows(i).Visible = False
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If row.Visible = True Then row.Cells(0).Selected = True
                    Next
                End If
                i = i + 1
            Loop
            parsing = False
            FillGrid()
            parsing = True
        ElseIf Combobox1.text = My.Settings.Names(4) Then
            selection = 0
            'ID, ObjID, XPosition, YPosition, ZPosition, XRoll, YRoll, ZRoll, XSize, YSize, ZSize, Route, Settings, Presence
            DataGridView1.Columns.Add(Datagridviewfill.Fill.GOBJ("ID"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.GOBJ("ObjID"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.GOBJ("XPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.GOBJ("YPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.GOBJ("ZPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.GOBJ("XRoll"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.GOBJ("YRoll"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.GOBJ("ZRoll"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.GOBJ("XSize"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.GOBJ("YSize"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.GOBJ("ZSize"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.GOBJ("Route"))
            Dim j As Byte = 0
            Do While j < 8
                DataGridView1.Columns.Add(Datagridviewfill.Fill.GOBJ("Settings"))
                j = 1 + j
            Loop
            DataGridView1.Columns.Add(Datagridviewfill.Fill.GOBJ("Presence"))
            Dim array(0 To (GOBJ.Length - 8) / &H3C) As PowerPacks.OvalShape
            oval = array
            Do While i < (GOBJ.Length - 8) / &H3C
                Dim objid As Short = Readkmp(GOBJ, 8 + i * &H3C, 2)
                Dim ID As String = ObjectRead(objid).Name
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
                DataGridView1.Rows.Add(i, ID, valueX, valueY, valueZ, rotX, rotY, rotZ, sclX, sclY, sclZ, routeid, set1, set2, set3, set4, set5, set6, set7, set8, presenceflag)
                oval(i) = NewOval(i, GridX + (GridPanel.Size.Width / 2) + (Math.Round(valueX, 0) / Gridscale), GridY + (GridPanel.Size.Height / 2) + (Math.Round(valueZ, 0) / Gridscale))
                i = i + 1
            Loop

        ElseIf Combobox1.text = My.Settings.Names(5) Then
            selection = 0
            '---------------------------------------------------------------------------------------------------------------------------------------
            '------------------------------------------------------------POTI-----------------------------------------------------------------------
            '---------------------------------------------------------------------------------------------------------------------------------------
            FillPOTI()
            Dim ip As Integer = 0
            Dim array(0 To Parsedroutes.Length) As PowerPacks.OvalShape
            oval = array
            Do While i < Parsedroutes.Length
                Dim S1 As String = 0
                Dim S2 As String = 0

                If Not XMLRead(Parsedroutes(i).Setting1, "/XML/POTI/Setting1", "Tablefill.xml") = "Invalid" Then
                    S1 = XMLRead(Parsedroutes(i).Setting1, "XML/POTI/Setting1", "Tablefill.xml")
                End If

                If Not XMLRead(Parsedroutes(i).Setting1, "/XML/POTI/Setting2", "Tablefill.xml") = "Invalid" Then
                    S2 = XMLRead(Parsedroutes(i).Setting2, "/XML/POTI/Setting2", "Tablefill.xml")
                End If

                Dim Amount As Short = Parsedroutes(i).points.Length
                ip = ip + Amount * &H10 + 4
                If i > RouteUsage.Length - 1 Then
                    DataGridView1.Rows.Add(i, Amount, S1, S2, "Show", "")
                Else
                    DataGridView1.Rows.Add(i, Amount, S1, S2, "Show", RouteUsage(i))
                End If
                i = i + 1
            Loop

        ElseIf Combobox1.text = My.Settings.Names(6) Then
            selection = 0
            FillAREA()

            Dim doc As XPathDocument = New XPathDocument("Tablefill.xml")
            Dim nav As XPathNavigator = doc.CreateNavigator
            Dim iter As XPathNodeIterator
            Dim array(0 To (AREA.Length - 8) / &H30) As PowerPacks.OvalShape
            oval = array
            Do While i < (AREA.Length - 8) / &H30
                Dim S1 As String = 0
                Try
                    iter = nav.Select("/XML/AREA/Variants") 'Your node name goes here
                    Dim temp1 As Short = Readkmp(AREA, 9 + i * 48, 1)
                    If Not XMLRead(temp1, "/XML/AREA/Variants", "Tablefill.xml") = "Invalid" Then
                        S1 = XMLRead(temp1, "/XML/AREA/Variants", "Tablefill.xml")
                    Else
                        S1 = "Default"
                    End If
                Catch ex As Exception
                    MsgBox("An error has occurred while reading from Tablefill.xml" & Chr(13) & Chr(13) & ex.Message)
                End Try
                Dim mode As Byte = Readkmp(AREA, 8 + i * 48, 1)
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

                DataGridView1.Rows.Add(i, modes, S1, Cames, valueX, valueY, valueZ, rotX, rotY, rotZ, sclX, sclY, sclZ, Sets, routes, enemies)
                oval(i) = NewOval(i, GridX + (GridPanel.Size.Width / 2) + (Math.Round(valueX, 0) / Gridscale), GridY + (GridPanel.Size.Height / 2) + (Math.Round(valueZ, 0) / Gridscale))
                i = i + 1
            Loop
        ElseIf Combobox1.text = My.Settings.Names(7) Then
            selection = 0
            FillCAME()

            Dim doc As XPathDocument = New XPathDocument("Tablefill.xml")
            Dim nav As XPathNavigator = doc.CreateNavigator
            Dim iter As XPathNodeIterator
            Dim array(0 To Readkmp(CAME, 4, 2)) As PowerPacks.OvalShape
            oval = array
            Do While i < (CAME.Length - 8) / 72
                Dim S1 As String = 0
                Try
                    iter = nav.Select("/XML/AREA/Variants") 'Your node name goes here
                    Dim temp1 As Short = Readkmp(CAME, 8 + i * 72, 1)
                    If Not XMLRead(temp1, "/XML/CAME/Variants", "Tablefill.xml") = "Invalid" Then
                        S1 = XMLRead(temp1, "/XML/CAME/Variants", "Tablefill.xml")
                    Else
                        S1 = "Default"
                    End If
                Catch ex As Exception
                    MsgBox("An error has occurred while reading from Tablefill.xml" & Chr(13) & Chr(13) & ex.Message)
                End Try
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
                Dim Time As Integer = Hextofloat(Readkmp(CAME, 76 + i * 72, 4))

                DataGridView1.Rows.Add(i, S1, Cames, Shakei, Routi, V, VZ, VPs, flagi, valueX, valueY, valueZ, rotX, rotY, rotZ, Zstart, Zend, viewX, viewY, viewZ, view2X, view2Y, view2Z, Time)
                oval(i) = NewOval(i, GridX + (GridPanel.Size.Width / 2) + (Math.Round(valueX, 0) / Gridscale), GridY + (GridPanel.Size.Height / 2) + (Math.Round(valueZ, 0) / Gridscale))
                i = i + 1
            Loop
        ElseIf Combobox1.text = My.Settings.Names(8) Then
            selection = 0
            DataGridView1.Columns.Add(Datagridviewfill.Fill.JGPT("ID"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.JGPT("XPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.JGPT("YPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.JGPT("ZPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.JGPT("XRoll"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.JGPT("YRoll"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.JGPT("ZRoll"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.JGPT("ID2"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.JGPT("Range"))
            Dim array(0 To (JGPT.Length - 8) / &H1C) As PowerPacks.OvalShape
            oval = array
            Do While i < (JGPT.Length - 8) / &H1C
                Dim valueX As Integer = Hextofloat(Readkmp(JGPT, 8 + i * 28, 4))
                Dim valueY As Integer = Hextofloat(Readkmp(JGPT, 12 + i * 28, 4))
                Dim valueZ As Integer = Hextofloat(Readkmp(JGPT, 16 + i * 28, 4))
                Dim rotX As Single = Hextofloat(Readkmp(JGPT, 20 + i * 28, 4))
                Dim rotY As Single = Hextofloat(Readkmp(JGPT, 24 + i * 28, 4))
                Dim rotZ As Single = Hextofloat(Readkmp(JGPT, 28 + i * 28, 4))
                Dim ID2 As Short = Readkmp(JGPT, 32 + i * 28, 2)
                Dim scale As Short = Readkmp(JGPT, 34 + i * 28, 2)
                Dim scales As String = scale.ToString("X4")

                DataGridView1.Rows.Add(i, valueX, valueY, valueZ, rotX, rotY, rotZ, ID2, scales)
                oval(i) = NewOval(i, GridX + (GridPanel.Size.Width / 2) + (Math.Round(valueX, 0) / Gridscale), GridY + (GridPanel.Size.Height / 2) + (Math.Round(valueZ, 0) / Gridscale))
                i = i + 1
            Loop
        ElseIf Combobox1.text = My.Settings.Names(9) Then
            selection = 0
            FillCNPT()

            Dim doc As XPathDocument = New XPathDocument("Tablefill.xml")
            Dim nav As XPathNavigator = doc.CreateNavigator
            Dim iter As XPathNodeIterator
            Dim array(0 To (CNPT.Length - 8) / 28) As PowerPacks.OvalShape
            oval = array
            Do While i < (CNPT.Length - 8) / 28
                Dim S1 As String = 0
                Try
                    iter = nav.Select("/XML/CNPT/Effects") 'Your node name goes here
                    Dim temp As Short = Readkmp(CNPT, 34 + i * 28, 2)
                    Dim temp1 As String = temp.ToString("X4")
                    Dim xml As String = XMLRead(temp1, "XML/CNPT/Effects", "Tablefill.xml")
                    If Not xml = "Invalid" Then
                        S1 = xml
                    Else
                        S1 = "Default"
                    End If
                Catch ex As Exception
                    MsgBox("An error has occurred while reading from Tablefill.xml" & Chr(13) & Chr(13) & ex.Message)
                End Try

                Dim valueX As Integer = Hextofloat(Readkmp(CNPT, 8 + i * 28, 4))
                Dim valueY As Integer = Hextofloat(Readkmp(CNPT, 12 + i * 28, 4))
                Dim valueZ As Integer = Hextofloat(Readkmp(CNPT, 16 + i * 28, 4))
                Dim rotX As Single = Hextofloat(Readkmp(CNPT, 20 + i * 28, 4))
                Dim rotY As Single = Hextofloat(Readkmp(CNPT, 24 + i * 28, 4))
                Dim rotZ As Single = Hextofloat(Readkmp(CNPT, 28 + i * 28, 4))
                Dim ID2 As Short = Readkmp(CNPT, 32 + i * 28, 2)

                DataGridView1.Rows.Add(i, valueX, valueY, valueZ, rotX, rotY, rotZ, ID2, S1)
                oval(i) = NewOval(i, GridX + (GridPanel.Size.Width / 2) + (Math.Round(valueX, 0) / Gridscale), GridY + (GridPanel.Size.Height / 2) + (Math.Round(valueZ, 0) / Gridscale))
                i = i + 1
            Loop
        ElseIf Combobox1.text = My.Settings.Names(10) Then
            selection = 0
            DataGridView1.Columns.Add(Datagridviewfill.Fill.MSPT("ID"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.MSPT("XPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.MSPT("YPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.MSPT("ZPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.MSPT("XRoll"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.MSPT("YRoll"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.MSPT("ZRoll"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.MSPT("ID2"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.MSPT("Settings"))

            Dim array(0 To Readkmp(MSPT, 4, 2)) As PowerPacks.OvalShape
            oval = array
            Do While i < (MSPT.Length - 8) / &H1C
                Dim valueX As Integer = Hextofloat(Readkmp(MSPT, 8 + i * 28, 4))
                Dim valueY As Integer = Hextofloat(Readkmp(MSPT, 12 + i * 28, 4))
                Dim valueZ As Integer = Hextofloat(Readkmp(MSPT, 16 + i * 28, 4))
                Dim rotX As Single = Hextofloat(Readkmp(MSPT, 20 + i * 28, 4))
                Dim rotY As Single = Hextofloat(Readkmp(MSPT, 24 + i * 28, 4))
                Dim rotZ As Single = Hextofloat(Readkmp(MSPT, 28 + i * 28, 4))
                Dim id2 As Short = Readkmp(MSPT, 32 + i * 28, 2)
                Dim Sets As Short = Readkmp(MSPT, 34 + i * 28, 2)
                Dim setting As String = Sets.ToString("X4")
                DataGridView1.Rows.Add(i, valueX, valueY, valueZ, rotX, rotY, rotZ, id2, setting)
                oval(i) = NewOval(i, GridX + (GridPanel.Size.Width / 2) + (Math.Round(valueX, 0) / Gridscale), GridY + (GridPanel.Size.Height / 2) + (Math.Round(valueZ, 0) / Gridscale))
                i = i + 1
            Loop
        ElseIf Combobox1.text = "ENPH" Then
            selection = 0
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("ID"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("From"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("Amount"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))

            Array.Clear(oval, 0, oval.Length)
            Do While i < SectionarrayENPT.Length
                Dim it As Section.ENPH.Section = SectionarrayENPT(i)
                DataGridView1.Rows.Add(i, it.Frompoint, it.Amount, it.Nextsection(0), it.Nextsection(1), it.Nextsection(2), it.Nextsection(3), it.Nextsection(4), it.Nextsection(5), it.Nextsection(0), it.Nextsection(1), it.Nextsection(2), it.Nextsection(3), it.Nextsection(4), it.Nextsection(5))
                i += 1
            Loop

            For i = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Cells(3).Value = 255 Then
                    DataGridView1.Rows(i).Cells(3).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(4).Value = 255 Then
                    DataGridView1.Rows(i).Cells(4).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(4).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(5).Value = 255 Then
                    DataGridView1.Rows(i).Cells(5).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(5).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(6).Value = 255 Then
                    DataGridView1.Rows(i).Cells(6).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(6).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(7).Value = 255 Then
                    DataGridView1.Rows(i).Cells(7).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(7).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(8).Value = 255 Then
                    DataGridView1.Rows(i).Cells(8).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(8).Style.ForeColor = Color.White
                End If
            Next
        ElseIf Combobox1.text = "ITPH" Then
            selection = 0
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("ID"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("From"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("Amount"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))

            Array.Clear(oval, 0, oval.Length)
            Do While i < SectionarrayITPT.Length
                Dim it As Section.ITPH.Section = SectionarrayITPT(i)
                DataGridView1.Rows.Add(i, it.Frompoint, it.Amount, it.Nextsection(0), it.Nextsection(1), it.Nextsection(2), it.Nextsection(3), it.Nextsection(4), it.Nextsection(5))
                i += 1
            Loop
            For i = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Cells(3).Value = 255 Then
                    DataGridView1.Rows(i).Cells(3).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(4).Value = 255 Then
                    DataGridView1.Rows(i).Cells(4).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(4).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(5).Value = 255 Then
                    DataGridView1.Rows(i).Cells(5).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(5).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(6).Value = 255 Then
                    DataGridView1.Rows(i).Cells(6).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(6).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(7).Value = 255 Then
                    DataGridView1.Rows(i).Cells(7).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(7).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(8).Value = 255 Then
                    DataGridView1.Rows(i).Cells(8).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(8).Style.ForeColor = Color.White
                End If
            Next
        ElseIf Combobox1.text = "CKPH" Then
            selection = 0
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("ID"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("From"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("Amount"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Section("To"))

            Array.Clear(oval, 0, oval.Length)
            Do While i < SectionarrayCKPT.Length
                Dim it As Section.CKPH.Section = SectionarrayCKPT(i)
                DataGridView1.Rows.Add(i, it.Frompoint, it.Amount, it.Nextsection(0), it.Nextsection(1), it.Nextsection(2), it.Nextsection(3), it.Nextsection(4), it.Nextsection(5))
                i += 1
            Loop
            For i = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Cells(3).Value = 255 Then
                    DataGridView1.Rows(i).Cells(3).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(4).Value = 255 Then
                    DataGridView1.Rows(i).Cells(4).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(4).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(5).Value = 255 Then
                    DataGridView1.Rows(i).Cells(5).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(5).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(6).Value = 255 Then
                    DataGridView1.Rows(i).Cells(6).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(6).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(7).Value = 255 Then
                    DataGridView1.Rows(i).Cells(7).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(7).Style.ForeColor = Color.White
                End If
                If DataGridView1.Rows(i).Cells(8).Value = 255 Then
                    DataGridView1.Rows(i).Cells(8).Style.BackColor = Color.FromArgb(&HFFA00000) : DataGridView1.Rows(i).Cells(8).Style.ForeColor = Color.White
                End If
            Next
        End If
        If Not oval Is Nothing Then
            If Not oval.Length = 0 Then
                If Not Combobox1.text = "" And Not oval(0) Is Nothing Then
                    oval(0).Select()
                End If
            End If
        End If
        If Not Combobox1.text = "ENPH" Or Combobox1.text = "ITPH" Or Combobox1.text = "CKPH" Then
            For y = 1 To DataGridView1.RowCount - 2 Step 2
                'DataGridView1.Rows(y).DefaultCellStyle.BackColor = Color.FromArgb(&HFFBDE8FF)
                DataGridView1.Rows(y).DefaultCellStyle.BackColor = Color.FromArgb(&HFFFFF0F0)
            Next
        End If
        parsing = False
    End Sub

    Private Sub Keyreleased(ByVal sender As Object, e As Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.Control Then
            GridPanel.Invalidate()
            FillGrid()
        End If
    End Sub

    Public Sub FillGrid()
        If parsing = False Then
            Filling = True
            'graph = GridPanel.CreateGraphics
            c.Shapes.Clear()
            d.Shapes.Clear()
            sc.Shapes.Clear()
            'If Not graph Is Nothing Then : graph.Clear(Color.Transparent) : End If
            Dim rowcount As Integer = 0
            If Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(3) Then
                For Each thing In DataGridView1.Rows
                    If thing.visible = True Then rowcount += 1
                Next
            Else
                rowcount = DataGridView1.RowCount
            End If
            Dim temp(0 To rowcount - 1) As PowerPacks.OvalShape
            Dim timp(0 To rowcount - 1) As PowerPacks.OvalShape
            oval = temp
            oval2 = timp
            Dim temp2(0 To rowcount - 1) As PowerPacks.LineShape
            line = temp2

            If Combobox1.text = My.Settings.Names(3) Then
                GridPanel.Invalidate()
            End If
            If Combobox1.text = My.Settings.Names(6) Then
                GridPanel.Invalidate()
            End If

            If Combobox1.text = My.Settings.Names(0) Then
                If My.Computer.Keyboard.CtrlKeyDown Then GridPanel.Invalidate()
            End If

            If Combobox1.text = My.Settings.Names(4) Then
                If My.Computer.Keyboard.CtrlKeyDown Then GridPanel.Invalidate()
            End If

            If Combobox1.text = My.Settings.Names(10) Then
                If My.Computer.Keyboard.CtrlKeyDown Then GridPanel.Invalidate()
            End If



            Dim rowcollection(rowcount - 1)
            Dim cur As Integer = 0
            For a = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(a).Visible = True Then
                    rowcollection(cur) = DataGridView1.Rows(a)
                    cur += 1
                End If
                If cur = rowcount Then Exit For
            Next


            Dim i As Integer
            Do While i < rowcount - 1
                If Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(4) Then
                    NewOval(i, GridX + (GridPanel.Size.Width / 2) + ((rowcollection(i).Cells(2).Value) / Gridscale), GridY + (GridPanel.Size.Height / 2) + ((rowcollection(i).Cells(4).Value) / Gridscale))
                ElseIf Combobox1.text = My.Settings.Names(6) Then
                    NewOval(i, GridX + (GridPanel.Size.Width / 2) + ((rowcollection(i).Cells(4).Value) / Gridscale), GridY + (GridPanel.Size.Height / 2) + ((rowcollection(i).Cells(6).Value) / Gridscale))
                ElseIf Combobox1.text = My.Settings.Names(7) Then
                    NewOval(i, GridX + (GridPanel.Size.Width / 2) + ((rowcollection(i).Cells(9).Value) / Gridscale), GridY + (GridPanel.Size.Height / 2) + ((rowcollection(i).Cells(11).Value) / Gridscale))
                ElseIf Combobox1.text = My.Settings.Names(3) Then
                    NewOval(i, GridX + (GridPanel.Size.Width / 2) + ((rowcollection(i).Cells(2).Value) / Gridscale), GridY + (GridPanel.Size.Height / 2) + ((rowcollection(i).Cells(3).Value) / Gridscale))
                    NewOval2(i, GridX + (GridPanel.Size.Width / 2) + ((rowcollection(i).Cells(4).Value) / Gridscale), GridY + (GridPanel.Size.Height / 2) + ((rowcollection(i).Cells(5).Value) / Gridscale))
                    NewLine(i)
                ElseIf Not Combobox1.text = My.Settings.Names(5) And Not Mid(Combobox1.text, 3) = "PH" Then
                    NewOval(i, GridX + (GridPanel.Size.Width / 2) + ((rowcollection(i).Cells(1).Value) / Gridscale), GridY + (GridPanel.Size.Height / 2) + ((rowcollection(i).Cells(3).Value) / Gridscale))
                End If
                i = i + 1
                If i > &HFFFF Then
                    Dim ex As OverflowException = New OverflowException("Parameter i can't be higher than 0xFFFF, terminating process")
                    Throw ex
                End If
            Loop
            If Not DataGridView1.RowCount < 2 Then
                If Combobox1.text = My.Settings.Names(7) Then
                    If XMLWrite(DataGridView1.CurrentRow.Cells(1).Value, "XML/CAME/Variants", "Tablefill.xml")(0) = 5 Then
                        redoval.Location = New Drawing.Point(GridX + (GridPanel.Size.Width / 2) + (DataGridView1.CurrentRow.Cells(17).Value / Gridscale), GridY + (GridPanel.Size.Height / 2) + (DataGridView1.CurrentRow.Cells(19).Value / Gridscale))
                        redoval.Cursor = Cursors.SizeAll
                        redoval.Size = New Drawing.Size(5, 5)
                        redoval.FillColor = Color.Yellow
                        redoval.FillStyle = PowerPacks.FillStyle.Solid
                        sc.Shapes.Add(redoval)
                        AddHandler redoval.MouseDown, AddressOf Redoval_Mousedown
                        yellowoval.Location = New Drawing.Point(GridX + (GridPanel.Size.Width / 2) + (DataGridView1.CurrentRow.Cells(20).Value / Gridscale), GridY + (GridPanel.Size.Height / 2) + (DataGridView1.CurrentRow.Cells(22).Value / Gridscale))
                        yellowoval.Cursor = Cursors.SizeAll
                        yellowoval.Width = 5
                        yellowoval.Height = 5
                        yellowoval.FillColor = Color.SkyBlue
                        yellowoval.FillStyle = PowerPacks.FillStyle.Solid
                        sc.Shapes.Add(yellowoval)
                        AddHandler yellowoval.MouseDown, AddressOf Yellowoval_Mousedown
                    End If
                End If
            End If
            Filling = False
        End If
    End Sub

    Dim moving As Boolean = False
    Private Sub Panel_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridPanel.Click
        Dim GetY As Boolean = False

        If sorting(0) = True Then
            Dim X As Single = (GridPanel.PointToClient(MousePosition).X - GridX - (GridPanel.Size.Width / 2)) * Gridscale
            Dim Y As Single = (GridPanel.PointToClient(MousePosition).Y - GridY - (GridPanel.Size.Height / 2)) * Gridscale
            center = New Point(X, Y)
            If sorting(1) = True Then Sort_Clockwise(sender, New EventArgs) Else Sort_Counterclockwise(sender, New EventArgs)
            GridPanel.Cursor = Cursors.Default
            Exit Sub
        End If

        If DataGridView1.ColumnCount = 0 Then Exit Sub
        If My.Settings.Addleft = True Then
            If Not Combobox1.text = My.Settings.Names(3) And e.Button = Windows.Forms.MouseButtons.Left Then
                Dim id As String = DataGridView1.RowCount - 1
                Dim X As Single = (GridPanel.PointToClient(MousePosition).X - GridX - (GridPanel.Size.Width / 2)) * Gridscale
                Dim Y As Single = (GridPanel.PointToClient(MousePosition).Y - GridY - (GridPanel.Size.Height / 2)) * Gridscale
                Dim Visicount As Integer
                If Sectionlist.SelectedItems.Count = 1 And Not Sectionlist.Items.Count = 1 And (Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(3)) Then
                    For Each row In DataGridView1.Rows
                        If row.visible = True Then Visicount += 1
                    Next
                    Visicount -= 1
                    DataGridView1.Rows.Insert(Visicount, {Visicount})
                    If Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(4) Then
                        DataGridView1.Rows(Visicount - 1).Cells(2).Value = X
                        DataGridView1.Rows(Visicount - 1).Cells(4).Value = Y
                    ElseIf Combobox1.text = My.Settings.Names(6) Then
                        DataGridView1.Rows(Visicount - 1).Cells(4).Value = X
                        DataGridView1.Rows(Visicount - 1).Cells(6).Value = Y
                    ElseIf Combobox1.text = My.Settings.Names(7) Then
                        DataGridView1.Rows(Visicount - 1).Cells(9).Value = X
                        DataGridView1.Rows(Visicount - 1).Cells(11).Value = Y
                    ElseIf Not Combobox1.text = My.Settings.Names(5) Then
                        DataGridView1.Rows(Visicount - 1).Cells(1).Value = X
                        DataGridView1.Rows(Visicount - 1).Cells(3).Value = Y
                    End If


                    Dim GroupID As Integer = Mid(Sectionlist.SelectedItem, 9)

                    If Combobox1.text = My.Settings.Names(1) Then
                        SectionarrayENPT(GroupID).Amount += 1
                        ENPH(GroupID * &H10 + 1) += 1
                        For ida = GroupID + 1 To SectionarrayENPT.Length - 1
                            SectionarrayENPT(ida).Frompoint += 1
                            ENPH(GroupID * &H10) += 1
                        Next
                    End If
                    If Combobox1.text = My.Settings.Names(2) Then
                        SectionarrayITPT(GroupID).Amount += 1
                        ITPH(GroupID * &H10 + 1) += 1
                        For ida = GroupID + 1 To SectionarrayITPT.Length - 1
                            SectionarrayITPT(ida).Frompoint += 1
                            ITPH(GroupID * &H10) += 1
                        Next
                    End If
                    If Combobox1.text = My.Settings.Names(3) Then
                        SectionarrayCKPT(GroupID).Amount += 1
                        CKPH(GroupID * &H10 + 1) += 1
                        For ida = GroupID + 1 To SectionarrayCKPT.Length - 1
                            SectionarrayCKPT(ida).Frompoint += 1
                            CKPH(GroupID * &H10) += 1
                        Next
                    End If
                Else
                    DataGridView1.Rows.Add({id})
                    If Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(4) Then
                        DataGridView1.Rows(id).Cells(2).Value = X
                        DataGridView1.Rows(id).Cells(4).Value = Y
                    ElseIf Combobox1.text = My.Settings.Names(6) Then
                        DataGridView1.Rows(id).Cells(4).Value = X
                        DataGridView1.Rows(id).Cells(6).Value = Y
                    ElseIf Combobox1.text = My.Settings.Names(7) Then
                        DataGridView1.Rows(id).Cells(9).Value = X
                        DataGridView1.Rows(id).Cells(11).Value = Y
                    ElseIf Not Combobox1.text = My.Settings.Names(5) Then
                        DataGridView1.Rows(id).Cells(1).Value = X
                        DataGridView1.Rows(id).Cells(3).Value = Y
                    End If
                End If
                Filesavedcheck = True
                Try
                    If Not PathBG = "" Then
                        If Mid(PathBG, PathBG.Length - 2) = "OBJ" Or Mid(PathBG, PathBG.Length - 2) = "obj" Then
                            If Not Combobox1.text = My.Settings.Names(3) Then GetY = True
                            If yValues.Length = 1 Then
                                Dim Vertices0 As New ArrayList
                                Dim Faces0 As New ArrayList
                                Dim yValues0 As New ArrayList

                                Dim lines As String()

                                Dim line As New ArrayList
                                Dim sr As New IO.StreamReader(PathBG)
                                Do While sr.Peek() >= 0
                                    line.Add(sr.ReadLine)
                                Loop
                                lines = CType(line.ToArray(GetType(String)), String())


                                'Dialog1.ShowDialog()


                                For i = 0 To lines.Count - 1
                                    If Mid(lines(i), 1, 2) = "v " Then
                                        lines(i) = lines(i).Replace(".", ",")
                                        lines(i) = lines(i).Replace("  ", " ")
                                        Dim a As String() = System.Text.RegularExpressions.Regex.Split(lines(i), " ")
                                        Vertices0.Add(New Point(a(1), a(3)))
                                        Dim y2 As Single = a(2)
                                        yValues0.Add(y2)
                                    ElseIf Mid(lines(i), 1, 2) = "f " Then
                                        Dim a As String() = System.Text.RegularExpressions.Regex.Split(lines(i), " ")
                                        Dim b(0 To 2) As Integer
                                        b(0) = System.Text.RegularExpressions.Regex.Split(a(1), "/")(0)
                                        b(1) = System.Text.RegularExpressions.Regex.Split(a(2), "/")(0)
                                        b(2) = System.Text.RegularExpressions.Regex.Split(a(3), "/")(0)
                                        Faces0.Add(b)
                                    End If
                                Next
                                Vertices = DirectCast(Vertices0.ToArray(GetType(Point)), Point())
                                Faces = DirectCast(Faces0.ToArray(GetType(Integer())), Object())
                                yValues = DirectCast(yValues0.ToArray(GetType(Single)), Single())
                            End If
                        End If
                    End If
                Catch ex As Exception : GetY = False : End Try

                Dim Z As New Single
                If GetY = True Then
                    For Each face In Faces
                        If CoordinateTriangleCheck({Vertices(face(0) - 1), Vertices(face(1) - 1), Vertices(face(2) - 1)}, _
                                                   {yValues(face(0) - 1), yValues(face(1) - 1), yValues(face(2) - 1)}, New Point(X, Y)) = True Then
                            Dim ztemp As New Single
                            ztemp = CoordinateTriangleY({Vertices(face(0) - 1), Vertices(face(1) - 1), Vertices(face(2) - 1)}, _
                                                    {yValues(face(0) - 1), yValues(face(1) - 1), yValues(face(2) - 1)}, New Point(X, Y))
                            If Math.Abs(ztemp) > Z Then Z = Math.Abs(ztemp)
                        End If
                    Next

                    If Sectionlist.SelectedItems.Count = 1 And Not Sectionlist.Items.Count = 1 And (Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2)) Then
                        If Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(4) Then
                            If GetY = True Then DataGridView1.Rows(Visicount - 1).Cells(3).Value = Z
                        ElseIf Combobox1.text = My.Settings.Names(6) Then
                            If GetY = True Then DataGridView1.Rows(Visicount - 1).Cells(5).Value = Z
                        ElseIf Combobox1.text = My.Settings.Names(7) Then
                            If GetY = True Then DataGridView1.Rows(Visicount - 1).Cells(10).Value = Z
                        ElseIf Not Combobox1.text = My.Settings.Names(5) Then
                            If GetY = True Then DataGridView1.Rows(Visicount - 1).Cells(2).Value = Z
                        End If
                    Else
                        If Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(4) Then
                            If GetY = True Then DataGridView1.Rows(id).Cells(3).Value = Z
                        ElseIf Combobox1.text = My.Settings.Names(6) Then
                            If GetY = True Then DataGridView1.Rows(id).Cells(5).Value = Z
                        ElseIf Combobox1.text = My.Settings.Names(7) Then
                            If GetY = True Then DataGridView1.Rows(id).Cells(10).Value = Z
                        ElseIf Not Combobox1.text = My.Settings.Names(5) Then
                            If GetY = True Then DataGridView1.Rows(id).Cells(2).Value = Z
                        End If
                    End If
                End If
                FillGrid()
            End If
        End If
    End Sub
    Private Sub Panal_Mousedown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridPanel.MouseDown
        If Combobox1.text = My.Settings.Names(3) And e.Button = Windows.Forms.MouseButtons.Left And My.Settings.Addleft = True Then
            Dim id As String = DataGridView1.RowCount - 1
            DataGridView1.Rows.Add({id})

            Dim X As Single = (GridPanel.PointToClient(MousePosition).X - GridX - (GridPanel.Size.Width / 2)) * Gridscale
            Dim Y As Single = (GridPanel.PointToClient(MousePosition).Y - GridY - (GridPanel.Size.Height / 2)) * Gridscale
            Filesavedcheck = True
            DataGridView1.Rows(id).Cells(2).Value = X
            DataGridView1.Rows(id).Cells(3).Value = Y
            DataGridView1.Rows(id).Cells(4).Value = X
            DataGridView1.Rows(id).Cells(5).Value = Y
            FillGrid()
            Timer2.Tag = id
            Timer2.Start()
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
            Timer3.Tag = GridPanel.PointToClient(MousePosition).X & ":" & GridPanel.PointToClient(MousePosition).Y
            Timer3.Start()
        End If
    End Sub

    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        If Not MouseButtons.ToString = "Right" Then
            Timer3.Stop()
        End If
        Dim thetags As String() = Timer3.Tag.ToString.Split(":")
        GridX = GridX + GridPanel.PointToClient(MousePosition).X - thetags(0)
        GridY = GridY + GridPanel.PointToClient(MousePosition).Y - thetags(1)
        Timer3.Tag = GridPanel.PointToClient(MousePosition).X & ":" & GridPanel.PointToClient(MousePosition).Y
        GridPanel.Invalidate()
        FillGrid()
    End Sub

    Dim dontreload As Boolean = False
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If Not Objectcatalogue Is Nothing Then Objectcatalogue.Close()

        If Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(3) Then
            CheckBox1.Checked = False
        End If

        Combobox1.text = If(TabControl1.SelectedIndex = -1, "", My.Settings.Names(TabControl1.SelectedIndex))
        RadioButton13.Checked = True

        Try
            AddHandler currentDomain.UnhandledException, AddressOf MyHandler
            'If Not Mid(Combobox1.text, 1, 10) = "POTI/Route" Then
            If dontreload = False Then
                If Not DataGridView1.ColumnCount = 0 Then
                    Try
                        ReadTable()
                        Dontsave = False
                    Catch ex As Exception
                        If Dontsave = True Then : MsgBox("Invalid data in datagridview") : Combobox1.text = group : dontreload = True : Exit Sub : Else : MsgBox("Error while saving table") : End If
                    End Try
                End If
                If Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(3) Then
                    Sectionlist.Visible = True
                    RadioButton14.Enabled = True
                    For i = 0 To Sectionlist.Items.Count - 1
                        Sectionlist.SetSelected(i, True)
                    Next
                    CheckBox1.Visible = True
                Else
                    RadioButton14.Enabled = False
                    Sectionlist.Visible = False
                    CheckBox1.Visible = False
                End If
                Sectionlist.Items.Clear()
                group = Combobox1.text
                selection = 0
                FillTable()

                Sectionlist.Visible = (Not Combobox1.text = My.Settings.Names(0))

                For i = 0 To Sectionlist.Items.Count - 1
                    Sectionlist.SetSelected(i, True)
                Next
                GridPanel.Invalidate()
            End If

            If Errorlist Is Nothing Then Exit Sub
            Try
                For Each failure As Errors In Errorlist
                    If failure.SectionLoc = TabControl1.SelectedIndex Then
                        DataGridView1.Rows(failure.IDLoc).Cells(failure.CellLoc).Style.BackColor = Color.Red
                    End If
                Next
            Catch ex As Exception : End Try
            '   End If
        Catch ex As Exception : End Try
    End Sub

    Private Sub MangePlugins(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PluginMange.ShowDialog()
    End Sub

    Public Sub LoadKMF(ByVal Filename As String)
        Dim FS As New IO.FileStream(Filename, IO.FileMode.Open)
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
            Dim AmPoint As Short = Readkmp(buffer2, Readkmp(buffer2, &H10 + i * 4, 4) + 6, 2)

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

            Group = Readkmp(buffer2, Readkmp(buffer2, &H10 + i * 4, 4), Pointlength * AmPoint + 8)
            Groups(i) = Group
        Next

        For Each Element In Groups

            Dim Section As Byte() = Element
            Dim Name As String
            Dim AtPoint As Byte
            Dim AmPoint As Byte

            Dim j As Integer = Readkmp(Section, 0, 4)
            Name = Hextostr(j.ToString("X8"))
            AtPoint = Readkmp(Section, 4, 2)
            AmPoint = Readkmp(Section, 6, 2)
            Dim Points(0 To AmPoint - 1) As Byte

            Points = Readkmp(Section, 8, Section.Length - 8)

            If Name = "KTPT" Then : Writekmp(KTPT, 8 + AtPoint * &H1C, Points) : End If
            If Name = "ENPT" Then : Writekmp(ENPT, 8 + AtPoint * &H14, Points) : End If
            If Name = "ITPT" Then : Writekmp(ITPT, 8 + AtPoint * &H14, Points) : End If
            If Name = "CKPT" Then : Writekmp(CKPT, 8 + AtPoint * &H14, Points) : End If
            If Name = "ENPH" Then : Writekmp(ENPH, 8 + AtPoint * &H10, Points) : End If
            If Name = "ITPH" Then : Writekmp(ITPH, 8 + AtPoint * &H10, Points) : End If
            If Name = "CKPH" Then : Writekmp(CKPH, 8 + AtPoint * &H10, Points) : End If
            If Name = "GOBJ" Then : Writekmp(GOBJ, 8 + AtPoint * &H3C, Points) : End If
            If Name = "POTI" Then : Writekmp(POTI, 8 + AtPoint * &H10, Points) : End If
            If Name = "AREA" Then : Writekmp(AREA, 8 + AtPoint * &H30, Points) : End If
            If Name = "CAME" Then : Writekmp(CAME, 8 + AtPoint * &H48, Points) : End If
            If Name = "JGPT" Then : Writekmp(JGPT, 8 + AtPoint * &H1C, Points) : End If
            If Name = "CNPT" Then : Writekmp(CNPT, 8 + AtPoint * &H1C, Points) : End If
            If Name = "MSPT" Then : Writekmp(GOBJ, 8 + AtPoint * &H1C, Points) : End If
            If Name = "STGI" Then : Writekmp(STGI, 8 + AtPoint * &HC, Points) : End If
        Next
        FillTable()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If Combobox1.text = My.Settings.Names(5) And e.ColumnIndex = 4 And Not e.RowIndex = DataGridView1.Rows.Count - 1 Then
            routeshow = True
            selection = 0
            ReadTable()
            Combobox1.text = "POTI/Route" & e.RowIndex
            group = Combobox1.text
            DataGridView1.Columns.Clear()
            c.Shapes.Clear()
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Route("ID"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Route("XPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Route("YPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Route("ZPosition"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Route("Sets"))
            DataGridView1.Columns.Add(Datagridviewfill.Fill.Route("Sets"))

            If Parsedroutes(e.RowIndex).Count = 0 And Not Parsedroutes(e.RowIndex).points.Length = 0 Then
                Parsedroutes(e.RowIndex).Count = 1
            End If
            Dim temp(0 To Parsedroutes(e.RowIndex).Count - 1) As PowerPacks.OvalShape
            oval = temp

            For Each Point In Parsedroutes(e.RowIndex).points
                DataGridView1.Rows.Add(DataGridView1.RowCount - 1, Point.Location(0), Point.Location(1), Point.Location(2), Point.Pointsettings.ToString("X4"), Point.Additional.ToString("X4"))
                NewOval(DataGridView1.RowCount - 2, (GridPanel.Size.Width / 2) + (Math.Round(Point.Location(0), 0) / Gridscale), (GridPanel.Size.Width / 2) + (Math.Round(Point.Location(2), 0) / Gridscale))
            Next
            routeshow = False
        End If
    End Sub

    Dim Doremove As Boolean = True
    Private Sub DataGridView1_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Filesavedcheck = True
        If Sectionlist.Items.Count = 0 Then GoTo nop
        If Mid(Combobox1.text, 3) = "PH" Then GoTo nop
        If Not DataGridView1.Rows(e.RowIndex).Cells(1).Value Is Nothing Then
            If Not DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString = "" Then
                GoTo nop
            End If
        End If
        If Sectionlist.SelectedItems.Count = 1 And Not Sectionlist.Items.Count = 1 And (Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(3)) Then
            Dim Moverow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim Visicount As Integer
            Dim moverowid As Integer = e.RowIndex - 1
            Dim lastvisirow As Integer = -1

            Dim chanelast As Boolean = True

            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Visible = True Then
                    Visicount += 1
                    If chanelast = True Then lastvisirow = row.Index
                ElseIf Not lastvisirow = -1 Then
                    chanelast = False
                End If
                If moverowid = e.RowIndex - 1 And row.Visible = True And Not row.Index = e.RowIndex Then
                    moverowid += 1
                End If
            Next
            If lastvisirow = -1 Then lastvisirow = DataGridView1.Rows.Count - 1

            Dim method As New Moverows(AddressOf Startmove)
            DataGridView1.BeginInvoke(method, moverowid, lastvisirow, Moverow)

            Dim GroupID As Integer = Mid(Sectionlist.SelectedItem, 9)

            If Combobox1.text = My.Settings.Names(1) Then
                SectionarrayENPT(GroupID).Amount += 1
                ENPH(GroupID * &H10 + 1) += 1
                For id = GroupID + 1 To SectionarrayENPT.Length - 1
                    SectionarrayENPT(id).Frompoint += 1
                    ENPH(GroupID * &H10) += 1
                Next
            End If
            If Combobox1.text = My.Settings.Names(2) Then
                SectionarrayITPT(GroupID).Amount += 1
                ITPH(GroupID * &H10 + 1) += 1
                For id = GroupID + 1 To SectionarrayITPT.Length - 1
                    SectionarrayITPT(id).Frompoint += 1
                    ITPH(GroupID * &H10) += 1
                Next
            End If
            If Combobox1.text = My.Settings.Names(3) Then
                SectionarrayCKPT(GroupID).Amount += 1
                CKPH(GroupID * &H10 + 1) += 1
                For id = GroupID + 1 To SectionarrayCKPT.Length - 1
                    SectionarrayCKPT(id).Frompoint += 1
                    CKPH(GroupID * &H10) += 1
                Next
            End If
        End If

nop:    If Combobox1.text = My.Settings.Names(5) Then
            Dim newrouteparse(0 To DataGridView1.RowCount - 2) As Route
            For i = 0 To DataGridView1.RowCount - 2
                Dim a As String = DataGridView1.Rows(i).Cells(0).Value
                If a = String.Empty Then
                    Dim temppoints(0 To 0) As Route.Point
                    temppoints(0) = New Route.Point
                    Dim loc As Integer() = {0, 0, 0}
                    temppoints(0).Location(0) = New Integer = 0
                    temppoints(0).Location(1) = New Integer = 0
                    temppoints(0).Location(2) = New Integer = 0

                    temppoints(0).Additional = 0
                    temppoints(0).Pointsettings = 0
                    newrouteparse(i) = New Route
                    newrouteparse(i).points = temppoints
                Else
                    newrouteparse(i) = Parsedroutes(DataGridView1.Rows(i).Cells(0).Value)
                End If
            Next
            Parsedroutes = newrouteparse
        End If

        'Checks if a new point has no section and then adds it to last section
        If (Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(3)) Then
            If DataGridView1.Rows(e.RowIndex).Cells(1).Value Is Nothing Then GoTo crash
            If DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString = "" Then
                If Combobox1.text = My.Settings.Names(1) Then
                    SectionarrayENPT(SectionarrayENPT.Length - 1).Amount += 1
                    ENPH(SectionarrayENPT.Length - 1 * &H10 + 1) += 1
                End If
                If Combobox1.text = My.Settings.Names(2) Then
                    SectionarrayITPT(SectionarrayITPT.Length - 1).Amount += 1
                    ITPH(SectionarrayITPT.Length - 1 * &H10 + 1) += 1
                End If
                If Combobox1.text = My.Settings.Names(3) Then
                    SectionarrayCKPT(SectionarrayCKPT.Length - 1).Amount += 1
                    CKPH(SectionarrayCKPT.Length - 1 * &H10 + 1) += 1
                End If
            End If
        End If
crash:  FillGrid()
    End Sub
    Delegate Sub Moverows(moverowid As Integer, lastvisirow As Integer, moverow As DataGridViewRow)
    Sub Startmove(moverowid As Integer, lastvisirow As Integer, moverow As DataGridViewRow)
        Doremove = False
        DataGridView1.Rows.RemoveAt(moverowid)
        Doremove = True
        DataGridView1.Rows.Insert(lastvisirow + 1, Moverow)
    End Sub

    Private Sub DataGridView1_RowsRemoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        If Doremove = False Then Exit Sub
        If e.RowIndex >= DataGridView1.RowCount Then Exit Sub
        Try
            Filesavedcheck = True
            If Sectionlist.Items.Count = 0 Then Exit Sub
            If Sectionlist.SelectedItems.Count = 1 And CheckBox1.Checked = True Then
                Dim GroupID As Integer = Mid(Sectionlist.SelectedItem, 9)

                If Combobox1.text = My.Settings.Names(1) Then
                    SectionarrayENPT(GroupID).Amount -= 1
                    ENPH(GroupID * &H10 + 1) -= 1
                    For id = GroupID + 1 To SectionarrayENPT.Length - 1
                        SectionarrayENPT(id).Frompoint -= 1
                        ENPH(GroupID * &H10) -= 1
                    Next
                End If
                If Combobox1.text = My.Settings.Names(2) Then
                    SectionarrayITPT(GroupID).Amount -= 1
                    ITPH(GroupID * &H10 + 1) -= 1
                    For id = GroupID + 1 To SectionarrayITPT.Length - 1
                        SectionarrayITPT(id).Frompoint -= 1
                        ITPH(GroupID * &H10) -= 1
                    Next
                End If
                If Combobox1.text = My.Settings.Names(3) Then
                    SectionarrayCKPT(GroupID).Amount -= 1
                    CKPH(GroupID * &H10 + 1) -= 1
                    For id = GroupID + 1 To SectionarrayCKPT.Length - 1
                        SectionarrayCKPT(id).Frompoint -= 1
                        CKPH(GroupID * &H10) -= 1
                    Next
                End If
            End If
            Sectionlist_MouseUp(DataGridView1, New EventArgs)
        Catch ex As Exception : End Try
    End Sub

    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError
        e.Cancel = True
    End Sub



    '------------------------------------------------------------------------------------------------------------------
    '------------------------------------------Reading and fillling----------------------------------------------------
    '------------------------------------------------------------------------------------------------------------------
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

    Public Sub fill_arrays(ByVal buffer As Byte())
        Dim Sectionlength As Integer = &H4C
        Dim Position As Integer = 0

        'Header
        Header = Readkmp(buffer, Position, Sectionlength)

        'KTPT
        Sectionlength = Readkmp(buffer, &H14, 4) - Readkmp(buffer, &H10, 4)
        Position = Position + Header.Length
        KTPT = Readkmp(buffer, Position, Sectionlength)

        'ENPT
        Sectionlength = Readkmp(buffer, &H18, 4) - Readkmp(buffer, &H14, 4)
        Position = Position + KTPT.Length
        ENPT = Readkmp(buffer, Position, Sectionlength)

        'ENPH
        Sectionlength = Readkmp(buffer, &H1C, 4) - Readkmp(buffer, &H18, 4)
        Position = Position + ENPT.Length
        ENPH = Readkmp(buffer, Position, Sectionlength)

        'ITPT
        Sectionlength = Readkmp(buffer, &H20, 4) - Readkmp(buffer, &H1C, 4)
        Position = Position + ENPH.Length
        ITPT = Readkmp(buffer, Position, Sectionlength)

        'ITPH
        Sectionlength = Readkmp(buffer, &H24, 4) - Readkmp(buffer, &H20, 4)
        Position = Position + ITPT.Length
        ITPH = Readkmp(buffer, Position, Sectionlength)

        'CKPT
        Sectionlength = Readkmp(buffer, &H28, 4) - Readkmp(buffer, &H24, 4)
        Position = Position + ITPH.Length
        CKPT = Readkmp(buffer, Position, Sectionlength)

        'CKPH
        Sectionlength = Readkmp(buffer, &H2C, 4) - Readkmp(buffer, &H28, 4)
        Position = Position + CKPT.Length
        CKPH = Readkmp(buffer, Position, Sectionlength)

        'GOBJ
        Sectionlength = Readkmp(buffer, &H30, 4) - Readkmp(buffer, &H2C, 4)
        Position = Position + CKPH.Length
        GOBJ = Readkmp(buffer, Position, Sectionlength)

        'POTI
        Sectionlength = Readkmp(buffer, &H34, 4) - Readkmp(buffer, &H30, 4)
        Position = Position + GOBJ.Length
        POTI = Readkmp(buffer, Position, Sectionlength)

        'AREA
        Sectionlength = Readkmp(buffer, &H38, 4) - Readkmp(buffer, &H34, 4)
        Position = Position + POTI.Length
        AREA = Readkmp(buffer, Position, Sectionlength)

        'CAME
        Sectionlength = Readkmp(buffer, &H3C, 4) - Readkmp(buffer, &H38, 4)
        Position = Position + AREA.Length
        CAME = Readkmp(buffer, Position, Sectionlength)

        'JGPT
        Sectionlength = Readkmp(buffer, &H40, 4) - Readkmp(buffer, &H3C, 4)
        Position = Position + CAME.Length
        JGPT = Readkmp(buffer, Position, Sectionlength)

        'CNPT
        Sectionlength = Readkmp(buffer, &H44, 4) - Readkmp(buffer, &H40, 4)
        Position = Position + JGPT.Length
        CNPT = Readkmp(buffer, Position, Sectionlength)

        'MSPT
        Sectionlength = Readkmp(buffer, &H48, 4) - Readkmp(buffer, &H44, 4)
        Position = Position + CNPT.Length
        MSPT = Readkmp(buffer, Position, Sectionlength)

        'STGI
        Sectionlength = buffer.Length - &H4C - Readkmp(buffer, &H48, 4)
        Position = Position + MSPT.Length
        STGI = Readkmp(buffer, Position, Sectionlength)

        FillSection()
        IgnoreSelect = True
        DataGridView1.Columns.Clear()
        IgnoreSelect = False
        TabControl1.Enabled = True
        TabControl1_SelectedIndexChanged(TabControl1, New EventArgs)

        CAMESetting1.Value = CAME(6)

        Dim temp(0 To Readkmp(POTI, 4, 2) - 1) As Route
        Parsedroutes = temp
        Dim ip As Integer = 0
        Dim i As Integer
        Do While i < Readkmp(POTI, 4, 2)
            Dim temp1 As Byte = Readkmp(POTI, 10 + ip, 1)
            Dim temp2 As Byte = Readkmp(POTI, 11 + ip, 1)
            Dim Amount As Short = Readkmp(POTI, 8 + ip, 2)
            Dim theroute As New Route
            theroute.Count = Amount
            theroute.Setting1 = temp1
            theroute.Setting2 = temp2
            Dim points(0 To Amount - 1) As Route.Point
            For j = 0 To Amount - 1
                points(j) = New Route.Point
                points(j).Location = {Hextofloat(Readkmp(POTI, 12 + ip + &H10 * j, 4)), Hextofloat(Readkmp(POTI, 16 + ip + &H10 * j, 4)), Hextofloat(Readkmp(POTI, 20 + ip + &H10 * j, 4))}
                points(j).Pointsettings = Readkmp(POTI, 24 + ip + &H10 * j, 2)
                points(j).Additional = Readkmp(POTI, 26 + ip + &H10 * j, 2)
            Next
            theroute.points = points
            Parsedroutes(i) = theroute
            ip = ip + Amount * &H10 + 4
            i = i + 1
        Loop
        Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath)

        If Not thrd2.ThreadState = Threading.ThreadState.Running And Not thrd2.ThreadState = Threading.ThreadState.Stopped Then
            thrd2.Start()
        End If
    End Sub
    Dim thrd2 As New Threading.Thread(AddressOf CheckKMP)

    Sub CheckKMP()
        RouteUsage = CheckRoutes()
        Errorlist = ErrorCheck()
    End Sub

    Private Sub Open_KMP(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        ReadTable()
        If Filesavedcheck = True Then
            Dim a As MsgBoxResult = MsgBox("You have made unsaved changes, do you wish to save?", vbYesNo)
            If a = MsgBoxResult.Yes Then
                SaveToolStripMenuItem_Click(SaveToolStripMenuItem, New System.EventArgs)
            End If
        End If
        Filesavedcheck = False
        Combobox1.text = ""

        AddHandler currentDomain.UnhandledException, AddressOf MyHandler
        Dim dlg As MsgBoxResult = LoadKMPdialog.ShowDialog
        If dlg = MsgBoxResult.Ok Then
            Try
retry:
                Dim FS As New IO.FileStream(LoadKMPdialog.FileName, IO.FileMode.Open)
                File = LoadKMPdialog.FileName
                Dim BR As New IO.BinaryReader(FS)
                BR.BaseStream.Position = 0
                Dim buffer As Byte() = BR.ReadBytes(FS.Length)
                Dim buffer2 As Byte() = buffer
                buffer2.Reverse()
                Dim list As ArrayList = New ArrayList(buffer2)
                'My.Settings.Filebackup = list
                FS.Close()
                BR.Close()
                If Not Readkmp(buffer, 0, 4) = &H524B4D44 Then
                    MsgBox("This is not a valid KMP or SZS file", MsgBoxStyle.Critical, "KMP Modifier")
                    Dim s As Integer = (Readkmp(buffer, 0, 4))
                Else
                    Combobox1.text = My.Settings.Names(0)
                    fill_arrays(buffer)
                    FillTable()
                    Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath)
                    'Maintabs.Enabled = True
                    'PointTab.Show()
                    'PointTab.Select()
                End If
            Catch iox As IO.IOException
                Dim box As MsgBoxResult = MsgBox(iox.Message & Chr(13) & Chr(13) & iox.StackTrace, MsgBoxStyle.RetryCancel)
                If box = MsgBoxResult.Retry Then
                    GoTo retry
                ElseIf box = MsgBoxResult.Cancel Then
                    Exit Try
                End If
            Catch aux As ArgumentException
                MsgBox("There's a problem with this KMP file, please use an errorfinder for details")
            End Try
        End If
    End Sub

    Private Sub NewKMP(sender As System.Object, e As System.EventArgs) Handles NewToolStripMenuItem.Click
        fill_arrays(My.Resources.Empty)
        'Maintabs.Enabled = True
    End Sub


    '------------------------------------------------------------------------------------------------------------------
    '---------------------------------------------Layout events--------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------------


    Private Sub Redo_Grid_AfterEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        If DataGridView1.Tag = "Section" Then
            'NumericUpDown1.Minimum = 0
            'NumericUpDown1.Maximum = DataGridView1.RowCount - 2
        End If
        FillGrid()
    End Sub

    Private Sub Redoval_Mousedown()
        Timer4.Start()
    End Sub
    Private Sub Yellowoval_Mousedown()
        Timer5.Start()
    End Sub


    Private Sub Timer4_Tick(sender As System.Object, e As System.EventArgs) Handles Timer4.Tick
        Dim senders As PowerPacks.OvalShape = redoval
        If MouseButtons.ToString = "Left" Then
            Dim dowork As Boolean = True
            Cursor = Cursors.SizeAll
            senders.Location = GridPanel.PointToClient(MousePosition)
            Dim X As Single = (GridPanel.PointToClient(MousePosition).X - GridX - (GridPanel.Size.Width / 2)) * Gridscale
            Dim Y As Single = (GridPanel.PointToClient(MousePosition).Y - GridY - (GridPanel.Size.Height / 2)) * Gridscale
            DataGridView1.CurrentRow.Cells(17).Value = X
            DataGridView1.CurrentRow.Cells(19).Value = Y
        Else
            Timer4.Stop()
        End If
    End Sub

    Private Sub Timer5_Tick(sender As System.Object, e As System.EventArgs) Handles Timer5.Tick
        Dim senders As PowerPacks.OvalShape = yellowoval
        If MouseButtons.ToString = "Left" Then
            Dim dowork As Boolean = True
            Cursor = Cursors.SizeAll
            senders.Location = GridPanel.PointToClient(MousePosition)
            Dim X As Single = (GridPanel.PointToClient(MousePosition).X - GridX - (GridPanel.Size.Width / 2)) * Gridscale
            Dim Y As Single = (GridPanel.PointToClient(MousePosition).Y - GridY - (GridPanel.Size.Height / 2)) * Gridscale
            DataGridView1.CurrentRow.Cells(20).Value = X
            DataGridView1.CurrentRow.Cells(22).Value = Y
        Else
            Timer5.Stop()
        End If
    End Sub

    Public sc As PowerPacks.ShapeContainer = New PowerPacks.ShapeContainer
    Dim redoval As New PowerPacks.OvalShape
    Dim yellowoval As New PowerPacks.OvalShape
    Private Sub Select_Oval(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.CurrentRow Is Nothing Then Exit Sub
        Dim invisicount As New Integer
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Visible = True Then
                invisicount = row.Index
                Exit For
            End If
        Next

        If Combobox1.text = My.Settings.Names(0) And My.Computer.Keyboard.CtrlKeyDown Then
            GridPanel.Invalidate()
        End If
        If Combobox1.text = My.Settings.Names(4) And My.Computer.Keyboard.CtrlKeyDown Then
            GridPanel.Invalidate()
        End If
        If Combobox1.text = My.Settings.Names(10) And My.Computer.Keyboard.CtrlKeyDown Then
            GridPanel.Invalidate()
        End If
        If Combobox1.text = My.Settings.Names(7) Then
            sc.Shapes.Clear()
        End If
        If Combobox1.text = My.Settings.Names(6) Then
            GridPanel.Invalidate()
        End If
        If Combobox1.text = My.Settings.Names(7) And IgnoreSelect = False Then
            If XMLWrite(DataGridView1.CurrentRow.Cells(1).Value, "XML/CAME/Variants", "Tablefill.xml")(0) = 5 Then
                redoval.Location = New Drawing.Point(GridX + (GridPanel.Size.Width / 2) + (DataGridView1.CurrentRow.Cells(17).Value / Gridscale), GridY + (GridPanel.Size.Height / 2) + (DataGridView1.CurrentRow.Cells(19).Value / Gridscale))
                redoval.Cursor = Cursors.SizeAll
                redoval.Size = New Drawing.Size(5, 5)
                redoval.FillColor = Color.Yellow
                redoval.FillStyle = PowerPacks.FillStyle.Solid
                sc.Shapes.Add(redoval)
                AddHandler redoval.MouseDown, AddressOf Redoval_Mousedown
                yellowoval.Location = New Drawing.Point(GridX + (GridPanel.Size.Width / 2) + (DataGridView1.CurrentRow.Cells(20).Value / Gridscale), GridY + (GridPanel.Size.Height / 2) + (DataGridView1.CurrentRow.Cells(22).Value / Gridscale))
                yellowoval.Cursor = Cursors.SizeAll
                yellowoval.Width = 5
                yellowoval.Height = 5
                yellowoval.FillColor = Color.SkyBlue
                yellowoval.FillStyle = PowerPacks.FillStyle.Solid
                sc.Shapes.Add(yellowoval)
                AddHandler yellowoval.MouseDown, AddressOf Yellowoval_Mousedown
            End If
        End If

        If Not selection - If(selection = 0, 0, invisicount) >= oval.Length Then
            If Not oval(selection - If(selection = 0, 0, invisicount)) Is Nothing Then
                oval(selection - If(selection = 0, 0, invisicount)).FillColor = Color.Blue
            End If
        End If

        If Not selection - If(selection = 0, 0, invisicount) >= oval2.Length Then
            If Not oval2(selection - If(selection = 0, 0, invisicount)) Is Nothing Then
                oval2(selection - If(selection = 0, 0, invisicount)).FillColor = Color.Green
            End If
        End If

        DataGridView1.Focus()
        If DataGridView1.Tag = "Points" And Not Combobox1.text = My.Settings.Names(5) Then

            If Not DataGridView1.CurrentRow Is Nothing And Not DataGridView1.CurrentRow.Index = DataGridView1.RowCount - 1 And Not routeshow = True Then
                selection = DataGridView1.CurrentRow.Index
            End If
            If Not DataGridView1.RowCount = 0 And Not DataGridView1.CurrentRow.Index = DataGridView1.RowCount - 1 Then

                Dim i As Integer = DataGridView1.CurrentRow.Index - invisicount
                If i > oval.Length - 1 Then i = 0
                If Not oval(i) Is Nothing Then
                    oval(i).FillColor = My.Settings.Selection
                    If Not oval2.Length = 0 Then : If Not oval2.Length <= i Then : If Not oval2(i) Is Nothing Then : oval2(i).FillColor = My.Settings.Selection : End If : End If : End If
                End If
            End If
        End If
    End Sub


    Private Sub MoveOvals_ToMouse(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim GetY As Boolean = False
        If My.Computer.Keyboard.CtrlKeyDown = False Then
            If MouseButtons.ToString = "Left" Then
                Dim dowork As Boolean = True
                Cursor = Cursors.SizeAll
                oval(Timer1.Tag).Location = GridPanel.PointToClient(MousePosition)
                Dim X As Single = (GridPanel.PointToClient(MousePosition).X - GridX - (GridPanel.Size.Width / 2)) * Gridscale
                Dim Y As Single = (GridPanel.PointToClient(MousePosition).Y - GridY - (GridPanel.Size.Height / 2)) * Gridscale
                If Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(4) Then
                    DataGridView1.Rows(Timer1.Tag).Cells(2).Value = X
                    DataGridView1.Rows(Timer1.Tag).Cells(4).Value = Y
                ElseIf Combobox1.text = My.Settings.Names(6) Then
                    DataGridView1.Rows(Timer1.Tag).Cells(4).Value = X
                    DataGridView1.Rows(Timer1.Tag).Cells(6).Value = Y
                ElseIf Combobox1.text = My.Settings.Names(7) Then
                    DataGridView1.Rows(Timer1.Tag).Cells(9).Value = X
                    DataGridView1.Rows(Timer1.Tag).Cells(11).Value = Y
                ElseIf Combobox1.text = My.Settings.Names(3) Then
                    If DataGridView1.Rows(Timer1.Tag).Cells(2).Value = X And DataGridView1.Rows(Timer1.Tag).Cells(3).Value = Y Then
                        dowork = False
                    End If
                    DataGridView1.Rows(Timer1.Tag).Cells(2).Value = X
                    DataGridView1.Rows(Timer1.Tag).Cells(3).Value = Y
                ElseIf Not Mid(Combobox1.text, 3) = "PH" Then
                    DataGridView1.Rows(Timer1.Tag).Cells(1).Value = X
                    DataGridView1.Rows(Timer1.Tag).Cells(3).Value = Y
                End If

                Dim id As Integer = Timer1.Tag
                Try
                    If Not PathBG = "" Then
                        If Mid(PathBG, PathBG.Length - 2) = "OBJ" Or Mid(PathBG, PathBG.Length - 2) = "obj" Then
                            If Not Combobox1.text = My.Settings.Names(3) Then GetY = True
                            If yValues.Length = 1 Then
                                Dim Vertices0 As New ArrayList
                                Dim Faces0 As New ArrayList
                                Dim yValues0 As New ArrayList

                                Dim lines As String()

                                Dim line As New ArrayList
                                Dim sr As New IO.StreamReader(PathBG)
                                Do While sr.Peek() >= 0
                                    line.Add(sr.ReadLine)
                                Loop
                                lines = CType(line.ToArray(GetType(String)), String())


                                'Dialog1.ShowDialog()


                                For i = 0 To lines.Count - 1
                                    If Mid(lines(i), 1, 2) = "v " Then
                                        lines(i) = lines(i).Replace(".", ",")
                                        lines(i) = lines(i).Replace("  ", " ")
                                        Dim a As String() = System.Text.RegularExpressions.Regex.Split(lines(i), " ")
                                        Vertices0.Add(New Point(a(1), a(3)))
                                        Dim y2 As Single = a(2)
                                        yValues0.Add(y2)
                                    ElseIf Mid(lines(i), 1, 2) = "f " Then
                                        Dim a As String() = System.Text.RegularExpressions.Regex.Split(lines(i), " ")
                                        Dim b(0 To 2) As Integer
                                        b(0) = System.Text.RegularExpressions.Regex.Split(a(1), "/")(0)
                                        b(1) = System.Text.RegularExpressions.Regex.Split(a(2), "/")(0)
                                        b(2) = System.Text.RegularExpressions.Regex.Split(a(3), "/")(0)
                                        Faces0.Add(b)
                                    End If
                                Next
                                Vertices = DirectCast(Vertices0.ToArray(GetType(Point)), Point())
                                Faces = DirectCast(Faces0.ToArray(GetType(Integer())), Object())
                                yValues = DirectCast(yValues0.ToArray(GetType(Single)), Single())
                            End If
                        End If
                    End If
                Catch ex As Exception : GetY = False : End Try

                Dim Z As New Single
                If GetY = True Then
                    For Each face In Faces
                        If CoordinateTriangleCheck({Vertices(face(0) - 1), Vertices(face(1) - 1), Vertices(face(2) - 1)}, _
                                                   {yValues(face(0) - 1), yValues(face(1) - 1), yValues(face(2) - 1)}, New Point(X, Y)) = True Then
                            Dim ztemp As New Single
                            ztemp = CoordinateTriangleY({Vertices(face(0) - 1), Vertices(face(1) - 1), Vertices(face(2) - 1)}, _
                                                    {yValues(face(0) - 1), yValues(face(1) - 1), yValues(face(2) - 1)}, New Point(X, Y))
                            If Math.Abs(ztemp) > Z Then Z = Math.Abs(ztemp)
                        End If
                    Next
                    If Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(4) Then
                        If GetY = True Then DataGridView1.Rows(id).Cells(3).Value = Z
                    ElseIf Combobox1.text = My.Settings.Names(6) Then
                        If GetY = True Then DataGridView1.Rows(id).Cells(5).Value = Z
                    ElseIf Combobox1.text = My.Settings.Names(7) Then
                        If GetY = True Then DataGridView1.Rows(id).Cells(10).Value = Z
                    ElseIf Not Combobox1.text = My.Settings.Names(5) Then
                        If GetY = True Then DataGridView1.Rows(id).Cells(2).Value = Z
                    End If
                End If

                If Combobox1.text = My.Settings.Names(3) And dowork = True Then
                    line(Timer1.Tag).X1 = oval(Timer1.Tag).Location.X
                    line(Timer1.Tag).Y1 = oval(Timer1.Tag).Location.Y
                    line(Timer1.Tag).BorderColor = Color.Blue
                    GridPanel.Invalidate()
                End If
                If Combobox1.text = My.Settings.Names(0) And My.Computer.Keyboard.CtrlKeyDown Then
                    FillGrid()
                End If
                If Combobox1.text = My.Settings.Names(4) And My.Computer.Keyboard.CtrlKeyDown Then
                    FillGrid()
                End If
                If Combobox1.text = My.Settings.Names(10) And My.Computer.Keyboard.CtrlKeyDown Then
                    FillGrid()
                End If
                If Combobox1.text = My.Settings.Names(6) Or Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(3) Then
                    FillGrid()
                End If
            Else
                Timer1.Stop()
                c.Refresh()
                d.Refresh()
                Cursor = Cursors.Default
            End If
        ElseIf MouseButtons.ToString = "Left" Then
            Dim a As PowerPacks.OvalShape = oval(Timer1.Tag)
            Dim cellno As Integer
            If Combobox1.text = My.Settings.Names(4) Then cellno = 6 Else cellno = 5
            oval(Timer1.Tag).Cursor = Cursors.Hand
            Dim Xdeg, Ydeg As New Integer
            Dim loc As Point = a.Location
            Xdeg = GridPanel.PointToClient(MousePosition).X - loc.X
            Ydeg = GridPanel.PointToClient(MousePosition).Y - loc.Y
            Dim angle As Short
            If Xdeg = 0 Then
                If Ydeg <= 0 Then angle = 180
                If Ydeg > 0 Then angle = 0
            End If
            If Ydeg = 0 Then
                If Xdeg <= 0 Then angle = -90
                If Xdeg > 0 Then angle = 90
            End If

            If Ydeg < 0 And Xdeg > 0 Then angle = 180 - Math.Atan(Math.Abs(Xdeg / Ydeg)) * (180 / Math.PI)
            If Ydeg > 0 And Xdeg > 0 Then angle = 90 - Math.Atan(Math.Abs(Ydeg / Xdeg)) * (180 / Math.PI)
            If Ydeg > 0 And Xdeg < 0 Then angle = -Math.Atan(Math.Abs(Xdeg / Ydeg)) * (180 / Math.PI)
            If Ydeg < 0 And Xdeg < 0 Then angle = -90 - Math.Atan(Math.Abs(Ydeg / Xdeg)) * (180 / Math.PI)

            DataGridView1.Rows(a.Tag).Cells(cellno).Value = angle
            GridPanel.Invalidate()
        End If
    End Sub

    Private Sub MoveOvals2_ToMouse(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If MouseButtons.ToString = "Left" Then
            Dim dowork As Boolean = True
            Cursor = Cursors.SizeAll
            oval2(Timer2.Tag).Location = GridPanel.PointToClient(MousePosition)
            Dim X As Single = (GridPanel.PointToClient(MousePosition).X - GridX - (GridPanel.Size.Width / 2)) * Gridscale
            Dim Y As Single = (GridPanel.PointToClient(MousePosition).Y - GridY - (GridPanel.Size.Height / 2)) * Gridscale
            If DataGridView1.Rows(Timer1.Tag).Cells(4).Value = X And DataGridView1.Rows(Timer1.Tag).Cells(5).Value = Y Then
                dowork = False
            End If
            DataGridView1.Rows(Timer2.Tag).Cells(4).Value = X
            DataGridView1.Rows(Timer2.Tag).Cells(5).Value = Y
            If dowork = True Then
                line(Timer2.Tag).X2 = oval2(Timer2.Tag).Location.X
                line(Timer2.Tag).Y2 = oval2(Timer2.Tag).Location.Y
                GridPanel.Invalidate()
            End If
        Else
            Timer2.Stop()
            c.Refresh()
            d.Refresh()
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub TrackBar_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar.Scroll
        If Not TrackBar.Value = (Gridscale - 1) / 10 Then
            Dim prevX As Integer = -GridX * Gridscale
            Dim prevY As Integer = -GridY * Gridscale

            Dim Diff As Integer = Gridscale - (TrackBar.Value * 10 + 1)
            Gridscale = TrackBar.Value * 10 + 1
            GridX = -(prevX / Gridscale)
            GridY = -(prevY / Gridscale)
            scrolling = True
            If Not Combobox1.text = My.Settings.Names(3) Then
                Me.SuspendLayout()
                GridPanel.Invalidate()
                Me.ResumeLayout()
            End If
            FillGrid()
            scrolling = False
        End If
    End Sub

    Private Sub FillGrid(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        FillGrid()
    End Sub

    '------------------------------------------------------------------------------------------------------------------

    Private Sub QuitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub


    '------------------------------------------------------------------------------------------------------------------
    '-------------------------------------------  _   ___  __  ___    __       ----------------------------------------
    '-------------------------------------------/__` |__  /  `  |  | /  \ |\ | ----------------------------------------
    '-------------------------------------------.__/ |___ \__,  |  | \__/ | \| ----------------------------------------
    '------------------------------------------------------------------------------------------------------------------
    Public SectionarrayENPT() As Section.ENPH.Section
    Public SectionarrayITPT() As Section.ITPH.Section
    Public SectionarrayCKPT() As Section.CKPH.Section


    Public Sub FillSection()
        Dim SectionarrayENPTtemp(Readkmp(ENPH, 4, 2) - 1) As Section.ENPH.Section
        Section.ENPH.Count = Readkmp(ENPH, 4, 2)

        Dim i As Integer = 0
        Do While i < (ENPH.Length - 8) / &H10
            Dim Sectionarray As Section.ENPH.Section = New Section.ENPH.Section()
            Sectionarray.Lastsection = {Readkmp(ENPH, 10 + i * 16, 1), Readkmp(ENPH, 11 + i * 16, 1), Readkmp(ENPH, 12 + i * 16, 1), Readkmp(ENPH, 13 + i * 16, 1), Readkmp(ENPH, 14 + i * 16, 1), Readkmp(ENPH, 15 + i * 16, 1)}
            Sectionarray.Nextsection = {Readkmp(ENPH, 16 + i * 16, 1), Readkmp(ENPH, 17 + i * 16, 1), Readkmp(ENPH, 18 + i * 16, 1), Readkmp(ENPH, 19 + i * 16, 1), Readkmp(ENPH, 20 + i * 16, 1), Readkmp(ENPH, 21 + i * 16, 1)}
            Sectionarray.Frompoint = Readkmp(ENPH, 8 + i * 16, 1)
            Sectionarray.Amount = Readkmp(ENPH, 9 + i * 16, 1)

            If Sectionarray.Lastsection(0) = 255 Then : Sectionarray.Lastsectioncount = 0
            ElseIf Sectionarray.Lastsection(1) = 255 Then : Sectionarray.Lastsectioncount = 1
            ElseIf Sectionarray.Lastsection(2) = 255 Then : Sectionarray.Lastsectioncount = 2
            ElseIf Sectionarray.Lastsection(3) = 255 Then : Sectionarray.Lastsectioncount = 3
            ElseIf Sectionarray.Lastsection(4) = 255 Then : Sectionarray.Lastsectioncount = 4
            ElseIf Sectionarray.Lastsection(5) = 255 Then : Sectionarray.Lastsectioncount = 5
            Else : Sectionarray.Lastsectioncount = 6
            End If

            If Sectionarray.Nextsection(0) = 255 Then : Sectionarray.Nextsectioncount = 0
            ElseIf Sectionarray.Nextsection(1) = 255 Then : Sectionarray.Nextsectioncount = 1
            ElseIf Sectionarray.Nextsection(2) = 255 Then : Sectionarray.Nextsectioncount = 2
            ElseIf Sectionarray.Nextsection(3) = 255 Then : Sectionarray.Nextsectioncount = 3
            ElseIf Sectionarray.Nextsection(4) = 255 Then : Sectionarray.Nextsectioncount = 4
            ElseIf Sectionarray.Nextsection(5) = 255 Then : Sectionarray.Nextsectioncount = 5
            Else : Sectionarray.Nextsectioncount = 6
            End If

            SectionarrayENPTtemp(i) = Sectionarray
            i = i + 1
        Loop
        SectionarrayENPT = SectionarrayENPTtemp

        i = 0
        Dim SectionarrayITPTtemp(Readkmp(ITPH, 4, 2) - 1) As Section.ITPH.Section
        Section.ITPH.Count = Readkmp(ITPH, 4, 2)
        Do While i < (ITPH.Length - 8) / &H10
            Dim Sectionarray As Section.ITPH.Section = New Section.ITPH.Section()
            Sectionarray.Lastsection = {Readkmp(ITPH, 10 + i * 16, 1), Readkmp(ITPH, 11 + i * 16, 1), Readkmp(ITPH, 12 + i * 16, 1), Readkmp(ITPH, 13 + i * 16, 1), Readkmp(ITPH, 14 + i * 16, 1), Readkmp(ITPH, 15 + i * 16, 1)}
            Sectionarray.Nextsection = {Readkmp(ITPH, 16 + i * 16, 1), Readkmp(ITPH, 17 + i * 16, 1), Readkmp(ITPH, 18 + i * 16, 1), Readkmp(ITPH, 19 + i * 16, 1), Readkmp(ITPH, 20 + i * 16, 1), Readkmp(ITPH, 21 + i * 16, 1)}
            Sectionarray.Frompoint = Readkmp(ITPH, 8 + i * 16, 1)
            Sectionarray.Amount = Readkmp(ITPH, 9 + i * 16, 1)

            If Sectionarray.Lastsection(0) = 255 Then : Sectionarray.Lastsectioncount = 0
            ElseIf Sectionarray.Lastsection(1) = 255 Then : Sectionarray.Lastsectioncount = 1
            ElseIf Sectionarray.Lastsection(2) = 255 Then : Sectionarray.Lastsectioncount = 2
            ElseIf Sectionarray.Lastsection(3) = 255 Then : Sectionarray.Lastsectioncount = 3
            ElseIf Sectionarray.Lastsection(4) = 255 Then : Sectionarray.Lastsectioncount = 4
            ElseIf Sectionarray.Lastsection(5) = 255 Then : Sectionarray.Lastsectioncount = 5
            Else : Sectionarray.Lastsectioncount = 6
            End If

            If Sectionarray.Nextsection(0) = 255 Then : Sectionarray.Nextsectioncount = 0
            ElseIf Sectionarray.Nextsection(1) = 255 Then : Sectionarray.Nextsectioncount = 1
            ElseIf Sectionarray.Nextsection(2) = 255 Then : Sectionarray.Nextsectioncount = 2
            ElseIf Sectionarray.Nextsection(3) = 255 Then : Sectionarray.Nextsectioncount = 3
            ElseIf Sectionarray.Nextsection(4) = 255 Then : Sectionarray.Nextsectioncount = 4
            ElseIf Sectionarray.Nextsection(5) = 255 Then : Sectionarray.Nextsectioncount = 5
            Else : Sectionarray.Nextsectioncount = 6
            End If

            SectionarrayITPTtemp(i) = Sectionarray
            i = i + 1
        Loop
        SectionarrayITPT = SectionarrayITPTtemp

        i = 0
        Dim SectionarrayCKPTtemp(Readkmp(CKPH, 4, 2) - 1) As Section.CKPH.Section
        Section.CKPH.Count = Readkmp(CKPH, 4, 2)
        Do While i < (CKPH.Length - 8) / &H10
            Dim Sectionarray As Section.CKPH.Section = New Section.CKPH.Section()
            Sectionarray.Lastsection = {Readkmp(CKPH, 10 + i * 16, 1), Readkmp(CKPH, 11 + i * 16, 1), Readkmp(CKPH, 12 + i * 16, 1), Readkmp(CKPH, 13 + i * 16, 1), Readkmp(CKPH, 14 + i * 16, 1), Readkmp(CKPH, 15 + i * 16, 1)}
            Sectionarray.Nextsection = {Readkmp(CKPH, 16 + i * 16, 1), Readkmp(CKPH, 17 + i * 16, 1), Readkmp(CKPH, 18 + i * 16, 1), Readkmp(CKPH, 19 + i * 16, 1), Readkmp(CKPH, 20 + i * 16, 1), Readkmp(CKPH, 21 + i * 16, 1)}
            Sectionarray.Frompoint = Readkmp(CKPH, 8 + i * 16, 1)
            Sectionarray.Amount = Readkmp(CKPH, 9 + i * 16, 1)

            If Sectionarray.Lastsection(0) = 255 Then : Sectionarray.Lastsectioncount = 0
            ElseIf Sectionarray.Lastsection(1) = 255 Then : Sectionarray.Lastsectioncount = 1
            ElseIf Sectionarray.Lastsection(2) = 255 Then : Sectionarray.Lastsectioncount = 2
            ElseIf Sectionarray.Lastsection(3) = 255 Then : Sectionarray.Lastsectioncount = 3
            ElseIf Sectionarray.Lastsection(4) = 255 Then : Sectionarray.Lastsectioncount = 4
            ElseIf Sectionarray.Lastsection(5) = 255 Then : Sectionarray.Lastsectioncount = 5
            Else : Sectionarray.Lastsectioncount = 6
            End If

            If Sectionarray.Nextsection(0) = 255 Then : Sectionarray.Nextsectioncount = 0
            ElseIf Sectionarray.Nextsection(1) = 255 Then : Sectionarray.Nextsectioncount = 1
            ElseIf Sectionarray.Nextsection(2) = 255 Then : Sectionarray.Nextsectioncount = 2
            ElseIf Sectionarray.Nextsection(3) = 255 Then : Sectionarray.Nextsectioncount = 3
            ElseIf Sectionarray.Nextsection(4) = 255 Then : Sectionarray.Nextsectioncount = 4
            ElseIf Sectionarray.Nextsection(5) = 255 Then : Sectionarray.Nextsectioncount = 5
            Else : Sectionarray.Nextsectioncount = 6
            End If

            SectionarrayCKPTtemp(i) = Sectionarray
            i = i + 1
        Loop
        SectionarrayCKPT = SectionarrayCKPTtemp
    End Sub

    'Public Sub FillButtons()
    '    Button1.Visible = False
    '    Button2.Visible = False
    '    Button3.Visible = False
    '    Button5.Visible = False
    '    Button6.Visible = False
    '    Button7.Visible = False
    '    LineShape1.Visible = False
    '    LineShape2.Visible = False
    '    LineShape3.Visible = False
    '    LineShape4.Visible = False
    '    LineShape5.Visible = False
    '    LineShape6.Visible = False
    '    If 'NumericUpDown1.Value = -1 Then
    '        Exit Sub
    '    End If
    '    If ComboBox2.Text = "ENPH" And Not SectionarrayENPT.Length = 0 Then
    '        Label2.Text = "From Sections"
    '        If SectionarrayENPT('NumericUpDown1.Value).Lastsectioncount = 1 Then
    '            Button2.Visible = True
    '            Button2.Text = "Section" & Chr(13) & SectionarrayENPT('NumericUpDown1.Value).Lastsection(0).ToString
    '            LineShape2.Visible = True
    '        ElseIf SectionarrayENPT('NumericUpDown1.Value).Lastsectioncount = 2 Then
    '            Button1.Visible = True
    '            Button3.Visible = True
    '            Button1.Text = "Section" & Chr(13) & SectionarrayENPT('NumericUpDown1.Value).Lastsection(0).ToString
    '            Button3.Text = "Section" & Chr(13) & SectionarrayENPT('NumericUpDown1.Value).Lastsection(1).ToString
    '            LineShape1.Visible = True
    '            LineShape3.Visible = True
    '        ElseIf SectionarrayENPT('NumericUpDown1.Value).Lastsectioncount >= 3 Then
    '            Button1.Visible = True
    '            Button2.Visible = True
    '            Button3.Visible = True
    '            Button1.Text = "Section" & Chr(13) & SectionarrayENPT('NumericUpDown1.Value).Lastsection(0).ToString
    '            Button2.Text = "Section" & Chr(13) & SectionarrayENPT('NumericUpDown1.Value).Lastsection(1).ToString
    '            Button3.Text = "Section" & Chr(13) & SectionarrayENPT('NumericUpDown1.Value).Lastsection(2).ToString
    '            LineShape1.Visible = True
    '            LineShape2.Visible = True
    '            LineShape3.Visible = True
    '        End If
    '    ElseIf ComboBox2.Text = "ITPH" And Not SectionarrayITPT.Length = 0 Then
    '        Label2.Text = "From Sections"
    '        If SectionarrayITPT('NumericUpDown1.Value).Lastsectioncount = 1 Then
    '            Button2.Visible = True
    '            Button2.Text = "Section" & Chr(13) & SectionarrayITPT('NumericUpDown1.Value).Lastsection(0).ToString
    '            LineShape2.Visible = True
    '        ElseIf SectionarrayITPT('NumericUpDown1.Value).Lastsectioncount = 2 Then
    '            Button1.Visible = True
    '            Button3.Visible = True
    '            Button1.Text = "Section" & Chr(13) & SectionarrayITPT('NumericUpDown1.Value).Lastsection(0).ToString
    '            Button3.Text = "Section" & Chr(13) & SectionarrayITPT('NumericUpDown1.Value).Lastsection(1).ToString
    '            LineShape1.Visible = True
    '            LineShape3.Visible = True
    '        ElseIf SectionarrayITPT('NumericUpDown1.Value).Lastsectioncount >= 3 Then
    '            Button1.Visible = True
    '            Button2.Visible = True
    '            Button3.Visible = True
    '            Button1.Text = "Section" & Chr(13) & SectionarrayITPT('NumericUpDown1.Value).Lastsection(0).ToString
    '            Button2.Text = "Section" & Chr(13) & SectionarrayITPT('NumericUpDown1.Value).Lastsection(1).ToString
    '            Button3.Text = "Section" & Chr(13) & SectionarrayITPT('NumericUpDown1.Value).Lastsection(2).ToString
    '            LineShape1.Visible = True
    '            LineShape2.Visible = True
    '            LineShape3.Visible = True
    '        End If
    '    ElseIf ComboBox2.Text = "CKPH" And Not SectionarrayCKPT.Length = 0 Then
    '        Label2.Text = "From Sections"
    '        If SectionarrayCKPT('NumericUpDown1.Value).Lastsectioncount = 1 Then
    '            Button2.Visible = True
    '            Button2.Text = "Section" & Chr(13) & SectionarrayCKPT('NumericUpDown1.Value).Lastsection(0).ToString
    '            LineShape2.Visible = True
    '        ElseIf SectionarrayCKPT('NumericUpDown1.Value).Lastsectioncount = 2 Then
    '            Button1.Visible = True
    '            Button3.Visible = True
    '            Button1.Text = "Section" & Chr(13) & SectionarrayCKPT('NumericUpDown1.Value).Lastsection(0).ToString
    '            Button3.Text = "Section" & Chr(13) & SectionarrayCKPT('NumericUpDown1.Value).Lastsection(1).ToString
    '            LineShape1.Visible = True
    '            LineShape3.Visible = True
    '        ElseIf SectionarrayCKPT('NumericUpDown1.Value).Lastsectioncount >= 3 Then
    '            Button1.Visible = True
    '            Button2.Visible = True
    '            Button3.Visible = True
    '            Button1.Text = "Section" & Chr(13) & SectionarrayCKPT('NumericUpDown1.Value).Lastsection(0).ToString
    '            Button2.Text = "Section" & Chr(13) & SectionarrayCKPT('NumericUpDown1.Value).Lastsection(1).ToString
    '            Button3.Text = "Section" & Chr(13) & SectionarrayCKPT('NumericUpDown1.Value).Lastsection(2).ToString
    '            LineShape1.Visible = True
    '            LineShape2.Visible = True
    '            LineShape3.Visible = True
    '        End If
    '    End If


    '    If ComboBox2.Text = "ENPH" Then
    '        Label2.Text = "From Sections"
    '        If SectionarrayENPT('NumericUpDown1.Value).Nextsectioncount = 1 Then
    '            Button6.Visible = True
    '            Button6.Text = "Section" & Chr(13) & SectionarrayENPT('NumericUpDown1.Value).Nextsection(0).ToString
    '            LineShape5.Visible = True
    '        ElseIf SectionarrayENPT('NumericUpDown1.Value).Nextsectioncount = 2 Then
    '            Button7.Visible = True
    '            Button5.Visible = True
    '            Button7.Text = "Section" & Chr(13) & SectionarrayENPT('NumericUpDown1.Value).Nextsection(0).ToString
    '            Button5.Text = "Section" & Chr(13) & SectionarrayENPT('NumericUpDown1.Value).Nextsection(1).ToString
    '            LineShape6.Visible = True
    '            LineShape4.Visible = True
    '        ElseIf SectionarrayENPT('NumericUpDown1.Value).Nextsectioncount >= 3 Then
    '            Button7.Visible = True
    '            Button6.Visible = True
    '            Button5.Visible = True
    '            Button7.Text = "Section" & Chr(13) & SectionarrayENPT('NumericUpDown1.Value).Nextsection(0).ToString
    '            Button6.Text = "Section" & Chr(13) & SectionarrayENPT('NumericUpDown1.Value).Nextsection(1).ToString
    '            Button5.Text = "Section" & Chr(13) & SectionarrayENPT('NumericUpDown1.Value).Nextsection(2).ToString
    '            LineShape6.Visible = True
    '            LineShape5.Visible = True
    '            LineShape4.Visible = True
    '        End If
    '    ElseIf ComboBox2.Text = "ITPH" Then
    '        Label2.Text = "From Sections"
    '        If SectionarrayITPT('NumericUpDown1.Value).Nextsectioncount = 1 Then
    '            Button6.Visible = True
    '            Button6.Text = "Section" & Chr(13) & SectionarrayITPT('NumericUpDown1.Value).Nextsection(0).ToString
    '            LineShape5.Visible = True
    '        ElseIf SectionarrayITPT('NumericUpDown1.Value).Nextsectioncount = 2 Then
    '            Button7.Visible = True
    '            Button5.Visible = True
    '            Button7.Text = "Section" & Chr(13) & SectionarrayITPT('NumericUpDown1.Value).Nextsection(0).ToString
    '            Button5.Text = "Section" & Chr(13) & SectionarrayITPT('NumericUpDown1.Value).Nextsection(1).ToString
    '            LineShape6.Visible = True
    '            LineShape4.Visible = True
    '        ElseIf SectionarrayITPT('NumericUpDown1.Value).Nextsectioncount >= 3 Then
    '            Button7.Visible = True
    '            Button6.Visible = True
    '            Button5.Visible = True
    '            Button7.Text = "Section" & Chr(13) & SectionarrayITPT('NumericUpDown1.Value).Nextsection(0).ToString
    '            Button6.Text = "Section" & Chr(13) & SectionarrayITPT('NumericUpDown1.Value).Nextsection(1).ToString
    '            Button5.Text = "Section" & Chr(13) & SectionarrayITPT('NumericUpDown1.Value).Nextsection(2).ToString
    '            LineShape6.Visible = True
    '            LineShape5.Visible = True
    '            LineShape4.Visible = True
    '        End If
    '    ElseIf ComboBox2.Text = "CKPH" Then
    '        Label2.Text = "From Sections"
    '        If SectionarrayCKPT('NumericUpDown1.Value).Nextsectioncount = 1 Then
    '            Button6.Visible = True
    '            Button6.Text = "Section" & Chr(13) & SectionarrayCKPT('NumericUpDown1.Value).Nextsection(0).ToString
    '            LineShape5.Visible = True
    '        ElseIf SectionarrayCKPT('NumericUpDown1.Value).Nextsectioncount = 2 Then
    '            Button7.Visible = True
    '            Button5.Visible = True
    '            Button7.Text = "Section" & Chr(13) & SectionarrayCKPT('NumericUpDown1.Value).Nextsection(0).ToString
    '            Button5.Text = "Section" & Chr(13) & SectionarrayCKPT('NumericUpDown1.Value).Nextsection(1).ToString
    '            LineShape6.Visible = True
    '            LineShape4.Visible = True
    '        ElseIf SectionarrayCKPT('NumericUpDown1.Value).Nextsectioncount >= 3 Then
    '            Button7.Visible = True
    '            Button6.Visible = True
    '            Button5.Visible = True
    '            Button7.Text = "Section" & Chr(13) & SectionarrayCKPT('NumericUpDown1.Value).Nextsection(0).ToString
    '            Button6.Text = "Section" & Chr(13) & SectionarrayCKPT('NumericUpDown1.Value).Nextsection(1).ToString
    '            Button5.Text = "Section" & Chr(13) & SectionarrayCKPT('NumericUpDown1.Value).Nextsection(2).ToString
    '            LineShape6.Visible = True
    '            LineShape5.Visible = True
    '            LineShape4.Visible = True
    '        End If
    '    End If
    'End Sub


    'Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    AddHandler currentDomain.UnhandledException, AddressOf MyHandler
    '    Button4.Text = "Section" & Chr(13) & 'NumericUpDown1.Value
    '    If 'NumericUpDown1.Value = -1 Then
    '        Button4.Enabled = False
    '    Else
    '        Button4.Enabled = True
    '        'NumericUpDown1.Minimum = 0
    '    End If
    '    If 'NumericUpDown1.Maximum > 0 Then
    '        FillButtons()
    '    End If
    'End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim but As Button = sender
            'NumericUpDown1.Value = Mid(but.Text, 9)
        Catch ex As Exception : MsgBox("Invalid Section ID")
        End Try
    End Sub


    'Plugins
    Public Shared Sub Replace_Group(ByVal Group As String, ByVal Buffer As Byte())
        If Group = My.Settings.Names(0) Then : KTPT = Buffer
        ElseIf Group = My.Settings.Names(1) Then : ENPT = Buffer
        ElseIf Group = "ENPH" Then : ENPH = Buffer
        ElseIf Group = My.Settings.Names(2) Then : ITPT = Buffer
        ElseIf Group = "ITPH" Then : ITPH = Buffer
        ElseIf Group = My.Settings.Names(3) Then : CKPT = Buffer
        ElseIf Group = "CKPH" Then : CKPH = Buffer
        ElseIf Group = My.Settings.Names(4) Then : GOBJ = Buffer
        ElseIf Group = My.Settings.Names(5) Then : POTI = Buffer
        ElseIf Group = My.Settings.Names(6) Then : AREA = Buffer
        ElseIf Group = My.Settings.Names(7) Then : CAME = Buffer
        ElseIf Group = My.Settings.Names(8) Then : JGPT = Buffer
        ElseIf Group = My.Settings.Names(9) Then : CNPT = Buffer
        ElseIf Group = My.Settings.Names(10) Then : MSPT = Buffer
        ElseIf Group = "STGI" Then : STGI = Buffer
        End If
    End Sub


    'All Groups
    Public Shared PointID As Integer

    '    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        PointID = Mid(Button4.Text, 9)
    '        Dim ct As Boolean = False
    '        If File = "CTools" Then
    '            File = "temp.kmp"
    '            SaveAsToolStripMenuItem_Click(Button4, New EventArgs)
    '            ct = True
    '        End If

    '        If (File = "" Or Filesavedcheck = True) And Not File = "CTools" Then
    '            MsgBox("First save the file before loading the plugin") : Exit Sub
    '        End If

    '        If ComboBox2.Text = "ENPH" Then
    '            If Not My.Settings.ENPHPlugin = String.Empty Then
    '                System.Diagnostics.Process.Start(My.Settings.ENPHPlugin, "ENPH" & PointID & " - " & File)
    '            Else
    '                MsgBox("No plugin found for ENPH")
    '            End If
    '        ElseIf ComboBox2.Text = "ITPH" Then
    '            If Not My.Settings.ITPHPlugin = String.Empty Then
    '                System.Diagnostics.Process.Start(My.Settings.ITPHPlugin, "ITPH" & PointID & " - " & File)
    '            Else
    '                MsgBox("No plugin found for ITPH")
    '            End If
    '        ElseIf ComboBox2.Text = "CKPH" Then
    '            If Not My.Settings.CKPHPlugin = String.Empty Then
    '                System.Diagnostics.Process.Start(My.Settings.CKPHPlugin, "CKPH" & PointID & " - " & File)
    '            Else
    '                MsgBox("No plugin found for CKPH")
    '            End If
    '        End If

    '        If ct = True Then
    '            File = "CTools"
    '        End If

    '        If MsgBox("Press ok to load the section to this program, do not press the ok button before saving with the plugin", vbOKCancel) = MsgBoxResult.Ok Then
    '            Try
    'roflfail:
    '                LoadKMF(ComboBox2.Text & ".kmf")
    '                FillSection()
    '                FillButtons()
    '            Catch ex As Exception
    '                If MsgBox("Unable to load the file, be sure the KMF file doesn't reach the total amount of points", MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then : GoTo roflfail : End If : End Try
    '        End If
    '    End Sub

    Private Sub LoadPlugin_Click(sender As System.Object, e As System.EventArgs)
        PointID = DataGridView1.CurrentRow.Index

        If File = "" Or Filesavedcheck = True Then
            MsgBox("First save the file before loading the plugin") : Exit Sub
        End If

        If Combobox1.text = My.Settings.Names(0) Then
            If Not My.Settings.KTPTPlugin = String.Empty Then
                System.Diagnostics.Process.Start(My.Settings.KTPTPlugin, "KTPT" & PointID & " - " & File)
            Else
                MsgBox("No plugin found for KTPT")
            End If
        ElseIf Combobox1.text = My.Settings.Names(1) Then
            If Not My.Settings.ENPTPlugin = String.Empty Then
                System.Diagnostics.Process.Start(My.Settings.ENPTPlugin, "ENPT" & PointID & " - " & File)
            Else
                MsgBox("No plugin found for ENPT")
            End If
        ElseIf Combobox1.text = My.Settings.Names(2) Then
            If Not My.Settings.ITPTPlugin = String.Empty Then
                System.Diagnostics.Process.Start(My.Settings.ITPTPlugin, "ITPT" & PointID & " - " & File)
            Else
                MsgBox("No plugin found for ITPT")
            End If
        ElseIf Combobox1.text = My.Settings.Names(3) Then
            If Not My.Settings.CKPTPlugin = String.Empty Then
                System.Diagnostics.Process.Start(My.Settings.CKPTPlugin, "CKPT" & PointID & " - " & File)
            Else
                MsgBox("No plugin found for CKPT")
            End If
        ElseIf Combobox1.text = My.Settings.Names(4) Then
            If Not My.Settings.GOBJPlugin = String.Empty Then
                System.Diagnostics.Process.Start(My.Settings.GOBJPlugin, "GOBJ" & PointID & " - " & File)
            Else
                MsgBox("No plugin found for GOBJ")
            End If
        ElseIf Combobox1.text = My.Settings.Names(5) Then
            If Not My.Settings.POTIPlugin = String.Empty Then
                System.Diagnostics.Process.Start(My.Settings.POTIPlugin, "POTI" & PointID & " - " & File)
            Else
                MsgBox("No plugin found for POTI")
            End If
        ElseIf Combobox1.text = My.Settings.Names(6) Then
            If Not My.Settings.AREAPlugin = String.Empty Then
                System.Diagnostics.Process.Start(My.Settings.AREAPlugin, "AREA" & PointID & " - " & File)
            Else
                MsgBox("No plugin found for AREA")
            End If
        ElseIf Combobox1.text = My.Settings.Names(7) Then
            If Not My.Settings.CAMEPlugin = String.Empty Then
                System.Diagnostics.Process.Start(My.Settings.CAMEPlugin, "CAME" & PointID & " - " & File)
            Else
                MsgBox("No plugin found for CAME")
            End If
        ElseIf Combobox1.text = My.Settings.Names(8) Then
            If Not My.Settings.JGPTPlugin = String.Empty Then
                System.Diagnostics.Process.Start(My.Settings.JGPTPlugin, "JGPT" & PointID & " - " & File)
            Else
                MsgBox("No plugin found for JGPT")
            End If
        ElseIf Combobox1.text = My.Settings.Names(9) Then
            If Not My.Settings.CNPTPlugin = String.Empty Then
                System.Diagnostics.Process.Start(My.Settings.CNPTPlugin, "CNPT" & PointID & " - " & File)
            Else
                MsgBox("No plugin found for CNPT")
            End If
        ElseIf Combobox1.text = My.Settings.Names(10) Then
            If Not My.Settings.MSPTPlugin = String.Empty Then
                Dim pro As New System.Diagnostics.Process
                System.Diagnostics.Process.Start(My.Settings.MSPTPlugin, "MSPT" & PointID & " - " & File)
            Else
                MsgBox("No plugin found for MSPT")
            End If
        End If

        If MsgBox("Press ok to load the section to this program, do not press the ok button before saving with the plugin", vbOKCancel) = MsgBoxResult.Ok Then
            Try
roflfail:
            Catch ex As Exception
                If MsgBox("Unable to load the file, be sure the KMF file doesn't reach the total amount of points", MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then : GoTo roflfail : End If : End Try
        End If
    End Sub



    '----------------------Menustrips---------------------

    Private Sub SaveAsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
        File = ""
        SaveToolStripMenuItem_Click(SaveToolStripMenuItem, New System.EventArgs)
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Filesavedcheck = False
        If DataGridView1.Tag = "Points" Then ReadTable()

        Dim pointcount As Integer
        Dim totalpoint As Integer
        For Each Routje In Parsedroutes
            pointcount += 4
            pointcount += Routje.points.Length * &H10
            totalpoint += Routje.points.Length
        Next

        Dim temp(0 To pointcount + 7) As Byte
        POTI = temp
        Dim name As Byte() = {Strtoint("P"), Strtoint("O"), Strtoint("T"), Strtoint("I")}
        Writekmp(POTI, 0, name)
        Dim amount As Byte() = BitConverter.GetBytes(Parsedroutes.Length) : Array.Reverse(amount)
        Writekmp(POTI, 4, amount, 2)
        Dim amount2 As Byte() = BitConverter.GetBytes(totalpoint) : Array.Reverse(amount2)
        Writekmp(POTI, 6, amount2, 2)

        Dim start As Integer = 8
        For i = 0 To Parsedroutes.Length - 1
            Dim count, rp1, rp2 As Byte()
            count = BitConverter.GetBytes(Parsedroutes(i).points.Length) : Array.Reverse(count) : Writekmp(POTI, start, count, 2)
            rp1 = XMLWrite(Parsedroutes(i).Setting1.ToString("X4"), "/XML/POTI/Setting1", "Tablefill.xml") : Array.Reverse(rp1) : Writekmp(POTI, start + 2, rp1, 1)
            rp2 = XMLWrite(Parsedroutes(i).Setting2.ToString("X4"), "/XML/POTI/Setting1", "Tablefill.xml") : Array.Reverse(rp2) : Writekmp(POTI, start + 3, rp2, 1)

            start = start + 4
            For Each Point In Parsedroutes(i).points
                Dim LocX, LocY, LocZ, S1, S2 As Byte()
                LocX = BitConverter.GetBytes(Floattohex(Point.Location(0))) : Array.Reverse(LocX) : Writekmp(POTI, start, LocX)
                LocY = BitConverter.GetBytes(Floattohex(Point.Location(1))) : Array.Reverse(LocY) : Writekmp(POTI, start + 4, LocY)
                LocZ = BitConverter.GetBytes(Floattohex(Point.Location(2))) : Array.Reverse(LocZ) : Writekmp(POTI, start + 8, LocZ)
                S1 = BitConverter.GetBytes(Point.Pointsettings) : Array.Reverse(S1) : Writekmp(POTI, start + 12, S1, 2)
                S2 = BitConverter.GetBytes(Point.Additional) : Array.Reverse(S2) : Writekmp(POTI, start + 14, S2, 2)
                start = start + &H10
            Next

        Next


        If File = "" Then
            Dim dlg As MsgBoxResult = SaveKMPdialog.ShowDialog
            If dlg = MsgBoxResult.Ok Then
                File = SaveKMPdialog.FileName
            Else : Exit Sub
            End If
        End If


        Dim length As Integer = 0
        Dim bytes As Byte() = BitConverter.GetBytes(0)

        Writekmp(Header, &H10, bytes)
        length = length + KTPT.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)
        Writekmp(Header, &H14, bytes)
        length = length + ENPT.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)
        Writekmp(Header, &H18, bytes)
        length = length + ENPH.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)
        Writekmp(Header, &H1C, bytes)
        length = length + ITPT.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)
        Writekmp(Header, &H20, bytes)
        length = length + ITPH.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)
        Writekmp(Header, &H24, bytes)
        length = length + CKPT.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)
        Writekmp(Header, &H28, bytes)
        length = length + CKPH.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)
        Writekmp(Header, &H2C, bytes)
        length = length + GOBJ.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)
        Writekmp(Header, &H30, bytes)
        length = length + POTI.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)
        Writekmp(Header, &H34, bytes)
        length = length + AREA.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)
        Writekmp(Header, &H38, bytes)
        length = length + CAME.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)
        Writekmp(Header, &H3C, bytes)
        length = length + JGPT.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)
        Writekmp(Header, &H40, bytes)
        length = length + CNPT.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)
        Writekmp(Header, &H44, bytes)
        length = length + MSPT.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)
        Writekmp(Header, &H48, bytes)
        length = length + STGI.Length : bytes = BitConverter.GetBytes(length) : Array.Reverse(bytes)


        Dim Allbytes(0 To length + &H4B) As Byte
        Dim current As New Integer
        Header.CopyTo(Allbytes, 0) : current = Header.Length
        KTPT.CopyTo(Allbytes, current) : current += KTPT.Length
        ENPT.CopyTo(Allbytes, current) : current += ENPT.Length
        ENPH.CopyTo(Allbytes, current) : current += ENPH.Length
        ITPT.CopyTo(Allbytes, current) : current += ITPT.Length
        ITPH.CopyTo(Allbytes, current) : current += ITPH.Length
        CKPT.CopyTo(Allbytes, current) : current += CKPT.Length
        CKPH.CopyTo(Allbytes, current) : current += CKPH.Length
        GOBJ.CopyTo(Allbytes, current) : current += GOBJ.Length
        POTI.CopyTo(Allbytes, current) : current += POTI.Length
        AREA.CopyTo(Allbytes, current) : current += AREA.Length
        CAME.CopyTo(Allbytes, current) : current += CAME.Length
        JGPT.CopyTo(Allbytes, current) : current += JGPT.Length
        CNPT.CopyTo(Allbytes, current) : current += CNPT.Length
        MSPT.CopyTo(Allbytes, current) : current += MSPT.Length
        STGI.CopyTo(Allbytes, current)
        Dim filesize As Byte() = BitConverter.GetBytes(Allbytes.Length)
        Array.Reverse(filesize)
        Writekmp(Allbytes, 4, filesize)
        If File = "CTools" Then
            Instance.Save(Allbytes)
            Exit Sub
        End If
        Dim FS As IO.FileStream = New IO.FileStream(File, IO.FileMode.Create)
        Dim BW As IO.BinaryWriter = New IO.BinaryWriter(FS)


        BW.Write(Allbytes)

        BW.Close()
        FS.Close()

    End Sub

    Private Sub ImportKMFFile_Click(sender As System.Object, e As System.EventArgs)
        Dim dlg As New OpenFileDialog
        dlg.Filter = "KMP Modifier Path|*.kmf"
        dlg.Title = "Import"
        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
roflfail:
                LoadKMF(dlg.FileName)
            Catch ex As Exception
                If MsgBox("Unable to load the file, be sure the KMF file doesn't reach the total amount of points", MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then : GoTo roflfail : End If : End Try
        End If
    End Sub

    Dim removeblock As Boolean = False
    Dim rows As DataGridView
    Private Sub Move_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Dim a As Integer = DataGridView1.CurrentRow.Index
retry:  Dim ipb As String = InputBox("What ID do you want to give this point?" & Chr(13) & "Note: This will move the selected row", "New ID")
        If ipb = "Cancel" Then : Exit Sub : End If
        If IsNumeric(ipb) = False Then : MsgBox("Invalid ID") : GoTo retry
        ElseIf DataGridView1.RowCount <= ipb Then : MsgBox("Invalid ID") : GoTo retry
        Else
            Dim therow As DataGridViewRow
            Dim Index As Integer = ipb
            therow = DataGridView1.Rows(a)
            DataGridView1.Rows.Remove(therow)
            If Index = DataGridView1.RowCount Then
                DataGridView1.Rows.Add(therow)
            Else
                DataGridView1.Rows.Insert(Index, therow)
            End If
            FillGrid()
        End If
    End Sub
    Private Sub Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        DataGridView1.Rows.Remove(DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex))
    End Sub


    Public Img1, Img2 As Point
    Private Sub ImportWavefrontObjToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportWavefrontObjToolStripItem.Click
        Try
            Dim dlg As OpenFileDialog = New OpenFileDialog
            dlg.Filter = "Wavefront OBJ files|*.obj"
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                PathBG = dlg.FileName
                ObjBG = True
                Dim a(0) As Single : yValues = a
                GridPanel.Invalidate()
                FillGrid()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub UseNewImageToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UseNewImageToolStripMenuItem.Click
        Try
            Dim dlg As OpenFileDialog = New OpenFileDialog
            dlg.Filter = "Bitmaps|*.bmp|Portable Network Graph|*.png|JPEG Image|*.jpg"
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
retry1:
                Dim a As String = InputBox("Top left location (X, Y)")
                If Not a = "" Then
                    If a.Contains(",") Then
                        Dim b As String() = a.Split(",")
                        If b.Count = 2 Then
                            Dim X, Y As New Integer
                            If IsNumeric(b(0)) Then
                                X = b(0)
                            Else
                                MsgBox("Invalid X")
                                GoTo retry1
                            End If
                            If IsNumeric(b(1)) Then
                                Y = b(1)
                            Else
                                MsgBox("Invalid Y")
                                GoTo retry1
                            End If
                            Img1 = New Point(X, Y)
                        Else
                            MsgBox("Invalid position")
                            GoTo retry1
                        End If
                    Else
                        MsgBox("Invalid values")
                        GoTo retry1
                    End If
                Else
                    Exit Sub
                End If
retry2:
                Dim a0 As String = InputBox("Bottom right location (X, Y)")
                If Not a0 = "" Then
                    If a0.Contains(",") Then
                        Dim b As String() = a0.Split(",")
                        If b.Count = 2 Then
                            Dim X, Y As New Integer
                            If IsNumeric(b(0)) Then
                                X = b(0)
                            Else
                                MsgBox("Invalid X")
                                GoTo retry2
                            End If
                            If IsNumeric(b(1)) Then
                                Y = b(1)
                            Else
                                MsgBox("Invalid Y")
                                GoTo retry2
                            End If
                            Img2 = New Point(X, Y)
                        Else
                            MsgBox("Invalid position")
                            GoTo retry2
                        End If
                    Else
                        MsgBox("Invalid values")
                        GoTo retry2
                    End If
                Else
                    Exit Sub
                End If
                GridPanel.Invalidate()
            End If
            PathBG = dlg.FileName
            ObjBG = False
            FillGrid()
        Catch ex As Exception
        End Try
    End Sub

    Dim clearonly As Boolean = False
    Private Sub GridPanel_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles GridPanel.Paint
        GridPanel.BackColor = My.Settings.Background
        If clearonly = True Then Exit Sub
        Dim bg As Graphics = e.Graphics

        Try
            If Not PathBG = "" Then
                If Mid(PathBG, PathBG.Length - 2) = "OBJ" Or Mid(PathBG, PathBG.Length - 2) = "obj" Then
                    If yValues.Length = 1 Then
                        Dim Vertices0 As New ArrayList
                        Dim Faces0 As New ArrayList
                        Dim yValues0 As New ArrayList

                        Dim lines As String()

                        Dim line As New ArrayList
                        Dim sr As New IO.StreamReader(PathBG)
                        Do While sr.Peek() >= 0
                            line.Add(sr.ReadLine)
                        Loop
                        lines = CType(line.ToArray(GetType(String)), String())


                        'Dialog1.ShowDialog()


                        For i = 0 To lines.Count - 1
                            If Mid(lines(i), 1, 2) = "v " Then
                                lines(i) = lines(i).Replace(".", ",")
                                lines(i) = lines(i).Replace("  ", " ")
                                Dim a As String() = System.Text.RegularExpressions.Regex.Split(lines(i), " ")
                                Vertices0.Add(New Point(a(1), a(3)))
                                Dim y0 As Single = a(2)
                                yValues0.Add(y0)
                            ElseIf Mid(lines(i), 1, 2) = "f " Then
                                Dim a As String() = System.Text.RegularExpressions.Regex.Split(lines(i), " ")
                                Dim b(0 To 2) As Integer
                                b(0) = System.Text.RegularExpressions.Regex.Split(a(1), "/")(0)
                                b(1) = System.Text.RegularExpressions.Regex.Split(a(2), "/")(0)
                                b(2) = System.Text.RegularExpressions.Regex.Split(a(3), "/")(0)
                                Faces0.Add(b)
                            End If
                        Next
                        Vertices = DirectCast(Vertices0.ToArray(GetType(Point)), Point())
                        Faces = DirectCast(Faces0.ToArray(GetType(Integer())), Object())
                        yValues = DirectCast(yValues0.ToArray(GetType(Single)), Single())
                    End If

                    Dim pens As SolidBrush = New SolidBrush(My.Settings.OBJCollor)
                    For i = 0 To Faces.Length - 1
                        Dim a As New Point(GridX + (GridPanel.Size.Width / 2) + ((Vertices(Faces(i)(0) - 1).X) / Gridscale), GridY + (GridPanel.Size.Height / 2) + ((Vertices(Faces(i)(0) - 1).Y) / Gridscale))
                        Dim b As New Point(GridX + (GridPanel.Size.Width / 2) + ((Vertices(Faces(i)(1) - 1).X) / Gridscale), GridY + (GridPanel.Size.Height / 2) + ((Vertices(Faces(i)(1) - 1).Y) / Gridscale))
                        Dim c As New Point(GridX + (GridPanel.Size.Width / 2) + ((Vertices(Faces(i)(2) - 1).X) / Gridscale), GridY + (GridPanel.Size.Height / 2) + ((Vertices(Faces(i)(2) - 1).Y) / Gridscale))

                        Dim d As New Point(50, 50)
                        Dim y As New Point(100, 50)
                        Dim f As New Point(100, 100)

                        bg.FillPolygon(pens, {a, b, c})
                    Next
                ElseIf Mid(PathBG, PathBG.Length - 2) = "BMP" Or Mid(PathBG, PathBG.Length - 2) = "bmp" Or Mid(PathBG, PathBG.Length - 2) = "PNG" Or Mid(PathBG, PathBG.Length - 2) = "png" Or _
                    Mid(PathBG, PathBG.Length - 2) = "JPG" Or Mid(PathBG, PathBG.Length - 2) = "jpg" Then

                    Dim BGimg As Image
                    BGimg = Image.FromFile(PathBG)

                    Dim x1 As Integer = GridX + (GridPanel.Size.Width / 2) + ((Img1.X) / Gridscale)
                    Dim y1 As Integer = GridY + (GridPanel.Size.Height / 2) + ((Img1.Y) / Gridscale)
                    Dim width As Integer = (GridX + (GridPanel.Size.Width / 2) + ((Img2.X) / Gridscale)) - (GridX + (GridPanel.Size.Width / 2) + ((Img1.X) / Gridscale))
                    Dim height As Integer = (GridY + (GridPanel.Size.Height / 2) + ((Img2.Y) / Gridscale)) - (GridY + (GridPanel.Size.Height / 2) + ((Img1.Y) / Gridscale))
                    Dim sqr As New Drawing.Rectangle(x1, y1, width, height)
                    bg.DrawImage(BGimg, sqr)
                End If
            End If
            If Combobox1.text = My.Settings.Names(3) Then
                For id = 0 To DataGridView1.RowCount - 2
                    Dim pens As Pen
                    If "&h" & "0" & DataGridView1.Rows(id).Cells(7).Value = 255 Then : pens = New Pen(My.Settings.Pointcolor, 3)
                    Else : pens = New Pen(My.Settings.Keycheckpoint, 3) : End If
                    If line(id) Is Nothing Then
                        Exit For
                    End If
                    bg.DrawLine(pens, line(id).X1, line(id).Y1, line(id).X2, line(id).Y2)

                    Dim dX As Integer
                    Dim dY As Integer
                    If line(id).Y2 < line(id).Y1 Then
                        dX = -1 : Else : dX = 1
                    End If
                    If line(id).X2 < line(id).X1 Then
                        dY = 1 : Else : dY = -1
                    End If

                    Dim Angle As Integer = Math.Atan((line(id).Y2 - line(id).Y1) / (line(id).X2 + 0.1 - line(id).X1)) * (180 / Math.PI)
                    Dim X1, X2, Y1, Y2 As Integer
                    X1 = (line(id).X1 + line(id).X2) / 2
                    Y1 = (line(id).Y1 + line(id).Y2) / 2
                    Angle = (Angle ^ 2) ^ 0.5

                    X2 = Math.Cos((90 - Angle) / (180 / Math.PI)) * 15 * dX + X1
                    Y2 = Math.Sin((90 - Angle) / (180 / Math.PI)) * 15 * dY + Y1

                    If My.Settings.Showarrow = True Then
                        Dim Drawpen As Pen
                        If "&h" & "0" & DataGridView1.Rows(id).Cells(7).Value = 255 Then : Drawpen = New Pen(My.Settings.Pointcolor, 3)
                        Else : Drawpen = New Pen(My.Settings.Keycheckpoint, 3) : End If
                        Drawpen.EndCap = Drawing2D.LineCap.ArrowAnchor
                        bg.DrawLine(Drawpen, X1, Y1, X2, Y2)
                    End If
                Next
            End If

            If Combobox1.text = My.Settings.Names(6) Then
                For i = 0 To oval.Length - 2
                    Dim location As Point = oval(i).Location
                    Dim scale(0 To 1) As Integer
                    scale = {DataGridView1.Rows(i).Cells(10).Value * 10000, DataGridView1.Rows(i).Cells(12).Value * 10000}
                    Dim rotation As Single = DataGridView1.Rows(i).Cells(8).Value
                    Dim halfWidth As Single = scale(0) / 2
                    Dim halfHeight As Single = scale(1) / 2
                    Dim rotationRadians As Single = rotation / (180 / Math.PI)
                    Dim points As Point() = {New Point(Math.Cos(rotationRadians) * halfWidth + Math.Sin(rotationRadians) * halfHeight, Math.Cos(rotationRadians) * halfHeight - Math.Sin(rotationRadians) * halfWidth), _
                                            New Point(Math.Cos(rotationRadians) * halfWidth + Math.Sin(rotationRadians) * -halfHeight, Math.Cos(rotationRadians) * -halfHeight - Math.Sin(rotationRadians) * halfWidth), _
                                            New Point(Math.Cos(rotationRadians) * -halfWidth + Math.Sin(rotationRadians) * -halfHeight, Math.Cos(rotationRadians) * -halfHeight - Math.Sin(rotationRadians) * -halfWidth), _
                                            New Point(Math.Cos(rotationRadians) * -halfWidth + Math.Sin(rotationRadians) * halfHeight, Math.Cos(rotationRadians) * halfHeight - Math.Sin(rotationRadians) * -halfWidth)}
                    Dim thecolor As Color
                    If i = DataGridView1.CurrentRow.Index Then
                        thecolor = Color.FromArgb(&H4FFF0000)
                    Else
                        thecolor = Color.FromArgb(&H4F0000FF)
                    End If
                    For abc = 0 To points.Length - 1
                        points(abc).X = ((points(abc).X) / Gridscale) + oval(i).Location.X
                        points(abc).Y = ((points(abc).Y) / Gridscale) + oval(i).Location.Y
                    Next
                    Dim brushing As Brush = New SolidBrush(thecolor)

                    bg.FillPolygon(brushing, points)
                Next
            End If

            If (Combobox1.text = My.Settings.Names(0) Or Combobox1.text = My.Settings.Names(4) Or Combobox1.text = My.Settings.Names(10)) And My.Computer.Keyboard.CtrlKeyDown = True Then
                Dim cellno As Integer
                If Combobox1.text = My.Settings.Names(4) Then cellno = 6 Else cellno = 5

                For i = 0 To DataGridView1.RowCount - 2
                    Dim drawpen As New Pen(Brushes.Blue, 1)
                    If i = DataGridView1.CurrentRow.Index Then
                        drawpen.Color = My.Settings.Selection
                    Else
                        drawpen.Color = My.Settings.Pointcolor
                    End If
                    'drawpen.StartCap = Drawing2D.LineCap.Round
                    drawpen.EndCap = Drawing2D.LineCap.ArrowAnchor

                    drawpen.Width = 4
                    Dim X1, X2, Y1, Y2 As Short
                    X1 = oval(i).Location.X
                    Y1 = oval(i).Location.Y
                    Dim angle As Decimal = (DataGridView1.Rows(i).Cells(cellno).Value - 90) / (180 / Math.PI)
                    X2 = Math.Cos(angle) * 12 + X1
                    Y2 = -1 * Math.Sin(angle) * 12 + Y1
                    bg.DrawLine(drawpen, X1, Y1, X2, Y2)
                    oval(i).FillColor = Color.FromArgb(0)
                    oval(i).BorderColor = Color.FromArgb(0)
                    Arrows = True
                Next
            ElseIf Combobox1.text = My.Settings.Names(0) Or Combobox1.text = My.Settings.Names(4) Or Combobox1.text = My.Settings.Names(10) Then
                Arrows = False
            End If

            If (Combobox1.text = My.Settings.Names(1) Or Combobox1.text = My.Settings.Names(2) Or Combobox1.text = My.Settings.Names(3)) And CheckBox1.Checked = True And oval.Length > 0 Then
                Dim points(oval.Length - 2) As Point
                For i = 0 To oval.Length - 2
                    If oval(i) Is Nothing Then
                        Exit For
                    End If
                    points(i) = oval(i).Location
                    points(i).X = oval(i).Location.X + 2.5
                    points(i).Y = oval(i).Location.Y + 2.5
                Next

                Dim drawpen As New Pen(Brushes.Blue)
                drawpen.Color = My.Settings.Pointcolor
                drawpen.Width = 1
                bg.DrawLines(drawpen, points)
            End If
        Catch ex As Exception : MsgBox(ex.StackTrace) : End Try

    End Sub


    Private Sub ExportPoints_Click(sender As System.Object, e As System.EventArgs)
        Dim location As New Short
        Dim length As New Short
        Dim name As String = Combobox1.text

        Try
            location = InputBox("Read from point:")
            length = InputBox("How many points?")
        Catch ex As Exception : MsgBox("Invalid") : Exit Sub : End Try

failure:
        Try
            name = InputBox("Optional, export as group:", , name)

            If name = My.Settings.Names(0) Then : name = "KTPT"
            ElseIf name = My.Settings.Names(1) Then : name = "ENPT"
            ElseIf name = My.Settings.Names(2) Then : name = "ITPT"
            ElseIf name = My.Settings.Names(3) Then : name = "CKPT"
            ElseIf name = My.Settings.Names(4) Then : name = "GOBJ"
            ElseIf name = My.Settings.Names(5) Then : MsgBox("POTI exporting is not supported") : Exit Sub
            ElseIf name = My.Settings.Names(6) Then : name = "AREA"
            ElseIf name = My.Settings.Names(7) Then : name = "CAME"
            ElseIf name = My.Settings.Names(8) Then : name = "JGPT"
            ElseIf name = My.Settings.Names(9) Then : name = "CNPT"
            ElseIf name = My.Settings.Names(10) Then : name = "MSPT"
            ElseIf name = "" Then : MsgBox("Invalid") : Exit Sub
            ElseIf Not name = "KTPT" Or "ENPT" Or "ITPT" Or "CKPT" Or "GOBJ" Or "AREA" Or "CAME" Or "JGPT" Or "CNPT" Or "MSPT" Then : MsgBox("Invalid name") : GoTo failure
            End If
        Catch ex As Exception : MsgBox("Unable to parse") : Exit Sub : End Try

        Dim buffer2 As Byte() = {0}
        Dim pointlength As Byte

        Dim value As New Integer
        If name = "KTPT" Then buffer2 = KTPT : value = &H4B545054
        If name = "ENPT" Then buffer2 = ENPT : value = &H454E5054
        If name = "ITPT" Then buffer2 = ITPT : value = &H49545054
        If name = "CKPT" Then buffer2 = CKPT : value = &H434B5054
        If name = "GOBJ" Then buffer2 = GOBJ : value = &H474F424A
        If name = "AREA" Then buffer2 = AREA : value = &H41524541
        If name = "CAME" Then buffer2 = CAME : value = &H43414D45
        If name = "JGPT" Then buffer2 = JGPT : value = &H4A475054
        If name = "CNPT" Then buffer2 = CNPT : value = &H434E5054
        If name = "MSPT" Then buffer2 = MSPT : value = &H4D535054

        Dim magicly As Integer = value

        If value = &H4B545054 Or value = &H4A475054 Or value = &H434E5054 Or value = &H4D535054 Then
            pointlength = &H1C
            If Not name = Hextostr("4B545054") And Not name = Hextostr("4A475054") And Not name = Hextostr("434E5054") And Not name = Hextostr("4D535054") Then
                MsgBox("Can not export " & Hextostr(value.ToString("X8")) & " as " & name) : GoTo failure
            End If
        ElseIf value = &H454E5054 Or value = &H49545054 Or value = &H434B5054 Then
            pointlength = &H14
            If Not name = Hextostr("454E5054") And Not name = Hextostr("49545054") And Not name = Hextostr("434B5054") Then
                MsgBox(name & Chr(13) & Hextostr("454E5054") & Chr(13) & Hextostr("49545054") & Chr(13) & Hextostr("434B5054"))
                MsgBox("Can not export " & Hextostr(value.ToString("X8")) & " as " & name) : GoTo failure
            End If
        ElseIf value = &H454E5048 Or value = &H49545048 Or value = &H434B5048 Or value = &H504F5449 Then
            pointlength = &H10
            If Not name = Hextostr("454E5048") And Not name = Hextostr("49545048") And Not name = Hextostr("434B5048") And Not name = Hextostr("504F5449") Then
                MsgBox("Can not export " & Hextostr(value.ToString("X8")) & " as " & name) : GoTo failure
            End If
        ElseIf value = &H474F424A Then
            pointlength = &H3C
            If Not name = Hextostr(value.ToString("X8")) Then MsgBox("Can not export " & Hextostr(value.ToString("X8")) & " as " & name)
        ElseIf value = &H41524541 Then
            pointlength = &H30
            If Not name = Hextostr(value.ToString("X8")) Then MsgBox("Can not export " & Hextostr(value.ToString("X8")) & " as " & name)
        ElseIf value = &H43414D45 Then
            pointlength = &H48
            If Not name = Hextostr(value.ToString("X8")) Then MsgBox("Can not export " & Hextostr(value.ToString("X8")) & " as " & name)
        ElseIf value = &H53544749 Then
            pointlength = &HC
            If Not name = Hextostr(value.ToString("X8")) Then MsgBox("Can not export " & Hextostr(value.ToString("X8")) & " as " & name)
        End If


        Dim dlg As New SaveFileDialog
        dlg.Filter = "KMP Modifier path files|*.kmf"
        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                Dim buffer As Byte() = Readkmp(buffer2, pointlength * location + 8, length * pointlength)
                buffer.Reverse()

                Dim FW As IO.FileStream
                FW = New IO.FileStream(dlg.FileName, IO.FileMode.Create)
                Dim BW As IO.BinaryWriter = New IO.BinaryWriter(FW)
                BW.BaseStream.Position = 0

                BW.BaseStream.Position = 0
                Dim total As Byte() = BitConverter.GetBytes(&H1C + pointlength * length)
                Array.Reverse(total)
                BW.BaseStream.Write({&H4B, &H4D, &H48, &H46, total(0), total(1), total(2), total(3), 2, 0, 0, 0, 0, 1, 0, &H14}, 0, 16)
                Dim magic As Byte() = BitConverter.GetBytes(magicly) : Array.Reverse(magic)

                BW.Write({0, 0, 0, &H14}, 0, 4)
                BW.Write(magic, 0, 4)
                Dim start As Byte() = BitConverter.GetBytes(location) : Array.Reverse(start)
                Dim length2 As Byte() = BitConverter.GetBytes(length) : Array.Reverse(length2)
                BW.Write(start, 0, 2)
                BW.Write(length2, 0, 2)
                BW.Write(buffer)

                BW.Close()
                FW.Close()
            Catch ex As Exception
                MsgBox("An error has occured while writing:" & ex.Message)
            End Try
        End If
    End Sub

    Private Sub SettingsToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles CustomizeToolStripMenuItem.Click
        If Settings.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TabControl1.TabPages.Clear()
            MainForm_Load(Me, New EventArgs)
        End If
    End Sub

    'Private Sub Deletesection_Click(sender As System.Object, e As System.EventArgs)
    '    Try
    '        If ComboBox2.Text = "ENPH" Then
    '            If ENPH.Length = 8 Then
    '                MsgBox("No points to delete")
    '                Exit Sub
    '            End If

    '            Dim temparray(0 To ENPH.Length - &H11) As Byte
    '            For i = 0 To ENPH.Length - &H11
    '                temparray(i) = ENPH(i)
    '            Next
    '            ENPH = temparray

    '            Dim count As Short = (ENPH.Length - 8) / &H10
    '            Dim length As Byte() = BitConverter.GetBytes(count) : Array.Reverse(length)

    '            Writekmp(ENPH, 4, length, 2)

    '            If 'NumericUpDown1.Value > count - 1 Then
    '                'NumericUpDown1.Maximum = count - 1
    '                If count - 1 = -1 Then 'NumericUpDown1.Minimum = -1
    '                    'NumericUpDown1.Value = count - 1
    '                End If
    '            ElseIf ComboBox2.Text = "ITPH" Then
    '                If ITPH.Length = 8 Then
    '                    MsgBox("No points to delete")
    '                    Exit Sub
    '                End If

    '                Dim temparray(0 To ITPH.Length - &H11) As Byte
    '                For i = 0 To ITPH.Length - &H11
    '                    temparray(i) = ITPH(i)
    '                Next
    '                ITPH = temparray

    '                Dim count As Short = (ITPH.Length - 8) / &H10
    '                Dim length As Byte() = BitConverter.GetBytes(count) : Array.Reverse(length)

    '                Writekmp(ITPH, 4, length, 2)

    '            If 'NumericUpDown1.Value > count - 1 Then
    '                    'NumericUpDown1.Maximum = count - 1
    '                    If count - 1 = -1 Then 'NumericUpDown1.Minimum = -1
    '                        'NumericUpDown1.Value = count - 1
    '                    End If
    '                ElseIf ComboBox2.Text = "CKPH" Then
    '                    If CKPH.Length = 8 Then
    '                        MsgBox("No points to delete")
    '                        Exit Sub
    '                    End If

    '                    Dim temparray(0 To CKPH.Length - &H11) As Byte
    '                    For i = 0 To CKPH.Length - &H11
    '                        temparray(i) = CKPH(i)
    '                    Next
    '                    CKPH = temparray

    '                    Dim count As Short = (CKPH.Length - 8) / &H10
    '                    Dim length As Byte() = BitConverter.GetBytes(count) : Array.Reverse(length)

    '                    Writekmp(CKPH, 4, length, 2)

    '            If 'NumericUpDown1.Value > count - 1 Then
    '                        'NumericUpDown1.Maximum = count - 1
    '                        If count - 1 = -1 Then 'NumericUpDown1.Minimum = -1
    '                            'NumericUpDown1.Value = count - 1
    '                        End If
    '                    End If
    '                    FillSection()
    '                    FillButtons()
    '                    Filesavedcheck = True
    '    Catch ex As Exception : MsgBox("Unable to delete section, be sure there are any sections" & Chr(13) & Chr(13) & ex.StackTrace)
    '    End Try
    'End Sub

    'Private Sub Addsection_Click(sender As System.Object, e As System.EventArgs)
    '    If ComboBox2.Text = "ENPH" Then
    '        Dim temparray(0 To (SectionarrayENPT.Length + 1) * &H10 + 7) As Byte
    '        For i = 0 To (SectionarrayENPT.Length) * &H10 - 1
    '            temparray(i + 8) = ENPH(i + 8)
    '        Next
    '        ENPH = temparray

    '        Dim count As Short = SectionarrayENPT.Length + 1

    '        Dim magic As Byte() = {Strtoint("E"), Strtoint("N"), Strtoint("P"), Strtoint("H")}
    '        Dim length As Byte() = BitConverter.GetBytes(count) : Array.Reverse(length)
    '        Array.Copy({magic(0), magic(1), magic(2), magic(3), length(0), length(1)}, ENPH, 6)

    '    ElseIf ComboBox2.Text = "ITPH" Then
    '        Dim temparray(0 To (SectionarrayITPT.Length + 1) * &H10 + 7) As Byte
    '        For i = 0 To (SectionarrayITPT.Length) * &H10 - 1
    '            temparray(i + 8) = ITPH(i + 8)
    '        Next
    '        ITPH = temparray

    '        Dim count As Short = SectionarrayITPT.Length + 1

    '        Dim magic As Byte() = {Strtoint("I"), Strtoint("T"), Strtoint("P"), Strtoint("H")}
    '        Dim length As Byte() = BitConverter.GetBytes(count) : Array.Reverse(length)
    '        Array.Copy({magic(0), magic(1), magic(2), magic(3), length(0), length(1)}, ITPH, 6)

    '    ElseIf ComboBox2.Text = "CKPH" Then
    '        Dim temparray(0 To (SectionarrayCKPT.Length + 1) * &H10 + 7) As Byte
    '        For i = 0 To (SectionarrayCKPT.Length) * &H10 - 1
    '            temparray(i + 8) = CKPH(i + 8)
    '        Next
    '        CKPH = temparray

    '        Dim count As Short = SectionarrayCKPT.Length + 1

    '        Dim magic As Byte() = {Strtoint("C"), Strtoint("K"), Strtoint("P"), Strtoint("H")}
    '        Dim length As Byte() = BitConverter.GetBytes(count) : Array.Reverse(length)
    '        Array.Copy({magic(0), magic(1), magic(2), magic(3), length(0), length(1)}, CKPH, 6)
    '    End If


    '    FillSection()
    '    'NumericUpDown1.Minimum = 0

    '    If ComboBox2.Text = "ENPH" Then
    '        'NumericUpDown1.Maximum = Section.ENPH.Count - 1
    '    ElseIf ComboBox2.Text = "ITPH" Then
    '        'NumericUpDown1.Maximum = Section.ITPH.Count - 1
    '    ElseIf ComboBox2.Text = "CKPH" Then
    '        'NumericUpDown1.Maximum = Section.CKPH.Count - 1
    '    End If
    '    Button4.Text = "Section" & Chr(13) & 'NumericUpDown1.Value
    '    Filesavedcheck = True
    '    If Not File = "" Then
    '        Try
    '            FillButtons()
    '            'NumericUpDown1.Minimum = 0
    '        Catch ex As Exception : End Try
    '    End If
    'End Sub

    Private Sub ClearTableToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ClearTableToolStripMenuItem.Click
        DataGridView1.Rows.Clear()
        FillGrid()
    End Sub

    Private Sub ChangePositionToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ChangePositionToolStripMenuItem.Click
retry1:
        Try
            Dim a As String = InputBox("Top left location (X, Y)", , Img1.X & "," & Img1.Y)
            If Not a = "" Then
                If a.Contains(",") Then
                    Dim b As String() = a.Split(",")
                    If b.Count = 2 Then
                        Dim X, Y As New Integer
                        If IsNumeric(b(0)) Then
                            X = b(0)
                        Else
                            MsgBox("Invalid X")
                            GoTo retry1
                        End If
                        If IsNumeric(b(1)) Then
                            Y = b(1)
                        Else
                            MsgBox("Invalid Y")
                            GoTo retry1
                        End If
                        Img1 = New Point(X, Y)
                    Else
                        MsgBox("Invalid position")
                        GoTo retry1
                    End If
                Else
                    MsgBox("Invalid values")
                    GoTo retry1
                End If
            Else
                Exit Sub
            End If
retry2:
            Dim a0 As String = InputBox("Bottom right location (X, Y)", , Img2.X & "," & Img2.Y)
            If Not a0 = "" Then
                If a0.Contains(",") Then
                    Dim b As String() = a0.Split(",")
                    If b.Count = 2 Then
                        Dim X, Y As New Integer
                        If IsNumeric(b(0)) Then
                            X = b(0)
                        Else
                            MsgBox("Invalid X")
                            GoTo retry2
                        End If
                        If IsNumeric(b(1)) Then
                            Y = b(1)
                        Else
                            MsgBox("Invalid Y")
                            GoTo retry2
                        End If
                        Img2 = New Point(X, Y)
                    Else
                        MsgBox("Invalid position")
                        GoTo retry2
                    End If
                Else
                    MsgBox("Invalid values")
                    GoTo retry2
                End If
            Else
                Exit Sub
            End If
            GridPanel.Invalidate()
        Catch ex As Exception : MsgBox("Couldn't change location.") : End Try
    End Sub


    Private Sub Sectionlist_MouseUp(sender As System.Object, e As System.EventArgs) Handles Sectionlist.MouseUp
        ReadTable()
        IgnoreSelect = True
        DataGridView1.Columns.Clear()
        IgnoreSelect = False
        FillTable()
        DataGridView1.Rows.Clear()
        If CheckBox1.Checked = True Then
            FillTable(Mid(Sectionlist.SelectedItem, 9), False)
        Else
            FillTable()
        End If
        FillGrid()
    End Sub

    Private Sub RadioButton14_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton14.CheckedChanged
        If Not sender.checked = True Then Exit Sub
        Try
            ReadTable()
        Catch ex As Exception : MsgBox("Error while saving table") : End Try
        CheckBox1.Checked = False
        CheckBox1.Visible = False
        If Combobox1.text = My.Settings.Names(1) Then : group = "ENPH" : Combobox1.text = "ENPH"
        ElseIf Combobox1.text = My.Settings.Names(2) Then : group = "ITPH" : Combobox1.text = "ITPH"
        ElseIf Combobox1.text = My.Settings.Names(3) Then : group = "CKPH" : Combobox1.text = "CKPH" : End If

        FillTable()
    End Sub

    Private Sub RadioButton13_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton13.CheckedChanged
        If sender.checked = False Then Exit Sub
        Try
            ReadTable()
        Catch ex As Exception : MsgBox("Error while saving table") : End Try
        CheckBox1.Visible = True
        If group = "ENPH" Then
            group = My.Settings.Names(1)
            Combobox1.text = My.Settings.Names(1)
        End If
        If group = "ITPH" Then
            group = My.Settings.Names(2)
            Combobox1.text = My.Settings.Names(2)
        End If
        If group = "CKPH" Then
            group = My.Settings.Names(3)
            Combobox1.text = My.Settings.Names(3)
        End If

        FillTable()
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem3.Click
        SaveAsToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub PointsFromObjToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PointsFromObjToolStripMenuItem.Click
        ObjImporter.ShowDialog(DataGridView1, Combobox1)
    End Sub

    Public Sub ReplaceDGV(ByVal dgv As DataGridView)
        DataGridView1 = dgv
    End Sub

    Private Sub GridPanel_MouseHover(sender As System.Object, e As System.EventArgs) Handles GridPanel.MouseMove
        Dim X As Single = (GridPanel.PointToClient(MousePosition).X - GridX - (GridPanel.Size.Width / 2)) * Gridscale
        Dim Y As Single = (GridPanel.PointToClient(MousePosition).Y - GridY - (GridPanel.Size.Height / 2)) * Gridscale
        XBox.Text = X
        YBox.Text = Y

        If My.Computer.Keyboard.CtrlKeyDown Then
            GridPanel.Cursor = Cursors.Hand
            GridPanel.Invalidate()
        Else
            GridPanel.Cursor = Cursors.Default
            If Arrows = True Then
                GridPanel.Invalidate()
                Arrows = False
                FillGrid()
            End If
        End If
    End Sub


    Dim sorting As Boolean() = {False, False} '{Soring?, Direction (True=Clockwise)}
    Dim center As New Point
    Private Sub Sort_Clockwise(sender As System.Object, e As System.EventArgs) Handles ClockwiseToolStripMenuItem.Click
        If sorting(0) = False Then
            MsgBox("Please click on the location you want to use as middle point. If you click any other control the function won't be cancled. If you call any sort function again point 0,0 will be used.")
            sorting = {True, True}
            center = New Point
            GridPanel.Cursor = Cursors.Cross
        Else

            sorting = {False, False}
            Dim result As String = InputBox("Which point ID do you wish to use as first point? -1 = start at 0 degrees", "First point", "-1")
            Dim ID As New Integer
            Try : ID = result : Catch ex As Exception : MsgBox("invalid ID, 0 will be used.") : End Try

            Dim Degrees(0 To oval.Length - 2) As Integer
            Dim i As Integer = 0
            For a = 0 To oval.Length - 2
                Dim point As PowerPacks.OvalShape = oval(a)
                Dim Xdeg, Ydeg As New Integer
                Xdeg = (point.Location.X - GridX - (GridPanel.Size.Width / 2)) * Gridscale - center.X
                Ydeg = (point.Location.Y - GridY - (GridPanel.Size.Height / 2)) * Gridscale - center.Y
                If Xdeg = 0 Then
                    If Ydeg <= 0 Then Degrees(i) = 0
                    If Ydeg > 0 Then Degrees(i) = 180
                End If
                If Ydeg = 0 Then
                    If Xdeg <= 0 Then Degrees(i) = 270
                    If Xdeg > 0 Then Degrees(i) = 90
                End If

                If Ydeg < 0 And Xdeg > 0 Then Degrees(i) = Math.Atan(Math.Abs(Xdeg / Ydeg)) * (180 / Math.PI)
                If Ydeg > 0 And Xdeg > 0 Then Degrees(i) = Math.Atan(Math.Abs(Ydeg / Xdeg)) * (180 / Math.PI) + 90
                If Ydeg > 0 And Xdeg < 0 Then Degrees(i) = Math.Atan(Math.Abs(Xdeg / Ydeg)) * (180 / Math.PI) + 180
                If Ydeg < 0 And Xdeg < 0 Then Degrees(i) = Math.Atan(Math.Abs(Ydeg / Xdeg)) * (180 / Math.PI) + 270

                i += 1
            Next

            Dim SortFrom As Integer = -1
            For rowno = 0 To Degrees.Length - 1
                Dim cur As Integer = Degrees(0)
                Dim curID As Integer = 0

                For i = 0 To Degrees.Length - 1
                    If Degrees(i) < cur Then
                        cur = Degrees(i)
                        curID = i
                    End If
                Next
                If curID = ID And Not ID = -1 Then
                    SortFrom = rowno
                End If
                DataGridView1.Rows(curID).Cells(0).Value = rowno
                Degrees(curID) = 400
            Next
            DataGridView1.Sort(DataGridView1.Columns(0), ListSortDirection.Ascending)

            If SortFrom = -1 Then GoTo done
            Dim idMinus As Integer = SortFrom
            For i = 0 To DataGridView1.Rows.Count - 2
                Dim newID As Integer = i - idMinus
                If newID < 0 Then newID = DataGridView1.RowCount Else GoTo done
                Dim row As DataGridViewRow = DataGridView1.Rows(0)
                DataGridView1.Rows.Remove(row)
                DataGridView1.Rows.Add(row)
            Next
done:
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Index = DataGridView1.RowCount - 1 Then Exit For
                row.Cells(0).Value = row.Index
            Next
            FillGrid()
        End If
    End Sub

    Private Sub Sort_Counterclockwise(sender As System.Object, e As System.EventArgs) Handles CounterClockwiseToolStripMenuItem.Click
        If sorting(0) = False Then
            MsgBox("Please click on the location you want to use as middle point. If you click any other control the function won't be cancled. If you call any sort function again point 0,0 will be used.")
            sorting = {True, False}
            center = New Point
            GridPanel.Cursor = Cursors.Cross
        Else

            sorting = {False, False}
            Dim result As String = InputBox("Which point ID do you wish to use as first point? -1 = start at 0 degrees", "First point", "-1")
            Dim ID As New Integer
            Try : ID = result : Catch ex As Exception : MsgBox("invalid ID, 0 will be used.") : End Try

            Dim Degrees(0 To oval.Length - 2) As Integer
            Dim i As Integer = 0
            For a = 0 To oval.Length - 2
                Dim point As PowerPacks.OvalShape = oval(a)
                Dim Xdeg, Ydeg As New Integer
                Xdeg = (point.Location.X - GridX - (GridPanel.Size.Width / 2)) * Gridscale - center.X
                Ydeg = (point.Location.Y - GridY - (GridPanel.Size.Height / 2)) * Gridscale - center.Y
                If Xdeg = 0 Then
                    If Ydeg <= 0 Then Degrees(i) = 0
                    If Ydeg > 0 Then Degrees(i) = 180
                End If
                If Ydeg = 0 Then
                    If Xdeg <= 0 Then Degrees(i) = 270
                    If Xdeg > 0 Then Degrees(i) = 90
                End If

                If Ydeg < 0 And Xdeg > 0 Then Degrees(i) = Math.Atan(Math.Abs(Xdeg / Ydeg)) * (180 / Math.PI)
                If Ydeg > 0 And Xdeg > 0 Then Degrees(i) = Math.Atan(Math.Abs(Ydeg / Xdeg)) * (180 / Math.PI) + 90
                If Ydeg > 0 And Xdeg < 0 Then Degrees(i) = Math.Atan(Math.Abs(Xdeg / Ydeg)) * (180 / Math.PI) + 180
                If Ydeg < 0 And Xdeg < 0 Then Degrees(i) = Math.Atan(Math.Abs(Ydeg / Xdeg)) * (180 / Math.PI) + 270

                i += 1
            Next

            Dim SortFrom As Integer = -1
            For rowno = 0 To Degrees.Length - 1
                Dim cur As Integer = Degrees(0)
                Dim curID As Integer = 0

                For i = 0 To Degrees.Length - 1
                    If Degrees(i) > cur Then
                        cur = Degrees(i)
                        curID = i
                    End If
                Next
                If curID = ID And Not ID = -1 Then
                    SortFrom = rowno
                End If
                DataGridView1.Rows(curID).Cells(0).Value = rowno
                Degrees(curID) = -400
            Next
            DataGridView1.Sort(DataGridView1.Columns(0), ListSortDirection.Ascending)

            If SortFrom = -1 Then GoTo done
            Dim idMinus As Integer = SortFrom
            For i = 0 To DataGridView1.Rows.Count - 2
                Dim newID As Integer = i - idMinus
                If newID < 0 Then newID = DataGridView1.RowCount Else GoTo done
                Dim row As DataGridViewRow = DataGridView1.Rows(0)
                DataGridView1.Rows.Remove(row)
                DataGridView1.Rows.Add(row)
            Next
done:
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Index = DataGridView1.RowCount - 1 Then Exit For
                row.Cells(0).Value = row.Index
            Next
            FillGrid()
        End If
    End Sub

    Private Sub SetAsFirstPointToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SetAsFirstPointToolStripMenuItem.Click
        Dim idMinus As Integer = DataGridView1.CurrentRow.Index
        For i = 0 To DataGridView1.Rows.Count - 2
            Dim newID As Integer = i - idMinus
            If newID < 0 Then newID = DataGridView1.RowCount Else Exit Sub
            Dim row As DataGridViewRow = DataGridView1.Rows(0)
            DataGridView1.Rows.Remove(row)
            DataGridView1.Rows.Add(row)
        Next
        FillGrid()
    End Sub

    Private Sub CSVFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CSVFileToolStripMenuItem.Click
        ExportAll()
    End Sub

    Private Sub CSVFileToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles CSVFileToolStripMenuItem1.Click
        ImportAll()
        Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath)
        RouteUsage = CheckRoutes()
        Errorlist = ErrorCheck()
        IgnoreSelect = True
        DataGridView1.Columns.Clear()
        IgnoreSelect = False
        FillTable()
    End Sub

    Dim Objectcatalogue As Objects
    Private Sub ObjectCatalogueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ObjectCatalogueToolStripMenuItem.Click
        If DataGridView1.ColumnCount = 0 Or Not Combobox1.text = My.Settings.Names(4) Then Exit Sub

        Dim ObjectList(0 To DataGridView1.RowCount - 2) As Item
        For i = 0 To DataGridView1.RowCount - 2
            Dim temp As Integer = ObjectWrite(DataGridView1.Rows(i).Cells(1).Value)
            ObjectList(i) = ObjectRead(ObjectWrite(DataGridView1.Rows(i).Cells(1).Value))
        Next
        Objectcatalogue = New Objects(ObjectList, DataGridView1)
        Objectcatalogue.Show()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If sender.checked = False Then
            Sectionlist.SelectionMode = SelectionMode.MultiExtended
            For i As Integer = 0 To Sectionlist.Items.Count - 1
                Sectionlist.SetSelected(i, True)
            Next
            Sectionlist.Enabled = False
        Else
            Sectionlist.Enabled = True
            Sectionlist.SelectionMode = SelectionMode.One
        End If
        Sectionlist_MouseUp(CheckBox1, New EventArgs)

    End Sub

    Private Sub STGISetting_SelectedItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STGISetting1.SelectedItemChanged, STGISetting2.SelectedItemChanged, STGISetting3.ValueChanged, CAMESetting1.ValueChanged
        If File = "" Then Exit Sub
        If STGI Is Nothing Then Exit Sub
        Writekmp(STGI, 8, {STGISetting3.Value}, 1)
        If Not STGISetting1.SelectedIndex = -1 Then Writekmp(STGI, 9, {STGISetting1.SelectedIndex})
        If Not STGISetting2.SelectedIndex = -1 Then Writekmp(STGI, 10, {STGISetting2.SelectedIndex})
        CAME(6) = CAMESetting1.Value

    End Sub
End Class



Public Class Comboboxje
    Public text As String
End Class

Public Class Section
    Public Class ENPH
        Public Shared Count As Integer
        Public Class Section
            Public Lastsection(0 To 5) As Byte
            Public Lastsectioncount As Integer
            Public Nextsection(0 To 5) As Byte
            Public Nextsectioncount As Integer
            Public Frompoint As Byte
            Public Amount As Byte

            Public Sub ResetLast()
                Lastsection = {255, 255, 255, 255, 255, 255}
            End Sub
            Public Sub AddLast(ByVal id As Integer, ByVal value As Integer, ByRef bytes As Byte())
                For i = 0 To 5
                    Dim a As Byte() = Lastsection
                    If Lastsection(i) = 255 Then
                        Lastsection(i) = value
                        Lastsectioncount += 1
                        MainForm.Writekmp(bytes, 17 + i * &H10, {value})
                        Exit Sub
                    End If
                Next
                MsgBox("Reached the maximal amount of pointers to section " & value & ". In this state the kmp will freeze, please remove a pointer to this section.")
            End Sub
        End Class
    End Class

    Public Class ITPH
        Public Shared Count As Integer
        Public Class Section
            Public Lastsection(0 To 5) As Byte
            Public Lastsectioncount As Integer
            Public Nextsection(0 To 5) As Byte
            Public Nextsectioncount As Integer
            Public Frompoint As Byte
            Public Amount As Byte

            Public Sub ResetLast()
                Lastsection = {255, 255, 255, 255, 255, 255}
            End Sub
            Public Sub AddLast(ByVal id As Integer, ByVal value As Integer, ByRef bytes As Byte())
                For i = 0 To 5
                    Dim a As Byte() = Lastsection
                    If Lastsection(i) = 255 Then
                        Lastsection(i) = value
                        Lastsectioncount += 1
                        MainForm.Writekmp(bytes, 17 + i * &H10, {value})
                        Exit Sub
                    End If
                Next
                MsgBox("Reached the maximal amount of pointers to section " & value & ". In this state the kmp will freeze, please remove a pointer to this section.")
            End Sub
        End Class
    End Class

    Public Class CKPH
        Public Shared Count As Integer
        Public Class Section
            Public Lastsection(0 To 5) As Byte
            Public Lastsectioncount As Integer
            Public Nextsection(0 To 5) As Byte
            Public Nextsectioncount As Integer
            Public Frompoint As Byte
            Public Amount As Byte

            Public Sub ResetLast()
                Lastsection = {255, 255, 255, 255, 255, 255}
            End Sub
            Public Sub AddLast(ByVal id As Integer, ByVal value As Integer, ByRef bytes As Byte())
                For i = 0 To 5
                    Dim a As Byte() = Lastsection
                    If Lastsection(i) = 255 Then
                        Lastsection(i) = value
                        Lastsectioncount += 1
                        MainForm.Writekmp(bytes, 17 + i * &H10, {value})
                        Exit Sub
                    End If
                Next
                MsgBox("Reached the maximal amount of pointers to section " & value & ". In this state the kmp will freeze, please remove a pointer to this section.")
            End Sub
        End Class
    End Class
End Class



Public Class Route
    Public Shared Function ToArray(ByVal POTI As Route()) As Byte()
        Dim routecount As Short = POTI.Length
        Dim pointcount As Short = 0
        Dim temparray As New ArrayList
        temparray.Add(BitConverter.GetBytes(MainForm.Strtoint("P"))(0)) : temparray.Add(BitConverter.GetBytes(MainForm.Strtoint("O"))(0)) : temparray.Add(BitConverter.GetBytes(MainForm.Strtoint("T"))(0)) : temparray.Add(BitConverter.GetBytes(MainForm.Strtoint("I"))(0))

        For Each Route In POTI
            pointcount = pointcount + Route.Count
        Next

        Dim header0(0 To 3) As Byte
        MainForm.Writekmp(header0, 0, BitConverter.GetBytes(routecount))
        MainForm.Writekmp(header0, 0, BitConverter.GetBytes(routecount))

        For lolz = 0 To 3
            temparray.Add(header0(lolz))
        Next

        For Each element In POTI
            Dim Routes(0 To element.points.Length * &H10 + 3) As Byte

            Dim header(0 To 3) As Byte
            Dim Temp As Byte() = BitConverter.GetBytes(element.Count)
            header(0) = Temp(0) : header(1) = Temp(1) : header(2) = element.Setting1 : header(3) = element.Setting2
            header.Reverse()
            header.CopyTo(Routes, 0)
            Dim i As Integer = 0
            Do While i < element.points.Length
                Dim Point As Route.Point() = element.points
                pointcount = pointcount + 1
                Dim thepoint(0 To 15) As Byte
                BitConverter.GetBytes(MainForm.Floattohex(Point(i).Location(0))).CopyTo(thepoint, 0)
                BitConverter.GetBytes(MainForm.Floattohex(Point(i).Location(1))).CopyTo(thepoint, 4)
                BitConverter.GetBytes(MainForm.Floattohex(Point(i).Location(2))).CopyTo(thepoint, 8)
                BitConverter.GetBytes(Point(i).Pointsettings).CopyTo(thepoint, 12)
                BitConverter.GetBytes(Point(i).Additional).CopyTo(thepoint, 14)
                Routes.Reverse()
                thepoint.CopyTo(Routes, 4 + i * &H10)
                i = i + 1
            Loop
            For i = 0 To Routes.Length - 1
                temparray.Add(Routes(i))
            Next
        Next

        Return CType(temparray.ToArray(GetType(Byte)), Byte())
    End Function

    Public Shared Sub Readtable(ByRef Route As Route)
        Route.Count = MainForm.DataGridView1.RowCount - 1
        Dim Thepoints As New ArrayList

        For i = 0 To MainForm.DataGridView1.RowCount - 2
            Dim Thepoint As New Route.Point
            Thepoint.Location(0) = MainForm.DataGridView1.Rows(i).Cells(1).Value
            Thepoint.Location(1) = MainForm.DataGridView1.Rows(i).Cells(2).Value
            Thepoint.Location(2) = MainForm.DataGridView1.Rows(i).Cells(3).Value
            Thepoint.Pointsettings = "&H" & "0" & MainForm.DataGridView1.Rows(i).Cells(4).Value
            Thepoint.Additional = "&H" & "0" & MainForm.DataGridView1.Rows(i).Cells(5).Value
            Thepoints.Add(Thepoint)
        Next

        Route.points = CType(Thepoints.ToArray(GetType(Route.Point)), Route.Point())
    End Sub

    Public Count As Short
    Public Setting1 As Byte
    Public Setting2 As Byte
    Public points As Route.Point()
    Public Class Point
        Public Location(0 To 2) As Single
        Public Pointsettings As Short
        Public Additional As Short
    End Class

End Class

Public Class Item
    Public ID As Integer
    Public Name As String
    Public Needsroute As Boolean
    Public IsSolid As Boolean

    Public s1, s2, s3, s4, s5, s6, s7, s8 As String() '{Explaination, kind}

    Public ids As Object() 'For each object with ids tag, add string() with all options. Gets added to combobox.

    Public Description As String

    Public Function HandleCharacteristics(ByRef Characteristics As String) As Boolean()
        Dim returnment(1) As Boolean
        If Characteristics.Contains("solid") Then returnment(0) = True Else returnment(0) = False
        If Characteristics.Contains("route needed") Then returnment(1) = True Else returnment(1) = False
        Return returnment
    End Function
End Class