Imports System.Windows.Forms

Public Class Settings
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        My.Settings.Showarrow = CheckBox1.Checked
        My.Settings.Addleft = CheckBox2.Checked
        My.Settings.Delright = CheckBox3.Checked
        My.Settings.Pointcolor = Panel1.BackColor
        My.Settings.Background = Panel2.BackColor
        My.Settings.Keycheckpoint = Panel3.BackColor
        My.Settings.Selection = Panel4.BackColor
        My.Settings.OBJCollor = Panel5.BackColor

        My.Settings.Names(0) = TextBox1.Text
        My.Settings.Names(1) = TextBox2.Text
        My.Settings.Names(2) = TextBox3.Text
        My.Settings.Names(3) = TextBox4.Text
        My.Settings.Names(4) = TextBox5.Text
        My.Settings.Names(5) = TextBox6.Text
        My.Settings.Names(6) = TextBox7.Text
        My.Settings.Names(7) = TextBox8.Text
        My.Settings.Names(8) = TextBox9.Text
        My.Settings.Names(9) = TextBox10.Text
        My.Settings.Names(10) = TextBox11.Text
        My.Settings.Names(11) = TextBox12.Text

        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Panel1_mouseclick(sender As System.Object, e As MouseEventArgs) Handles Panel1.MouseClick, Panel2.MouseClick, Panel3.MouseClick, Panel4.MouseClick, Panel5.MouseClick
        Dim a As Panel = sender
        Dim dlg As New ColorDialog
        dlg.AllowFullOpen = True
        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            a.BackColor = dlg.Color
        End If
    End Sub

    Private Sub Settings_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Settings.Names(0)
        TextBox2.Text = My.Settings.Names(1)
        TextBox3.Text = My.Settings.Names(2)
        TextBox4.Text = My.Settings.Names(3)
        TextBox5.Text = My.Settings.Names(4)
        TextBox6.Text = My.Settings.Names(5)
        TextBox7.Text = My.Settings.Names(6)
        TextBox8.Text = My.Settings.Names(7)
        TextBox9.Text = My.Settings.Names(8)
        TextBox10.Text = My.Settings.Names(9)
        TextBox11.Text = My.Settings.Names(10)
        TextBox12.Text = My.Settings.Names(11)
    End Sub
End Class
