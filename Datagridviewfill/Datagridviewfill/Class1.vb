Imports System.Windows.Forms
Imports System.Xml.XPath

Public Class Fill

    Shared cellstyle As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
    Public Shared file As Byte()

    

    Public Shared Function KTPT(ByVal box As String) As DataGridViewColumn

        If box = "ID" Then
            'IDBOX
            Dim IDBox As DataGridViewColumn = New DataGridViewColumn
            IDBox.Width = 40
            IDBox.SortMode = DataGridViewColumnSortMode.Automatic
            IDBox.ReadOnly = True
            IDBox.HeaderText = "ID"
            IDBox.DefaultCellStyle = cellstyle
            Dim IDBoxCell As DataGridViewCell = New DataGridViewTextBoxCell
            IDBox.CellTemplate = IDBoxCell
            Return IDBox
        End If

        If box = "XPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "X"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Y"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Z"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Roll"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Yaw"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Pitch"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Player" Then
            'Playerbox
            Dim Playerbox As DataGridViewColumn = New DataGridViewColumn
            Playerbox.SortMode = DataGridViewColumnSortMode.Automatic
            Playerbox.Width = 50
            Playerbox.HeaderText = "Player"
            Playerbox.DefaultCellStyle = cellstyle
            Dim PlayerboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            Playerbox.CellTemplate = PlayerboxCell
            Return Playerbox
        End If
        Return Nothing
    End Function

    Public Shared Function ENPT(ByVal box As String)
        Dim Setting1values(0 To 5) As String
        Setting1values(0) = "Default"
        Setting1values(1) = "Only with cutting item"
        Setting1values(2) = "Force cutting item"
        Setting1values(3) = "Swerve"
        Setting1values(4) = "Swerve a little"
        Setting1values(5) = "Unknown (0x05)"

        Dim Setting2values(0 To 3) As String
        Setting2values(0) = "Default"
        Setting2values(1) = "Zigzag (no drifting)"
        Setting2values(2) = "Forbidden wheely"
        Setting2values(3) = "Force drift"

        If box = "ID" Then
            'IDBOX
            Dim IDBox As DataGridViewColumn = New DataGridViewColumn
            IDBox.SortMode = DataGridViewColumnSortMode.Automatic
            IDBox.Width = 40
            IDBox.ReadOnly = True
            IDBox.HeaderText = "ID"
            IDBox.DefaultCellStyle = cellstyle
            Dim IDBoxCell As DataGridViewCell = New DataGridViewTextBoxCell
            IDBox.CellTemplate = IDBoxCell
            Return IDBox
        End If

        If box = "Section" Then
            'Sectionbox
            Dim Sectionbox As DataGridViewColumn = New DataGridViewColumn
            Sectionbox.SortMode = DataGridViewColumnSortMode.Automatic
            Sectionbox.Width = 50
            Sectionbox.ReadOnly = True
            Sectionbox.HeaderText = "Section"
            Sectionbox.DefaultCellStyle = cellstyle
            Dim SectionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            Sectionbox.CellTemplate = SectionboxCell
            Return Sectionbox
        End If

        If box = "XPosition" Then
            'XPositionbox
            Dim XPositionbox As DataGridViewColumn = New DataGridViewColumn
            XPositionbox.SortMode = DataGridViewColumnSortMode.Automatic
            XPositionbox.HeaderText = "X"
            XPositionbox.DefaultCellStyle = cellstyle
            Dim XPositionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            XPositionbox.CellTemplate = XPositionboxCell
            Return XPositionbox
        End If

        If box = "YPosition" Then
            'YPositionbox
            Dim YPositionbox As DataGridViewColumn = New DataGridViewColumn
            YPositionbox.SortMode = DataGridViewColumnSortMode.Automatic
            YPositionbox.HeaderText = "Y"
            YPositionbox.DefaultCellStyle = cellstyle
            Dim YPositionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            YPositionbox.CellTemplate = YPositionboxCell
            Return YPositionbox
        End If

        If box = "ZPosition" Then
            'ZPositionbox
            Dim ZPositionbox As DataGridViewColumn = New DataGridViewColumn
            ZPositionbox.SortMode = DataGridViewColumnSortMode.Automatic
            ZPositionbox.HeaderText = "Z"
            ZPositionbox.DefaultCellStyle = cellstyle
            Dim ZPositionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            ZPositionbox.CellTemplate = ZPositionboxCell
            Return ZPositionbox
        End If

        If box = "Scale" Then
            'Scalebox
            Dim Scalebox As DataGridViewColumn = New DataGridViewColumn
            Scalebox.SortMode = DataGridViewColumnSortMode.Automatic
            Scalebox.Width = 50
            Scalebox.HeaderText = "Scale"
            Scalebox.DefaultCellStyle = cellstyle
            Dim ZPositionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            Scalebox.CellTemplate = ZPositionboxCell
            Return Scalebox
        End If

        If box = "Setting1" Then
            'Setting1box
            Dim Setting1box As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn
            Setting1box.SortMode = DataGridViewColumnSortMode.Automatic
            Setting1box.HeaderText = "Setting1"
            Setting1box.DefaultCellStyle = cellstyle
            Dim ZPositionboxCell As DataGridViewCell = New DataGridViewComboBoxCell
            Setting1box.Items.AddRange(Setting1values(1), Setting1values(2), Setting1values(3), Setting1values(4), Setting1values(5))


            Setting1box.CellTemplate = ZPositionboxCell
            Return Setting1box
        End If

        If box = "Setting2" Then
            'Setting2box
            Dim Setting2box As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn
            Setting2box.SortMode = DataGridViewColumnSortMode.Automatic
            Setting2box.HeaderText = "Setting2"
            Setting2box.DefaultCellStyle = cellstyle
            Dim ZPositionboxCell As DataGridViewCell = New DataGridViewComboBoxCell
            Setting2box.Items.AddRange(Setting2values(1), Setting2values(2), Setting2values(3))
            Setting2box.CellTemplate = ZPositionboxCell
            Return Setting2box
        End If

        If box = "Setting" Then
            Dim Column As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Settings"
            Column.Width = 50
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        Return Nothing
    End Function

    Public Shared Function ITPT(ByVal box As String)
        Dim Setting1values(0 To 5) As String
        Setting1values(0) = "Default"
        Setting1values(1) = "Only with cutting item"
        Setting1values(2) = "Force cutting item"
        Setting1values(3) = "Swerve"
        Setting1values(4) = "Swerve a little"
        Setting1values(5) = "Unknown (0x05)"

        Dim Setting2values(0 To 3) As String
        Setting2values(0) = "Default"
        Setting2values(1) = "Zigzag (no drifting)"
        Setting2values(2) = "Forbidden wheely"
        Setting2values(3) = "Force drift"

        If box = "ID" Then
            'IDBOX
            Dim IDBox As DataGridViewColumn = New DataGridViewColumn
            IDBox.SortMode = DataGridViewColumnSortMode.Automatic
            IDBox.Width = 40
            IDBox.ReadOnly = True
            IDBox.HeaderText = "ID"
            IDBox.DefaultCellStyle = cellstyle
            Dim IDBoxCell As DataGridViewCell = New DataGridViewTextBoxCell
            IDBox.CellTemplate = IDBoxCell
            Return IDBox
        End If

        If box = "Section" Then
            'Sectionbox
            Dim Sectionbox As DataGridViewColumn = New DataGridViewColumn
            Sectionbox.SortMode = DataGridViewColumnSortMode.Automatic
            Sectionbox.Width = 50
            Sectionbox.ReadOnly = True
            Sectionbox.HeaderText = "Section"
            Sectionbox.DefaultCellStyle = cellstyle
            Dim SectionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            Sectionbox.CellTemplate = SectionboxCell
            Return Sectionbox
        End If

        If box = "XPosition" Then
            'XPositionbox
            Dim XPositionbox As DataGridViewColumn = New DataGridViewColumn
            XPositionbox.SortMode = DataGridViewColumnSortMode.Automatic
            XPositionbox.HeaderText = "X"
            XPositionbox.DefaultCellStyle = cellstyle
            Dim XPositionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            XPositionbox.CellTemplate = XPositionboxCell
            Return XPositionbox
        End If

        If box = "YPosition" Then
            'YPositionbox
            Dim YPositionbox As DataGridViewColumn = New DataGridViewColumn
            YPositionbox.SortMode = DataGridViewColumnSortMode.Automatic
            YPositionbox.HeaderText = "Y"
            YPositionbox.DefaultCellStyle = cellstyle
            Dim YPositionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            YPositionbox.CellTemplate = YPositionboxCell
            Return YPositionbox
        End If

        If box = "ZPosition" Then
            'ZPositionbox
            Dim ZPositionbox As DataGridViewColumn = New DataGridViewColumn
            ZPositionbox.SortMode = DataGridViewColumnSortMode.Automatic
            ZPositionbox.HeaderText = "Z"
            ZPositionbox.DefaultCellStyle = cellstyle
            Dim ZPositionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            ZPositionbox.CellTemplate = ZPositionboxCell
            Return ZPositionbox
        End If

        If box = "Scale" Then
            'Scalebox
            Dim Scalebox As DataGridViewColumn = New DataGridViewColumn
            Scalebox.SortMode = DataGridViewColumnSortMode.Automatic
            Scalebox.Width = 50
            Scalebox.HeaderText = "Scale"
            Scalebox.DefaultCellStyle = cellstyle
            Dim ZPositionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            Scalebox.CellTemplate = ZPositionboxCell
            Return Scalebox
        End If

        If box = "Setting1" Then
            'Setting1box
            Dim Setting1box As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn
            Setting1box.SortMode = DataGridViewColumnSortMode.Automatic
            Setting1box.HeaderText = "Setting1"
            Setting1box.DefaultCellStyle = cellstyle
            Dim ZPositionboxCell As DataGridViewCell = New DataGridViewComboBoxCell
            Setting1box.Items.AddRange(Setting1values(1), Setting1values(2), Setting1values(3), Setting1values(4), Setting1values(5))
            Setting1box.CellTemplate = ZPositionboxCell
            Return Setting1box
        End If

        If box = "Setting2" Then
            'Setting2box
            Dim Setting2box As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn
            Setting2box.SortMode = DataGridViewColumnSortMode.Automatic
            Setting2box.HeaderText = "Setting2"
            Setting2box.DefaultCellStyle = cellstyle
            Dim ZPositionboxCell As DataGridViewCell = New DataGridViewComboBoxCell
            Setting2box.Items.AddRange(Setting2values(1), Setting2values(2), Setting2values(3))
            Setting2box.CellTemplate = ZPositionboxCell
            Return Setting2box
        End If

        Return Nothing
    End Function

    Public Shared Function CKPT(ByVal box As String)
        'ID, Section, XPosition1, ZPosition1, XPosition2, Zposition2, Respawn, Type, Last, Next

        If box = "ID" Then
            'IDBOX
            Dim IDBox As DataGridViewColumn = New DataGridViewColumn
            IDBox.SortMode = DataGridViewColumnSortMode.Automatic
            IDBox.Width = 40
            IDBox.ReadOnly = True
            IDBox.HeaderText = "ID"
            IDBox.DefaultCellStyle = cellstyle
            Dim IDBoxCell As DataGridViewCell = New DataGridViewTextBoxCell
            IDBox.CellTemplate = IDBoxCell
            Return IDBox
        End If

        If box = "Section" Then
            'Sectionbox
            Dim Sectionbox As DataGridViewColumn = New DataGridViewColumn
            Sectionbox.SortMode = DataGridViewColumnSortMode.Automatic
            Sectionbox.Width = 50
            Sectionbox.ReadOnly = True
            Sectionbox.HeaderText = "Section"
            Sectionbox.DefaultCellStyle = cellstyle
            Dim SectionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            Sectionbox.CellTemplate = SectionboxCell
            Return Sectionbox
        End If

        If box = "XPosition1" Then
            'XPositionbox
            Dim XPositionbox As DataGridViewColumn = New DataGridViewColumn
            XPositionbox.SortMode = DataGridViewColumnSortMode.Automatic
            XPositionbox.HeaderText = "X1"
            XPositionbox.DefaultCellStyle = cellstyle
            Dim XPositionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            XPositionbox.CellTemplate = XPositionboxCell
            Return XPositionbox
        End If

        If box = "ZPosition1" Then
            'YPositionbox
            Dim YPositionbox As DataGridViewColumn = New DataGridViewColumn
            YPositionbox.SortMode = DataGridViewColumnSortMode.Automatic
            YPositionbox.HeaderText = "Z1"
            YPositionbox.DefaultCellStyle = cellstyle
            Dim YPositionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            YPositionbox.CellTemplate = YPositionboxCell
            Return YPositionbox
        End If

        If box = "XPosition2" Then
            'XPositionbox
            Dim XPositionbox As DataGridViewColumn = New DataGridViewColumn
            XPositionbox.SortMode = DataGridViewColumnSortMode.Automatic
            XPositionbox.HeaderText = "X2"
            XPositionbox.DefaultCellStyle = cellstyle
            Dim XPositionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            XPositionbox.CellTemplate = XPositionboxCell
            Return XPositionbox
        End If

        If box = "ZPosition2" Then
            'YPositionbox
            Dim YPositionbox As DataGridViewColumn = New DataGridViewColumn
            YPositionbox.SortMode = DataGridViewColumnSortMode.Automatic
            YPositionbox.HeaderText = "Z2"
            YPositionbox.DefaultCellStyle = cellstyle
            Dim YPositionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            YPositionbox.CellTemplate = YPositionboxCell
            Return YPositionbox
        End If

        If box = "Respawn" Then
            'Respawn
            Dim Respawn As DataGridViewColumn = New DataGridViewColumn
            Respawn.SortMode = DataGridViewColumnSortMode.Automatic
            Respawn.Width = 50
            Respawn.HeaderText = "Respawn"
            Respawn.DefaultCellStyle = cellstyle
            Dim YPositionboxCell As DataGridViewCell = New DataGridViewTextBoxCell
            Respawn.CellTemplate = YPositionboxCell
            Return Respawn
        End If

        If box = "Type" Then
            'Type
            Dim Type As DataGridViewColumn = New DataGridViewColumn
            Type.SortMode = DataGridViewColumnSortMode.Automatic
            Type.Width = 50
            Type.HeaderText = "Type"
            Type.DefaultCellStyle = cellstyle
            Dim TypeCell As DataGridViewCell = New DataGridViewTextBoxCell
            Type.CellTemplate = TypeCell
            Return Type
        End If

        If box = "Last" Then
            'Type
            Dim Type As DataGridViewColumn = New DataGridViewColumn
            Type.SortMode = DataGridViewColumnSortMode.Automatic
            Type.Width = 50
            Type.HeaderText = "Last"
            Type.DefaultCellStyle = cellstyle
            Dim TypeCell As DataGridViewCell = New DataGridViewTextBoxCell
            Type.CellTemplate = TypeCell
            Return Type
        End If

        If box = "Next" Then
            'Type
            Dim Type As DataGridViewColumn = New DataGridViewColumn
            Type.SortMode = DataGridViewColumnSortMode.Automatic
            Type.Width = 50
            Type.HeaderText = "Next"
            Type.DefaultCellStyle = cellstyle
            Dim TypeCell As DataGridViewCell = New DataGridViewTextBoxCell
            Type.CellTemplate = TypeCell
            Return Type
        End If
        Return Nothing
    End Function

    Public Shared Function GOBJ(ByVal box As String)
        'ID, ObjID, XPosition, YPosition, ZPosition, XRoll, YRoll, ZRoll, XSize, YSize, ZSize, Route, Settings, Presence

        If box = "ID" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 40
            Column.ReadOnly = True
            Column.HeaderText = "ID"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ObjID" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "ObjID"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "X"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Y"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Z"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Roll"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Yaw"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Pitch"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XSize" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "XScale"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YSize" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "YScale"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZSize" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "ZScale"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Route" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Route ID"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Settings" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Settings"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Presence" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Presence"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If
        Return Nothing
    End Function

    Public Shared Function POTI(ByVal box As String)
        If box = "ID" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 40
            Column.ReadOnly = True
            Column.HeaderText = "ID"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Amount" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Amount"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Setting1" Then
            Dim Column As DataGridViewColumn = New DataGridViewComboBoxColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Settings"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewComboBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Setting2" Then
            Dim Column As DataGridViewColumn = New DataGridViewComboBoxColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Settings"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewComboBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Showbtn" Then
            Dim Column As DataGridViewColumn = New DataGridViewButtonColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Show"
            Column.Width = 50
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewButtonCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Usage" Then
            Dim Column As DataGridViewColumn = New DataGridViewTextBoxColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.ReadOnly = True
            Column.HeaderText = "Usage of this route"
            Column.Width = 125
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If
        Return Nothing
    End Function

    Public Shared Function Route(ByVal box As String)
        If box = "ID" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 40
            Column.ReadOnly = True
            Column.HeaderText = "ID"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "X"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Y"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Z"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Sets" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Settings"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If
        Return Nothing
    End Function

    Public Shared Function AREA(ByVal box As String)
        If box = "ID" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 40
            Column.ReadOnly = True
            Column.HeaderText = "ID"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Mode" Then
            Dim Column As DataGridViewColumn = New DataGridViewTextBoxColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Mode"
            Column.Width = 40
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Type" Then
            Dim Column As DataGridViewColumn = New DataGridViewComboBoxColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Type"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewComboBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Camera" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Camera"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "X"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Y"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Z"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Roll"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Yaw"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Pitch"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XSize" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "XScale"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YSize" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "YScale"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZSize" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "ZScale"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Settings" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 75
            Column.HeaderText = "Settings"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Route" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Route"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Enemy" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "ENPT"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        Return Nothing
    End Function

    Public Shared Function CAME(ByVal box As String)
        If box = "ID" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 40
            Column.ReadOnly = True
            Column.HeaderText = "ID"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Type" Then
            Dim Column As DataGridViewColumn = New DataGridViewComboBoxColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Type"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewComboBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Next" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Next"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Shake" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Shake"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Route" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Route"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "VCam" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "V(Cam)"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "VZoom" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "V(Zoom)"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "VView" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "V(View)"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Flag" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 70
            Column.HeaderText = "Flag"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "X"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Y"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Z"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Roll"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Yaw"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Pitch"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Zoom" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Zoom"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Zoom2" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Zoom2"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ViewX" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "View(x)"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ViewY" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "View(y)"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ViewZ" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "View(z)"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "View2X" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "View2(x)"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "View2Y" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "View2(y)"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "View2Z" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "View2(z)"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Secs" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 40
            Column.HeaderText = "Time"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        Return Nothing
    End Function

    Public Shared Function JGPT(ByVal box As String)
        If box = "ID" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 40
            Column.ReadOnly = True
            Column.HeaderText = "ID"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "X"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Y"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Z"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Roll"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Yaw"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Pitch"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ID2" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 40
            Column.HeaderText = "ID"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Range" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Range"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If
        Return Nothing
    End Function

    Public Shared Function CNPT(ByVal box As String)
        If box = "ID" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 40
            Column.ReadOnly = True
            Column.HeaderText = "ID"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "X"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Y"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Z"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Roll"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Yaw"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Pitch"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ID2" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 40
            Column.HeaderText = "KCL"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Effect" Then
            Dim Column As DataGridViewColumn = New DataGridViewComboBoxColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Effect"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewComboboxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If
        Return Nothing
    End Function

    Public Shared Function MSPT(ByVal box As String)
        If box = "ID" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 40
            Column.ReadOnly = True
            Column.HeaderText = "ID"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "X"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Y"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZPosition" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.HeaderText = "Z"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "XRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Roll"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "YRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Yaw"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ZRoll" Then
            'Column
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 50
            Column.HeaderText = "Pitch"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "ID2" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 40
            Column.HeaderText = "ID"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Settings" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.SortMode = DataGridViewColumnSortMode.Automatic
            Column.Width = 70
            Column.HeaderText = "Settings"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If
        Return Nothing
    End Function

    'Sections
    Public Shared Function Section(ByVal box As String)
        If box = "ID" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.Width = 40
            Column.ReadOnly = True
            Column.HeaderText = "ID"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "From" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.Width = 40
            Column.HeaderText = "From"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Amount" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.Width = 40
            Column.HeaderText = "Amount"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "Last" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.Width = 40
            Column.HeaderText = "Prev."
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        If box = "To" Then
            Dim Column As DataGridViewColumn = New DataGridViewColumn
            Column.Width = 40
            Column.HeaderText = "To"
            Column.DefaultCellStyle = cellstyle
            Dim ColumnCell As DataGridViewCell = New DataGridViewTextBoxCell
            Column.CellTemplate = ColumnCell
            Return Column
        End If

        Return Nothing

    End Function
End Class
